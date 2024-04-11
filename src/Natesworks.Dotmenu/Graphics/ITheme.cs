namespace Natesworks.Dotmenu.Graphics;

/// <summary>
/// Represents a theme for menus.
/// </summary>
public interface ITheme
{
    /// <summary>
    /// Gets the natural foreground color for the menu.
    /// </summary>
    ThemeColor Foreground { get; }

    /// <summary>
    /// Gets the natural background color for the menu.
    /// </summary>
    ThemeColor Background { get; }

    /// <summary>
    /// Gets the selection foreground color for menu options.
    /// </summary>
    ThemeColor SelectionForeground { get; }
    
    /// <summary>
    /// Gets the selection background color for menu options.
    /// </summary>
    ThemeColor SelectionBackground { get; }
}