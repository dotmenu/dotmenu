namespace Hangman;

/// <summary>
/// Represents a scene in the game.
/// </summary>
public abstract class Scene
{
    private bool _running;
    
    /// <summary>
    /// Starts the scene.
    /// </summary>
    public void Start()
    {
        if (_running)
            return;
        
        _running = true;
        while (_running)
            Update();
    }

    /// <summary>
    /// Ends the scene.
    /// </summary>
    public void End() =>
        _running = false;
    
    /// <summary>
    /// Updates the scene.
    /// </summary>
    protected abstract void Update();

    /// <summary>
    /// Renders the specified text centered on the console.
    /// </summary>
    /// <param name="text">The text to render.</param>
    protected static void WriteCentered(string text)
    {
        var verticalPadding = Console.WindowHeight / 2;
        var horizontalPadding = (Console.WindowWidth - text.Length) / 2;
        Console.SetCursorPosition(horizontalPadding, verticalPadding);
        Console.Write(text);
    }
    
    /// <summary>
    /// Renders the specified text in the lower-left corner of the console.
    /// </summary>
    /// <param name="text">The text to render.</param>
    protected static void WriteLowerLeft(string text)
    {
        var consoleHeight = Console.WindowHeight;
        Console.SetCursorPosition(0, consoleHeight - 1);
        Console.Write(text);
    }
}