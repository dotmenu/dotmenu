using System.Text;

namespace Natesworks.DotMenu
{
    public class MultiSelectMenu : Menu
    {
        public List<int> _selectedOptions = new List<int>();
        private OptionColor _checkedFg = OptionColor.White;
        private OptionColor _checkedBg = OptionColor.Black;
        private string _selectedOptionPrefix = "";
        private string _checkedOptionPrefix = "[x]";
        private Action _enterAction = () => { };

        public MultiSelectMenu()
        {
            Console.Clear();

            _initialCursorY = Console.CursorTop;
        }

        /// <summary>
        /// Creates a new MultiSelectMenu.
        /// </summary>
        public static new MultiSelectMenu New()
        {
            return new MultiSelectMenu();
        }
        public MultiSelectMenu ColorsWhenChecked(OptionColor checkedFg, OptionColor checkedBg)
        {
            _checkedFg = checkedFg;
            _checkedBg = checkedBg;
            return this;
        }
        /// <summary>
        /// Adds a new option to the MultiSelectMenu.
        /// </summary>
        /// <param name="textFunction">Function providing text content for this option.</param>
        /// <param name="action">Action to be performed after the option is chosen.</param>
        /// <param name="shortcut">A key to bind with this option (optional).</param>
        public MultiSelectMenu AddOption(Func<string> textFunction, ConsoleKey? shortcut = null)
        {
            _options.Add(new Option(textFunction, () => { }));
            string val = textFunction.Invoke();
            _optionTextValues.Add(new(val, val, textFunction));
            if (shortcut.HasValue)
            {
                _shortcutMap[shortcut.Value] = _options.Count - 1;
            }
            return this;
        }
        public MultiSelectMenu SetCheckedOptionPrefix(string prefix)
        {
            if (string.IsNullOrEmpty(_checkedOptionPrefix))
                return this;

            _checkedOptionPrefix = prefix;

            return this;
        }
        /// <summary>
        /// The action 
        /// </summary>
        /// <param name="enterAction">The action to run.</param>
        public MultiSelectMenu ActionOnEnter(Action enterAction)
        {
            _enterAction = enterAction;
            return this;
        }

        /// <summary>
        /// Runs MultiSelectMenu and starts a task that updates the menu text.
        /// </summary>
        /// <returns>Index of option selected by the user.</returns>
        public override int Run()
        {
            if (!SupportsAnsi)
            {
                Console.WriteLine("Please use a terminal that supports ANSI escape codes.");
                return -1;
            }
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

            WriteOptions();

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
                        if (keyPressed == ConsoleKey.Tab)
                        {
                            if (_selectedIndex >= 0 && _selectedIndex < _options.Count)
                            {
                                if (_selectedOptions.Contains(_selectedIndex))
                                {
                                    _selectedOptions.Remove(_selectedIndex);
                                }
                                else
                                {
                                    _selectedOptions.Add(_selectedIndex);
                                }
                            }
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
            Console.SetCursorPosition(0, _initialCursorY + _options.Count + 1);
            Console.Clear();
            _enterAction?.Invoke();
            return _selectedIndex;
        }
        protected override void WriteOptions()
        {
            lock (_optionsBuilder)
            {
                if (!string.IsNullOrEmpty(_prompt))
                {
                    _optionsBuilder.AppendLine(_prompt);
                }

                for (int i = 0; i < _options.Count; i++)
                {
                    int maxOptionLength = Console.BufferWidth - _options[i].GetText().Length;
                    string currentOption = _options[i].GetText();

                    OptionColor fgColor;
                    OptionColor bgColor;

                    if (_selectedOptions.Contains(i) && _selectedIndex != i)
                    {
                        fgColor = _checkedFg;
                        bgColor = _checkedBg;
                    }
                    else if (i == _selectedIndex)
                    {
                        fgColor = selectedFg;
                        bgColor = selectedBg;
                    }
                    else
                    {
                        fgColor = fg;
                        bgColor = bg;
                    }

                    var optionTuple = GetOptionText(i, currentOption, maxOptionLength);

                    _optionsBuilder.Append(
                        string.Format(_colorEscapeCode,
                        fgColor.R, fgColor.G, fgColor.B,
                        bgColor.R, bgColor.G, bgColor.B,
                        optionTuple.optionText));
                    _optionsBuilder.Append(new string(' ', optionTuple.whitespaceCount + _options[i].GetText().Length));
                }

                UpdateConsole();
            }
        }
        private (string optionText, int whitespaceCount) GetOptionText(int index, string optionText, int maxOptionLength)
        {
            string fullOptionText = optionText;
            string prefix = _optionPrefix;

            if (_selectedOptions.Contains(index))
            {
                prefix += _checkedOptionPrefix;
            }
            else if (index == _selectedIndex)
            {
                prefix += _selectedOptionPrefix;
            }

            fullOptionText = prefix + fullOptionText;

            int paddingSpaces = maxOptionLength - fullOptionText.Length;

            return (fullOptionText, paddingSpaces);
        }
    }
}