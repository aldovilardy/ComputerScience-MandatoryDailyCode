/*
Nth Fibonacci

A Fibonacci sequence is a list of numbers that begins with 0 and, each subsequent number is the sum of the previous two.
 * For example, the first five Fibonacci numbers are:
   0 1 1 2 3
  If n were 4, your function should return 3; for 5, it should return 5
 * Write a function that accepts a number, n, and returns the nth Fibonacci number.
   Use a recursive solution to this problem; if you finish with time left over, implement an iterative solution.

Example:
 * nthFibonacci(1); // => 1
 * nthFibonacci(3); // => 2
 * nthFibonacci(6); // => 8

*/

Console.WriteLine(NthFibonacciRecursive(6));
Console.WriteLine(NthFibonacciIterative(6));

/// <summary>
/// Finds the nth Fibonacci number using recursion.
/// </summary>
/// <param name="n">The position in the Fibonacci sequence.</param>
/// <returns>The nth Fibonacci number.</returns>
/// <remarks>
/// This implementation uses a simple recursive approach to calculate the nth Fibonacci number.
/// Example:
///   NthFibonacciRecursive(1); // => 1
///   NthFibonacciRecursive(3); // => 2
///   NthFibonacciRecursive(6); // => 8
/// </remarks>
/// <warning>
/// This recursive implementation has exponential time complexity and is not efficient for large values of n due to repeated calculations.
/// </warning>
static int NthFibonacciRecursive(int n) =>
    n switch
    {
        <= 0 => 0,
        1 => 1,
        _ => NthFibonacciRecursive(n - 1) + NthFibonacciRecursive(n - 2)
    };

/// <summary>
/// Finds the nth Fibonacci number using iteration.
/// </summary>
/// <param name="n">The position in the Fibonacci sequence.</param>
/// <returns>The nth Fibonacci number.</returns>
/// <remarks>
/// This implementation uses an iterative approach to calculate the nth Fibonacci number, which is more efficient than the recursive method for larger values of n.
/// Example:
///   NthFibonacciIterative(1); // => 1
///   NthFibonacciIterative(3); // => 2
///   NthFibonacciIterative(6); // => 8
/// </remarks>
static int NthFibonacciIterative(int n)
{
    if (n <= 0) return 0;
    if (n == 1) return 1;

    int previous = 0, current = 1, nthFibonacci = 0;

    for (int i = 2; i <= n; i++)
    {
        nthFibonacci = previous + current;
        previous = current;
        current = nthFibonacci;
    }

    return nthFibonacci;
}

