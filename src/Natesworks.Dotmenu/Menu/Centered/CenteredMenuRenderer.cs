using System.Numerics;

using Natesworks.Dotmenu.Extensions;
using Natesworks.Dotmenu.Graphics;

namespace Natesworks.Dotmenu;

/// <summary>
/// Represents a menu renderer that centers items horizontally and vertically in the console.
/// </summary>
internal sealed class CenteredMenuRenderer
    : ConsoleRenderer
{
    private int _currentRow;
    
    /// <summary>
    /// Creates a new instance of <see cref="CenteredMenuRenderer"/>.
    /// </summary>
    /// <param name="prefix">The prefix for unselected menu options.</param>
    /// <param name="selector">The prefix for selected menu options.</param>
    /// <param name="theme">The theme to use.</param>
    public CenteredMenuRenderer(
        string? selector,
        string? prefix,
        ITheme? theme)
        : base(selector, prefix, theme)
    {
    }
    
    /// <inheritdoc />
    public override void Render(IMenu menu)
    {
        Clear();
        ResetCurrentRow(menu);
        
        var titleElement = menu.Elements.SingleOrDefault(IsTitleElement);
        if (titleElement is { Visible: true })
            RenderTitle(titleElement);

        foreach (var element in menu.Elements)
        {
            switch (element)
            {
                case not { Visible: true } or MenuTitle:
                    continue;
                case IMenuOption option:
                    RenderOption(option);
                    continue;
                default:
                    RenderElement(element);
                    break;
            }
        }
        
        static bool IsTitleElement(IMenuElement element)
            => element is MenuTitle;
    }

    /// <inheritdoc />
    protected override void RenderTitle(IMenuElement title)
    {
        var column = CalculateColumn(title);
        var color = Theme?.GetAnsiColor(title);
        var position = new Vector2(column, _currentRow++);
        AnsiConsole.Write(title.Text, color, position);
    }

    /// <inheritdoc />
    protected override void RenderOption(IMenuOption option)
    {
        var column = CalculateColumn(option);
        var color = Theme?.GetAnsiColor(option);
        var position = new Vector2(column, _currentRow++);
        var prefix = option.Selected ? Selector : Prefix;
        var text = $"{prefix} {option.Text}".Trim();
        AnsiConsole.Write(text, color, position);
    }
    
    protected override void RenderElement(IMenuElement element)
    {
        var column = CalculateColumn(element);
        var color = Theme?.GetAnsiColor(element);
        var position = new Vector2(column, _currentRow++);
        AnsiConsole.Write(element.Text, color, position);
    }

    private void ResetCurrentRow(IMenu menu)
    {
        var halfHeight = menu.Elements.Length / 2;
        var center = Console.BufferHeight / 2 - halfHeight;
        _currentRow = center - halfHeight;
    }

    private int CalculateColumn(IMenuElement element)
    {
        if (element.Text is null)
            throw new InvalidOperationException("Element text is null.");
        
        var width = Console.BufferWidth;
        var length = element.Text.Length;
        return width / 2 - length / 2;
    }
}