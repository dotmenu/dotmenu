namespace dotmenu;

/// <summary>
/// Defines extension methods for the <see cref="IMenuBuilder" /> interface.
/// </summary>
public static partial class MenuBuilderExtensions
{
    /// <summary>
    /// Adds a new option to the menu being built.
    /// </summary>
    /// <param name="source">The menu builder to add the option to.</param>
    /// <param name="text">The text to be displayed for this option.</param>
    /// <param name="enabled">
    ///     <see langword="true"/> if the option is enabled; otherwise, <see langword="false"/>.
    /// </param>
    /// <param name="visible">
    ///     <see langword="true"/> if the option is visible; otherwise, <see langword="false"/>.
    /// </param>
    /// <param name="action">The action to be performed after this option is chosen.</param>
    /// <returns>The menu with the added option.</returns>
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="source"/> is <see langword="null"/> -or-
    ///     <paramref name="text"/> is <see langword="null"/> -or-
    ///     validation the created option throws <see cref="ArgumentNullException"/>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     <paramref name="text"/> is <see langword="null"/>, empty, or whitespace -or-
    ///     validation the created option throws <see cref="ArgumentException"/>.
    /// </exception>
    public static IMenuBuilder AddOption(
        this IMenuBuilder source,
        string text,
        bool enabled = true,
        bool visible = true,
        Action? action = null)
    {
        ArgumentNullException.ThrowIfNull(source);
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("The text must be a non-empty string.", nameof(text));
        
        var option = new MenuOption(text, enabled, visible, action);
        source.AddOption(option);
        return source;
    }
}