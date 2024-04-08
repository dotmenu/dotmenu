namespace dotmenu;

/// <summary>
/// Represents a menu option.
/// </summary>
public class MenuOption
    : IMenuOption
{
    private readonly Action? _action;
    private readonly Func<string> _textFunction;

    /// <summary>
    /// Creates a new <see cref="MenuOption"/> instance with the specified text.
    /// </summary>
    /// <param name="text">The text to be displayed for the option.</param>
    /// <param name="enabled">
    ///     <see langword="true"/> if the option is enabled; otherwise, <see langword="false"/>.
    /// </param>
    /// <param name="visible">
    ///    <see langword="true"/> if the option is visible; otherwise, <see langword="false"/>.
    /// </param>
    /// <param name="action">The action to be performed when the option is selected.</param>
    public MenuOption(
        string text,
        bool enabled = true,
        bool visible = true,
        Action? action = null)
        : this(
            textFunction: () => text,
            enabled,
            visible,
            action)
    {
    }
    
    /// <summary>
    /// Creates a new <see cref="MenuOption"/> instance with the specified text function.
    /// </summary>
    /// <param name="textFunction">The function to retrieve the text to be displayed for the option.</param>
    /// <param name="enabled">
    ///     <see langword="true"/> if the option is enabled; otherwise, <see langword="false"/>.
    /// </param>
    /// <param name="visible">
    ///    <see langword="true"/> if the option is visible; otherwise, <see langword="false"/>.
    /// </param>
    /// <param name="action">The action to be performed when the option is selected.</param>
    public MenuOption(
        Func<string> textFunction,
        bool enabled = true,
        bool visible = true,
        Action? action = null)
    {
        ArgumentNullException.ThrowIfNull(textFunction);
        _textFunction = textFunction;
        
        Enabled = enabled;
        Visible = visible;

        _action = action;
    }
    
    /// <inheritdoc />
    public bool Enabled { get; set; }

    /// <inheritdoc />
    public bool Visible { get; set; }

    /// <inheritdoc />
    public bool Selected { get; set; }

    /// <inheritdoc />
    public string Text =>
        _textFunction.Invoke();

    /// <inheritdoc />
    public virtual void Invoke() =>
        _action?.Invoke();
}