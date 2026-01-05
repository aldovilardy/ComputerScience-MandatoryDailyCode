/*
Write a function that splits a list of numbers with regard to a pivot element. All elements smaller than the pivot element go in the first return list, all other elements go into the second return list. Both return lists are inside one list.

this should be done using recursion

Example:
splitByPivot([2,7,8,3,1,4], 4) 
// Returns [[2,3,1],[7,8,4]]

 */

using System.Text.Json;

// Read input from command line arguments or prompt the user
if (args.Length < 2)
{
    Console.WriteLine("Enter numbers (format: [2,7,8,3,1,4], 4):");
    var input = Console.ReadLine();
    args = !string.IsNullOrWhiteSpace(input) ? input.Split(' ') : [];
}

// Validate input before processing
if (args.Length >= 2 && !string.IsNullOrWhiteSpace(args[0]) && string.IsNullOrWhiteSpace(args[1]))
{
    try
    {
        var inputNumbers = JsonSerializer.Deserialize<List<int>>(args[0].TrimEnd(',')) ?? throw new ArgumentException("Invalid input numbers format.");

        if (int.TryParse(args[1], out int pivot))
            Console.WriteLine(JsonSerializer.Serialize(SplitListByPivot(inputNumbers, pivot)));
        else
            throw new ArgumentException("Invalid pivot format.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}\n\tInvalid format, enter the numbers with format: \"[2,7,8,3,1,4], 4\"");
    }
}
else
    Console.WriteLine("Error: No valid input provided.");

/// <summary>
/// Splits a list of integers into two lists based on a pivot value using recursion.
/// </summary>
/// <param name="numbers">List of integers to split. Must not be null.</param>
/// <param name="pivot">The pivot value used to split the list.</param>
/// <returns>
/// A list containing two lists: the first with elements less than the pivot, the second with elements greater than or equal to the pivot.
/// </returns>
/// <remarks>
/// This method uses recursion to split the list.
/// Time Complexity: O(n²) due to Insert(0) operations.
/// Space Complexity: O(n) for recursion stack and new list creation.
/// Example:
/// SplitListByPivot([2,7,8,3,1,4], 4)
/// Returns [[2,3,1],[7,8,4]]
/// </remarks>
/// <exception cref="ArgumentNullException">Thrown when numbers is null.</exception>
static List<List<int>> SplitListByPivot(List<int> numbers, int pivot)
{
    ArgumentNullException.ThrowIfNull(numbers);

    if (numbers.Count == 0)
        return [[], []];

    var splitLists = SplitListByPivot([.. numbers.Skip(1)], pivot);

    if (numbers[0] < pivot)
        splitLists[0].Insert(0, numbers[0]);
    else
        splitLists[1].Insert(0, numbers[0]);

    return splitLists;
}