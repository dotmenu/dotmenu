using Natesworks.Dotmenu.Extensions;

namespace Natesworks.Dotmenu.Graphics;

public abstract class ConsoleRenderer
    : IMenuRenderer
{
    /// <summary>
    /// Creates a new instance of <see cref="ConsoleRenderer"/>.
    /// </summary>
    /// <param name="prefix">The prefix for unselected menu options.</param>
    /// <param name="selector">The prefix for selected menu options.</param>
    /// <param name="theme">The theme to use.</param>
    public ConsoleRenderer(
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
    public virtual void Render(IMenu menu)
    {
        Clear();
        
        var titleElement = menu.Elements.SingleOrDefault(IsTitleElement);
        if (titleElement is { Visible: true })
            RenderTitle(titleElement);

        foreach (var element in menu.Elements)
        {
            if (element is not { Visible: true } or MenuTitle)
                continue;

            if (element is IMenuOption option)
            {
                RenderOption(option);
                continue;
            }

            RenderElement(element);
        }
        
        static bool IsTitleElement(IMenuElement element)
            => element is MenuTitle;
    }

    /// <summary>
    /// Clears the console.
    /// </summary>
    protected virtual void Clear() =>
        AnsiConsole.Clear();

    /// <summary>
    /// Renders the title of the menu.
    /// </summary>
    /// <param name="title">The title to render.</param>
    protected virtual void RenderTitle(IMenuElement title) =>
        RenderElement(title);

    /// <summary>
    /// Renders a menu option.
    /// </summary>
    /// <param name="option">The option to render.</param>
    protected abstract void RenderOption(IMenuOption option);
    
    protected virtual void RenderElement(IMenuElement element)
    {
        var color = Theme?.GetAnsiColor(element);
        AnsiConsole.WriteLine(element.Text, color);
    }
}