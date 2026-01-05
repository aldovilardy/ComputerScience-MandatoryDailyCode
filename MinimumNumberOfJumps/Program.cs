/*
Minimum number of jumps
Given an array of N integers arr[] where each element represents the maximum length of the jump that can be made forward from that element. 
This means if arr[i] = x, then we can jump any distance y such that y ≤ x.
Find the minimum number of jumps to reach the end of the array (starting from the first element). If an element is 0, then you cannot move through that element.

Note: Return -1 if you can't reach the end of the array.

Example 1:

Input:
N = 11 
arr[] = {1, 3, 5, 8, 9, 2, 6, 7, 6, 8, 9} 
Output: 
3 
Explanation: 
First jump from 1st element to 2nd 
element with value 3. Now, from here 
we jump to 5th element with value 9, 
and from here we will jump to the last. 

Example 2:

Input:
N = 6
arr = {1, 4, 3, 2, 6, 7}
Output: 
2 
Explanation: 
First we jump from the 1st to 2nd element 
and then jump to the last element.

*/

Console.WriteLine(MinimumJumps([1, 3, 5, 8, 9, 2, 6, 7, 6, 8, 9]));
Console.WriteLine(MinimumJumps([1, 4, 3, 2, 6, 7]));

/// <summary>
/// Finds the minimum number of jumps to reach the end of the array.
/// </summary>
/// <param name="arr">The input array.</param>
/// <returns>The minimum number of jumps, or -1 if not possible.</returns>
/// <remarks>
/// This algorithm uses a greedy approach to determine the minimum jumps required to reach the end of the array.
/// Example:
/// MinimumJumps([1, 3, 5, 8, 9, 2, 6, 7, 6, 8, 9])
/// Returns 3
/// </remarks>
static int MinimumJumps(int[] arr)
{
    if (arr.Length <= 1)
        return 0;

    if (arr[0] == 0)
        return -1;

    var maxReach = arr[0];
    var steps = arr[0];
    var jumps = 1;

    for (var i = 1; i < arr.Length; i++)
    {
        if (i == arr.Length - 1)
            return jumps;

        maxReach = Math.Max(maxReach, i + arr[i]);
        steps--;

        if (steps == 0)
        {
            jumps++;
            if (i >= maxReach)
                return -1;
            steps = maxReach - i;
        }
    }

    return -1;
}