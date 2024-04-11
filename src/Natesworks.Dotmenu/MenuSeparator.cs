namespace Natesworks.Dotmenu;

/// <summary>
/// Represents a <see cref="IMenuElement" /> that is rendered as a visual separator.
/// </summary>
/// <param name="text">The text that represents the separator.</param>
/// <param name="enabled">Whether or not the separator is enabled.</param>
/// <param name="visible">Whether or not the separator is visible.</param>
public sealed class MenuSeparator(
    string text,
    bool enabled = true,
    bool visible = true
) : IMenuElement
{
    /// <inheritdoc />
    public bool Visible { get; set; } = visible;

    /// <inheritdoc />
    public bool Enabled { get; set; } = enabled;

    /// <inheritdoc />
    public string? Text { get; } = text;
}