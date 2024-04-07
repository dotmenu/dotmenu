using System.Text;

namespace dotmenu.Graphics;

/// <summary>
/// Represents a default menu renderer.
/// </summary>
internal sealed class DefaultMenuRenderer
    : IMenuRenderer
{
    private readonly StringBuilder _renderer = new();
    
    /// <summary>
    /// Creates a new instance of <see cref="DefaultMenuRenderer"/>.
    /// </summary>
    /// <param name="prefix">The prefix for unselected menu options.</param>
    /// <param name="selector">The prefix for selected menu options.</param>
    /// <param name="theme">The theme to use.</param>
    public DefaultMenuRenderer(
        string? selector,
        string? prefix,
        ITheme? theme)
    {
        Selector = selector;
        Prefix = prefix;
        Theme = theme;
    }
    
    /// <inheritdoc />
    public string? Selector { get; set; }

    /// <inheritdoc />
    public string? Prefix { get; set; }

    /// <inheritdoc />
    public ITheme? Theme { get; set; }

    /// <inheritdoc />
    public void Render(IMenu menu)
    {
        Console.Clear();
        _renderer.Clear();
        
        RenderTitle(menu);
        RenderOptions(menu);

        Console.SetCursorPosition(0, 0);
        Console.Write(_renderer.ToString());
    }

    private void RenderTitle(IMenu menu)
    {
        var titleElement = menu.Elements.SingleOrDefault(IsTitleElement);
        RenderElement(titleElement, usePrefix: false);
        
        static bool IsTitleElement(IMenuElement element)
            => element is MenuTitle;
    }
    
    private void RenderOptions(IMenu menu)
    {
        foreach (var element in menu.Elements.OfType<IMenuOption>())
        {
            if (element is not { Visible: true })
                continue;

            RenderElement(element);
        }
    }

    private void RenderElement(
        IMenuElement? element,
        bool usePrefix = true)
    {
        if (element is not { Visible: true })
            return;

        var useSelectionColors = false;
        var prefix = usePrefix ? Prefix : string.Empty;
        if (usePrefix && element is IMenuOption option)
        {
            useSelectionColors = option.Selected;
            prefix = option.Selected ? Selector : Prefix;
        }

        var line = $"{prefix} {element.Text}".Trim();
        var formattedLine = FormatLine(line, useSelectionColors);
        _renderer.AppendLine(formattedLine);
    }
    
    private string FormatLine(
        string text,
        bool useSelectionColors)
    {
        var (foregroundRed, foregroundGreen, foregroundBlue) = useSelectionColors
            ? Theme?.SelectionForeground ?? ThemeColor.White
            : Theme?.Foreground ?? ThemeColor.White;
        
        var (backgroundRed, backgroundGreen, backgroundBlue) = useSelectionColors
            ? Theme?.SelectionBackground ?? ThemeColor.Black
            : Theme?.Background ?? ThemeColor.Black;
        
        const string colorEscapeCode = "\x1b[38;2;{0};{1};{2}m\x1b[48;2;{3};{4};{5}m{6}\x1b[0m";
        var coloredText = string.Format(colorEscapeCode,
            foregroundRed, foregroundGreen, foregroundBlue,
            backgroundRed, backgroundGreen, backgroundBlue,
            text);
        
        var paddingLength = Math.Max(0, Console.BufferWidth - text.Length);
        var padding = new string(' ', paddingLength);
        return $"{coloredText}{padding}";
    }
}