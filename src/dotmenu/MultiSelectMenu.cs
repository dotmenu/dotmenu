using System.Text;

namespace dotmenu
{
    public class MultiSelectMenu : Menu
    {
        public List<int> _selectedOptions = new List<int>();
        private OptionColor _checkedFg = OptionColor.White;
        private OptionColor _checkedBg = OptionColor.Black;
        private string _selectedOptionPrefix = "";
        private string _checkedOptionPrefix = "[x] ";
        private Action _enterAction = () => { };
        private readonly List<int> _hiddenOptions = new List<int>();
        private readonly List<int> _disabledOptions = new List<int>();

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
        /// <summary>
        /// Sets the foreground and background colors when an option is checked.
        /// </summary>
        /// <param name="checkedFg">The foreground color for checked options.</param>
        /// <param name="checkedBg">The background color for checked options.</param>
        /// <returns>The current instance of MultiSelectMenu.</returns>
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
        /// <param name="shortcut">A key to bind with this option (optional).</param>
        /// <param name="hidden">Specifies whether the option should be hidden (optional).</param>
        /// <param name="fg">The foreground color for this option (optional).</param>
        /// <param name="bg">The background color for this option (optional).</param>
        /// <param name="selectedFg">The foreground color when this option is selected (optional).</param>
        /// <param name="selectedBg">The background color when this option is selected (optional).</param>
        /// <param name="optionPrefix">The prefix to be displayed before the option text (optional).</param>
        /// <param name="selector">The selector to be displayed when the option is selected (optional).</param>
        /// <returns>The current instance of MultiSelectMenu.</returns>
        public MultiSelectMenu AddOption(Func<string> textFunction, ConsoleKey? shortcut = null, bool? hidden = false, bool? disabled = false, OptionColor? fg = null, OptionColor? bg = null, OptionColor? selectedFg = null, OptionColor? selectedBg = null, string? optionPrefix = null, string? selector = null)
        {
            options.Add(new Option(textFunction, hidden, disabled, fg, bg, selectedFg, selectedBg, optionPrefix, selector));
            string val = textFunction.Invoke();
            _optionTextValues.Add(new(val, val, textFunction));
            if (shortcut.HasValue)
            {
                _shortcutMap[shortcut.Value] = options.Count - 1;
            }
            return this;
        }

        /// <summary>
        /// Sets the prefix for checked options.
        /// </summary>
        /// <param name="prefix">The prefix to be displayed before checked options.</param>
        /// <returns>The current instance of MultiSelectMenu.</returns>
        public MultiSelectMenu SetCheckedOptionPrefix(string prefix)
        {
            if (string.IsNullOrEmpty(_checkedOptionPrefix))
                return this;

            _checkedOptionPrefix = prefix;

            return this;
        }
        /// <summary>
        /// Sets the action to be performed when Enter key is pressed.
        /// </summary>
        /// <param name="enterAction">The action to be performed when Enter key is pressed.</param>
        /// <returns>The current instance of MultiSelectMenu.</returns>
        public MultiSelectMenu ActionOnEnter(Action enterAction)
        {
            _enterAction = enterAction;
            return this;
        }

        /// <summary>
        /// Gets the list of indices of checked options.
        /// </summary>
        public List<int> GetCheckedOptions()
        {
             return _selectedOptions;
        }
        /// <summary>
        /// Runs the MultiSelectMenu and starts a task that updates the menu text.
        /// </summary>
        /// <returns>Index of option selected by the user.</returns>
        public override int Run()
        {
            if (!SupportsAnsi)
            {
                Console.WriteLine("Please use a terminal that supports ANSI escape codes.");
                Enviroment.Exit(2);
            }
            while(true)
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

                WriteOptions();

                do
                {
                    try
                    {
                        var keyInfo = Console.ReadKey(true);
                        keyPressed = keyInfo.Key;

                        if (_shortcutMap.TryGetValue(keyPressed, out int optionIndex))
                        {
                            if (optionIndex >= 0 && optionIndex < options.Count)
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
                            if (keyPressed == ConsoleKey.Tab)
                            {
                                if (_selectedIndex >= 0 && _selectedIndex < options.Count)
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
                } while (keyPressed != _altEnterKey);

                cancellationTokenSource.Cancel();
                updateTask.Wait();
                Console.SetCursorPosition(0, _initialCursorY + options.Count + 1);
                Console.Clear();
                _enterAction?.Invoke();
                return _selectedIndex;
            }
        }
        /// <summary>
        /// Writes the options to the console.
        /// </summary>
        protected override void WriteOptions()
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

                            _optionsBuilder.Append(
                                string.Format(_colorEscapeCode,
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
        /// Retrieves the formatted option text and the count of whitespace for padding.
        /// </summary>
        /// <param name="index">The index of the option.</param>
        /// <param name="optionText">The text content of the option.</param>
        /// <param name="maxOptionLength">The maximum length allowed for an option.</param>
        /// <returns>A tuple containing the formatted option text and the count of whitespace for padding.</returns>
        private (string optionText, int whitespaceCount) GetOptionText(int index, string optionText, int maxOptionLength)
        {
            string prefix = options[index].OptionPrefix ?? _optionPrefix;
            string selector = options[index].Selector ?? _selector;

            string fullOptionText = optionText;

            if (_selectedOptions.Contains(index))
            {
                prefix = _checkedOptionPrefix;
            }
            else if (index == _selectedIndex)
            {
                prefix = selector;
            }

            fullOptionText = prefix + fullOptionText;

            int paddingSpaces = Math.Max(0, maxOptionLength - fullOptionText.Length);

            return (fullOptionText, paddingSpaces);
        }
    }
}
