using Natesworks.Dotmenu;

namespace Hangman.Credits;

/// <summary>
/// Represents an <see cref="MenuOption"/> to manage game settings.
/// </summary>
public sealed class CreditsOption()
    : MenuOption(text: "Credits")
{
    /// <inheritdoc />
    public override void Invoke()
    {
        var creditsScene = new CreditsScene();
        creditsScene.Start();
    }
}