namespace dotmenu;

/// <summary>
/// Defines extension methods for the <see cref="Menu" /> class.
/// </summary>
public static partial class MenuExtensions
{
    /// <summary>
    /// Adds a predefined option to the menu.
    /// </summary>
    /// <param name="menu">The menu to add the option to.</param>
    /// <typeparam name="TOption">Specifies the type of option to add.</typeparam>
    /// <returns>The menu with the added option.</returns>
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="menu"/> is <see langword="null"/> -or-
    ///     Validation the created option throws <see cref="ArgumentNullException"/>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     Validation the created option throws <see cref="ArgumentException"/>.
    /// </exception>
    public static Menu AddOption<TOption>(this Menu menu)
        where TOption : Option, new()
    {
        ArgumentNullException.ThrowIfNull(menu, nameof(menu));
        
        var option = new TOption();
        option.Validate();
        
        menu.options.Add(option);
        return menu;
    }
}