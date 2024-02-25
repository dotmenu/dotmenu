namespace CrystalSharp
{
    public class Menu
    {
        private int _selectedIndex;
        private string[] _options;
        private string _prompt;
        private string _prefix;
        private string _suffix;
        private string _selectedPrefix;
        private string _selectedSuffix;
        private ConsoleColor _fg = ConsoleColor.White;
        private ConsoleColor _bg = ConsoleColor.Black;
        private ConsoleColor _selectedFg = ConsoleColor.Black;
        private ConsoleColor _selectedBg = ConsoleColor.White;
        private Dictionary<ConsoleKey, int> _shortcutMap = new Dictionary<ConsoleKey, int>();
        private Action[] _actions;

        public Menu(string[] options, Action[] actions)
        {
            _options = options;
            _actions = actions;

            for (int i = 0; i < options.Length && i < 9; i++)
            {
                _shortcutMap[ConsoleKey.D1 + i] = i;
            }
        }

        public Menu Prompt(string prompt)
        {
            _prompt = prompt;
            return this;
        }

        public Menu Prefix(string prefix)
        {
            _prefix = prefix;
            return this;
        }

        public Menu Suffix(string suffix)
        {
            _suffix = suffix;
            return this;
        }

        public Menu PrefixWhenSelected(string selectedPrefix)
        {
            _selectedPrefix = selectedPrefix;
            return this;
        }

        public Menu SuffixWhenSelected(string selectedSuffix)
        {
            _selectedSuffix = selectedSuffix;
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

                    _actions[_shortcutMap[keyPressed]]?.Invoke();
                    return _shortcutMap[keyPressed];
                }
                else
                {
                    if (keyPressed == ConsoleKey.UpArrow && _selectedIndex > 0)
                    {
                        _selectedIndex--;
                    }
                    if (keyPressed == ConsoleKey.DownArrow && _selectedIndex != _options.Length - 1)
                    {
                        _selectedIndex++;
                    }
                }
            } while (keyPressed != ConsoleKey.Enter);

            _actions[_selectedIndex]?.Invoke();

            return _selectedIndex;
        }

        private void DisplayOptions()
        {
            if (!string.IsNullOrEmpty(_prompt))
            {
                Console.WriteLine(_prompt);
            }

            for (int i = 0; i < _options.Length; i++)
            {
                string selectedOption = _options[i];

                if (i == _selectedIndex)
                {
                    Console.ForegroundColor = _selectedFg;
                    Console.BackgroundColor = _selectedBg;
                    Console.Write(_selectedPrefix);
                    Console.Write(selectedOption);
                    Console.Write(_selectedSuffix);
                }
                else
                {
                    Console.BackgroundColor = _bg;
                    Console.ForegroundColor = _fg;
                    Console.Write(_prefix);
                    Console.Write(selectedOption);
                    Console.Write(_suffix);
                }

                Console.WriteLine();
            }

            Console.ResetColor();
        }
    }
}