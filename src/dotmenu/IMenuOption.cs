namespace dotmenu;

/// <summary>
/// Represents a menu option.
/// </summary>
public interface IMenuOption
    : IMenuElement
{
    /// <summary>
    /// Invokes the action associated with this menu option.
    /// </summary>
    void Invoke();
}