using System.Text;

namespace dotmenu
{
    public class Menu
    {
        protected int _selectedIndex;
        public List<Option> options = new List<Option>();
        protected string _prompt = "";
        protected OptionColor fg = OptionColor.White;
        protected OptionColor bg = OptionColor.Black;
        protected OptionColor selectedFg = OptionColor.Black;
        protected OptionColor selectedBg = OptionColor.White;
        protected readonly Dictionary<ConsoleKey, int> _shortcutMap = new Dictionary<ConsoleKey, int>();
        protected readonly List<(string, string, Func<string>)> _optionTextValues = new List<(string, string, Func<string>)>();
        protected StringBuilder _optionsBuilder = new StringBuilder();
        protected int _initialCursorY;
        protected string _optionPrefix = "";
        protected string _selector = "";
        protected ConsoleKey _altEnterKey = ConsoleKey.Enter;
        protected int _delay = 100;
        protected static readonly string _colorEscapeCode = "\x1b[38;2;{0};{1};{2}m\x1b[48;2;{3};{4};{5}m{6}\x1b[0m";
        public static readonly bool SupportsAnsi = AnsiDetector.SupportsAnsi;

        public Menu()
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
        /// <param name="prompt">The prompt to be displayed.</param>
        /// <returns>The current instance of Menu.</returns>
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
        /// <returns>The current instance of Menu.</returns>
        public Menu Colors(OptionColor fg, OptionColor bg)
        {
            this.fg = fg;
            this.bg = bg;
            return this;
        }
        /// <summary>
        /// Specifies colors for selected options (optional).
        /// If not called, default colors will be black for the foreground and white for the background.
        /// </summary>
        /// <param name="selectedFg">Foreground color (text color).</param>
        /// <param name="selectedBg">Background color.</param>
        /// <returns>The current instance of Menu.</returns>
        public Menu ColorsWhenSelected(OptionColor selectedFg, OptionColor selectedBg)
        {
            this.selectedFg = selectedFg;
            this.selectedBg = selectedBg;
            return this;
        }
        /// <summary>
        /// Adds a new option to the menu.
        /// </summary>
        /// <param name="textFunction">Function providing text content for this option.</param>
        /// <param name="action">Action to be performed after the option is chosen.</param>
        /// <param name="shortcut">A key to bind with this option (optional).</param>
        /// <param name="hidden">Specifies whether the option should be hidden (optional).</param>
        /// <param name="fg">The foreground color for this option (optional).</param>
        /// <param name="bg">The background color for this option (optional).</param>
        /// <param name="selectedFg">The foreground color when this option is selected (optional).</param>
        /// <param name="selectedBg">The background color when this option is selected (optional).</param>
        /// <param name="optionPrefix">The prefix to be displayed before the option text (optional).</param>
        /// <param name="selector">The selector to be displayed when the option is selected (optional).</param>
        /// <returns>The current instance of Menu.</returns>
        public Menu AddOption(Func<string> textFunction, Action action, ConsoleKey? shortcut = null, bool? hidden = false, bool? disabled = false, OptionColor? fg = null, OptionColor? bg = null, OptionColor? selectedFg = null, OptionColor? selectedBg = null, string? optionPrefix = null, string? selector = null)
        {
            options.Add(new Option(textFunction, action, hidden, disabled, fg, bg, selectedFg, selectedBg, optionPrefix, selector));
            string val = textFunction.Invoke();
            _optionTextValues.Add(new(val, val, textFunction));
            if (shortcut.HasValue)
            {
                _shortcutMap[shortcut.Value] = options.Count - 1;
            }
            return this;
        }
        /// <summary>
        /// Sets options selector (optional).
        /// If not called, '>' will be the default selector.
        /// </summary>
        /// <param name="selector">The selector to be displayed before the option text.</param>
        /// <returns>The current instance of Menu.</returns>
        public Menu SetOptionSelector(string selector)
        {
            _selector = selector;

            return this;
        }
        /// <summary>
        /// Sets prefix that will be displayed with each of unselected options (optional).
        /// Prefix cannot be empty.
        /// </summary>
        /// <param name="prefix">The prefix to be displayed before the option text.</param>
        /// <returns>The current instance of Menu.</returns>
        public Menu SetOptionPrefix(string prefix)
        {
            if (string.IsNullOrEmpty(prefix))
                return this;

            _optionPrefix = prefix;

            return this;
        }
        /// <summary>
        /// Sets the alternative Enter key.
        /// </summary>
        /// <param name="altEnterKey">The alternative Enter key to be set.</param>
        /// <returns>The current instance of Menu.</returns>
        public Menu SetAltEnterKey(ConsoleKey altEnterKey)
        {
            _altEnterKey = altEnterKey;

            return this;
        }
        /// <summary>
        /// Sets the delay of when the Menu alongside it's options update.
        /// </summary>
        /// <param name="delay">The delay before updating.</param>
        /// <returns></returns>
        public Menu SetDelay(int delay)
        {
            _delay = delay;

            return this;
        }
        /// <summary>
        /// Sets the selected index of the menu.
        /// </summary>
        /// <param name="selectedIndex">The index of the option to be selected.</param>
        /// <returns>The current instance of Menu.</returns>
        public Menu SetSelectedIndex(int selectedIndex)
        {
            _selectedIndex = selectedIndex;
            
            return this;
        }
        /// <summary>
        /// Runs menu and starts a task that updates menu at regular time intervals.
        /// </summary>
        /// <returns>Index of option selected by the user.</returns>
public virtual int Run()
{
    if (!SupportsAnsi)
    {
        Console.WriteLine("Please use a terminal that supports ANSI escape codes.");
        Environment.Exit(2);
    }
    
    while (true)
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

                    await Task.Delay(_delay, cancellationTokenSource.Token);
                } while (!cancellationTokenSource.Token.IsCancellationRequested);
            }
            catch (TaskCanceledException) { }
        }, cancellationTokenSource.Token);

        WriteOptions();

        do
        {
            try
            {
                var keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                if (_shortcutMap.TryGetValue(keyPressed, out int optionIndex))
                {
                    if (optionIndex >= 0 && optionIndex < options.Count && !options[optionIndex].Hidden.HasValue)
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
                    if (keyPressed == ConsoleKey.UpArrow)
                    {
                        do
                        {
                            _selectedIndex = (_selectedIndex - 1 + options.Count) % options.Count;
                        } while (options[_selectedIndex].Hidden is true);
                    }
                    if (keyPressed == ConsoleKey.DownArrow)
                    {
                        do
                        {
                            _selectedIndex = (_selectedIndex + 1) % options.Count;
                        } while (options[_selectedIndex].Hidden is true);
                    }
                    WriteOptions();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        } while (!(keyPressed == ConsoleKey.Enter && !(options[_selectedIndex].Disabled ?? false)));
        cancellationTokenSource.Cancel();
        updateTask.Wait();
        Console.Clear();
        Console.SetCursorPosition(0, _initialCursorY + options.Count + 1);
        options[_selectedIndex].Action?.Invoke();                    
        return _selectedIndex;
    }
}
        /// <summary>
        /// Writes the options to the console.
        /// </summary>
        protected virtual void WriteOptions()
        {
            lock (_optionsBuilder)
            {
                if (!string.IsNullOrEmpty(_prompt))
                {
                    _optionsBuilder.AppendLine(_prompt);
                }

                for (int i = 0; i < options.Count; i++)
                {
                    if (options[i].Hidden is not true)
                    {
                        var (fullOptionText, paddingSpaces) = GetOptionText(i, options[i].GetText(), Console.BufferWidth);

                        OptionColor fgColor;
                        OptionColor bgColor;

                        if (i == _selectedIndex)
                        {
                            fgColor = options[i].SelectedFg ?? selectedFg;
                            bgColor = options[i].SelectedBg ?? selectedBg;
                        }
                        else
                        {
                            fgColor = options[i].Fg ?? fg;
                            bgColor = options[i].Bg ?? bg;
                        }

                        _optionsBuilder.Append(string.Format(_colorEscapeCode,
                            fgColor.R, fgColor.G, fgColor.B,
                            bgColor.R, bgColor.G, bgColor.B,
                            fullOptionText));
                        _optionsBuilder.Append(new string(' ', paddingSpaces));
                    }
                }

                UpdateConsole();
            }
        }
        /// <summary>
        /// Gets the option text along with the prefix and whitespace count to remove previous text.
        /// </summary>
        /// <returns>Tuple containing option text and whitespace count.</returns>
        private (string optionText, int whitespaceCount) GetOptionText(int index, string optionText, int maxOptionLength)
        {
            string prefix = options[index].OptionPrefix ?? _optionPrefix;
            string selector = options[index].Selector ?? _selector;

            string fullOptionText = optionText;
            if (index == _selectedIndex)
            {
                fullOptionText = selector + fullOptionText;
            }
            else
            {
                fullOptionText = prefix + fullOptionText;
            }

            int paddingSpaces = Math.Max(0, maxOptionLength - fullOptionText.Length);

            return (fullOptionText, paddingSpaces);
        }
        /// <summary>
        /// Updates the console.
        /// </summary>
        protected void UpdateConsole()
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
}
