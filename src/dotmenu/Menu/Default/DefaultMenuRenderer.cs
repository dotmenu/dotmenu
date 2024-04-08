using System.Text;

using dotmenu.Extensions;

namespace dotmenu.Graphics;

/// <summary>
/// Represents a default menu renderer.
/// </summary>
internal sealed class DefaultMenuRenderer
    : ConsoleRenderer
{
    /// <inheritdoc />
    public DefaultMenuRenderer(
        string? selector,
        string? prefix,
        ITheme? theme)
        : base(selector, prefix, theme)
    {
    }

    /// <inheritdoc />
    protected override void RenderTitle(IMenuElement title)
    {
        var color = Theme?.GetAnsiColor(title);
        AnsiConsole.WriteLine(title.Text, color);
    }

    /// <inheritdoc />
    protected override void RenderOption(IMenuOption option)
    {
        var color = Theme?.GetAnsiColor(option);
        var text = option.Selected
            ? $"{Selector} {option.Text}"
            : $"{Prefix} {option.Text}";

        AnsiConsole.WriteLine(text, color);
    }
}