namespace dotmenu;

/// <summary>
/// Represents a menu option.
/// </summary>
public interface IMenuOption
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
    string Text { get; set; }

    /// <summary>
    /// Invokes the action associated with this menu option.
    /// </summary>
    void Invoke();
}