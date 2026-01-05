/*
Given a square matrix, calculate the absolute difference between the sums of its diagonals.
For example, the square matrix is shown below:
1 2 3
4 5 6
9 8 9

Function description:
It must return an integer representing the absolute diagonal difference.
diagonalDifference takes the following parameter:
matrix: an array of integers.

Example:
const matrix = [
  [1, 2, 3],
  [4, 5, 6], 
  [9, 8, 9]
];

diagonalDifference(matrix); // Returns 2
The primary diagonal is:
1 + 5 + 9 = 15
The secondary diagonal is:
3 + 5 + 9 = 17
Their absolute difference is |15 - 17| = 2
*/

using MathNet.Numerics.LinearAlgebra;

// Example square matrix
IEnumerable<IEnumerable<int>> matrix = [
  [1, 2, 3],
  [4, 5, 6],
  [9, 8, 9]
];

Console.WriteLine("Matrix:");
matrix
    .ToList()
    .ForEach(row => Console.WriteLine(string.Join(" ", row.Select(it => $"{it}".PadLeft(3)))));

Console.WriteLine($"\nDiagonal Difference: {DiagonalDifference(matrix)}");

/// <summary>
/// Calculates the absolute difference between the sums of the diagonals of a square matrix.
/// </summary>
/// <param name="matrix">A square matrix represented as a collection of rows.</param>
/// <returns>The absolute difference between primary and secondary diagonal sums.</returns>
static int DiagonalDifference(IEnumerable<IEnumerable<int>> matrix)
{
    var matrixDouble = matrix == null || !matrix.Any()
    ? Matrix<double>.Build.Dense(0, 0)
    : Matrix<double>.Build.DenseOfRowArrays(matrix.Select(row => row.Select(val => (double)val).ToArray()));

    // Primary diagonal: top-left to bottom-right
    var primaryDiagonalSum = matrixDouble
        .Diagonal()
        .Sum();

    // Secondary diagonal: top-right to bottom-left
    var secondaryDiagonalSum = Enumerable.Range(0, matrixDouble.RowCount)
        .Select(i => matrixDouble[i, matrixDouble.ColumnCount - i - 1])
        .Sum();

    return (int)Math.Abs(primaryDiagonalSum - secondaryDiagonalSum);
}