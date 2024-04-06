using dotmenu;

namespace Hangman.Gameplay;

/// <summary>
/// Represents an option <see cref="Option"/> to start the game.
/// </summary>
public sealed class PlayOption()
    : Option(
        text: "Play",
        action: StartGame)
{
    private static void StartGame()
    {
        var guessScene = new GuessScene();
        guessScene.Start();
    }
}