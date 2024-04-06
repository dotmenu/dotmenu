namespace Hangman.Gameplay;

/// <summary>
/// Represents the scene where the player guesses the word.
/// </summary>
public sealed class GuessScene
    : Scene
{
    private readonly string _word = WordBag.Pick();
    private char[]? _placeholder = null;
    private int _lives = 6;

    /// <inheritdoc />
    protected override void Update()
    {
        if (!TryBeginRound())
            return;

        RenderDisplay();
        HandlePlayerGuess();
    }

    private bool TryBeginRound()
    {
        Console.Clear();
        var gameOver = _lives <= 0
            || _placeholder is not null
            && !_placeholder.Contains('_');
        
        if (!gameOver)
            return true;
            
        var gameOverScene = new GameOverScene(_word);
        gameOverScene.Start();
        End();

        return false;
    }

    private void RenderDisplay()
    {
        if (_placeholder is null)
        {
            _placeholder = new char[_word.Length];
            for (var i = 0; i < _word.Length; i++)
                _placeholder[i] = '_';
        }

        var displayString = new string(_placeholder);
        WriteCentered(displayString);
        WriteLowerLeft($"Lives Remaining: {_lives}");
    }

    private void HandlePlayerGuess()
    {
        if (_placeholder is null)
            throw new InvalidOperationException("The placeholder has not been initialized.");
        
        var guessedCorrectly = false;
        var key = Console.ReadKey(intercept: true).KeyChar;
        for (var i = 0; i < _word.Length; i++)
        {
            if (_word[i] != key)
                continue;
            
            _placeholder[i] = key;
            guessedCorrectly = true;
        }
        
        if (!guessedCorrectly)
            _lives--;
    }
}