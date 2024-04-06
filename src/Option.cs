namespace dotmenu;

public class Option
{
    public Action? Action { get; set; }
    public Func<string>? TextFunction { get; set; }
    public bool? Hidden { get; set; }
    public bool? Disabled { get; set; }
    public OptionColor? Fg { get; set; }
    public OptionColor? Bg { get; set; }
    public OptionColor? SelectedFg { get; set; }
    public OptionColor? SelectedBg { get; set; }
    public string? OptionPrefix { get; set; }
    public string? Selector { get; set; }

    /// <summary>
    /// Creates a new Option with the provided text function and action.
    /// </summary>
    /// <param name="textFunction">Function providing text content for this option.</param>
    /// <param name="action">Action triggered after this option is chosen.</param>
    public Option(Func<string>? textFunction, Action? action, bool? hidden, bool? disabled, OptionColor? fg, OptionColor? bg, OptionColor? selectedFg, OptionColor? selectedBg, string? optionPrefix, string? selector)
    {
        TextFunction = textFunction;
        Action = action;
        Hidden = hidden;
        Disabled = disabled;
        Fg = fg;
        Bg = bg;
        SelectedFg = selectedFg;
        SelectedBg = selectedBg;
        OptionPrefix = optionPrefix;
        Selector = selector;
    }

    /// <summary>
    /// Creates a new Option with the provided text function.
    /// </summary>
    /// <param name="textFunction">Function providing text content for this option.</param>
    public Option(Func<string>? textFunction, bool? hidden, bool? disabled, OptionColor? fg, OptionColor? bg, OptionColor? selectedFg, OptionColor? selectedBg, string? optionPrefix, string? selector)
    {
        TextFunction = textFunction;
        Hidden = hidden;
        Disabled = disabled;
        Fg = fg;
        Bg = bg;
        SelectedFg = selectedFg;
        SelectedBg = selectedBg;
        OptionPrefix = optionPrefix;
        Selector = selector;
    }

    /// <summary>
    /// Sets the text function for this option.
    /// </summary>
    /// <param name="textFunction">The function providing text content for this option.</param>
    public void SetTextFunction(Func<string>? textFunction)
    {
        TextFunction = textFunction;
    }

    /// <summary>
    /// Gets the text content of this option using the text function.
    /// </summary>
    /// <returns>The text content of this option.</returns>
    public string? GetText()
    {
        return TextFunction?.Invoke();
    }

    /// <summary>
    /// Sets the action to be performed after this option is chosen.
    /// </summary>
    /// <param name="action">The action to be performed.</param>
    public void SetAction(Action? action)
    {
        Action = action;
    }

    /// <summary>
    /// Gets the action to be performed after this option is chosen.
    /// </summary>
    /// <returns>The action to be performed.</returns>
    public Action? GetAction()
    {
        return Action;
    }

    /// <summary>
    /// Sets whether this option is hidden.
    /// </summary>
    /// <param name="hidden">True if the option is hidden; otherwise, false.</param>
    public void SetHidden(bool? hidden)
    {
        Hidden = hidden;
    }

    /// <summary>
    /// Gets whether this option is hidden.
    /// </summary>
    /// <returns>True if the option is hidden; otherwise, false.</returns>
    public bool? GetHidden()
    {
        return Hidden;
    }
}
