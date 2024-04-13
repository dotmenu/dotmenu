namespace Dotmenu.Tests.Extensions;

public class AddOptionTests
{
    [Fact]
    public void AddOption_WithNullMenu_ThrowsArgumentNullException()
    {
        Menu? menu = null;
        Assert.Throws<ArgumentNullException>(TestAction);
        return;
        
        void TestAction() =>
            menu!.AddOption("text");
    }
    
    [Theory]
    [InlineData(null!)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    public void AddOption_WithInvalidText_ThrowsArgumentException(string? text)
    {
        var menu = new Menu();
        Assert.Throws<ArgumentException>(TestAction);
        return;
        
        void TestAction() =>
            menu.AddOption(text!);
    }
    
    [Fact]
    public void AddOption_WithValidText_AddsOptionToMenu()
    {
        var menu = new Menu();
        menu.AddOption("text");
        Assert.Single(menu.options);
    }
}