/*
Roll the dice

You have x dice, and each dice has n faces numbered from 1 to n.

Given three integers x, n, and target, return the number of possible ways (out of the nx total ways) to roll the dice, so the sum of the face-up numbers equals target. 
Since the answer may be too large, return it modulo 10^9 + 7.

Examples :
Input: x = 1, n = 6, target = 3

Output: 1

Explanation: You throw one dice with 6 faces. There is only one way to get a sum of 3.


Input: x = 2, n = 6, target = 7

Output: 6

Explanation: You throw two dice, each with 6 faces.
There are 6 ways to get a sum of 7: 1+6, 2+5, 3+4, 4+3, 5+2, 6+1.

Constraints : 1 <= x, n <= 30, 1 <= target <= 1000

*/

Console.WriteLine(RollTheDice(1, 6, 3));

/// <summary>
/// Calculates the number of ways to roll x dice with n faces to achieve a target sum.
/// </summary>
/// <param name="x">The number of dice.</param>
/// <param name="n">The number of faces on each die.</param>
/// <param name="target">The target sum.</param>
/// <returns>The number of ways to achieve the target sum.</returns>
static int RollTheDice(int x, int n, int target)
{
    // If target is out of possible sum range, return 0
    if (target < x || target > x * n)
        return 0;

    // Modulo value for large results
    const int Modulo = (int)1e9 + 7;

    // Previous array for the previous number of dice    
    var prev = new long[target + 1];

    // Current array for the current number of dice
    var curr = new long[target + 1];

    // Base case: 0 dice, 0 sum = 1 way
    prev[0] = 1;

    // For each die
    for (int dice = 1; dice <= x; dice++)
    {
        Array.Fill(curr, 0); // Reset current array
        long windowSum = 0; // Sliding window sum

        // For each possible sum
        for (int sum = 1; sum <= target; sum++)
        {
            // Add the value entering the sliding window
            windowSum = (windowSum + prev[sum - 1]) % Modulo;

            // Remove the value leaving the sliding window (if window > n)
            windowSum = curr[sum] = sum > n ? (windowSum - prev[sum - n - 1] + Modulo) % Modulo : windowSum;
        }

        // Swap arrays for next iteration
        (prev, curr) = (curr, prev);
    }

    return (int)prev[target];
}