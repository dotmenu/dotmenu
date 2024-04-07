using dotmenu;

namespace Hangman.Credits;

/// <summary>
/// Represents an <see cref="MenuOption"/> to manage game settings.
/// </summary>
public sealed class CreditsOption()
    : MenuOption(
        text: "Credits",
        action: ShowCredits)
{
    private static void ShowCredits()
    {
        var creditsScene = new CreditsScene();
        creditsScene.Start();
    }
}