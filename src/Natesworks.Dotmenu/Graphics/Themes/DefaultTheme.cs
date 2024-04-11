namespace dotmenu.Graphics.Themes;

/// <summary>
/// Represents the default theme.
/// </summary>
public sealed class DefaultTheme
    : ITheme
{
    /// <inheritdoc />
    /// <remarks>Returns <see cref="ThemeColor.White"/>.</remarks>
    public ThemeColor Foreground =>
        ThemeColor.White;

    /// <inheritdoc />
    /// <remarks>Returns <see cref="ThemeColor.Black"/>.</remarks>
    public ThemeColor Background =>
        ThemeColor.Black;

    /// <inheritdoc />
    /// <remarks>Returns <see cref="ThemeColor.Blue"/>.</remarks>
    public ThemeColor SelectionForeground =>
        ThemeColor.White;

    /// <inheritdoc />
    /// <remarks>Returns <see cref="ThemeColor.Blue"/>.</remarks>
    public ThemeColor SelectionBackground =>
        ThemeColor.Blue;
}