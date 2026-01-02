using System;
using System.Collections.Generic;
using System.Linq;
using leetcode.Easy;
using leetcode.Medium;
using leetcode.Hard;

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
            RunBinaryInorderDfs();
            RunGenerateParentheses();
            RunWildcardMatching();

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

        static void RunBinaryInorderDfs()
        {
            Console.WriteLine("--- Problem 94: Binary Tree Inorder Traversal ---");
            var solver = new BinaryInorderDfs();

            // Helper function to build tree from array representation
            TreeNode? BuildTree(int?[] arr)
            {
                if (arr == null || arr.Length == 0 || arr[0] == null) return null;

                TreeNode root = new TreeNode(arr[0].Value);
                var queue = new Queue<TreeNode>();
                queue.Enqueue(root);
                int i = 1;

                while (queue.Count > 0 && i < arr.Length)
                {
                    TreeNode node = queue.Dequeue();

                    if (i < arr.Length && arr[i].HasValue)
                    {
                        node.left = new TreeNode(arr[i].Value);
                        queue.Enqueue(node.left);
                    }
                    i++;

                    if (i < arr.Length && arr[i].HasValue)
                    {
                        node.right = new TreeNode(arr[i].Value);
                        queue.Enqueue(node.right);
                    }
                    i++;
                }

                return root;
            }

            // Test cases from the problem description
            int?[][] testCases = {
                new int?[] { 1, null, 2, 3 },  // Expected: [1,3,2]
                new int?[] { 1, 2, 3, 4, 5, null, 8, null, null, 6, 7, 9 },  // Expected: [4,2,6,5,7,1,3,9,8]
                new int?[] { },  // Expected: []
                new int?[] { 1 }  // Expected: [1]
            };

            foreach (var testCase in testCases)
            {
                TreeNode? root = BuildTree(testCase);
                var result = solver.Solve(root);
                string inputStr = testCase.Length == 0 ? "[]" : $"[{string.Join(",", testCase.Select(x => x?.ToString() ?? "null"))}]";
                Console.WriteLine($"Input: {inputStr}");
                Console.WriteLine($"Output: [{string.Join(",", result)}]\n");
            }
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

        static void RunWildcardMatching()
        {
            Console.WriteLine("--- Problem 44: Wildcard Matching (Hard) ---");
            var solver = new WildcardMatching();
            
            // Test cases from problem description and edge cases
            (string s, string p, bool expected)[] testCases = {
                ("aa", "a", false),           // Example 1: "a" does not match entire "aa"
                ("aa", "*", true),             // Example 2: '*' matches any sequence
                ("cb", "?a", false),           // Example 3: '?' matches 'c', but 'a' != 'b'
                ("adceb", "*a*b", true),       // Example from header
                ("", "*", true),                // Empty string matches '*'
                ("", "a", false),               // Empty string doesn't match 'a'
                ("a", "", false),               // Non-empty doesn't match empty pattern
                ("", "", true),                 // Empty matches empty
                ("abc", "a?c", true),          // '?' matches single char
                ("abc", "a*c", true),          // '*' matches sequence
                ("abc", "a*bc", true),         // '*' in middle
                ("abc", "a*", true),           // '*' at end
                ("abc", "*c", true),           // '*' at start
                ("abc", "a**c", true),         // Multiple consecutive '*' (equivalent to '*')
                ("abc", "a?*c", true),         // '?' and '*' together
                ("mississippi", "m??*ss*?i*pi", false), // Complex pattern that doesn't match
                ("ho", "ho**", true),          // Multiple '*' at end
                ("aaab", "b**", false),        // Pattern doesn't match start
            };

            int passed = 0;
            int total = testCases.Length;

            foreach (var (s, p, expected) in testCases)
            {
                bool result = solver.IsMatch(s, p);
                string status = result == expected ? "✓" : "✗";
                if (result == expected) passed++;
                Console.WriteLine($"{status} IsMatch(\"{s}\", \"{p}\") => {result} (expected: {expected})");
            }

            Console.WriteLine($"\nResults: {passed}/{total} test cases passed\n");
        }
    }
}
