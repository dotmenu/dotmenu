namespace CrystalSharp
{
    public class Menu
    {
        private int _selectedIndex;
        private List<Option> _options = new List<Option>();
        private string _prompt;
        private ConsoleColor _fg = ConsoleColor.White;
        private ConsoleColor _bg = ConsoleColor.Black;
        private ConsoleColor _selectedFg = ConsoleColor.Black;
        private ConsoleColor _selectedBg = ConsoleColor.White;
        private Dictionary<ConsoleKey, int> _shortcutMap = new Dictionary<ConsoleKey, int>();

        public Menu(){}

        public Menu(string prompt) : this()
        {
            _prompt = prompt;
        }

        public Menu Prompt(string prompt)
        {
            _prompt = prompt;
            return this;
        }

        public Menu AddOption(string text, Action action, string prefix = "", string suffix = "")
        {
            _options.Add(new Option(text, action, prefix, suffix));
            return this;
        }

        public Menu Colors(ConsoleColor fg, ConsoleColor bg)
        {
            _fg = fg;
            _bg = bg;
            return this;
        }

        public Menu ColorsWhenSelected(ConsoleColor selectedFg, ConsoleColor selectedBg)
        {
            _selectedFg = selectedFg;
            _selectedBg = selectedBg;
            return this;
        }

        public Menu Shortcut(ConsoleKey key, int optionIndex)
        {
            _shortcutMap[key] = optionIndex;
            return this;
        }

        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                DisplayOptions();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                if (_shortcutMap.ContainsKey(keyPressed))
                {
                    _options[_shortcutMap[keyPressed]].Action?.Invoke();
                    return _shortcutMap[keyPressed];
                }
                else
                {
                    if (keyPressed == ConsoleKey.UpArrow && _selectedIndex > 0)
                    {
                        _selectedIndex--;
                    }
                    if (keyPressed == ConsoleKey.DownArrow && _selectedIndex != _options.Count - 1)
                    {
                        _selectedIndex++;
                    }
                }
            } while (keyPressed != ConsoleKey.Enter);

            _options[_selectedIndex].Action?.Invoke();
            return _selectedIndex;
        }

        private void DisplayOptions()
        {
            if (!string.IsNullOrEmpty(_prompt))
            {
                Console.WriteLine(_prompt);
            }

            for (int i = 0; i < _options.Count; i++)
            {
                string selectedOption = _options[i].Text;

                if (i == _selectedIndex)
                {
                    Console.ForegroundColor = _selectedFg;
                    Console.BackgroundColor = _selectedBg;
                    Console.Write(_options[i].Prefix);
                    Console.Write(selectedOption);
                    Console.Write(_options[i].Suffix);
                }
                else
                {
                    Console.BackgroundColor = _bg;
                    Console.ForegroundColor = _fg;
                    Console.Write(_options[i].Prefix);
                    Console.Write(selectedOption);
                    Console.Write(_options[i].Suffix);
                }

                Console.WriteLine();
            }

            Console.ResetColor();
        }
    }

    public class Option
    {
        public string Text { get; }
        public Action Action { get; }
        public string Prefix { get; }
        public string Suffix { get; }

        public Option(string text, Action action, string prefix = "", string suffix = "")
        {
            Text = text;
            Action = action;
            Prefix = prefix;
            Suffix = suffix;
        }
    }
}
