using dotmenu;

namespace Hangman.Credits;

/// <summary>
/// Represents an <see cref="Option"/> to manage game settings.
/// </summary>
public sealed class CreditsOption()
    : Option(
        text: "Credits",
        action: ShowCredits)
{
    private static void ShowCredits()
    {
        var creditsScene = new CreditsScene();
        creditsScene.Start();
    }
}