using Natesworks.Dotmenu;

namespace Hangman.Gameplay;

/// <summary>
/// Represents an option <see cref="MenuOption"/> to start the game.
/// </summary>
public sealed class PlayOption()
    : MenuOption(text: "Play")
{
    /// <inheritdoc />
    public override void Invoke()
    {
        var guessScene = new GuessScene();
        guessScene.Start();
    }
}