namespace dotmenu;

/// <summary>
/// Represents a menu configuration.
/// </summary>
public interface IMenuConfiguration
{
    /// <summary>
    /// Gets or sets the refresh rate of the menu.
    /// </summary>
    TimeSpan RefreshRate { get; set; }
}