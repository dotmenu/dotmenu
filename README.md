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
    .SetPrefix("")
    .SetSelector(">")
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
Will be adding more samples soon aswell as screenshots.

# Special thanks

Special thanks to tacosontitan for adding unit tests, making hangman sample and making new API Surface.
and samwise-the-very-brave for fixing text flickering on windows and adding inheritance.

# Documentation

For more documentation on how to create and configure menus please read the wiki at [dotmenu.natesworks.com](https://dotmenu.natesworks.com)
