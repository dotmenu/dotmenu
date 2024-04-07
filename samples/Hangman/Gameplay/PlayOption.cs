using dotmenu;

namespace Hangman.Gameplay;

/// <summary>
/// Represents an option <see cref="MenuOption"/> to start the game.
/// </summary>
public sealed class PlayOption()
    : MenuOption(
        text: "Play",
        action: StartGame)
{
    private static void StartGame()
    {
        var guessScene = new GuessScene();
        guessScene.Start();
    }
}