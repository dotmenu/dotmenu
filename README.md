[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/dotmenu/dotmenu/dotnet.yml)
# About Dotmenu
A straightforward console menu library designed to streamline user interactions in console applications.
Works on every terminal that supports ANSI escape codes.

## Installation
Dotmenu can be added to your project with:

```bash
dotnet add package Natesworks.Dotmenu
```

## Usage

Creating a menu is similar to creating a host for modern .NET applications:

```csharp
Menu.CreateDefaultBuilder()
    .SetTitle("Sample")
    .SetPrefix("[ ]")
    .SetSelector("[x]")
    .AddOption<PlayOption>()
    .AddOption<CreditsOption>()
    .AddOption(text: "Exit", static () => Environment.Exit(0))
    .Build()
    .Run();
```

### Builders

Currently, there are a few builders available to help you create menus:

- `Menu.CreateDefaultBuilder()`
- `Menu.CreateCenteredBuilder()`

## Menu Samples

[Hangman](https://github.com/dotmenu/dotmenu/tree/main/samples/Hangman)

## MultiSelectMenu Example

```cs
var menu = MultiSelectMenu.New();
menu.SetPrompt("Select an option:");
menu.SetOptionSelector(">");

menu.AddOption(
    () => "Option 1",
    bg: OptionColor.Blue,
    optionPrefix: "Custom Prefix: "
);

menu.AddOption(
    () => "Hidden Option",
    hidden: true
);

menu.AddOption(
    () => "Option 3",
    ConsoleKey.A
);

menu.ActionOnEnter(() =>
{
    Console.WriteLine("Selected(tab) options:");
    foreach (var index in menu.GetCheckedOptions())
    {
        Console.WriteLine($"Option {index + 1}");
    }
});

menu.Run();
```

![](https://raw.githubusercontent.com/dotmenu/dotmenu/main/screenshots/multiselectmenu.png)

# Documentation

For more documentation on how to create and configure menus please read the wiki at [dotmenu.natesworks.com](https://dotmenu.natesworks.com)
