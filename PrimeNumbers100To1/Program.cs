/*
Print all prime numbers that exists between 100 to 1
Print them from largest number to smallest
Do not use loops (for, foreach, do, while)
Create your own method to define if a number is prime or not

Output:
97, 89, 83, ….  2
*/

PrintPrimesDescending(100);

/// <summary>
/// Determines if a number is prime.
/// </summary>
/// <param name="number">The number to check for primality.</param>
/// <returns>True if the number is prime; otherwise, false.</returns>
static bool IsPrime(int number) =>
    number > 1 && VerifyDivisor(number, 2);

/// <summary>
/// Recursively verifies if a number is prime by checking divisibility.
/// Uses the mathematical principle that if a number n has a divisor, 
/// at least one divisor must be less than or equal to √n.
/// Therefore, we only need to check divisors up to √n.
/// </summary>
/// <param name="number">The number being tested for primality.</param>
/// <param name="currentDivisor">The current divisor being tested.</param>
/// <returns>True if no divisors are found (number is prime); otherwise, false.</returns>
static bool VerifyDivisor(int number, int currentDivisor) =>
    (currentDivisor * currentDivisor > number) || number % currentDivisor != 0 && VerifyDivisor(number, ++currentDivisor);

/// <summary>
/// Recursively prints all prime numbers in descending order from a given number.
/// </summary>
/// <param name="current">The current number being evaluated.</param>
/// <param name="isFirstPrime">Tracks if this is the first prime to be printed (to handle comma formatting).</param>
static void PrintPrimesDescending(int current, bool isFirstPrime = true)
{
    if (current < 2)
        return;

    if (IsPrime(current))
    {
        if (!isFirstPrime)
            Console.Write(", ");
        Console.Write(current);
        PrintPrimesDescending(--current, false);
    }
    else
        PrintPrimesDescending(--current, isFirstPrime);
}