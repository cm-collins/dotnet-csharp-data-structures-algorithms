using System;
using leetcode.Easy;

var sol = new Solution();

Console.Write("Enter N (number of strings): ");
if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
{
    Console.WriteLine("N must be a positive integer.");
    return;
}

var strs = new string[n];
for (int i = 0; i < n; i++)
{
    Console.Write($"str[{i}]: ");
    strs[i] = Console.ReadLine() ?? string.Empty;
}

Console.WriteLine($"Longest Common Prefix: \"{sol.LongestCommonPrefix(strs)}\"");
