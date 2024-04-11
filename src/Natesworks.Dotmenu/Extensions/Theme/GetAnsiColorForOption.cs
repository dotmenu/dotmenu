using Natesworks.Dotmenu.Graphics;

namespace Natesworks.Dotmenu.Extensions;

/// <summary>
/// Defines extension methods for <see cref="ITheme"/>.
/// </summary>
public static partial class ThemeExtensions
{
    /// <summary>
    /// Applies ANSI color coding to the option's text.
    /// </summary>
    /// <param name="theme">The theme to apply the color with.</param>
    /// <param name="option">The option to apply the color to.</param>
    /// <returns>The ANSI color coded text.</returns>
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="theme"/> is <see langword="null"/> -or-
    ///     <paramref name="option"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     <paramref name="option"/> has text that is <see langword="null"/>, empty, or whitespace.
    /// </exception>
    public static AnsiConsoleColor GetAnsiColor(
        this ITheme theme,
        IMenuOption option)
    {
        ArgumentNullException.ThrowIfNull(theme);
        ArgumentNullException.ThrowIfNull(option);
        if (string.IsNullOrWhiteSpace(option.Text))
            throw new ArgumentException("Menu option text cannot be null, empty, or whitespace.", nameof(option));
        
        var foreground = option.Selected
            ? theme.SelectionForeground
            : theme.Foreground;
        
        var background = option.Selected
            ? theme.SelectionBackground
            : theme.Background;

        return new AnsiConsoleColor(foreground, background);
    }
}