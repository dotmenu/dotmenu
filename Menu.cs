using Spectre.Console;

namespace CrystalSharp
{
    public class Menu
    {
        private int _selectedIndex;
        private readonly List<Option> _options = new List<Option>();
        private string _prompt = "";
        private Color _fg = Color.White;
        private Color _bg = Color.Black;
        private Color _selectedFg = Color.Black;
        private Color _selectedBg = Color.White;
        private readonly Dictionary<ConsoleKey, int> _shortcutMap = new Dictionary<ConsoleKey, int>();

        public Menu SetPrompt(string prompt)
        {
            _prompt = prompt;
            return this;
        }

        public Menu Colors(Color fg, Color bg)
        {
            _fg = fg;
            _bg = bg;
            return this;
        }

        public Menu ColorsWhenSelected(Color selectedFg, Color selectedBg)
        {
            _selectedFg = selectedFg;
            _selectedBg = selectedBg;
            return this;
        }

        public Menu AddOption(string text, Action action, ConsoleKey? shortcut = null)
        {
            _options.Add(new Option(text, action));
            if (shortcut.HasValue)
            {
                _shortcutMap[shortcut.Value] = _options.Count - 1;
            }
            return this;
        }

        public Menu EditOptions(Action<List<Option>> editAction)
        {
            editAction?.Invoke(_options);
            return this;
        }

        public int Run()
        {
            if (_options.Count == 0)
            {
                if (!string.IsNullOrEmpty(_prompt))
                {
                    AnsiConsole.WriteLine(_prompt);
                }
                return -1;
            }

            ConsoleKey keyPressed;
            do
            {
                AnsiConsole.Clear();
                DisplayOptions();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                try
                {
                    if (_shortcutMap.TryGetValue(keyPressed, out int optionIndex))
                    {
                        if (optionIndex >= 0 && optionIndex < _options.Count)
                        {
                            _options[optionIndex].Action?.Invoke();
                            return optionIndex;
                        }
                        else
                        {
                            AnsiConsole.WriteLine("Invalid option.");
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
                    }
                }
                catch (Exception ex)
                {
                    AnsiConsole.WriteException(ex);
                }
            } while (keyPressed != ConsoleKey.Enter);

            _options[_selectedIndex].Action?.Invoke();
            return _selectedIndex;
        }

        private void DisplayOptions()
        {
            if (!string.IsNullOrEmpty(_prompt))
            {
                AnsiConsole.WriteLine(_prompt);
            }
            for (int i = 0; i < _options.Count; i++)
            {
                string selectedOption = _options[i].Text;

                if (i == _selectedIndex)
                {
                    AnsiConsole.Markup($"[bg={_selectedBg.ToHex()} fg={_selectedFg.ToHex()}]{selectedOption}[/]");
                }
                else
                {
                    AnsiConsole.Markup($"[bg={_bg.ToHex()} fg={_fg.ToHex()}]{selectedOption}[/]");
                }

                Console.WriteLine();
            }
        }
    }

    public class Option
    {
        public string Text { get; }
        public Action Action { get; }

        public Option(string text, Action action)
        {
            Text = text;
            Action = action;
        }
    }
}
