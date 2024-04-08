namespace dotmenu;

/// <summary>
/// Represents an element that can be rendered in a menu.
/// </summary>
public interface IMenuElement
{
    /// <summary>
    /// Gets or sets whether or not this menu option is visible.
    /// </summary>
    bool Visible { get; set; }
    
    /// <summary>
    /// Gets or sets whether or not this menu option is enabled.
    /// </summary>
    bool Enabled { get; set; }

    /// <summary>
    /// Gets or sets the text to display for this menu option.
    /// </summary>
    string? Text { get; }
}