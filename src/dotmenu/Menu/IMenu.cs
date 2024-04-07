using System.Collections.Immutable;

namespace dotmenu;

/// <summary>
/// Represents a menu.
/// </summary>
public interface IMenu
{
    /// <summary>
    /// Gets the elements of the menu.
    /// </summary>
    ImmutableArray<IMenuElement> Elements { get; }
    
    /// <summary>
    /// Runs the menu.
    /// </summary>
    /// <returns>The index of the option selected by the user.</returns>
    void Run();
}