using FluentAssertions;

namespace VerifyNumberWithOperationsConsoleApp.Tests;

public class InputParserTests
{
    private readonly InputParser _parser;

    public InputParserTests()
    {
        _parser = new InputParser();
    }

    [Theory]
    [InlineData("list = [1,2,3,4] target = 21", new[] { 1.0, 2.0, 3.0, 4.0 }, 21.0)]
    [InlineData("list = [4,3,2,5] target = 33", new[] { 4.0, 3.0, 2.0, 5.0 }, 33.0)]
    [InlineData("list = [3,4,2] target = 5", new[] { 3.0, 4.0, 2.0 }, 5.0)]
    [InlineData("list=[1,2,3]target=10", new[] { 1.0, 2.0, 3.0 }, 10.0)]
    [InlineData("list = [1.5, 2.5, 3.5] target = 7.5", new[] { 1.5, 2.5, 3.5 }, 7.5)]
    public void Parse_ValidInput_ReturnsCorrectNumbersAndTarget(string input, double[] expectedNumbers, double expectedTarget)
    {
        // Act
        var (numbers, target) = _parser.Parse(input);

        // Assert
        numbers.Should().Equal(expectedNumbers);
        target.Should().Be(expectedTarget);
    }

    [Theory]
    [InlineData("list = [1] target = 1")]
    [InlineData("list = [10,20,30,40,50] target = 100")]
    public void Parse_DifferentListSizes_ParsesCorrectly(string input)
    {
        // Act
        var result = _parser.Parse(input);

        // Assert
        result.Numbers.Should().NotBeNull();
        result.Numbers.Should().NotBeEmpty();
    }

    [Theory]
    [InlineData("list = [-1,-2,-3] target = -6")]
    [InlineData("list = [1,2,3] target = -10")]
    [InlineData("list = [-5] target = 5")]
    public void Parse_NegativeNumbers_ParsesCorrectly(string input)
    {
        // Act
        var action = () => _parser.Parse(input);

        // Assert
        action.Should().NotThrow();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Parse_NullOrWhiteSpaceInput_ThrowsArgumentException(string? input)
    {
        // Act
        var action = () => _parser.Parse(input);

        // Assert
        action.Should().Throw<ArgumentException>()
            .WithMessage("*Input cannot be null or empty*");
    }

    [Theory]
    [InlineData("no list here target = 10")]
    [InlineData("list = target = 10")]
    [InlineData("target = 10")]
    [InlineData("random text")]
    public void Parse_MissingList_ThrowsFormatException(string input)
    {
        // Act
        var action = () => _parser.Parse(input);

        // Assert
        action.Should().Throw<FormatException>()
            .WithMessage("*List not found*");
    }

    [Theory]
    [InlineData("list = [1,2,3]")]
    [InlineData("list = [1,2,3] invalid")]
    [InlineData("list = [1,2,3] target =")]
    public void Parse_MissingTarget_ThrowsFormatException(string input)
    {
        // Act
        var action = () => _parser.Parse(input);

        // Assert
        action.Should().Throw<FormatException>()
            .WithMessage("*Target value not found*");
    }

    [Theory]
    [InlineData("list = [] target = 10")]
    public void Parse_EmptyList_ThrowsFormatException(string input)
    {
        // Act
        var action = () => _parser.Parse(input);

        // Assert
        action.Should().Throw<FormatException>()
            .WithMessage("*List cannot be empty*");
    }

    [Theory]
    [InlineData("list = [1,abc,3] target = 10")]
    [InlineData("list = [1,2,three] target = 10")]
    public void Parse_InvalidNumberFormat_ThrowsFormatException(string input)
    {
        // Act
        var action = () => _parser.Parse(input);

        // Assert
        action.Should().Throw<FormatException>()
            .WithMessage("*Invalid number format*");
    }

    [Fact]
    public void Parse_WhitespaceInNumbers_ParsesCorrectly()
    {
        // Arrange
        var input = "list = [ 1 , 2 , 3 ] target = 6";

        // Act
        var (numbers, target) = _parser.Parse(input);

        // Assert
        numbers.Should().Equal(1.0, 2.0, 3.0);
        target.Should().Be(6.0);
    }

    [Theory]
    [InlineData("list = [1,2,3] target = 10.5")]
    [InlineData("list = [1.1,2.2,3.3] target = 6.6")]
    [InlineData("list = [0.1,0.2,0.3] target = 0.6")]
    public void Parse_DecimalNumbers_ParsesCorrectly(string input)
    {
        // Act
        var action = () => _parser.Parse(input);

        // Assert
        action.Should().NotThrow();
    }
}
