namespace dotmenu;

/// <summary>
/// Defines extension methods for the <see cref="Option" /> class.
/// </summary>
public static partial class OptionExtensions
{
    /// <summary>
    /// Validates the provided <see cref="Option" />.
    /// </summary>
    /// <param name="option">The option to validate.</param>
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="option"/> is <see langword="null"/> -or-
    ///     <see cref="Option.TextFunction"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     <see cref="Option.TextFunction"/> returns <see langword="null"/>, empty, or whitespace.
    /// </exception>
    public static void Validate(this Option option)
    {
        ArgumentNullException.ThrowIfNull(option, nameof(option));
        ArgumentNullException.ThrowIfNull(option.TextFunction, nameof(option.TextFunction));

        var text = option.TextFunction.Invoke();
        ArgumentException.ThrowIfNullOrWhiteSpace(text);
    }
}