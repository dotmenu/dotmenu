namespace Natesworks.Dotmenu;

/// <summary>
/// Defines extension methods for the <see cref="IMenuBuilder" /> interface.
/// </summary>
public static partial class MenuBuilderExtensions
{
    /// <summary>
    /// Adds a new option to the menu being built.
    /// </summary>
    /// <param name="source">The menu builder to add the option to.</param>
    /// <param name="value">The character to be displayed for this separator.</param>
    /// <param name="enabled">
    ///     <see langword="true"/> if the separator is enabled; otherwise, <see langword="false"/>.
    /// </param>
    /// <param name="visible">
    ///    <see langword="true"/> if the separator is visible; otherwise, <see langword="false"/>.
    /// </param>
    /// <param name="length">The length of the separator.</param>
    /// <returns>The menu with the added separator.</returns>
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="source"/> is <see langword="null"/>.
    /// </exception>
    public static IMenuBuilder AddSeparator(
        this IMenuBuilder source,
        char value,
        bool enabled = true,
        bool visible = true,
        int length = 1)
    {
        ArgumentNullException.ThrowIfNull(source);
        
        var text = new string(value, length);
        var separator = new MenuSeparator(text, enabled, visible);
        source.AddElement(separator);
        return source;
    }
}