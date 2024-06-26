[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/dotmenu/dotmenu/dotnet.yml)
# About Dotmenu
A straightforward console menu library designed to streamline user interactions in console applications.
Works on every terminal that supports ANSI escape codes.

## Installation
Dotmenu can be added to your project with:

```bash
dotnet add package Dotmenu
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
![Hangman Screenshot
](https://raw.githubusercontent.com/dotmenu/dotmenu/main/resources/screenshots/hangman.png)

# Special thanks

Special thanks to [tacosontitan](https://github.com/tacosontitan) for adding unit tests, making hangman sample and making new API Surface.
And thanks to [samwise](https://github.com/samwise-the-very-brave) for fixing bugs.

# Documentation

For more documentation on how to create and configure menus please read the wiki at [dotmenu.natesworks.com](https://dotmenu.natesworks.com)
