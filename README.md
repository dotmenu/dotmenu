[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/dotmenu/dotmenu/dotnet.yml)

## Important update

I (Natesworks) will no longer be making any major updates to dotmenu.

**Why?**

There are many reasons so I'll just mention a few:

- Working on other projects

  
I am activly working on multiple other projects and there are just better packages for menus like Terminal.GUI.
- Demotivated

  
A big reason is also because I am simply demotivated. I just do not want to work on this project anymore and this is the reason I have not made any major contributions in ~1 month.

**Old readme below**

# Dotmenu
A straightforward console menu library designed to streamline user interactions in console applications.

## Installation
Dotmenu can be added to your project with:

```bash
dotnet add package Dotmenu
```

## Menu Example

```cs
var menu = Menu.New();
menu.SetPrompt("Select an option:");
menu.SetOptionSelector(">");

menu.AddOption(
    "Option 1",
    () => Console.WriteLine("Option 1 selected"),
    fg: OptionColor.White,
    bg: OptionColor.Blue,
    optionPrefix: "Custom Prefix: "
);

menu.AddOption(
    "Hidden Option",
    () => Console.WriteLine("Hidden option selected"),
    hidden: true
);

menu.AddOption(
    "Option 3",
    () => Console.WriteLine("Option 3 selected"),
    shortcut:ConsoleKey.A
);
menu.Run();
```
![](https://raw.githubusercontent.com/dotmenu/dotmenu/main/resources/screenshots/menu.png)

## MultiSelectMenu Example

```cs
var menu = MultiSelectMenu.New();
menu.SetPrompt("Select an option:");
menu.SetOptionSelector(">");

menu.AddOption(
    "Option 1",
    bg: OptionColor.Blue,
    optionPrefix: "Custom Prefix: "
);

menu.AddOption(
    "Hidden Option",
    hidden: true
);

menu.AddOption(
    "Option 3",
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

![](https://raw.githubusercontent.com/dotmenu/dotmenu/main/resources/screenshots/multiselectmenu.png)

## Documentation

For more documentation on how to create and configure menus please read the wiki at [dotmenu.natesworks.com](https://dotmenu.natesworks.com)
