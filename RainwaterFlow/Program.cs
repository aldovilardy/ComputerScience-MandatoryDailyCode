/*
Rainwater Flow

The grid below represents an island completely surrounded by water. 
The numbers on the grid represent the elevation for the respective locations. 
Write code to determine if rainwater would flow into the ocean when it rains at a given xy coordinate.

      0   1   2   3   4
    ---------------------
0  | 9 | 4 | 8 | 2 | 7 |
    ---------------------
1  | 1 | 5 | 9 | 5 | 6 |
    ---------------------
2  | 2 | 7 | 3 | 8 | 6 |
    ---------------------
3  | 4 | 5 | 4 | 6 | 1 |
    ---------------------
4  | 1 | 2 | 7 | 9 | 8 |
    ---------------------

Remember:

Water can only move Left, Right, Up and Down.
Water can only move to tiles of lower elevation.
Water at any tile on the edge of the island (shore) can reach the ocean.

Please implement below method. Feel free to write additional methods.

Boolean WillWaterFlowToTheOcean(int x, int y)

*/

using CommunityToolkit.HighPerformance;

Memory2D<int> islandElevations = new int[,]
{
    { 9, 4, 8, 2, 7 },
    { 1, 5, 9, 5, 6 },
    { 2, 7, 3, 8, 6 },
    { 4, 5, 4, 6, 1 },
    { 1, 2, 7, 9, 8 }
};

Console.WriteLine(WillWaterFlowToTheOcean(0, 0)); // true
Console.WriteLine(WillWaterFlowToTheOcean(2, 2)); // false
Console.WriteLine(WillWaterFlowToTheOcean(2, 3)); // true
Console.WriteLine(WillWaterFlowToTheOcean(2, 4)); // true

/// <summary>
/// Determines if water will flow to the ocean from the given coordinates.
/// </summary>
/// <param name="x">Row x coordinate index (0-based, increases downward)</param>
/// <param name="y">Column y coordinate index (0-based, increases rightward)</param>
/// <returns>True if water can flow to the ocean, otherwise false</returns>
/// <remarks>
/// Water flows only to adjacent cells (left, right, up, down) with lower elevation.
/// Any cell on the grid edge can reach the ocean.
/// </remarks>
bool WillWaterFlowToTheOcean(int x, int y)
{
    if (x < 0 || x >= islandElevations.Height ||
        y < 0 || y >= islandElevations.Width)
        throw new ArgumentOutOfRangeException($"Coordinates ({x}, {y}) are outside the valid range.");

    return CanReachOcean(x, y, new bool[islandElevations.Height, islandElevations.Width]);
}

/// <summary>
/// Recursively determines if water can reach the ocean from the given position.
/// </summary>
/// <param name="row">Current row index</param>
/// <param name="col">Current column index</param>
/// <param name="visited">Tracks visited cells to prevent cycles</param>
/// <returns>True if a path to the ocean exists, otherwise false</returns>
bool CanReachOcean(int row, int col, bool[,] visited)
{
    if ((!IsWithinBounds(row, col)) || // Bounds check
        visited[row, col]) // Already visited check
        return false;

    // Mark current cell as visited
    visited[row, col] = true;

    // Check if we've reached the ocean (any edge cell)
    if (IsOnEdge(row, col))
        return true;

    // Explore all four directions
    int currentElevation = islandElevations.Span[row, col];

    return TryFlowUp(row, col, currentElevation, visited) ||
           TryFlowDown(row, col, currentElevation, visited) ||
           TryFlowLeft(row, col, currentElevation, visited) ||
           TryFlowRight(row, col, currentElevation, visited);
}

/// <summary>
/// Checks if the given coordinates are within the grid bounds.
/// </summary>
bool IsWithinBounds(int row, int col) =>
    row >= 0 && row < islandElevations.Height &&
    col >= 0 && col < islandElevations.Width;

/// <summary>
/// Checks if the given coordinates are on the edge of the grid.
/// </summary>
bool IsOnEdge(int row, int col) =>
    row == 0 || row == islandElevations.Height - 1 ||
    col == 0 || col == islandElevations.Width - 1;

/// <summary>
/// Attempts to flow water upward (decreasing row index).
/// </summary>
bool TryFlowUp(int row, int col, int currentElevation, bool[,] visited) =>
    row > 0 &&
    islandElevations.Span[row - 1, col] < currentElevation &&
    CanReachOcean(row - 1, col, visited);

/// <summary>
/// Attempts to flow water downward (increasing row index).
/// </summary>
bool TryFlowDown(int row, int col, int currentElevation, bool[,] visited) =>
    row < islandElevations.Height - 1 &&
    islandElevations.Span[row + 1, col] < currentElevation &&
    CanReachOcean(row + 1, col, visited);

/// <summary>
/// Attempts to flow water left (decreasing column index).
/// </summary>
bool TryFlowLeft(int row, int col, int currentElevation, bool[,] visited) =>
    col > 0 &&
    islandElevations.Span[row, col - 1] < currentElevation &&
    CanReachOcean(row, col - 1, visited);

/// <summary>
/// Attempts to flow water right (increasing column index).
/// </summary>
bool TryFlowRight(int row, int col, int currentElevation, bool[,] visited) =>
    col < islandElevations.Width - 1 &&
    islandElevations.Span[row, col + 1] < currentElevation &&
    CanReachOcean(row, col + 1, visited);