[![License: GPL v2](https://img.shields.io/badge/License-GPL_v2-blue.svg)](https://www.gnu.org/licenses/old-licenses/gpl-2.0.en.html)
# About Dotmenu
A straightforward console menu library designed to streamline user interactions in console applications.
Works on every terminal that supports ANSI escale codes.

## Installation
Dotmenu can be added to your project with:

```bash
dotnet add package Natesworks.Dotmenu
```

## Example

```cs
using Natesworks.Dotmenu

class Program
{
    static void Main(string[] args)
    {
        Menu mainMenu = Menu.New()
            .SetPrompt("Main Menu")
            .AddOption(() => "Option 1", () => Console.WriteLine("Option 1 selected"))
            .AddOption(() => "Option 2", () => Console.WriteLine("Option 2 selected"))
            .AddOption(() => "Sub Menu", () => ShowSubMenu());
        while(true)
        {
            mainMenu.Run();
        }
    }

    static void ShowSubMenu()
    {
        var subMenu1 = Menu.New()
            .SetPrompt("Sub Menu 1")
            .AddOption(() => "Sub Option 1-1", () => Console.WriteLine("Sub Option 1-1 selected"))
            .AddOption(() => "Sub Option 1-2", () => Console.WriteLine("Sub Option 1-2 selected"))
            .AddOption(() => "Back", () => {});

        subMenu1.Run();
    }
}
```

# Documentation

Explore our wiki sections for guidance [here](https://github.com/dotmenu/dotmenu/wiki)
