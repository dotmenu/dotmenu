namespace dotmenu.Graphics;

/// <summary>
/// Defines methods for rendering menu elements.
/// </summary>
public interface IMenuRenderer
{
    /// <summary>
    /// Gets or sets the prefix for unselected menu options.
    /// </summary>
    string? Prefix { get; set; }
    
    /// <summary>
    /// Gets or sets the prefix for selected menu options.
    /// </summary>
    string? Selector { get; set; }
    
    /// <summary>
    /// Gets or sets the theme of the menu.
    /// </summary>
    ITheme? Theme { get; set; }
    
    /// <summary>
    /// Renders the specified menu.
    /// </summary>
    void Render(IMenu menu);
}