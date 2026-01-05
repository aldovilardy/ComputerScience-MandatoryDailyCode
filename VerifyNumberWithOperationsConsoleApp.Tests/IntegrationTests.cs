using FluentAssertions;

namespace VerifyNumberWithOperationsConsoleApp.Tests;

/// <summary>
/// Integration tests that verify the complete workflow from input parsing to solution finding.
/// </summary>
public class IntegrationTests
{
    private readonly InputParser _parser;
    private readonly CalculatorSolver _solver;

    public IntegrationTests()
    {
        _parser = new InputParser();
        _solver = new CalculatorSolver();
    }

    [Theory]
    [InlineData("list = [1,2,3,4] target = 21", true)]
    [InlineData("list = [4,3,2,5] target = 33", true)]
    [InlineData("list = [3,4,2] target = 5", true)]  // (3+4)-2 = 5
    public void EndToEnd_ChallengeExamples_WorkCorrectly(string input, bool expectedResult)
    {
        // Act
        var (numbers, target) = _parser.Parse(input);
        var result = _solver.CanMakeTarget(numbers, target);

        // Assert
        result.Should().Be(expectedResult);
    }

    [Fact]
    public void EndToEnd_Example1_FindsCorrectSolution()
    {
        // Arrange
        var input = "list = [1,2,3,4] target = 21";

        // Act
        var (numbers, target) = _parser.Parse(input);
        var canMake = _solver.CanMakeTarget(numbers, target);
        var solution = _solver.FindSolution(numbers, target);

        // Assert
        canMake.Should().BeTrue();
        solution.Should().NotBeNull();
        solution.Should().NotBeEmpty();
        solution!.Count.Should().Be(3); // 3 operations for 4 numbers
    }

    [Fact]
    public void EndToEnd_Example2_FindsCorrectSolution()
    {
        // Arrange
        var input = "list = [4,3,2,5] target = 33";

        // Act
        var (numbers, target) = _parser.Parse(input);
        var canMake = _solver.CanMakeTarget(numbers, target);
        var solution = _solver.FindSolution(numbers, target);

        // Assert
        canMake.Should().BeTrue();
        solution.Should().NotBeNull();
        solution.Should().NotBeEmpty();
    }

    [Fact]
    public void EndToEnd_Example3_FindsSolution()
    {
        // Arrange
        var input = "list = [3,4,2] target = 5";

        // Act
        var (numbers, target) = _parser.Parse(input);
        var canMake = _solver.CanMakeTarget(numbers, target);
        var solution = _solver.FindSolution(numbers, target);

        // Assert
        canMake.Should().BeTrue(); // (3+4)-2 = 5
        solution.Should().NotBeNull();
        solution.Should().NotBeEmpty();
    }

    [Theory]
    [InlineData("list = [2,3] target = 5", true)]       // 2+3 = 5
    [InlineData("list = [2,3] target = 6", true)]       // 2*3 = 6
    [InlineData("list = [10,5] target = 2", true)]      // 10/5 = 2
    [InlineData("list = [10,5] target = 5", true)]      // 10-5 = 5
    [InlineData("list = [2,3] target = 10", false)]     // No solution
    public void EndToEnd_TwoNumbers_AllOperations(string input, bool expectedResult)
    {
        // Act
        var (numbers, target) = _parser.Parse(input);
        var result = _solver.CanMakeTarget(numbers, target);

        // Assert
        result.Should().Be(expectedResult);
    }

    [Fact]
    public void EndToEnd_WithDecimalNumbers_WorksCorrectly()
    {
        // Arrange
        var input = "list = [1.5,2.5,3.0] target = 12.0";

        // Act
        var (numbers, target) = _parser.Parse(input);
        var result = _solver.CanMakeTarget(numbers, target);

        // Assert
        result.Should().BeTrue(); // (1.5+2.5)*3 = 12
    }

    [Fact]
    public void EndToEnd_WithNegativeNumbers_WorksCorrectly()
    {
        // Arrange
        var input = "list = [-1,2,3] target = 4";

        // Act
        var (numbers, target) = _parser.Parse(input);
        var result = _solver.CanMakeTarget(numbers, target);

        // Assert
        result.Should().BeTrue(); // -1+2+3 = 4
    }

    [Fact]
    public void EndToEnd_SingleNumber_MatchesTarget()
    {
        // Arrange
        var input = "list = [42] target = 42";

        // Act
        var (numbers, target) = _parser.Parse(input);
        var result = _solver.CanMakeTarget(numbers, target);
        var solution = _solver.FindSolution(numbers, target);

        // Assert
        result.Should().BeTrue();
        solution.Should().NotBeNull();
        solution.Should().BeEmpty(); // No operations needed
    }

    [Fact]
    public void EndToEnd_SingleNumber_DoesNotMatchTarget()
    {
        // Arrange
        var input = "list = [42] target = 24";

        // Act
        var (numbers, target) = _parser.Parse(input);
        var result = _solver.CanMakeTarget(numbers, target);
        var solution = _solver.FindSolution(numbers, target);

        // Assert
        result.Should().BeFalse();
        solution.Should().BeNull();
    }

    [Fact]
    public void EndToEnd_ComplexExpression_FindsSolution()
    {
        // Arrange
        var input = "list = [1,2,3,4,5] target = 100";

        // Act
        var (numbers, target) = _parser.Parse(input);
        var result = _solver.CanMakeTarget(numbers, target);

        // Assert
        // (5*4)*(3+2)*1 = 20*5 = 100 or similar combinations
        result.Should().BeTrue();
    }

    [Fact]
    public void EndToEnd_InvalidInput_ThrowsException()
    {
        // Arrange
        var input = "invalid input";

        // Act
        var action = () => _parser.Parse(input);

        // Assert
        action.Should().Throw<FormatException>();
    }

    [Fact]
    public void EndToEnd_MultipleSolverCalls_WithCacheClear_WorkCorrectly()
    {
        // Arrange
        var input1 = "list = [1,2,3,4] target = 21";
        var input2 = "list = [4,3,2,5] target = 33";

        // Act & Assert - First call
        var (numbers1, target1) = _parser.Parse(input1);
        var result1 = _solver.CanMakeTarget(numbers1, target1);
        result1.Should().BeTrue();

        // Clear cache between different problems
        _solver.ClearCache();

        // Act & Assert - Second call
        var (numbers2, target2) = _parser.Parse(input2);
        var result2 = _solver.CanMakeTarget(numbers2, target2);
        result2.Should().BeTrue();
    }

    [Fact]
    public void EndToEnd_ParserAndSolver_HandleWhitespace()
    {
        // Arrange
        var input = "list = [ 1 , 2 , 3 , 4 ] target = 21";

        // Act
        var (numbers, target) = _parser.Parse(input);
        var result = _solver.CanMakeTarget(numbers, target);

        // Assert
        result.Should().BeTrue();
    }
}
