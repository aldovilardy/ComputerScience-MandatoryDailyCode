/*
Write a function named findMostVisitedPages that takes an array of objects as input, where each object represents a log entry from a web server log file. 
Each log entry contains the webpage visited (url) and the unique visitor identifier (userId). 
The function should return an array of the most frequently visited webpages, considering unique visitors, in descending order of their visit counts.

Example :
Input: logData = [
  { url: "/home", userId: "A" },
  { url: "/about", userId: "B" },
  { url: "/products", userId: "A" },
  { url: "/home", userId: "C" },
  { url: "/contact", userId: "B" },
  { url: "/products", userId: "D" },
  { url: "/home", userId: "A" },
  { url: "/home", userId: "B" },
  { url: "/products", userId: "A" }]


Output: ["/home", "/products", "/about", "/contact"]

Explanation: /home has 3 unique visitors (A,B,C), /products has 2 unique visitors (A,D), /about has 1 unique visitor (B), and /contact has 1 unique visitor (B)

*/

var logData = new (string url, string userId)[]
{
    ("/home", "A"),
    ("/about", "B"),
    ("/products", "A"),
    ("/home", "C"),
    ("/contact", "B"),
    ("/products", "D"),
    ("/home", "A"),
    ("/home", "B"),
    ("/products", "A")
};

var mostVisitedPages = FindMostVisitedPages(logData);

Console.WriteLine(string.Join(", ", mostVisitedPages)); // Output: ["/home", "/products", "/about", "/contact"]

/// <summary>
/// Finds the most visited pages by unique visitors in descending order
/// </summary>
/// <param name="logData">Array of tuples containing url and userId</param>
/// <returns>Array of URLs sorted by unique visitor count (descending)</returns>
/// <remarks>
/// Group log entries by URL.
/// First Group by URL, selecting userId 
/// then count unique users creating an anonymous object with URL and unique user count
/// later sort by unique visitor count descending
/// and finally select only the URLs.
/// </remarks>
static string[] FindMostVisitedPages((string url, string userId)[] logData) =>
    [..
        logData
        .GroupBy(entry => entry.url, entry => entry.userId) // Group by URL, selecting userId
        .Select(group => new
            {
                Url = group.Key,
                UniqueUsers = group.Distinct().Count() // Count unique userIds deleting duplicates
            }) // Create an anonymous object with URL and unique user count
        .OrderByDescending(it => it.UniqueUsers) // Sort by unique visitor count descending
        .Select(it => it.Url)]; // Select only the URLs