using dotmenu.Graphics;

namespace dotmenu.Extensions;

/// <summary>
/// Defines extension methods for <see cref="ITheme"/>.
/// </summary>
public static partial class ThemeExtensions
{
    /// <summary>
    /// Gets the ANSI color for the specified menu element.
    /// </summary>
    /// <param name="theme">The theme to get the ANSI color from.</param>
    /// <param name="element">The menu element to get the ANSI color for.</param>
    /// <returns>The ANSI color for the specified menu element.</returns>
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="theme"/> is <see langword="null"/> -or-
    ///     <paramref name="element"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     <paramref name="element"/> has text that is <see langword="null"/>, empty, or whitespace.
    /// </exception>
    public static AnsiConsoleColor GetAnsiColor(
        this ITheme theme,
        IMenuElement element)
    {
        ArgumentNullException.ThrowIfNull(theme);
        ArgumentNullException.ThrowIfNull(element);
        if (string.IsNullOrWhiteSpace(element.Text))
            throw new ArgumentException("Menu element text cannot be null, empty, or whitespace.", nameof(element));
        
        return new AnsiConsoleColor(theme.Foreground, theme.Background);
    }
}