namespace Hangman.Gameplay;

/// <summary>
/// Represents the scene that is displayed when the game is over.
/// </summary>
internal sealed class GameOverScene(string word)
    : Scene
{
    /// <inheritdoc />
    protected override void Update()
    {
        WriteCentered("Game Over!");
        WriteLowerLeft($"The word was: {word}");
        End();
    }
}