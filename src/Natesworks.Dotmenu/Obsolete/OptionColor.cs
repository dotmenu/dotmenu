namespace dotmenu;

/// <summary>
/// Represents a RGB color and provides some default color values.
/// </summary>
public struct OptionColor
{
    public byte R { get; set; }

    public byte G { get; set; }

    public byte B { get; set; }

    public OptionColor(
        byte r,
        byte g,
        byte b)
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

    /// <summary>
    /// DarkBlue color RGB(0, 0, 128).
    /// </summary>
    public static OptionColor DarkBlue = new OptionColor(0, 0, 128);

    /// <summary>
    /// DarkCyan color RGB(0, 128, 128).
    /// </summary>
    public static OptionColor DarkCyan = new OptionColor(0, 128, 128);

    /// <summary>
    /// DarkGray color RGB(128, 128, 128).
    /// </summary>
    public static OptionColor DarkGray = new OptionColor(128, 128, 128);

    /// <summary>
    /// DarkGreen color RGB(0, 128, 0).
    /// </summary>
    public static OptionColor DarkGreen = new OptionColor(0, 128, 0);

    /// <summary>
    /// DarkMagenta color RGB(128, 0, 128).
    /// </summary>
    public static OptionColor DarkMagenta = new OptionColor(128, 0, 128);

    /// <summary>
    /// DarkRed color RGB(128, 0, 0).
    /// </summary>
    public static OptionColor DarkRed = new OptionColor(128, 0, 0);

    /// <summary>
    /// DarkYellow color RGB(128, 128, 0).
    /// </summary>
    public static OptionColor DarkYellow = new OptionColor(128, 128, 0);

    /// <summary>
    /// Gray color RGB(192, 192, 192).
    /// </summary>
    public static OptionColor Gray = new OptionColor(192, 192, 192);

    /// <summary>
    /// Yellow color RGB(255, 255, 0).
    /// </summary>
    public static OptionColor Yellow = new OptionColor(255, 255, 0);

    /// <summary>
    /// Converts ConsoleColor to OptionColor.
    /// </summary>
    /// <param name="color">ConsoleColor to convert.</param>
    /// <returns>Equivalent OptionColor.</returns>
    public static OptionColor ConvertConsoleColor(ConsoleColor color)
    {
        switch (color)
        {
            case ConsoleColor.Black:
                return OptionColor.Black;
            case ConsoleColor.Blue:
                return OptionColor.Blue;
            case ConsoleColor.Cyan:
                return OptionColor.Cyan;
            case ConsoleColor.DarkBlue:
                return OptionColor.DarkBlue;
            case ConsoleColor.DarkCyan:
                return OptionColor.DarkCyan;
            case ConsoleColor.DarkGray:
                return OptionColor.DarkGray;
            case ConsoleColor.DarkGreen:
                return OptionColor.DarkGreen;
            case ConsoleColor.DarkMagenta:
                return OptionColor.DarkMagenta;
            case ConsoleColor.DarkRed:
                return OptionColor.DarkRed;
            case ConsoleColor.DarkYellow:
                return OptionColor.DarkYellow;
            case ConsoleColor.Gray:
                return OptionColor.Gray;
            case ConsoleColor.Green:
                return OptionColor.Green;
            case ConsoleColor.Magenta:
                return OptionColor.Magenta;
            case ConsoleColor.Red:
                return OptionColor.Red;
            case ConsoleColor.White:
                return OptionColor.White;
            case ConsoleColor.Yellow:
                return OptionColor.Yellow;
            default:
                return OptionColor.White;
        }
    }
}