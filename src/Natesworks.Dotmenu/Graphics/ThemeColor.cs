namespace dotmenu.Graphics;

/// <summary>
/// Represents a RGB color and provides some default color values.
/// </summary>
public struct ThemeColor
{
    /// <summary>
    /// Creates a new color with the specified RGB components.
    /// </summary>
    /// <param name="r">The red component of the color.</param>
    /// <param name="g">The green component of the color.</param>
    /// <param name="b">The blue component of the color.</param>
    public ThemeColor(
        byte r,
        byte g,
        byte b)
    {
        R = r;
        G = g;
        B = b;
    }
    
    /// <summary>
    /// The color white (<c>255, 255, 255</c>).
    /// </summary>
    public static ThemeColor White { get; } = new ThemeColor(255, 255, 255);
    
    /// <summary>
    /// The color black (<c>0, 0, 0</c>).
    /// </summary>
    public static ThemeColor Black { get; } = new ThemeColor(0, 0, 0);
    
    /// <summary>
    /// The color red (<c>255, 0, 0</c>).
    /// </summary>
    public static ThemeColor Red { get; } = new ThemeColor(255, 0, 0);
    
    /// <summary>
    /// The color green (<c>0, 255, 0</c>).
    /// </summary>
    public static ThemeColor Green { get; } = new ThemeColor(0, 255, 0);
    
    /// <summary>
    /// The color blue (<c>0, 0, 255</c>).
    /// </summary>
    public static ThemeColor Blue { get; } = new ThemeColor(0, 0, 255);
    
    /// <summary>
    /// The color magenta (<c>255, 0, 255</c>).
    /// </summary>
    public static ThemeColor Magenta { get; } = new ThemeColor(255, 0, 255);
    
    /// <summary>
    /// The color cyan (<c>0, 255, 255</c>).
    /// </summary>
    public static ThemeColor Cyan { get; } = new ThemeColor(0, 255, 255);
    
    /// <summary>
    /// The color yellow (<c>255, 255, 0</c>).
    /// </summary>
    public static ThemeColor Yellow { get; } = new ThemeColor(255, 255, 0);
    
    /// <summary>
    /// Gets or sets the red component of the color.
    /// </summary>
    public byte R { get; set; }

    /// <summary>
    /// Gets or sets the green component of the color.
    /// </summary>
    public byte G { get; set; }

    /// <summary>
    /// Gets or sets the blue component of the color.
    /// </summary>
    public byte B { get; set; }
    
    /// <summary>
    /// Deconstructs the color into its individual components.
    /// </summary>
    /// <param name="r">The red component of the color.</param>
    /// <param name="g">The green component of the color.</param>
    /// <param name="b">The blue component of the color.</param>
    public void Deconstruct(
        out byte r,
        out byte g,
        out byte b)
    {
        r = R;
        g = G;
        b = B;
    }
}