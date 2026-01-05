/*
Background
You run a small grocery store that currently has a single checkout line. Some customers are complaining about long wait times, and you want to ensure you're handling the checkout process efficiently. There's only one cashier available, but you want to strategize when to open a second checkout counter to keep customers happy.

Problem Statement
Design a simple algorithm that helps you decide when to open a second checkout counter based on the current queue. The algorithm should consider the following:

Each customer has a number of items to checkout.
It takes about 1 minute to process every 5 items.
A new checkout counter should be opened if the estimated wait time for the 
last person in line exceeds 10 minutes.

Input/Output Specifications
Input: A list of customers in the queue represented by the number of items they have.

Format: [num_items1, num_items2, num_items3, ...]
Example: [15, 3, 7, 12]

Output: A decision whether to open a second checkout counter.

true / false
*/

using System.Text.Json;

// Constants for clarity and maintainability
const double MinutesPerItem = 1.0 / 5.0; // 0.2 minutes per item
const double MaxWaitTimeMinutes = 10.0;

IEnumerable<int>? customerItemCounts = null;

// Read input from command line arguments or prompt the user
if (args.Length > 0)
    customerItemCounts = ParseInput(args[0]);
else
{
    Console.WriteLine("Enter customer items (format: [15, 3, 7, 12]):");
    var input = Console.ReadLine();
    if (!string.IsNullOrEmpty(input))
        customerItemCounts = ParseInput(input);
}

// Validate input before processing
if (customerItemCounts == null)
{
    Console.WriteLine("Error: No valid input provided.");
    return;
}

// Output the decision to open a second checkout counter
bool shouldOpen = ShouldOpenSecondCheckout(customerItemCounts);
Console.WriteLine(shouldOpen.ToString().ToLower());

/// <summary>
/// Determines whether to open a second checkout counter based on the wait time 
/// for the last person in the queue.
/// </summary>
/// <param name="itemCounts">Number of items each customer has in the queue (in order)</param>
/// <returns>True if the last person's wait time exceeds 10 minutes, false otherwise</returns>
/// <remarks>
/// Wait time calculation: The last person must wait for all customers ahead of them 
/// to be processed. Each item takes 0.2 minutes (1 minute per 5 items).
/// Example: [15, 3, 7, 12] → Last person waits: 3 + 0.6 + 1.4 + 2.4 = 7.4 minutes
/// </remarks>
static bool ShouldOpenSecondCheckout(IEnumerable<int> itemCounts)
{
    // Validate input
    if (!itemCounts.Any())
        return false; // Empty queue, no need for second checkout

    if (itemCounts.Any(count => count < 0))
        throw new ArgumentException("Item counts cannot be negative", nameof(itemCounts));

    // Calculate total wait time for the last person in line
    // This is the sum of processing times for all customers
    return (double)itemCounts.Sum(items => items * MinutesPerItem) > MaxWaitTimeMinutes;
}

/// <summary>
/// Parses a JSON array string into a collection of integers representing customer item counts.
/// </summary>
/// <param name="input">JSON formatted string (e.g., "[15, 3, 7, 12]")</param>
/// <returns>Array of integers representing item counts, or null if parsing fails</returns>
static int[]? ParseInput(string input)
{
    try
    {
        var result = JsonSerializer.Deserialize<int[]>(input);
        return result ?? throw new JsonException("Deserialized result is null.");
    }
    catch (JsonException ex)
    {
        Console.WriteLine($"Invalid format: {ex.Message}");
        Console.WriteLine("Please use format: [15, 3, 7, 12]");
        return null;
    }
}