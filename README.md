[![License: GPL v2](https://img.shields.io/badge/License-GPL_v2-blue.svg)](https://www.gnu.org/licenses/old-licenses/gpl-2.0.en.html)
![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/dotmenu/dotmenu/dotnet.yml)
# About Dotmenu
A straightforward console menu library designed to streamline user interactions in console applications.
Works on every terminal that supports ANSI escape codes.

## Installation
Dotmenu can be added to your project with:

```bash
dotnet add package Natesworks.Dotmenu
```

## Menu Example

```cs
var menu = Menu.New();
menu.SetPrompt("Select an option:");
menu.Colors(OptionColor.Green, OptionColor.Blue);
menu.ColorsWhenSelected(OptionColor.Blue, OptionColor.Green);
menu.SetOptionSelector(">");

menu.AddOption(
    () => "Option 1",
    () => Console.WriteLine("Option 1 selected"),
    fg: OptionColor.Green,
    bg: OptionColor.Black,
    optionPrefix: "Custom Prefix: "
);

menu.AddOption(
    () => "Hidden Option",
    () => Console.WriteLine("Hidden option selected"),
    hidden: true
);

menu.AddOption(
    () => "Option 3",
    () => Console.WriteLine("Option 3 selected"),
    ConsoleKey.D3
);

menu.Run();
```

# Documentation

Explore our wiki sections for guidance [here](https://github.com/dotmenu/dotmenu/wiki)
