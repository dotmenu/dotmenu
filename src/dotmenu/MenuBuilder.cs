using System.Collections.Immutable;

using dotmenu.Graphics;
using dotmenu.Graphics.Themes;

namespace dotmenu;

/// <summary>
/// Represents the basic implementation of a menu builder.
/// </summary>
internal abstract class MenuBuilder
    : IMenuBuilder
{
    private readonly IList<IMenuElement> _elements = new List<IMenuElement>(capacity: 1);
    private readonly IMenuConfiguration _configuration = new MenuConfiguration();
    private ITheme _theme = new DefaultTheme();
    private string? _selector;
    private string? _prefix;
    private string? _title;

    /// <inheritdoc />
    public IMenuBuilder SetTitle(string title)
    {
        _title = title;
        return this;
    }

    /// <inheritdoc />
    public IMenuBuilder SetPrefix(string prefix)
    {
        _prefix = prefix;
        return this;
    }

    /// <inheritdoc />
    public IMenuBuilder SetSelector(string selector)
    {
        _selector = selector;
        return this;
    }

    /// <inheritdoc />
    public IMenuBuilder SetRefreshRate(TimeSpan refreshRate)
    {
        _configuration.RefreshRate = refreshRate;
        return this;
    }

    /// <inheritdoc />
    public IMenuBuilder AddOption(IMenuOption option)
    {
        option.Validate();
        _elements.Add(option);
        return this;
    }

    /// <inheritdoc />
    public IMenuBuilder SetTheme(ITheme theme)
    {
        _theme = theme;
        return this;
    }

    /// <inheritdoc />
    public IMenu Build()
    {
        if (!string.IsNullOrWhiteSpace(_title))
        {
            var title = new MenuTitle(_title);
            _elements.Insert(0, title);
        }
        
        var elements = _elements.ToImmutableArray();
        var renderer = CreateRenderer(_selector, _prefix, _theme);
        return new Menu(
            _configuration,
            renderer,
            elements);
    }

    /// <summary>
    /// Creates a new instance of the renderer.
    /// </summary>
    /// <param name="selector">The selector for the menu.</param>
    /// <param name="prefix">The prefix for the menu.</param>
    /// <param name="theme">The theme for the menu.</param>
    protected abstract IMenuRenderer CreateRenderer(
        string? selector,
        string? prefix,
        ITheme theme);
}