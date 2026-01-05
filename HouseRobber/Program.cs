/*
You are a professional robber planning to rob houses along a street. Each house has a certain amount of money stashed, the only constraint stopping you from robbing each of them is that adjacent houses have security systems connected and it will automatically contact the police if two adjacent houses were broken into on the same night.
Given an integer array nums representing the amount of money of each house, return the maximum amount of money you can rob tonight without alerting the police.

Example 1:
Input: nums = [1,2,3,1]
Output: 4
Explanation: Rob house 1 (money = 1) and then rob house 3 (money = 3).
Total amount you can rob = 1 + 3 = 4.

Example 2:
Input: nums = [2,7,9,3,1]
Output: 12
Explanation: Rob house 1 (money = 2), rob house 3 (money = 9) and rob house 5 (money = 1).
Total amount you can rob = 2 + 9 + 1 = 12.

C# console app and the method should receive an "IEnumwrable<int> nums" parameter and return a int
 */

using System.Text.Json;

List<int>? nums = null;

if (args.Length > 0)
    nums = ParseInput(args[0]);
else
{
    Console.WriteLine("Enter house values (format: [1,2,3,1]):");
    var input = Console.ReadLine();
    if (!string.IsNullOrEmpty(input))
        nums = ParseInput(input);
}

// Validate input before processing
if (nums == null || nums.Count == 0)
{
    Console.WriteLine("Error: No valid input provided.");
    return;
}

Console.WriteLine(RobHouses(nums));

/// <summary>
/// Calculates the maximum amount of money that can be robbed without alerting the police.
/// Uses dynamic programming with O(n) time and O(1) space complexity.
/// </summary>
/// <param name="nums">The amount of money in each house.</param>
/// <returns>Maximum amount that can be robbed without triggering adjacent alarms.</returns>
static int RobHouses(List<int> nums)
{    
    var prevMoney1 = 0; // Max money up to previous house
    var prevMoney2 = 0; // Max money up to two houses back

    nums.ForEach(houseValue =>
    {
        var maxMoney = Math.Max(prevMoney1, houseValue + prevMoney2); // Choice: skip current house (take prevMoney1) OR rob it (add to prevMoney2)
        prevMoney2 = prevMoney1; // Shift the window
        prevMoney1 = maxMoney; // Update for next iteration
    });

    return prevMoney1;
}

/// <summary>
/// Parses the input string into a list of integers.
/// </summary>
/// <param name="input">JSON-formatted string array (e.g., "[1,2,3,1]").</param>
/// <returns>List of integers, or null if parsing fails.</returns>
static List<int>? ParseInput(string input)
{
    try
    {
        var result = JsonSerializer.Deserialize<List<int>>(input);
        return result ?? throw new JsonException("Deserialized result is null.");
    }
    catch (JsonException ex)
    {
        Console.WriteLine($"Invalid format: {ex.Message}");
        Console.WriteLine("Please use format: [1,2,3,1]");
        return null;
    }
}