# Dotmenu

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
        var subMenu1 = new Menu()
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
