///*
// * Challenge name: Verify Number With Operations
// * Description: 
// * Given a list of numbers and a target number, write a program to determine whether the target number can be calculated by applying "+ - * /" operations to the number list? You can assume () is automatically added when necessary.
// * Example 1:
// * Input: list = [1,2,3,4] target = 21
// * Output:  true
// * Explanation= (1+2)(3+4)=21 
// * 
// * 
// * Example 2:
// * Input: list = [4,3,2,5] target = 33
// * Output:  true
// * Explanation= 3+5(2+4)=33
// * 
// * Example 3:
// * Input: list = [3,4,2] target = 5
// * Output:  false
// * Explanation= There are no operations that achieve the result
// */
//namespace VerifyNumberWithOperationsConsoleApp;

///// <summary>
///// Solves the problem of determining if a target number can be reached
///// using a set of numbers and basic arithmetic operations (+, -, *, /).
///// </summary>
///// <remarks>
///// Initializes a new instance of the CalculatorSolver.
///// </remarks>
///// <param name="epsilon">Epsilon value for floating-point comparison (default: 1e-6)</param>
//public class CalculatorSolver(double epsilon = 1e-6)
//{
//    /// <summary>
//    /// Memoization cache to store previously computed states.
//    /// </summary>
//    private readonly Dictionary<string, bool> memoization = [];

//    /// <summary>
//    /// Determines if the target number can be reached using the given numbers.
//    /// </summary>
//    /// <param name="numbers">List of numbers to use</param>
//    /// <param name="target">Target number to reach</param>
//    /// <returns>True if target can be reached, false otherwise</returns>
//    public bool CanMakeTarget(List<double> numbers, double target) => CanMakeTarget(numbers, target, epsilon);

//    /// <summary>
//    /// Attempts to find a solution and returns the steps to reach the target.
//    /// </summary>
//    /// <param name="numbers">List of numbers to use</param>
//    /// <param name="target">Target number to reach</param>
//    /// <returns>List of operation steps if solution exists, null otherwise</returns>
//    public List<string>? FindSolution(List<double> numbers, double target)
//    {
//        var solution = new List<string>();
//        return FindSolutionRecursive(numbers, target, solution) ? solution : null;
//    }

//    /// <summary>
//    /// Recursive helper method to determine if target can be made.
//    /// </summary>
//    /// <param name="numbers">List of numbers to use</param>
//    /// <param name="target">Target number to reach</param>
//    /// <param name="epsilon">Epsilon value for floating-point comparison</param>
//    /// <returns>True if target can be reached, false otherwise</returns>
//    private bool CanMakeTarget(List<double> numbers, double target, double epsilon)
//    {
//        // Base case: if we have only one number left, check if equals target
//        if (numbers.Count == 1)
//            return Math.Abs(numbers[0] - target) < epsilon;

//        // Create a normalized key for memoization
//        var key = GetStateKey(numbers, target);
//        if (memoization.TryGetValue(key, out bool cached))
//            return cached;

//        // Try every pair of numbers (optimize: only i < j to avoid duplicates)
//        for (int i = 0; i < numbers.Count; i++)
//        {
//            for (int j = i + 1; j < numbers.Count; j++)
//            {
//                double a = numbers[i];
//                double b = numbers[j];

//                // Collect remaining numbers
//                var rest = new List<double>();

//                for (int k = 0; k < numbers.Count; k++)
//                {
//                    if (k != i && k != j)
//                        rest.Add(numbers[k]);
//                }

//                // Try all possible operations (both a op b and b op a for non-commutative ops)
//                foreach (var (result, _) in GenerateResultsWithOperations(a, b, epsilon))
//                {
//                    rest.Add(result);
//                    if (CanMakeTarget(rest, target, epsilon))
//                    {
//                        memoization[key] = true;
//                        return true;
//                    }
//                    rest.RemoveAt(rest.Count - 1);
//                }                
//            }
//        }

//        memoization[key] = false;
//        return false;
//    }

//    /// <summary>
//    /// Recursive helper method to find the solution steps.
//    /// </summary>
//    /// <param name="nums">List of numbers to use</param>
//    /// <param name="target">Target number to reach</param>
//    /// <param name="solution">List to store the operations leading to the target</param>
//    /// <returns>True if a solution is found, false otherwise</returns>
//    private bool FindSolutionRecursive(List<double> nums, double target, List<string> solution)
//    {
//        // Base case: if we have only one number left, check if equals target
//        if (nums.Count == 1)
//            return Math.Abs(nums[0] - target) < epsilon;

//        // Try every pair of numbers (optimize: only i < j to avoid duplicates)
//        for (int i = 0; i < nums.Count; i++)
//        {
//            for (int j = i + 1; j < nums.Count; j++)
//            {
//                double a = nums[i];
//                double b = nums[j];

//                // Collect remaining numbers
//                var rest = new List<double>();
//                for (int k = 0; k < nums.Count; k++)
//                {
//                    if (k != i && k != j)
//                        rest.Add(nums[k]);
//                }

//                // Try all possible operations
//                foreach (var (result, operation) in GenerateResultsWithOperations(a, b, epsilon))
//                {
//                    rest.Add(result);
//                    solution.Add(operation);

//                    if (FindSolutionRecursive(rest, target, solution))
//                        return true;

//                    solution.RemoveAt(solution.Count - 1);
//                    rest.RemoveAt(rest.Count - 1);
//                }
//            }
//        }

//        return false;
//    }

//    /// <summary>
//    /// Generates all possible results and their corresponding operations
//    /// </summary>
//    /// <param name="firstNum">First number</param>
//    /// <param name="secondNum">Second number</param>
//    /// <param name="eps">Epsilon value for floating point comparison</param>
//    /// <returns>Enumerable of tuples containing result and operation string</returns>
//    private static IEnumerable<(double result, string operation)> GenerateResultsWithOperations(double firstNum, double secondNum, double eps)
//    {
//        // Addition (commutative)
//        yield return (firstNum + secondNum, $"{firstNum} + {secondNum} = {firstNum + secondNum}");

//        // Subtraction (try both orders)
//        yield return (firstNum - secondNum, $"{firstNum} - {secondNum} = {firstNum - secondNum}");
//        yield return (secondNum - firstNum, $"{secondNum} - {firstNum} = {secondNum - firstNum}");

//        // Multiplication (commutative)
//        yield return (firstNum * secondNum, $"{firstNum} * {secondNum} = {firstNum * secondNum}");

//        // Division (try both orders, avoid division by zero)
//        if (Math.Abs(secondNum) > eps)
//            yield return (firstNum / secondNum, $"{firstNum} / {secondNum} = {firstNum / secondNum}");
//        if (Math.Abs(firstNum) > eps)
//            yield return (secondNum / firstNum, $"{secondNum} / {firstNum} = {secondNum / firstNum}");
//    }

//    /// <summary>
//    /// Generates a sequence containing the sum, difference, and, if possible, the quotient of two double-precision
//    /// values.
//    /// </summary>
//    /// <remarks>The returned sequence will contain two or three results depending on whether division is
//    /// performed. If <paramref name="secondOperand"/> is close to zero (within ±<paramref name="epsilon"/>), the quotient is not
//    /// included to prevent division by zero.</remarks>
//    /// <param name="firstOperand">The first operand used in the calculations.</param>
//    /// <param name="secondOperand">The second operand used in the calculations.</param>
//    /// <param name="epsilon">The minimum absolute value for <paramref name="secondOperand"/> required to compute the quotient. If <paramref name="secondOperand"/> is
//    /// within ±<paramref name="epsilon"/>, division is omitted to avoid division by zero.</param>
//    /// <returns>An enumerable collection of double values containing the sum (<paramref name="firstOperand"/> + <paramref name="secondOperand"/>), the
//    /// difference (<paramref name="firstOperand"/> - <paramref name="secondOperand"/>), and, if applicable, the quotient (<paramref name="firstOperand"/>
//    /// / <paramref name="secondOperand"/>).</returns>
//    private static IEnumerable<double> GenerateResults(double firstOperand, double secondOperand, double epsilon)
//    {
//        yield return firstOperand + secondOperand;
//        yield return firstOperand - secondOperand;

//        if (Math.Abs(secondOperand) > epsilon)  // avoid division by zero
//            yield return firstOperand / secondOperand;
//    }

//    /// <summary>
//    /// Generates a unique key for the current state of numbers and target for memoization.
//    /// </summary>
//    /// <param name="numbers"></param>
//    /// <param name="target"></param>
//    /// <returns>Unique string key representing the state</returns>
//    private static string GetStateKey(List<double> numbers, double target)
//    {
//        // Sort numbers to ensure consistent key regardless of order
//        var sorted = numbers.OrderBy(n => n).Select(n => Math.Round(n, 6));
//        return $"{string.Join(",", sorted)}|{Math.Round(target, 6)}";
//    }

//    /// <summary>
//    /// Clears the internal memoization cache. Call this between independent problems.
//    /// </summary>
//    public void ClearCache() => memoization.Clear();
//}
