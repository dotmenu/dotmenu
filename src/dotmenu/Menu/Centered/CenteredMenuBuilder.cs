using dotmenu.Graphics;

namespace dotmenu;

/// <summary>
/// Represents a menu builder that creates a centered menu.
/// </summary>
internal sealed class CenteredMenuBuilder
    : MenuBuilder
{
    /// <inheritdoc />
    protected override IMenuRenderer CreateRenderer(
        string selector,
        string? prefix,
        ITheme theme) =>
        new CenteredMenuRenderer(selector, prefix, theme);
}