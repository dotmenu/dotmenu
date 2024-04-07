namespace dotmenu;

/// <summary>
/// Defines extension methods for the <see cref="Menu" /> class.
/// </summary>
public static partial class MenuExtensions
{
    /// <summary>
    /// Adds a predefined option to the menu.
    /// </summary>
    /// <param name="source">The menu to add the option to.</param>
    /// <typeparam name="TOption">Specifies the type of option to add.</typeparam>
    /// <returns>The menu with the added option.</returns>
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="source"/> is <see langword="null"/> -or-
    ///     validation the created option throws <see cref="ArgumentNullException"/>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     validation the created option throws <see cref="ArgumentException"/>.
    /// </exception>
    public static IMenuBuilder AddOption<TOption>(this IMenuBuilder source)
        where TOption : IMenuOption, new()
    {
        ArgumentNullException.ThrowIfNull(source);
        
        var option = new TOption();
        source.AddOption(option);
        return source;
    }
}