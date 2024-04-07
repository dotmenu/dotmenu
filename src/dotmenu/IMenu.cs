namespace dotmenu;

/// <summary>
/// Represents a menu.
/// </summary>
public interface IMenu
{
    /// <summary>
    /// Runs the menu.
    /// </summary>
    /// <returns>The index of the option selected by the user.</returns>
    void Run();
}