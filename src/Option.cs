namespace Natesworks.DotMenu;

/// <summary>
/// Represents a menu option.
/// </summary>
public class Option
{
    /// <summary>
    /// Action to be performed after this option is chosen.
    /// </summary>
    public Action Action { get; set; }

    private readonly Func<string> _textFunction;
    public bool? _hidden;
    public bool? _disabled;
    public OptionColor? fg;
    public OptionColor? bg;
    public OptionColor? selectedFg;
    public OptionColor? selectedBg;

    /// <summary>
    /// Create a new Option, with source function and action provided.
    /// </summary>
    /// <param name="textFunction">Function providing text content for this option.</param>
    /// <param name="action">Action triggered after this option is chosen.</param>
    public Option(Func<string> textFunction, Action action, bool? hidden, bool? disabled, OptionColor? fg, OptionColor? bg, OptionColor? selectedFg, OptionColor? selectedBg)
    {
        _textFunction = textFunction;
        Action = action;
        _hidden = hidden;
        _disabled = disabled;
        this.bg = bg;
        this.fg = fg;
        this.selectedFg = selectedFg;
        this.selectedBg = selectedBg;
    }

    public string GetText()
    {
        return _textFunction.Invoke();
    }

    public void SetAction(Action action)
    {
        Action = action;
    }

    public void SetHidden(bool hidden)
    {
        _hidden = hidden;
    }
}