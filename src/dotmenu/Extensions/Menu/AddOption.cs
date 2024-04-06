namespace dotmenu;

/// <summary>
/// Defines extension methods for the <see cref="Menu" /> class.
/// </summary>
public static partial class MenuExtensions
{
    /// <summary>
    /// Adds a new option to the menu.
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
    ///     <paramref name="text"/> is <see langword="null"/> -or-
    ///     validation the created option throws <see cref="ArgumentNullException"/>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     <paramref name="text"/> is <see langword="null"/>, empty, or whitespace -or-
    ///     validation the created option throws <see cref="ArgumentException"/>.
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
        
        var option = new Option(text, action, hidden, disabled, fg, bg, selectedFg, selectedBg, optionPrefix, selector);
        option.Validate();
        
        menu.options.Add(option);
        return menu;
    }
}