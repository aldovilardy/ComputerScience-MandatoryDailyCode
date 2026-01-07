/*
Minimum number of operations to make an array empty

You are given a 0-indexed array nums consisting of positive integers.
There are two types of operations that you can apply on the array any number of times:
Choose two elements with equal values and delete them from the array.
Choose three elements with equal values and delete them from the array.
Return the minimum number of operations required to make the array empty, or -1 if it is not possible

Example 1:

Input: nums = [2,3,3,2,2,4,2,3,4]
Output: 4

Example 2:

Input: nums = [2,1,2,2,3,3]
Output: -1

*/

Console.WriteLine(MinimumOperationsToEmptyArray([2, 3, 3, 2, 2, 4, 2, 3, 4])); // Output: 4
Console.WriteLine(MinimumOperationsToEmptyArray([2, 1, 2, 2, 3, 3])); // Output: -1

/// <summary>
/// Calculates the minimum number of operations to make the array empty.
/// </summary>
/// <param name="nums">The input array of positive integers.</param>
/// <returns>The minimum number of operations required to make the array empty, or -1 if not possible.</returns>
/// <remarks>
/// Each operation can remove either two or three equal elements.
/// Count the frequency of each number
/// Process each unique number
/// If count is 1, we can't remove it (need at least 2 for any operation)
/// If count % 3 == 0: count / 3 operations of size 3
/// If count % 3 == 1: (count / 3 - 1) operations of size 3, plus 2 operations of size 2 (since 3+1 = 2+2)
/// If count % 3 == 2: count / 3 operations of size 3, plus 1 operation of size 2
/// </remarks>
static int MinimumOperationsToEmptyArray(int[] nums)
{
    // Count the frequency of each number
    var frequences = nums
        .CountBy(n => n)
        .Select(it => it.Value);

    // Process each unique number
    return frequences.Any(count => count == 1) ? -1 : frequences.Sum(count => (count + 2) / 3);
}