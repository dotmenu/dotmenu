# CrystalSharp

A simple console menu library

## How To Use

### Prompt

The prompt is the message sent before the menu. For example: "Select an option:"
```cs
.Prompt("Select an option:")
```

### Prefix & Suffix
The prefix is the text shown before the name of the option
You can set the prefix with .Prefix(prefix)
Theres also an option to have the prefix or/and suffix change for the option that is selected
You can do that using .PrefixWhenSelected(prefix) or .SuffixWhenSelected(suffix)

### Colors
You can change the colors of the foreground or background using:
```cs
.Colors(fg, bg)
```
Theres also an option to have the foreground or background be diffrent for the selected option with:
```cs
.ColorsWhenSelected(fg, bg)
```

### Create Menu

You can add or not add any option (prompt, prefix, suffix etc.) except options which are required.
```cs
Menu mainMenu = new Menu(options)
                    .Prompt("Select an option:")
                    .Colors(ConsoleColor.White, ConsoleColor.Black)
                    .ColorsWhenSelected(ConsoleColor.Black, ConsoleColor.White);
```

## Example Usage

```cs
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

You can contribute by making a PR/Issue @ github.com/natesworks/crystalsharp