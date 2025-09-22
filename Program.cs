using System;
using leetcode.Easy;

// var sol = new LongestCommon();

// // Console.Write("Enter N (number of strings): ");
// // if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
// // {
// //     Console.WriteLine("N must be a positive integer.");
// //     return;
// // }

// // var strs = new string[n];
// // for (int i = 0; i < n; i++)
// // { 
// //     Console.Write($"str[{i}]: ");
// //     strs[i] = Console.ReadLine() ?? string.Empty;
// // }

// // Console.WriteLine($"Longest Common Prefix: \"{sol.LongestCommonPrefix(strs)}\"");

var validparentheses = new Solution();

// --- Run sample tests for Valid Parentheses ---
string[] vpTests = { "()", "()[]{}", "(]", "([])", "([)]" };
Console.WriteLine("Valid Parentheses tests:");
foreach (var s in vpTests)
{
    Console.WriteLine($"IsValid(\"{s}\") => {validparentheses.IsValid(s)}");
}
Console.WriteLine();

