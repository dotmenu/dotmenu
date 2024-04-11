namespace Natesworks.Dotmenu.Extensions.Option;

/// <summary>
/// Defines extension methods for <see cref="IMenuOption" /> instances.
/// </summary>
public static partial class OptionExtensions
{
    /// <summary>
    /// Validates the specified <see cref="IMenuOption" />.
    /// </summary>
    /// <param name="option">The option to validate.</param>
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="option" /> is <see langword="null" />.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The text for the option is <see langword="null" />, empty, or whitespace.
    /// </exception>
    public static void Validate(this IMenuOption option)
    {
        ArgumentNullException.ThrowIfNull(option);
        if (string.IsNullOrWhiteSpace(option.Text))
            throw new ArgumentException("The text for a menu option must be a non-empty string.", nameof(option));
    }
}