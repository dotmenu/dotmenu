using System.Text;

namespace Natesworks.Dotmenu
{
    public class Menu
    {
        private int _selectedIndex;
        private readonly List<Option> _options = new List<Option>();
        private string _prompt = "";
        private OptionColor _fg = OptionColor.White;
        private OptionColor _bg = OptionColor.Black;
        private OptionColor _selectedFg = OptionColor.Black;
        private OptionColor _selectedBg = OptionColor.White;
        private readonly Dictionary<ConsoleKey, int> _shortcutMap = new Dictionary<ConsoleKey, int>();
        private readonly List<(string, string, Func<string>)> _optionTextValues = new List<(string, string, Func<string>)>();
        private StringBuilder _optionsBuilder = new StringBuilder();
        private int _initialCursorY;
        private string _optionPrefix = " ";
        private string _selector = "";
        private string _noAnsiSelector = ">";
        private static readonly string _colorEscapeCode = "\x1b[38;2;{0};{1};{2}m\x1b[48;2;{3};{4};{5}m{6}\x1b[0m";
        private static readonly bool SupportsAnsi = SpectreConsoleColorSystemDetector.Detect() == ColorSystem.TrueColor;

        private Menu()
        {
            Console.Clear();

            _initialCursorY = Console.CursorTop;
        }

        /// <summary>
        /// Creates a new Menu.
        /// </summary>
        public static Menu New()
        {
            return new Menu();
        }
        /// <summary>
        /// Sets the menu prompt (optional).
        /// </summary>
        public Menu SetPrompt(string prompt)
        {
            _prompt = prompt;
            return this;
        }
        /// <summary>
        /// Specifies colors for unselected options (optional).
        /// If not called, default colors will be white for the foreground and black for the background.
        /// </summary>
        /// <param name="fg">Foreground color (text color).</param>
        /// <param name="bg">Background color.</param>
        public Menu Colors(OptionColor fg, OptionColor bg)
        {
            _fg = fg;
            _bg = bg;
            return this;
        }
        /// <summary>
        /// Specifies colors for selected options (optional).
        /// If not called, default colors will be black for the foreground and white for the background.
        /// </summary>
        /// <param name="selectedFg">Foreground color (text color).</param>
        /// <param name="selectedBg">Background color.</param>
        public Menu ColorsWhenSelected(OptionColor selectedFg, OptionColor selectedBg)
        {
            _selectedFg = selectedFg;
            _selectedBg = selectedBg;
            return this;
        }
        /// <summary>
        /// Adds a new option to the menu.
        /// </summary>
        /// <param name="textFunction">Function providing text content for this option.</param>
        /// <param name="action">Action to be performed after the option is chosen.</param>
        /// <param name="shortcut">A key to bind with this option (optional).</param>
        public Menu AddOption(Func<string> textFunction, Action action, ConsoleKey? shortcut = null)
        {
            _options.Add(new Option(textFunction, action));
            string val = textFunction.Invoke();
            _optionTextValues.Add(new (val, val, textFunction));
            if (shortcut.HasValue)
            {
                _shortcutMap[shortcut.Value] = _options.Count - 1;
            }
            return this;
        }
        /// <summary>
        /// Sets options selector (optional).
        /// If not called, '>' will be the default selector.
        /// </summary>
        public Menu SetOptionSelector(string selector)
        {
            _selector = selector;

            return this;
        }
        /// <summary>
        /// Sets the option selector that will be used, if ANSI is not supported (optional).
        /// '>' is set as a default selector.
        /// </summary>
        public Menu SetNoAnsiOptionSelector(string selector)
        {
            _noAnsiSelector = selector;

            return this;
        }
        /// <summary>
        /// Sets prefix that will be displayed with each of unselected options (optional).
        /// Prefix cannot be empty.
        /// </summary>
        public Menu SetOptionPrefix(string prefix)
        {
            if (string.IsNullOrEmpty(prefix))
                return this;

            _optionPrefix = prefix;

            return this;
        }
        /// <summary>
        /// Runs menu and starts a task that updates menu at regular time intervals.
        /// </summary>
        /// <returns>Index of option selected by the user.</returns>
        public int Run()
        {
            ConsoleKey keyPressed = default;
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            Task updateTask = Task.Run(async () =>
            {
                try
                {
                    do
                    {
                        bool update = false;
                        
                        for (int i = 0; i < _optionTextValues.Count; i++)
                        {
                            var oldNewText = _optionTextValues[i];

                            oldNewText.Item2 = oldNewText.Item3.Invoke();
                            
                            if (oldNewText.Item1 != oldNewText.Item2)
                            {
                                update = true;
                                oldNewText.Item1 = oldNewText.Item2;
                            }
                        }

                        if (update)
                            WriteOptions();

                        await Task.Delay(500, cancellationTokenSource.Token);
                    } while (!cancellationTokenSource.Token.IsCancellationRequested);
                }
                catch (TaskCanceledException) { }
            }, cancellationTokenSource.Token);

            do
            {
                try
                {
                    var keyInfo = Console.ReadKey(true);
                    keyPressed = keyInfo.Key;

                    if (_shortcutMap.TryGetValue(keyPressed, out int optionIndex))
                    {
                        if (optionIndex >= 0 && optionIndex < _options.Count)
                        {
                            _selectedIndex = optionIndex;
                            Console.SetCursorPosition(0, 0);
                        }
                        else
                        {
                            Console.WriteLine("Invalid option.");
                        }
                    }
                    else
                    {
                        if (keyPressed == ConsoleKey.UpArrow && _selectedIndex > 0)
                        {
                            _selectedIndex--;
                        }
                        if (keyPressed == ConsoleKey.DownArrow && _selectedIndex < _options.Count - 1)
                        {
                            _selectedIndex++;
                        }
                        WriteOptions();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (keyPressed != ConsoleKey.Enter);

            cancellationTokenSource.Cancel();
            updateTask.Wait();
            Console.Clear();
            Console.SetCursorPosition(0, _initialCursorY + _options.Count + 1);
            _options[_selectedIndex].Action?.Invoke();
            return _selectedIndex;
        }
        /// <summary>
        /// Allows for edition of options after menu is ran.
        /// </summary>
        public void EditOptions(Action<List<Option>> editAction)
        {
            editAction?.Invoke(_options);
        }
        private void WriteOptions()
        {
            lock (_optionsBuilder)
            {
                if (!string.IsNullOrEmpty(_prompt))
                {
                    _optionsBuilder.AppendLine(_prompt);
                }

                for (int i = 0; i < _options.Count; i++)
                {
                    string currentOption = _options[i].GetText();

                    OptionColor fgColor;
                    OptionColor bgColor;

                    if (i == _selectedIndex)
                    {
                        fgColor = _selectedFg;
                        bgColor = _selectedBg;
                    }
                    else
                    {
                        fgColor = _fg;
                        bgColor = _bg;
                    }

                    if (SupportsAnsi)
                    {
                        string fullOptionText = (i == _selectedIndex ? _selector : _optionPrefix) + currentOption;

                        _optionsBuilder.AppendLine(
                            string.Format(_colorEscapeCode,
                            fgColor.R, fgColor.G, fgColor.B,
                            bgColor.R, bgColor.G, bgColor.B,
                            fullOptionText));
                    }
                    else
                    {
                        string prefix = i == _selectedIndex && !SupportsAnsi ? _noAnsiSelector : _optionPrefix;
                        _optionsBuilder.AppendLine(prefix + currentOption);
                    }
                }

                UpdateConsole();
            }
        }
        private void UpdateConsole()
        {
            byte[] buffer = Encoding.ASCII.GetBytes(_optionsBuilder.ToString());

            Console.SetCursorPosition(0, _initialCursorY);
            Console.CursorVisible = false;

            using (Stream sOut = Console.OpenStandardOutput(Console.WindowHeight * Console.WindowHeight))
            {
                sOut.Write(buffer, 0, buffer.Length);
            }

            _optionsBuilder.Clear();
        }
    }


    /// <summary>
    /// Represents a menu option.
    /// </summary>
    public class Option
    {
        /// <summary>
        /// Action to be performed after this option is chosen.
        /// </summary>
        public Action Action { get; set; }

        private readonly Func<string> _textFunction;

        /// <summary>
        /// Create a new Option, with source function and action provided.
        /// </summary>
        /// <param name="textFunction">Function providing text content for this option.</param>
        /// <param name="action">Action triggered after this option is chosen.</param>
        public Option(Func<string> textFunction, Action action)
        {
            _textFunction = textFunction;
            Action = action;
        }

        public string GetText()
        {
            return _textFunction.Invoke();
        }
    }

    /// <summary>
    /// Represents a RGB color and provides some default color values.
    /// </summary>
    public struct OptionColor
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        public OptionColor(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        /// <summary>
        /// White color RGB(255, 255, 255).
        /// </summary>
        public static OptionColor White = new OptionColor(255, 255, 255);
        /// <summary>
        /// Black color RGB(0, 0, 0).
        /// </summary>
        public static OptionColor Black = new OptionColor(0, 0, 0);
        /// <summary>
        /// Red color RGB(255, 0, 0).
        /// </summary>
        public static OptionColor Red = new OptionColor(255, 0, 0);
        /// <summary>
        /// Green color RGB(0, 255, 0).
        /// </summary>
        public static OptionColor Green = new OptionColor(0, 255, 0);
        /// <summary>
        /// Blue color RGB((0, 0, 255).
        /// </summary>
        public static OptionColor Blue = new OptionColor(0, 0, 255);
        /// <summary>
        /// Magenta color RGB(255, 0, 255).
        /// </summary>
        public static OptionColor Magenta = new OptionColor(255, 0, 255);
        /// <summary>
        /// Cyan color RGB(0, 255, 255).
        /// </summary>
        public static OptionColor Cyan = new OptionColor(0, 255, 255);
    }
    }