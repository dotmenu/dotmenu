using Natesworks.Dotmenu.Graphics;

namespace Natesworks.Dotmenu.Extensions;

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
    public static AnsiConsoleColor GetAnsiColor(
        this ITheme theme,
        IMenuElement element)
    {
        ArgumentNullException.ThrowIfNull(theme);
        ArgumentNullException.ThrowIfNull(element);
        return new AnsiConsoleColor(theme.Foreground, theme.Background);
    }
}