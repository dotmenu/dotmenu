using Natesworks.Dotmenu.Extensions.MenuBuilder;

namespace Natesworks.Dotmenu.Tests.Extensions;

public class AddOptionTests
{
    [Fact]
    public void AddOption_WithNullMenu_ThrowsArgumentNullException()
    {
        IMenuBuilder? builder = null;
        Assert.Throws<ArgumentNullException>(TestAction);
        return;
        
        void TestAction() =>
            builder!.AddOption("text");
    }
    
    [Theory]
    [InlineData(null!)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    public void AddOption_WithInvalidText_ThrowsArgumentException(string? text)
    {
        var builder = Menu.CreateDefaultBuilder();
        Assert.Throws<ArgumentException>(TestAction);
        return;
        
        void TestAction() =>
            builder.AddOption(text!);
    }
    
    [Fact]
    public void AddOption_WithValidText_AddsOptionToMenu()
    {
        var menu = Menu
            .CreateDefaultBuilder()
            .AddOption("sample")
            .Build();
        
        Assert.Single(menu.Elements);
    }
}