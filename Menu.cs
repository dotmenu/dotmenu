using System;
using System.Collections.Generic;

namespace CrystalSharp
{
    public class Menu
    {
        private int _selectedIndex;
        private readonly List<Option> _options = new List<Option>();
        private string _prompt = "";
        private ConsoleColor _fg = ConsoleColor.White;
        private ConsoleColor _bg = ConsoleColor.Black;
        private ConsoleColor _selectedFg = ConsoleColor.Black;
        private ConsoleColor _selectedBg = ConsoleColor.White;
        private readonly Dictionary<ConsoleKey, int> _shortcutMap = new Dictionary<ConsoleKey, int>();

        public Menu SetPrompt(string prompt)
        {
            _prompt = prompt;
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

        public Menu AddOption(string text, Action action, ConsoleKey? shortcut = null)
        {
            _options.Add(new Option(text, action));
            if (shortcut.HasValue)
            {
                _shortcutMap[shortcut.Value] = _options.Count - 1;
            }
            return this;
        }

        public int Run()
        {
            if (_options.Count == 0)
            {
                if (!string.IsNullOrEmpty(_prompt))
                {
                    Console.WriteLine(_prompt);
                }
                return -1;
            }

            ConsoleKey keyPressed;
            do
            {
                Console.Clear(); // Clear only the current console window
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
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
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
                    Console.BackgroundColor = _selectedBg;
                    Console.ForegroundColor = _selectedFg;
                    Console.WriteLine(selectedOption);
                    Console.ResetColor();
                }
                else
                {
                    Console.BackgroundColor = _bg;
                    Console.ForegroundColor = _fg;
                    Console.WriteLine(selectedOption);
                    Console.ResetColor();
                }
            }
        }

        public void EditOptions(Action<List<Option>> editAction)
        {
            editAction?.Invoke(_options);
        }
    }

    public class Option
    {
        public string Text { get; set; }
        public Action Action { get; set; }

        public Option(string text, Action action)
        {
            Text = text;
            Action = action;
        }
    }
}
