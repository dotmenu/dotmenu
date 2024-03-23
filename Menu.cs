using System;
using System.Collections.Generic;

namespace Natesworks.Dotmenu
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
        private int _textUpdateDelay = 1000;

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

        public Menu AddOption(Func<string> textFunction, Action action, ConsoleKey? shortcut = null)
        {
            _options.Add(new Option(textFunction, action));
            if (shortcut.HasValue)
            {
                _shortcutMap[shortcut.Value] = _options.Count - 1;
            }
            return this;
        }
        public Menu TextUpdateDelay(int textUpdateDelay)
        {
            _textUpdateDelay = textUpdateDelay;
            return this;
        }

        public int Run()
        {
            ConsoleKey keyPressed = default;
            var updateTask = Task.Run(async () =>
            {
                do
                {
                    Console.Clear();
                    DisplayOptions();
                    await Task.Delay(_textUpdateDelay);
                } while (keyPressed != ConsoleKey.Enter);
            });

            do
            {
                var keyInfoTask = Task.Run(() => Console.ReadKey(true));
                Task.WaitAny(updateTask, keyInfoTask);
                var keyInfo = keyInfoTask.Result;
                keyPressed = keyInfo.Key;

                try
                {
                    if (_shortcutMap.TryGetValue(keyPressed, out int optionIndex))
                    {
                        if (optionIndex >= 0 && optionIndex < _options.Count)
                        {
                            _selectedIndex = optionIndex;
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
                string selectedOption = _options[i].GetText();

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
        private readonly Func<string> _textFunction; 
        public Action Action { get; set; }

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
}