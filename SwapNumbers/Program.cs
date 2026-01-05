/*
Given a linked list, swap every two adjacent nodes and return its head. You must solve the problem without 
modifying the values in the list's nodes (i.e., only nodes themselves may be changed.)

Example 1:
Input: head = [1,2,3,4]
Output: [2,1,4,3]

Example 2:
Input: head = []
Output: []

Example 3:
Input: head = [1]
Output: [1]
*/

string? input;

if (args != null && args.Length > 0)
    input = string.Join(" ", args);
else
{
    Console.WriteLine("Please enter the list string (e.g., head = [1,2,3,4]):");
    input = Console.ReadLine();
}

if (string.IsNullOrWhiteSpace(input))
{
    Console.WriteLine("No input provided. Exiting...");
    return;
}

if (!input.StartsWith("head = [") || !input.EndsWith(']'))
{
    Console.WriteLine("Invalid input format. Please provide input in the format: head = [1,2,3,4]. Exiting...");
    return;
}

try
{
    var head = new LinkedList<int>(
        input
        .Replace("head = [", "")
        .Replace("]", "")
        .Trim()
        .Split(',')
        .Where(n => !string.IsNullOrWhiteSpace(n))
        .Select(n => int.Parse(n.Trim())));

    SwapPairs(head);

    Console.WriteLine($"[{string.Join(",", head)}]");
}
catch (FormatException)
{
    Console.WriteLine("The input list contains invalid values (letters or decimals are not allowed). Please provide a list of integers. Exiting...");
    return;
}
catch (OverflowException)
{
    Console.WriteLine("Input contains values that are too large or too small for an integer. Exiting...");
    return;
}

/// <summary>
/// Swaps every two adjacent nodes in the linked list.
/// </summary>
static void SwapPairs(LinkedList<int> linkedList)
{
    if (linkedList == null || linkedList.Count < 2)
        return;

    LinkedListNode<int>? current = linkedList.First;

    while (current != null && current.Next != null)
    {
        LinkedListNode<int> first = current;
        LinkedListNode<int> second = current.Next;

        linkedList.Remove(second);
        linkedList.AddBefore(first, second);
        current = first.Next;
    }
}
