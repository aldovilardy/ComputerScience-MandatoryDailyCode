/*

Within a text you need to find the Dates (in different formats) and return a list of Dates found.
Dates format to support:
month day, year
Month/day/year
Month-day-year
It must support cardinal and ordinal numbers, ex: March 1st, Feb 5th as well as March 1, February 5
 
String Example Input

The Mexican Revolution, which occurred from 1910 to 1920, was a pivotal period in Mexican history. 
It began on November 20, 1910, with an armed uprising against President Porfirio Díaz, who had held power for over three decades. 
The promulgation of the Mexican Constitution on Feb 5th, 1917, introduced significant reforms, addressing issues such as land reform, 
workers' rights, and the separation of church and state. March 21st, 1917, marked the presidency of Venustiano Carranza, 
whose tenure focused on consolidating power and implementing reforms. 
The assassination of Emiliano Zapata on 04/10/1919, deeply impacted the revolution, particularly the agrarian movement. 
Finally, on 05-21-1920, Álvaro Obregón led a successful revolt against Carranza's government, resulting in his presidency. 
These important dates highlight key moments in the Mexican Revolution's trajectory, shaping the course of Mexico's future.

Example Output: 
November 20, 1910
Feb 5th, 1917
March 21st, 1917
04/10/1919
05-21-1920

*/
using System.Globalization;
using System.Text.RegularExpressions;

var input = args.Length > 0 ? string.Join(" ", args) : Console.ReadLine();

List<string> patterns = [
            // Pattern: Month day(st/nd/rd/th optional), year. Examples: November 20, 1910 | Feb 5th, 1917 | March 21st, 1917
            @"\b(" + string.Join("|", DateTimeFormatInfo.InvariantInfo.MonthNames.Take(12).Concat(DateTimeFormatInfo.InvariantInfo.AbbreviatedMonthNames.Take(12))) + @")\s+(\d{1,2})(st|nd|rd|th)?,\s+(\d{4})\b",
            // Pattern: Month/day/year. Example: 1/15/2020
            @"\b(\d{1,2})/(\d{1,2})/(\d{4})\b",
            // Pattern 3: Month-day-year. Example: 1-15-2020
            @"\b(\d{1,2})-(\d{1,2})-(\d{4})\b"];

// Find all matches for patterns and print them
patterns
    .SelectMany(p => Regex.Matches(input, p, RegexOptions.IgnoreCase)
    .Select(match => match.Value))
    .ToList()
    .ForEach(Console.WriteLine);
