using dotmenu;

namespace Hangman.Options;

/// <summary>
/// Represents an <see cref="Option"/> to manage game settings.
/// </summary>
public sealed class Settings()
    : Option(
        text: "Settings",
        action: OpenSettingsMenu,
        hidden: false,
        disabled: false,
        fg: null,
        bg: null,
        selectedFg: null,
        selectedBg: null,
        optionPrefix: null,
        selector: null)
{
    private static void OpenSettingsMenu() =>
        Console.WriteLine("Settings menu opened!");
}