using System.Collections.Immutable;

using Natesworks.Dotmenu.Formatting;
using Natesworks.Dotmenu.Graphics;

namespace Natesworks.Dotmenu;

/// <summary>
/// Represents a menu.
/// </summary>
public sealed partial class Menu
    : IMenu
{
    private readonly IMenuConfiguration _configuration;
    private readonly IMenuRenderer _renderer;
    private int _selectedIndex;

    /// <summary>
    /// Creates a new menu instance.
    /// </summary>
    /// <param name="configuration">The configuration to use.</param>
    /// <param name="renderer">The renderer to use.</param>
    /// <param name="elements">The elements of the menu.</param>
    internal Menu(
        IMenuConfiguration configuration,
        IMenuRenderer renderer,
        ImmutableArray<IMenuElement> elements)
    {
        _configuration = configuration;
        _renderer = renderer;
        Elements = elements;
    }
    
    /// <inheritdoc />
    public ImmutableArray<IMenuElement> Elements { get; }

    /// <inheritdoc />
    public void Run()
    {
        if (!AnsiDetector.SupportsAnsi)
        {
            Console.WriteLine("Please use a terminal that supports ANSI escape codes.");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            
            Environment.Exit(2);
            return;
        }

        Initialize();
        CancellationToken cancellationToken = default;
        UpdateMenu().Wait(cancellationToken);
        
        async Task UpdateMenu()
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(_configuration.RefreshRate);
                if (!TryGetInput())
                    continue;
            
                _renderer.Render(this);
            }
        }
    }

    private void Initialize()
    {
        var firstOption = Elements.OfType<IMenuOption>().FirstOrDefault();
        if (firstOption is null)
        {
            Console.WriteLine("No options found.");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            
            Environment.Exit(3);
            return;
        }
        
        firstOption.Selected = true;
        _renderer.Render(this);
    }

    private bool TryGetInput()
    {
        var key = Console.ReadKey(intercept: true).Key;
        switch (key)
        {
            case ConsoleKey.UpArrow:
                MoveSelection(-1);
                return true;
            case ConsoleKey.DownArrow:
                MoveSelection(1);
                return true;
            case ConsoleKey.Enter:
                InvokeSelectedOption();
                return true;
            default:
                return false;
        }
    }
    
    private void MoveSelection(int offset)
    {
        var options = Elements.OfType<IMenuOption>().ToArray();
        var selectedOption = options[_selectedIndex];
        selectedOption.Selected = false;

        _selectedIndex += offset;
        if (_selectedIndex < 0)
            _selectedIndex = options.Length - 1;
        else if (_selectedIndex >= options.Length)
            _selectedIndex = 0;

        selectedOption = options[_selectedIndex];
        selectedOption.Selected = true;
    }
    
    private void InvokeSelectedOption()
    {
        var selectedOption = Elements.OfType<IMenuOption>().ElementAtOrDefault(_selectedIndex);
        selectedOption?.Invoke();
    }
}