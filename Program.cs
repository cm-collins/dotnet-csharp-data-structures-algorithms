using System;
using leetcode.Easy;
using leetcode.Medium;

namespace leetcode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== LeetCode Problems - C# Solutions ===\n");

            // Example: Run different problems
            RunLongestCommonPrefix();
            RunValidParentheses();
            RunGenerateParentheses();

            Console.WriteLine("\n=== All tests completed ===");
        }

        static void RunLongestCommonPrefix()
        {
            Console.WriteLine("--- Problem 14: Longest Common Prefix ---");
            var solver = new LongestCommonPrefix();

            string[][] testCases = {
                new string[] { "flower", "flow", "flight" },
                new string[] { "dog", "racecar", "car" },
                new string[] { "interspecies", "interstellar", "interstate" },
                new string[] { "throne", "throne" },
                new string[] { "throne", "dungeon" }
            };

            foreach (var testCase in testCases)
            {
                string result = solver.Solve(testCase);
                Console.WriteLine($"Input: [{string.Join(", ", testCase.Select(s => $"\"{s}\""))}]");
                Console.WriteLine($"Output: \"{result}\"\n");
            }
        }

        static void RunValidParentheses()
        {
            Console.WriteLine("--- Problem 20: Valid Parentheses ---");
            var solver = new ValidParentheses();

            string[] testCases = { "()", "()[]{}", "(]", "([])", "([)]", "((()))", "" };

            foreach (var testCase in testCases)
            {
                bool result = solver.Solve(testCase);
                Console.WriteLine($"IsValid(\"{testCase}\") => {result}");
            }
            Console.WriteLine();
        }

        static void RunGenerateParentheses()
        {
            Console.WriteLine("--- Problem 22: Generate Parentheses ---");
            
            // Backtracking approach
            Console.WriteLine("Backtracking Approach:");
            var backtrackingSolver = new GenerateParenthesesBacktracking();
            for (int n = 1; n <= 3; n++)
            {
                var result = backtrackingSolver.Solve(n);
                Console.WriteLine($"n={n}: [{string.Join(", ", result.Select(s => $"\"{s}\""))}]");
            }

            // DP approach
            Console.WriteLine("\nDynamic Programming Approach:");
            var dpSolver = new GenerateParenthesesDP();
            for (int n = 1; n <= 3; n++)
            {
                var result = dpSolver.Solve(n);
                Console.WriteLine($"n={n}: [{string.Join(", ", result.Select(s => $"\"{s}\""))}]");
            }
            Console.WriteLine();
        }
    }
}
