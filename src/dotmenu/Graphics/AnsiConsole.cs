using System.Numerics;

namespace dotmenu.Graphics;

/// <summary>
/// Provides a set of methods for interacting with the console.
/// </summary>
public static class AnsiConsole
{
    private const string EscapeSequence = "\x1b";
    
    /// <summary>
    /// Clears the console.
    /// </summary>
    public static void Clear() =>
        Console.Clear();
    
    /// <summary>
    /// Writes the specified text to the console.
    /// </summary>
    /// <param name="text">The text to write.</param>
    /// <param name="color">The color to use.</param>
    /// <param name="position">The position within the console to write the text.</param>
    public static void Write(
        string? text,
        AnsiConsoleColor? color = null,
        Vector2? position = null)
    {
        if (text is null)
            return;

        AddForegroundColor(ref text, color?.Foreground);
        AddBackgroundColor(ref text, color?.Background);
        ResetAttributes(ref text);
        
        SetCursorPosition(position ?? Vector2.Zero);
        Console.Write(text);
    }
    
    /// <summary>
    /// Writes the specified text to the console followed by a new line.
    /// </summary>
    /// <param name="text">The text to write.</param>
    /// <param name="color">The color to use.</param>
    /// <param name="position">The position within the console to write the text.</param>
    public static void WriteLine(
        string? text,
        AnsiConsoleColor? color = null,
        Vector2? position = null)
    {
        if (text is null)
            return;
        
        AddForegroundColor(ref text, color?.Foreground);
        AddBackgroundColor(ref text, color?.Background);
        ResetAttributes(ref text);
        
        SetCursorPosition(position);
        Console.WriteLine(text);
    }

    /// <summary>
    /// Sets the console's cursor position.
    /// </summary>
    /// <param name="position">The position to set the cursor to.</param>
    public static void SetCursorPosition(Vector2? position)
    {
        if (position is null)
            return;
        
        var x = checked((int)position.Value.X);
        var y = checked((int)position.Value.Y);
        Console.SetCursorPosition(x, y);
    }

    private static void ResetAttributes(ref string text) =>
        text += $"{EscapeSequence}[0m";

    public static void AddForegroundColor(
        ref string text,
        ThemeColor? foreground = null)
    {
        if (foreground is null)
            return;
        
        var (red, green, blue) = foreground.Value;
        text = $"{EscapeSequence}[38;2;{red};{green};{blue}m{text}";
    }
    
    public static void AddBackgroundColor(
        ref string text,
        ThemeColor? background = null)
    {
        if (background is null)
            return;
        
        var (red, green, blue) = background.Value;
        text = $"{EscapeSequence}[48;2;{red};{green};{blue}m{text}";
    }
}