namespace Dotmenu;

/// <summary>
/// Represents a RGB color and provides some default color values.
/// </summary>
public struct OptionColor
{
    public byte R { get; set; }
    public byte G { get; set; }
    public byte B { get; set; }

    public OptionColor(byte r, byte g, byte b)
    {
        R = r;
        G = g;
        B = b;
    }

    /// <summary>
    /// White color RGB(255, 255, 255).
    /// </summary>
    public static OptionColor White = new OptionColor(255, 255, 255);
    /// <summary>
    /// Black color RGB(0, 0, 0).
    /// </summary>
    public static OptionColor Black = new OptionColor(0, 0, 0);
    /// <summary>
    /// Red color RGB(255, 0, 0).
    /// </summary>
    public static OptionColor Red = new OptionColor(255, 0, 0);
    /// <summary>
    /// Green color RGB(0, 255, 0).
    /// </summary>
    public static OptionColor Green = new OptionColor(0, 255, 0);
    /// <summary>
    /// Blue color RGB((0, 0, 255).
    /// </summary>
    public static OptionColor Blue = new OptionColor(0, 0, 255);
    /// <summary>
    /// Magenta color RGB(255, 0, 255).
    /// </summary>
    public static OptionColor Magenta = new OptionColor(255, 0, 255);
    /// <summary>
    /// Cyan color RGB(0, 255, 255).
    /// </summary>
    public static OptionColor Cyan = new OptionColor(0, 255, 255);
}
