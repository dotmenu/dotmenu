namespace Natesworks.Dotmenu;

/// <summary>
/// Represents the standard configuration for <see cref="IMenu"/> instances.
/// </summary>
public sealed class MenuConfiguration
    : IMenuConfiguration
{
    /// <inheritdoc />
    public TimeSpan RefreshRate { get; set; } = TimeSpan.FromMilliseconds(100);
}