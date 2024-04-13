using System;

namespace Dotmenu
{
    /// <summary>
    /// Defines extension methods for the <see cref="Menu" /> class.
    /// </summary>
    public static partial class MenuExtensions
    {
        /// <summary>
        /// Adds a new option to the menu.
        /// </summary>
        /// <param name="menu">The menu to add the option to.</param>
        /// <param name="textFunction">The function that returns the text to be displayed for this option.</param>
        /// <param name="action">The action to be performed after this option is chosen.</param>
        /// <param name="hidden">
        ///     <see langword="true"/> if the option is hidden; otherwise, <see langword="false"/>.
        /// </param>
        /// <param name="disabled">
        ///     <see langword="true"/> if the option is disabled; otherwise, <see langword="false"/>.
        /// </param>
        /// <param name="fg">The foreground color of the option.</param>
        /// <param name="bg">The background color of the option.</param>
        /// <param name="selectedFg">The foreground color of the option when selected.</param>
        /// <param name="selectedBg">The background color of the option when selected.</param>
        /// <param name="optionPrefix">
        ///     The value to be displayed before the text of the option when it is not selected.
        /// </param>
        /// <param name="selector">
        ///     The value to be displayed before the text of the option when it is selected.
        /// </param>
        /// <returns>The menu with the added option.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="menu"/> is <see langword="null"/> -or-
        ///     <paramref name="textFunction"/> is <see langword="null"/>.
        /// </exception>
        public static Menu AddOption(
            this Menu menu,
            Func<string> textFunction,
            Action? action = null,
            bool? hidden = null,
            bool? disabled = null,
            OptionColor? fg = null,
            OptionColor? bg = null,
            OptionColor? selectedFg = null,
            OptionColor? selectedBg = null,
            string? optionPrefix = null,
            string? selector = null)
        {
            ArgumentNullException.ThrowIfNull(menu, nameof(menu));
            ArgumentNullException.ThrowIfNull(textFunction, nameof(textFunction));
            
            var option = new Option(textFunction, action, hidden, disabled, fg, bg, selectedFg, selectedBg, optionPrefix, selector);
            option.Validate();
            
            menu.options.Add(option);
            string val = textFunction.Invoke();
            return menu;
        }

        /// <summary>
        /// Adds a new option to the menu with static text.
        /// </summary>
        /// <param name="menu">The menu to add the option to.</param>
        /// <param name="text">The text to be displayed for this option.</param>
        /// <param name="action">The action to be performed after this option is chosen.</param>
        /// <param name="hidden">
        ///     <see langword="true"/> if the option is hidden; otherwise, <see langword="false"/>.
        /// </param>
        /// <param name="disabled">
        ///     <see langword="true"/> if the option is disabled; otherwise, <see langword="false"/>.
        /// </param>
        /// <param name="fg">The foreground color of the option.</param>
        /// <param name="bg">The background color of the option.</param>
        /// <param name="selectedFg">The foreground color of the option when selected.</param>
        /// <param name="selectedBg">The background color of the option when selected.</param>
        /// <param name="optionPrefix">
        ///     The value to be displayed before the text of the option when it is not selected.
        /// </param>
        /// <param name="selector">
        ///     The value to be displayed before the text of the option when it is selected.
        /// </param>
        /// <returns>The menu with the added option.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="menu"/> is <see langword="null"/> -or-
        ///     <paramref name="text"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="text"/> is empty or whitespace.
        /// </exception>
        public static Menu AddOption(
            this Menu menu,
            string text,
            Action? action = null,
            bool? hidden = null,
            bool? disabled = null,
            OptionColor? fg = null,
            OptionColor? bg = null,
            OptionColor? selectedFg = null,
            OptionColor? selectedBg = null,
            string? optionPrefix = null,
            string? selector = null)
        {
            ArgumentNullException.ThrowIfNull(menu, nameof(menu));
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("The text must be a non-empty string.", nameof(text));
            
            Func<string> textFunction = () => text;
            return menu.AddOption(textFunction, action, hidden, disabled, fg, bg, selectedFg, selectedBg, optionPrefix, selector);
        }
    }
}
