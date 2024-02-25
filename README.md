# CrystalSharp

CrystalSharp is a straightforward console menu library designed to streamline user interactions in console applications.

## How To Use

### Prompt

The prompt is the message displayed before presenting the menu options. For instance: "Select an option:"
```cs
.Prompt("Select an option:")
```

### Prefix & Suffix
The prefix is the text shown before the name of each option. You can set the prefix using:
```cs
.Prefix(prefix)
```
Additionally, there's an option to dynamically change the prefix and/or suffix for the selected option:
```cs
.PrefixWhenSelected(prefix) or .SuffixWhenSelected(suffix)
```

### Colors
Customize the foreground and background colors using:
```cs
.Colors(fg, bg)
```
You can also configure the foreground and background colors differently for the selected option:
```cs
.ColorsWhenSelected(fg, bg)
```

### Creating a Menu

You can add any desired options, along with customizations like prompt, prefix, suffix, and colors.
```
Menu mainMenu = new Menu(options)
                    .Prompt("Select an option:")
                    .Colors(ConsoleColor.White, ConsoleColor.Black)
                    .ColorsWhenSelected(ConsoleColor.Black, ConsoleColor.White);
```

## Example Usage

```
using CrystalSharp;

class Program
{
    static void Main(string[] args)
    {
        string[] options = {"Option 1", "Option 2"};
        string prompt = "Select an option:";
        Menu mainMenu = new Menu(options)
                            .Prompt(prompt)
                            .Colors(ConsoleColor.White, ConsoleColor.Black)
                            .ColorsWhenSelected(ConsoleColor.Black, ConsoleColor.White);
        int selectedIndex = mainMenu.Run();

        if(selectedIndex == 0)
        {
            Console.WriteLine("Option 1 selected");
        }
        else if(selectedIndex == 1)
        {
            Console.WriteLine("Option 2 selected");
        }

        Console.ReadKey();
    }
}
```

## Contributing

You can contribute by submitting a Pull Request or opening an Issue on GitHub [@natesworks/crystalsharp](https://github.com/natesworks/crystalsharp).
