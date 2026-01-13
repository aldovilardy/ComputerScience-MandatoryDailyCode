/*
[DEV] Coding - Days Of The Week
Given current day as day of the week and an integer K, the task is to find the day of the week after K days.

Assumptions:

day can be Mon, Tue, Wed, Thu, Fri, Sat, Sun.
K is a positive non-zero integer.
Sample:

Input	Output
Mon 7

Tue 3

Mon

Fri

Wed 10	Sat
Skills Assessed:

Problem Analysis
Coding
Sample input

Mon 7
Tue 3
Wed 10
Sample output

Mon
Fri
Sat
 */

var input = Console.ReadLine();

while (input != null)
{
    // Retrieve Input
    var splittedString = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    // Execute Method
    var result = DaysOfTheWeek(splittedString[0], int.Parse(splittedString[1]));
    Console.WriteLine(result);

    input = Console.ReadLine();
}

static string? DaysOfTheWeek(string day, int K) =>
    ((DayOfWeek)(((int)Enum.Parse<DayOfWeek>(Enum.GetNames(typeof(DayOfWeek)).First(d => d.StartsWith(day))) + K) % 7)).ToString()[..3];

static string DaysOfTheWeekEfficient(string day, int K)
{
    string[] daysOfWeek = ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"];
    return daysOfWeek[(daysOfWeek.IndexOf(day) + K) % 7];
}