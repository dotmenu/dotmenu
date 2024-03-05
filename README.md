# CrystalSharp

CrystalSharp is a C# package designed to simplify the creation of menus in console applications. With CrystalSharp, you can easily create interactive menus for user interaction.

## Features

- Create customizable menus.
- Assign keyboard shortcuts to menu options for quicker navigation.
- Edit menu options dynamically during runtime.

## Installation

CrystalSharp can be added to your project with:

```bash
dotnet add package CrystalSharpMenu
```

## Basic Example

```cs
using CrystalSharp;

class Program
{
    static void Main(string[] args)
    {
        Menu menu = new Menu();

        menu.SetPrompt("Select an option:");
        menu.Colors(Color.Yellow, Color.DarkBlue);
        menu.ColorsWhenSelected(Color.DarkBlue, Color.Yellow);

        menu.AddOption("Option 1", () => Console.WriteLine("You selected Option 1"));
        menu.AddOption("Option 2", () => Console.WriteLine("You selected Option 2"), ConsoleKey.D2);
        menu.AddOption("Option 3", () => Console.WriteLine("You selected Option 3"), ConsoleKey.D3);

        menu.Run();
    }
}
```

# Documentation

Explore our wiki sections for guidance [here](https://github.com/natesworks/crystalsharp/wiki)
