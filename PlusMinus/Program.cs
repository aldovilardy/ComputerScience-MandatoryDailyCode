/*
Plus Minus

Given an array of integers, calculate the fractions of its elements that are positive,negative, and are zero.
Print the decimal value of each fractions on a new line with 6 places after the decimal
Print the decimal value of each fraction on a new line.
It should print out the ratio of positive, negative and zero items in the array,
each on a separate line rounded to six decimals.

Example:
arr = [1, 1, 0, -1, -1]
2/5 = 0.400000 // positive
2/5 = 0.400000 // negative
1/5 = 0.200000 // zero
—----------------------------------------
Sample Input:
-4 3 -9 0 4 1  
Sample Output:
0.500000
0.333333
0.166667
*/

const int MaxArraySize = 100000;
int[]? arr = null;

// Read input from command line arguments or prompt the user
if (args.Length > 0)
    arr = ParseInput(args[0]);
else
{
    Console.WriteLine("Enter customer items (format: -4 3 -9 0 4 1):");
    var input = Console.ReadLine();
    if (!string.IsNullOrEmpty(input))
        arr = ParseInput(input);
}

// Validate input before processing
if (arr == null)
{
    Console.WriteLine("Error: No valid input provided.");
    return;
}

PlusMinus(arr);

/// <summary>
/// Parses a space-separated string of integers into an array.
/// <param name="input">Formatted string (e.g., "-4 3 -9 0 4 1")</param>
/// <returns>Array of integers, or null if parsing fails</returns>
/// </summary>
static int[]? ParseInput(string inputString)
{
    try
    {
        var parsedNumbers = Array.ConvertAll(
            inputString.Split(' ', StringSplitOptions.RemoveEmptyEntries),
            int.Parse);

        if (parsedNumbers.Length == 0)
            throw new FormatException("No valid integers found in input.");

        if (parsedNumbers.Length > MaxArraySize)
            throw new IndexOutOfRangeException("Input array size exceeds maximum limit.");

        return parsedNumbers;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Invalid format: {ex.Message}\nPlease use format: -4 3 -9 0 4 1");
        return null;
    }
}

/// <summary>
/// Calculates and prints the fractions of positive, negative, and zero elements in the array.
/// <param name="arr">Array of integers</param>
/// <remarks>
/// Prints the fractions of positive, negative, and zero elements in the array,
/// each on a separate line rounded to six decimals.
/// </remarks>
/// </summary>
static void PlusMinus(int[] numbers)
{
    Console.WriteLine($"{numbers.Count(n => n > 0) / (double)numbers.Length:F6}");
    Console.WriteLine($"{numbers.Count(n => n < 0) / (double)numbers.Length:F6}");
    Console.WriteLine($"{numbers.Count(n => n == 0) / (double)numbers.Length:F6}");
}