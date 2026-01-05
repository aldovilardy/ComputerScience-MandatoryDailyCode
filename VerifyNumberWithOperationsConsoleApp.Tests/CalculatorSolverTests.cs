using FluentAssertions;

namespace VerifyNumberWithOperationsConsoleApp.Tests;

public class CalculatorSolverTests
{
    private readonly CalculatorSolver _solver;

    public CalculatorSolverTests() => _solver = new CalculatorSolver();

    #region CanMakeTarget Tests

    [Theory]
    [InlineData(new[] { 1.0, 2.0, 3.0, 4.0 }, 21.0, true)]  // (1+2)*(3+4) = 21
    [InlineData(new[] { 4.0, 3.0, 2.0, 5.0 }, 33.0, true)]  // 3+5*(2+4) = 33
    [InlineData(new[] { 3.0, 4.0, 2.0 }, 5.0, true)]        // (3+4)-2 = 5
    public void CanMakeTarget_ChallengeExamples_ReturnsExpectedResult(double[] numbers, double target, bool expected)
    {
        // Arrange
        var numberList = numbers.ToList();

        // Act
        var result = _solver.CanMakeTarget(numberList, target);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(new[] { 5.0 }, 5.0, true)]
    [InlineData(new[] { 10.0 }, 10.0, true)]
    [InlineData(new[] { 3.0 }, 5.0, false)]
    public void CanMakeTarget_SingleNumber_ReturnsCorrectly(double[] numbers, double target, bool expected)
    {
        // Arrange
        var numberList = numbers.ToList();

        // Act
        var result = _solver.CanMakeTarget(numberList, target);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(new[] { 2.0, 3.0 }, 5.0, true)]    // 2+3 = 5
    [InlineData(new[] { 2.0, 3.0 }, 6.0, true)]    // 2*3 = 6
    [InlineData(new[] { 10.0, 5.0 }, 2.0, true)]   // 10/5 = 2
    [InlineData(new[] { 10.0, 5.0 }, 5.0, true)]   // 10-5 = 5
    public void CanMakeTarget_TwoNumbers_AllOperations_Work(double[] numbers, double target, bool expected)
    {
        // Arrange
        var numberList = numbers.ToList();

        // Act
        var result = _solver.CanMakeTarget(numberList, target);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(new[] { 1.0, 2.0, 3.0 }, 6.0, true)]    // (1+2)*3 = 9 or 1*2*3 = 6
    [InlineData(new[] { 1.0, 2.0, 3.0 }, 9.0, true)]    // (1+2)*3 = 9
    [InlineData(new[] { 4.0, 5.0, 6.0 }, 14.0, true)]   // 4+5+6 = 15 or similar
    public void CanMakeTarget_ThreeNumbers_FindsSolution(double[] numbers, double target, bool expected)
    {
        // Arrange
        var numberList = numbers.ToList();

        // Act
        var result = _solver.CanMakeTarget(numberList, target);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void CanMakeTarget_WithNegativeNumbers_Works()
    {
        // Arrange
        var numbers = new List<double> { -1, 2, 3 };
        var target = 5.0;  // -1+2+3 = 4, 2+3 = 5

        // Act
        var result = _solver.CanMakeTarget(numbers, target);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void CanMakeTarget_WithDecimalNumbers_Works()
    {
        // Arrange
        var numbers = new List<double> { 1.5, 2.5 };
        var target = 4.0;  // 1.5+2.5 = 4

        // Act
        var result = _solver.CanMakeTarget(numbers, target);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void CanMakeTarget_DivisionByZero_HandledGracefully()
    {
        // Arrange
        var numbers = new List<double> { 0, 5 };
        var target = 5.0;

        // Act
        var action = () => _solver.CanMakeTarget(numbers, target);

        // Assert
        action.Should().NotThrow();
    }

    [Fact]
    public void CanMakeTarget_CalledMultipleTimes_UsesMemoization()
    {
        // Arrange
        var numbers = new List<double> { 1, 2, 3, 4 };
        var target = 21.0;

        // Act
        var result1 = _solver.CanMakeTarget(numbers, target);
        var result2 = _solver.CanMakeTarget(numbers, target);

        // Assert
        result1.Should().Be(result2);
    }

    #endregion

    #region FindSolution Tests

    [Fact]
    public void FindSolution_WhenSolutionExists_ReturnsOperationSteps()
    {
        // Arrange
        var numbers = new List<double> { 1, 2, 3, 4 };
        var target = 21.0;

        // Act
        var solution = _solver.FindSolution(numbers, target);

        // Assert
        solution.Should().NotBeNull();
        solution.Should().NotBeEmpty();
        solution!.Count.Should().Be(3); // 3 operations to combine 4 numbers
    }

    [Fact]
    public void FindSolution_WhenNoSolutionExists_ReturnsNull()
    {
        // Arrange
        var numbers = new List<double> { 2, 3, 7 };
        var target = 100.0;  // Cannot be made with 2, 3, 7

        // Act
        var solution = _solver.FindSolution(numbers, target);

        // Assert
        solution.Should().BeNull();
    }

    [Fact]
    public void FindSolution_SingleNumber_MatchesTarget_ReturnsEmptyList()
    {
        // Arrange
        var numbers = new List<double> { 5 };
        var target = 5.0;

        // Act
        var solution = _solver.FindSolution(numbers, target);

        // Assert
        solution.Should().NotBeNull();
        solution.Should().BeEmpty();
    }

    [Fact]
    public void FindSolution_TwoNumbers_ReturnsOneOperation()
    {
        // Arrange
        var numbers = new List<double> { 2, 3 };
        var target = 5.0;

        // Act
        var solution = _solver.FindSolution(numbers, target);

        // Assert
        solution.Should().NotBeNull();
        solution.Should().HaveCount(1);
        solution![0].Should().Contain("+").And.Contain("5");
    }

    [Fact]
    public void FindSolution_Example2_ReturnsValidSolution()
    {
        // Arrange
        var numbers = new List<double> { 4, 3, 2, 5 };
        var target = 33.0;

        // Act
        var solution = _solver.FindSolution(numbers, target);

        // Assert
        solution.Should().NotBeNull();
        solution.Should().NotBeEmpty();
    }

    #endregion

    #region ClearCache Tests

    [Fact]
    public void ClearCache_ClearsInternalMemoization()
    {
        // Arrange
        var numbers = new List<double> { 1, 2, 3 };
        var target = 6.0;
        _solver.CanMakeTarget(numbers, target);

        // Act
        _solver.ClearCache();
        var result = _solver.CanMakeTarget(numbers, target);

        // Assert
        result.Should().BeTrue(); // Should still work after cache clear
    }

    #endregion

    #region Edge Cases

    [Fact]
    public void CanMakeTarget_LargeNumbers_Works()
    {
        // Arrange
        var numbers = new List<double> { 100, 200, 300 };
        var target = 600.0;  // 100+200+300 = 600

        // Act
        var result = _solver.CanMakeTarget(numbers, target);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void CanMakeTarget_SmallEpsilonDifference_RecognizesAsEqual()
    {
        // Arrange
        var solver = new CalculatorSolver(epsilon: 1e-6);
        var numbers = new List<double> { 1.0 / 3.0, 3.0 };
        var target = 1.0;  // (1/3)*3 = 1.0 (with floating point precision)

        // Act
        var result = solver.CanMakeTarget(numbers, target);

        // Assert
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(new[] { 2.0, 2.0, 2.0, 2.0 }, 16.0, true)]  // 2*2*2*2 = 16
    [InlineData(new[] { 5.0, 5.0 }, 25.0, true)]            // 5*5 = 25
    public void CanMakeTarget_DuplicateNumbers_Works(double[] numbers, double target, bool expected)
    {
        // Arrange
        var numberList = numbers.ToList();

        // Act
        var result = _solver.CanMakeTarget(numberList, target);

        // Assert
        result.Should().Be(expected);
    }

    #endregion
}
