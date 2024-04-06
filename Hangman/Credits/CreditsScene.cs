namespace Hangman.Credits;

/// <summary>
/// Represents the scene that is displayed when the credits are shown.
/// </summary>
internal sealed class CreditsScene
    : Scene
{
    /// <inheritdoc />
    protected override void Update()
    {
        WriteCentered("Written By: tacosontitan");
        End();
    }
}