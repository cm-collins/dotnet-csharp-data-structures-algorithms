using System;
using System.Text.Json;
using leetcode.Easy;


var sol = Solution()

// Prompt: N (number of strings). Empty => run demos
Console.Write("Enter N (number of strings). Press Enter for demo: ");
var nLine = Console.ReadLine();
string[] strs;
if (string.IsNullOrWhiteSpace(nLine) || !int.TryParse(nLine, out int n) || n <= 0)
{
    // Demo cases
    var demoCases = new List<string[]>
    {
        new string[] { "flower", "flow", "flight" },
        new string[] { "dog", "racecar", "car" },
        new string[] { "interspecies", "interstellar", "interstate" },
        new string[] { "throne", "dungeon" },
        new string[] { "throne", "throne" },
        new string[] { "" },
        new string[] { "a" },
        new string[] { "a", "b" },
        new string[] { "cir", "car" }
    };

    foreach (var demo in demoCases)
    {
        var result = sol.LongestCommonPrefix(demo);
        Console.WriteLine($"Input: {JsonSerializerizer.Serialize(demo)} => Output: \"{result}\"");
    }
}
else
{
    // Read N strings from user input
    strs = new string[n];
    for (int i = 0; i < n; i++)
    {
        Console.Write($"Enter string {i + 1}: ");
        strs[i] = Console.ReadLine() ?? string.Empty;
    }

    var result = sol.LongestCommonPrefix(strs);
    Console.WriteLine($"Longest Common Prefix: \"{result}\"");
}