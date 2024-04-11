namespace Natesworks.Dotmenu;

public sealed partial class Menu
{
    /// <summary>
    /// Creates a new instance of <see cref="IMenuBuilder"/> with default settings.
    /// </summary>
    /// <returns>A new instance of <see cref="IMenuBuilder"/>.</returns>
    public static IMenuBuilder CreateCenteredBuilder()
    {
        var builder = new CenteredMenuBuilder();
        return builder;
    }
}