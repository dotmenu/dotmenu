using dotmenu.Graphics;

namespace dotmenu;

/// <summary>
/// Defines methods for building a menu.
/// </summary>
public interface IMenuBuilder
{
    /// <summary>
    /// Sets the title of the menu.
    /// </summary>
    /// <param name="title">The title of the menu.</param>
    /// <returns>The current instance of the <see cref="IMenuBuilder"/>.</returns>
    IMenuBuilder SetTitle(string title);
    
    /// <summary>
    /// Sets the prefix for unselected menu options.
    /// </summary>
    /// <param name="prefix">The prefix for unselected menu options.</param>
    /// <returns>The current instance of the <see cref="IMenuBuilder"/>.</returns>
    IMenuBuilder SetPrefix(string prefix);
    
    /// <summary>
    /// Sets the prefix for selected menu options.
    /// </summary>
    /// <param name="selector">The prefix for selected menu options.</param>
    /// <returns>The current instance of the <see cref="IMenuBuilder"/>.</returns>
    IMenuBuilder SetSelector(string selector);
    
    /// <summary>
    /// Sets the refresh rate for the menu.
    /// </summary>
    /// <param name="refreshRate">The refresh rate for the menu.</param>
    /// <returns>The current instance of the <see cref="IMenuBuilder"/>.</returns>
    IMenuBuilder SetRefreshRate(TimeSpan refreshRate);

    /// <summary>
    /// Adds an option to the menu.
    /// </summary>
    /// <typeparam name="TOption">Specifies the type of the option to add to the menu.</typeparam>
    /// <returns>The current instance of the <see cref="IMenuBuilder"/>.</returns>
    IMenuBuilder AddOption<TOption>()
        where TOption : IMenuOption, new();
    
    /// <summary>
    /// Adds a theme to the menu.
    /// </summary>
    /// <typeparam name="TTheme">Specifies the type of the theme to add to the menu.</typeparam>
    /// <returns>The current instance of the <see cref="IMenuBuilder"/>.</returns>
    IMenuBuilder AddTheme<TTheme>()
        where TTheme : ITheme, new();
    
    /// <summary>
    /// Builds the menu.
    /// </summary>
    /// <returns>The built menu.</returns>
    IMenu Build();
}