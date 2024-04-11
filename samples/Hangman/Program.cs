using Hangman.Credits;
using Hangman.Gameplay;

using Natesworks.Dotmenu;
using Natesworks.Dotmenu.Extensions.MenuBuilder;

Menu
    .CreateCenteredBuilder()
    .SetTitle("Hangman (1.0.0)")
    .AddOption<PlayOption>()
    .AddOption<CreditsOption>()
    .AddOption(text: "Exit", action: Exit)
    .Build()
    .Run();

Console.ReadKey();
return;

static void Exit() =>
    Environment.Exit(exitCode: 0);