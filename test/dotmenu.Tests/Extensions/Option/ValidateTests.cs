namespace Dotmenu.Tests.Extensions;

public class ValidateTests
{
    [Fact]
    public void Validate_WithNullOption_ThrowsArgumentNullException()
    {
        Option? option = null;
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
        var option = new Option(text);
        Assert.Throws<ArgumentException>(TestAction);
        return;
        
        void TestAction() =>
            option.Validate();
    }
    
    [Fact]
    public void Validate_WithValidText_DoesNotThrow()
    {
        var option = new Option("text");
        option.Validate();
    }
    
    [Fact]
    public void Validate_WithNullTextFunction_ThrowsArgumentNullException()
    {
        var option = new Option(null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null);
        
        Assert.Throws<ArgumentNullException>(TestAction);
        return;
        
        void TestAction() =>
            option.Validate();
    }
}