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

## Menu Example

```cs
using dotmenu;
var menu = Menu.New();
menu.SetPrompt("Select an option:");
menu.SetOptionSelector(">");

menu.AddOption(
    () => "Option 1",
    () => Console.WriteLine("Option 1 selected"),
    fg: OptionColor.White,
    bg: OptionColor.Blue,
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
    shortcut:ConsoleKey.A
);
menu.Run();
```
![](https://i.ibb.co/8XR37T5/image.png)

## MultiSelectMenu Example

```cs
var menu = MultiSelectMenu.New();
menu.SetPrompt("Select an option:");
menu.SetOptionSelector(">");

menu.AddOption(
    () => "Option 1",
    () => Console.WriteLine("Option 1 selected"),
    bg: OptionColor.Blue,
    optionPrefix: "Custom Prefix: "
);

menu.AddOption(
    () => "Hidden Option",
    () => Console.WriteLine("Hidden option selected"),
    hidden: true
);

menu.AddOption(
    () => "Option 3",
    () => menu.options[1].SetHidden(false),
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

![](https://i.ibb.co/BGqvHVk/image.png)

# Documentation

For more documentation on how to create and configure menus please read the wiki at [dotmenu.natesworks.com](https://dotmenu.natesworks.com)
