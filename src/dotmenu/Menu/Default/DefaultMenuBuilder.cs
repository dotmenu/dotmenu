﻿using System.Collections.Immutable;

using dotmenu.Graphics;
using dotmenu.Graphics.Themes;

namespace dotmenu;

public class DefaultMenuBuilder
    : IMenuBuilder
{
    private readonly IList<IMenuElement> _elements = new List<IMenuElement>(capacity: 1);
    private readonly IMenuConfiguration _configuration = new MenuConfiguration();
    private ITheme _theme = new DefaultTheme();
    private string _selector = ">";
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
        var renderer = new DefaultMenuRenderer(_selector, _prefix, _theme);
        return new Menu(
            _configuration,
            renderer,
            elements);
    }
}