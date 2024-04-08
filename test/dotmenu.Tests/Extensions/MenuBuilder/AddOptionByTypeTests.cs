namespace dotmenu.Tests.Extensions;

public class AddPredfinedOptionTests
{
    [Fact]
    public void AddPredfinedOption_WithNullMenu_ThrowsArgumentNullException()
    {
        IMenuBuilder? builder = null;
        Assert.Throws<ArgumentNullException>(TestAction);
        return;
        
        void TestAction() =>
            builder!.AddOption<PredefinedOption>();
    }
    
    [Fact]
    public void AddPredfinedOption_AddsOptionToMenu()
    {
        var menu = Menu
            .CreateDefaultBuilder()
            .AddOption<PredefinedOption>()
            .Build();
        
        Assert.Single(menu.Elements);
    }

    private sealed class PredefinedOption
        : MenuOption
    {
        public PredefinedOption()
            : base("Predefined Option")
        {
        }
    }
}