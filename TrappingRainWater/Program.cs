/*
Trapping Rain Water
Given an array arr[] of N non-negative integers representing the height of blocks. 
If the width of each block is 1, compute how much water can be trapped between the blocks during the rainy season. 

Example 1:

Input:
N = 6
arr[] = {3,0,0,2,0,4}
Output:
10

Example 2:

Input:
N = 4
arr[] = {7,4,0,9}
Output:
10
Explanation:
Water trapped by above 
block of height 4 is 3 units and above 
block of height 0 is 7 units. So, the 
total unit of water trapped is 10 units

Example 3:

Input:
N = 3
arr[] = {6,9,9}
Output:
0
Explanation:
No water will be trapped

*/

Console.WriteLine(CalculateTrappedWaterLL([3, 0, 0, 2, 0, 4]));
Console.WriteLine(CalculateTrappedWaterAR([7, 4, 0, 9]));
Console.WriteLine(CalculateTrappedWaterLL([6, 9, 9]));

/// <summary>
/// Calculates the total amount of trapped water between blocks represented by their heights.
/// </summary>
/// <param name="arr">A collection of non-negative integers representing the heights of the blocks.</param>
/// <returns>The total amount of trapped water between the blocks.</returns>
/// <remarks>
/// This algorithm uses a two-pointer approach to calculate the trapped water in linear time.
/// Time Complexity: O(n)
/// Space Complexity: O(n) due to LinkedList creation
/// Example:
/// CalculateTrappedWater([3, 0, 0, 2, 0, 4]) 
/// Returns 10
/// </remarks>
static int CalculateTrappedWaterLL(ICollection<int> arr)
{
    if (arr == null || arr.Count < 3)
        return 0;

    LinkedList<int> height = new(arr);

    var left = height.First;
    var right = height.Last;

    var leftMax = 0;
    var rightMax = 0;
    var water = 0;

    while (left != right)
    {
        if (left!.Value <= right!.Value)
        {
            leftMax = Math.Max(leftMax, left.Value);
            water += leftMax - left.Value;
            left = left.Next;
        }
        else
        {
            rightMax = Math.Max(rightMax, right.Value);
            water += rightMax - right.Value;
            right = right.Previous;
        }
    }

    return water;
}

/// <summary>
/// Calculates the total amount of trapped water between blocks represented by their heights.
/// </summary>
/// <param name="heights">A collection of non-negative integers representing the heights of the blocks.</param>
/// <returns>The total amount of trapped water between the blocks.</returns>
/// <remarks>
/// This algorithm uses a two-pointer approach to calculate the trapped water in linear time.
/// Time Complexity: O(n)
/// Space Complexity: O(1) - only uses a constant amount of extra space
/// Example:
/// CalculateTrappedWater([3, 0, 0, 2, 0, 4]) 
/// Returns 10
/// </remarks>
static int CalculateTrappedWaterAR(int[] heights)
{
    if (heights == null || heights.Length < 3)
        return 0;

    if (heights.Any(h => h < 0))
        throw new ArgumentException("Heights must be non-negative", nameof(heights));

    int left = 0;
    int right = heights.Length - 1;
    int leftMax = 0;
    int rightMax = 0;
    int water = 0;

    while (left < right)
    {
        if (heights[left] <= heights[right])
        {
            leftMax = Math.Max(leftMax, heights[left]);
            water += leftMax - heights[left];
            left++;
        }
        else
        {
            rightMax = Math.Max(rightMax, heights[right]);
            water += rightMax - heights[right];
            right--;
        }
    }

    return water;
}