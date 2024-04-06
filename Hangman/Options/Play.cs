using dotmenu;

namespace Hangman.Options;

/// <summary>
/// Represents an option <see cref="Option"/> to start the game.
/// </summary>
public sealed class Play()
    : Option(
        text: "Play",
        action: StartGame,
        hidden: false,
        disabled: false,
        fg: null,
        bg: null,
        selectedFg: null,
        selectedBg: null,
        optionPrefix: null,
        selector: null)
{
    private static void StartGame() =>
        Console.WriteLine("Let's play!");
}