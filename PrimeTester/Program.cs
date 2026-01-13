/*
Prime Tester

A prime number is a whole number that has no other divisors other than
itself and 1.

Write a function that accepts a number and returns true if it's
 a prime number, false if it's not.

Example:
const numberToTest = 17;
console.log(primeTester(numberToTest)); 
// true

 */

const int numberToTest = 17;
Console.WriteLine(PrimeTester(numberToTest));

/// <summary>
/// Determines if a number is prime using recursion.
/// </summary>
/// <param name="number">The number to check for primality.</param>
/// <param name="divisor">Current divisor (used internally).</param>
/// <returns>True if the number is prime; otherwise, false.</returns>
static bool PrimeTester(int number, int divisor = 2) =>
    number > 1 && (divisor * divisor > number || (number % divisor != 0 && PrimeTester(number, divisor + 1)));