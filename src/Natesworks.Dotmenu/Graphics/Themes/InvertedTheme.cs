namespace Natesworks.Dotmenu.Graphics.Themes;

/// <summary>
/// Represents the default theme.
/// </summary>
public sealed class InvertedTheme
    : ITheme
{
    /// <inheritdoc />
    /// <remarks>Returns <see cref="ThemeColor.White"/>.</remarks>
    public ThemeColor Foreground =>
        ThemeColor.Black;

    /// <inheritdoc />
    /// <remarks>Returns <see cref="ThemeColor.Black"/>.</remarks>
    public ThemeColor Background =>
        ThemeColor.White;

    /// <inheritdoc />
    /// <remarks>Returns <see cref="ThemeColor.Blue"/>.</remarks>
    public ThemeColor SelectionForeground =>
        ThemeColor.Black;

    /// <inheritdoc />
    /// <remarks>Returns <see cref="ThemeColor.Blue"/>.</remarks>
    public ThemeColor SelectionBackground =>
        ThemeColor.White;
}