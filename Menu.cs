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
        private int _textAutoUpdateDelay = 1000;
        private StringBuilder _optionsBuilder = new StringBuilder();
        private int _initialCursorY;
        private static readonly string _colorEscapeCode = "\x1b[38;2;{0};{1};{2}m\x1b[48;2;{3};{4};{5}m{6}\x1b[0m";

        private Menu()
        {
            Console.Clear();
            _initialCursorY = Console.CursorTop;
        }

        public static Menu New()
        {
            return new Menu();
        }
        public Menu SetPrompt(string prompt)
        {
            _prompt = prompt;
            return this;
        }
        public Menu Colors(OptionColor fg, OptionColor bg)
        {
            _fg = fg;
            _bg = bg;
            return this;
        }
        public Menu ColorsWhenSelected(OptionColor selectedFg, OptionColor selectedBg)
        {
            _selectedFg = selectedFg;
            _selectedBg = selectedBg;
            return this;
        }
        public Menu AddOption(Func<string> textFunction, Action action, ConsoleKey? shortcut = null)
        {
            _options.Add(new Option(textFunction, action));
            if (shortcut.HasValue)
            {
                _shortcutMap[shortcut.Value] = _options.Count - 1;
            }
            return this;
        }
        public Menu TextAutoUpdateDelay(int textAutoUpdateDelay)
        {
            _textAutoUpdateDelay = textAutoUpdateDelay;
            return this;
        }
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
                        WriteOptions();
                        await Task.Delay(_textAutoUpdateDelay, cancellationTokenSource.Token);
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
                    string selectedOption = _options[i].GetText();

                    if (i == _selectedIndex)
                    {
                        _optionsBuilder.AppendLine(
                            string.Format(_colorEscapeCode,
                            _selectedFg.R, _selectedFg.G, _selectedFg.B,
                            _selectedBg.R, _selectedBg.G, _selectedBg.B,
                            selectedOption));
                    }
                    else
                    {
                        _optionsBuilder.AppendLine(
                           string.Format(_colorEscapeCode,
                           _fg.R, _fg.G, _fg.B,
                           _bg.R, _bg.G, _bg.B,
                           selectedOption));
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

    public class Option
    {
        public Action Action { get; set; }

        private readonly Func<string> _textFunction;

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

        public static OptionColor White = new OptionColor(255, 255, 255);
        public static OptionColor Black = new OptionColor(0, 0, 0);
        public static OptionColor Red = new OptionColor(255, 0, 0);
        public static OptionColor Green = new OptionColor(0, 255, 0);
        public static OptionColor Blue = new OptionColor(0, 0, 255);
        public static OptionColor Magenta = new OptionColor(255, 0, 255);
        public static OptionColor Cyan = new OptionColor(0, 255, 255);
    }
}