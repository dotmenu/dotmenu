namespace dotmenu.Tests.Extensions;

public class AddPredfinedOptionTests
{
    [Fact]
    public void AddPredfinedOption_WithNullMenu_ThrowsArgumentNullException()
    {
        Menu? menu = null;
        Assert.Throws<ArgumentNullException>(TestAction);
        return;
        
        void TestAction() =>
            menu!.AddOption<PredefinedOption>();
    }
    
    [Fact]
    public void AddPredfinedOption_AddsOptionToMenu()
    {
        var menu = new Menu();
        menu.AddOption<PredefinedOption>();
        Assert.Single(menu.options);
    }

    private sealed class PredefinedOption
        : Option
    {
        public PredefinedOption()
            : base("Predefined Option")
        {
            
        }
    }
}