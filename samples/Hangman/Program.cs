using dotmenu;

using Hangman.Credits;
using Hangman.Gameplay;

Menu
    .New()
    .SetPrompt("Hangman (1.0.0)\n")
    .SetOptionSelector(">")
    .AddOption<PlayOption>()
    .AddOption<CreditsOption>()
    .AddOption(text: "Exit", action: Exit)
    .Run();

Console.ReadKey();
return;

static void Exit() =>
    Environment.Exit(exitCode: 0);