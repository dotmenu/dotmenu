using dotmenu;

using Hangman.Options;

Menu
    .New()
    .SetPrompt("Select an option:")
    .SetOptionSelector(">")
    .AddOption<Play>()
    .AddOption<Settings>()
    .AddOption(text: "Exit", action: Exit)
    .Run();

return;

static void Exit() =>
    Environment.Exit(exitCode: 0);