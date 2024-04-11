using Natesworks.Dotmenu.Extensions.Option;

namespace Natesworks.Dotmenu.Tests.Extensions;

public class ValidateTests
{
    [Fact]
    public void Validate_WithNullOption_ThrowsArgumentNullException()
    {
        IMenuOption? option = null;
        Assert.Throws<ArgumentNullException>(TestAction);
        return;
        
        void TestAction() =>
            option!.Validate();
    }
    
    [Theory]
    [InlineData(null!)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    public void Validate_WithInvalidText_ThrowsArgumentException(string? text)
    {
        var option = new MenuOption(text);
        Assert.Throws<ArgumentException>(TestAction);
        return;
        
        void TestAction() =>
            option.Validate();
    }
    
    [Fact]
    public void Validate_WithValidText_DoesNotThrow()
    {
        var option = new MenuOption("text");
        option.Validate();
    }
}