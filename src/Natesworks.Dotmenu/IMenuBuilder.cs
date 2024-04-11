using Natesworks.Dotmenu.Graphics;

namespace Natesworks.Dotmenu;

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
    /// Sets the theme for the menu.
    /// </summary>
    /// <param name="theme">The theme to use for the menu.</param>
    /// <returns>The current instance of the <see cref="IMenuBuilder"/>.</returns>
    IMenuBuilder SetTheme(ITheme theme);

    /// <summary>
    /// Adds an element to the menu.
    /// </summary>
    /// <param name="element">The element to add to the menu.</param>
    /// <returns>The current instance of the <see cref="IMenuBuilder"/>.</returns>
    IMenuBuilder AddElement(IMenuElement element);
    
    /// <summary>
    /// Builds the menu.
    /// </summary>
    /// <returns>The built menu.</returns>
    IMenu Build();
}