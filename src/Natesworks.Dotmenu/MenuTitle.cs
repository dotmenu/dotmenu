namespace Natesworks.Dotmenu;

/// <summary>
/// Represents a menu title.
/// </summary>
public sealed class MenuTitle
    : IMenuElement
{
    /// <summary>
    /// Creates a new <see cref="MenuTitle"/> instance with the specified text.
    /// </summary>
    /// <param name="text">The text to be displayed as the title.</param>
    /// <param name="visible">
    ///     <see langword="true"/> if the title is visible; otherwise, <see langword="false"/>.
    /// </param>
    /// <param name="enabled">
    ///     <see langword="true"/> if the title is enabled; otherwise, <see langword="false"/>.
    /// </param>
    public MenuTitle(
        string text,
        bool visible = true,
        bool enabled = true)
    {
        Text = text;
        Visible = visible;
        Enabled = enabled;
    }

    /// <inheritdoc />
    public bool Visible { get; set; }

    /// <inheritdoc />
    public bool Enabled { get; set; }

    /// <inheritdoc />
    public string Text { get; set; }
}