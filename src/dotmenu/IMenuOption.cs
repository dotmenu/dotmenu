namespace dotmenu;

/// <summary>
/// Represents a menu option.
/// </summary>
public interface IMenuOption
    : IMenuElement
{
    /// <summary>
    /// Gets or sets whether or not this menu option is selected.
    /// </summary>
    bool Selected { get; set; }

    /// <summary>
    /// Invokes the action associated with this menu option.
    /// </summary>
    void Invoke();
}