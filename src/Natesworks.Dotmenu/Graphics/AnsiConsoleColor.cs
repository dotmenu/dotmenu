namespace Natesworks.Dotmenu.Graphics;

/// <summary>
/// Represents an ANSI console color.
/// </summary>
public class AnsiConsoleColor
{
    /// <summary>
    /// Creates a new <see cref="AnsiConsoleColor"/> instance with the specified foreground and background colors.
    /// </summary>
    /// <param name="foreground">The foreground color.</param>
    /// <param name="background">The background color.</param>
    public AnsiConsoleColor(
        ThemeColor foreground,
        ThemeColor background)
    {
        Foreground = foreground;
        Background = background;
    }
    
    /// <summary>
    /// Gets the foreground color.
    /// </summary>
    public ThemeColor Foreground { get; }

    /// <summary>
    /// Gets the background color.
    /// </summary>
    public ThemeColor Background { get; }
}