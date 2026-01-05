//using System.Text.RegularExpressions;

//namespace VerifyNumberWithOperationsConsoleApp;

///// <summary>
///// Parses input strings to extract a list of numbers and a target value.
///// </summary>
//public class InputParser
//{
//    /// <summary>
//    /// Parses the input string and extracts the list of numbers and target value.
//    /// </summary>
//    /// <param name="input">Input string in format: "list = [1,2,3] target = 10"</param>
//    /// <returns>A tuple containing the list of numbers and the target value</returns>
//    /// <exception cref="ArgumentException">Thrown when input is null or empty</exception>
//    /// <exception cref="FormatException">Thrown when input format is invalid</exception>
//    public (List<double> Numbers, double Target) Parse(string? input)
//    {
//        if (string.IsNullOrWhiteSpace(input))
//            throw new ArgumentException("Input cannot be null or empty.", nameof(input));

//        var numbers = ParseNumberList(input);
//        var target = ParseTarget(input);

//        return (numbers, target);
//    }

//    /// <summary>
//    /// Extracts the list of numbers from the input string.
//    /// </summary>
//    /// <param name="input">Input string containing the list</param>
//    /// <returns>List of parsed numbers</returns>
//    /// <exception cref="FormatException">Thrown when list format is invalid</exception>
//    private static List<double> ParseNumberList(string input)
//    {
//        // Extract the list part: everything inside [...]
//        var listMatch = Regex.Match(input, @"\[(.*?)\]");
//        if (!listMatch.Success)
//            throw new FormatException("List not found. Expected format: list = [number1,number2,...]");

//        var numberStrings = listMatch.Groups[1].Value
//            .Split(',', StringSplitOptions.RemoveEmptyEntries);

//        var numbers = new List<double>();
//        foreach (var numStr in numberStrings)
//        {
//            if (!double.TryParse(numStr.Trim(), out double number))
//                throw new FormatException($"Invalid number format: '{numStr.Trim()}'");
            
//            numbers.Add(number);
//        }

//        if (numbers.Count == 0)
//            throw new FormatException("List cannot be empty.");

//        return numbers;
//    }

//    /// <summary>
//    /// Extracts the target value from the input string.
//    /// </summary>
//    /// <param name="input">Input string containing the target</param>
//    /// <returns>Parsed target value</returns>
//    /// <exception cref="FormatException">Thrown when target format is invalid</exception>
//    private static double ParseTarget(string input)
//    {
//        // Extract target value
//        var targetMatch = Regex.Match(input, @"target\s*=\s*([-+]?\d+(\.\d+)?)");
//        if (!targetMatch.Success)
//            throw new FormatException("Target value not found. Expected format: target = number");

//        if (!double.TryParse(targetMatch.Groups[1].Value, out double target))
//            throw new FormatException($"Invalid target format: '{targetMatch.Groups[1].Value}'");

//        return target;
//    }
//}
