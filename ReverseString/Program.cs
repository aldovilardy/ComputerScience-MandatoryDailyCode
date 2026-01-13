/*
Reverse a string

Write a function that reverses a string. The input string is given as an array of characters char[].

Do not allocate extra space for another array, you must do this by modifying the input array in-place with O(1) extra memory.


Note: You may assume all the characters consist of printable ascii characters.

Example :
Input: “qwerty”

Output: “ytrewq”

**
 * @param {char[]} s
 * @return {string}
 *
var reverseString = function(s) {
};

*/

Console.WriteLine(new string([.. "qwerty".ToCharArray().Reverse()])); // Output: "ytrewq" 

Console.WriteLine(ReverseString("qwerty".ToCharArray())); // Output: "ytrewq"

/// <summary>
/// Reverses the input character array in place and returns the reversed string.
/// </summary>
/// <param name="array">The character array to be reversed.</param>
/// <returns>The reversed string.</returns>
/// <exception cref="ArgumentNullException">Throws ArgumentNullException if the input array is null.</exception>
/// <remarks>
/// This method modifies the input array in place and does not allocate extra space for another array.
/// Time complexity is O(n).
/// Space complexity is O(1).
/// </remarks>
static string ReverseString(char[] array)
{
    ArgumentNullException.ThrowIfNull(array);

    if (array.Length == 0)
        return string.Empty;

    int start = 0;
    int end = array.Length - 1;

    while (start < end)
    {
        (array[end], array[start]) = (array[start], array[end]);
        start++;
        end--;
    }

    return new string(array);
}