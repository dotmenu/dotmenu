using dotmenu;

using Hangman.Credits;
using Hangman.Gameplay;

public class MainMenu
{
    static void Main()
    {
        Run();
    }
    public static void Run()
    {
        Menu
        .New()
        .SetPrompt("Hangman\n")
        .SetOptionSelector(">")
        .AddOption<PlayOption>()
        .AddOption<CreditsOption>()
        .AddOption(text: "Exit", action: Exit)
        .Run();

        Console.ReadKey();
        return;

        static void Exit() =>
            Environment.Exit(exitCode: 0);
    }
}