/*
Letter Tally

Given a string, write a function that returns an object containing tallies of each letter.
Use recursion for the solution

Example:
letterTally('potato'); 
// Returns {p:1, o:2, t:2, a:1}

*/

using System.Text.Json;

Console.WriteLine(JsonSerializer.Serialize(LetterTally("potato")));

/// <summary>
/// Tallies the occurrences of each letter in the given text using recursion.
/// </summary>
/// <param name="text">The input string to tally letters from.</param>
/// <returns>A dictionary containing the tally of each letter.</returns>
/// <remarks>
/// This implementation uses recursion to traverse the string and count the occurrences of each character.
/// Example:
/// LetterTally("potato")
/// Returns {"p":1, "o":2, "t":2, "a":1}
/// </remarks>
/// <warning>
/// This recursive implementation may lead to stack overflow for very long strings due to deep recursion.
/// </warning>
static Dictionary<char, int> LetterTally(string text, int index = 0, Dictionary<char, int>? tally = null)
{
    tally ??= [];

    if (index >= text.Length)
        return tally;

    tally[text[index]] = tally.GetValueOrDefault(text[index], 0) + 1;

    return LetterTally(text, ++index, tally);
}