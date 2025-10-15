// ============================================================================
// Problem: 22. Generate Parentheses (LeetCode - Medium)
// Task: Given n pairs of parentheses, generate all combinations of well-formed
//       parentheses.
// 
// Examples:
//   n=3 -> ["((()))","(()())","(())()","()(())","()()()"]
//   n=1 -> ["()"]
//
// Constraints:
//   1 <= n <= 8
// ============================================================================--------------------------------------------------------------------------------
// THINKING PROCESS
// --------------------------------------------------------------------------------
// What does “well-formed” mean?
// - You can never close more than you’ve opened at any point (prefix-valid).
// - By the end, you must have placed exactly n '(' and n ')'.

// This suggests exploring strings by decisions—place '(' or ')'—while enforcing
// validity as you go. That’s classic **backtracking** (a depth-first search) with
// two counters:
// - open: how many '(' used so far (must be <= n)
// - close: how many ')' used so far (must be <= open)

// Stop when the built string has length 2*n.

// Why not “generate all 2^(2n) strings and filter”? Because it’s exponential
// waste. With constraints baked into the recursion, we **never** create invalid
// prefixes.

// Catalan insight (DP view):
// - The count of valid strings is the n-th **Catalan number** Cn.
// - There’s also a DP construction: 
//   G(0) = {""}
//   G(n) = ⋃_{i=0..n-1} "(" + G(i) + ")" + G(n-1-i)
// - Backtracking and DP both enumerate the same set; backtracking is simpler and
// memory-light for this task.
// DATA STRUCTURES
// --------------------------------------------------------------------------------
// - List<string>: to collect results (IList<string> return type).
// - StringBuilder: to build the current string in-place (append/remove) efficiently.
//   (Avoids creating new strings at each step.)

// ALGORITHM (Backtracking / DFS with pruning)
// --------------------------------------------------------------------------------
// 1) Start with empty StringBuilder, open=0, close=0.
// 2) If length == 2*n: add current string to results and return.
// 3) If open < n: append '(', recurse with open+1, then remove last char.
// 4) If close < open: append ')', recurse with close+1, then remove last char.

// Correctness:
// - We never allow more ')' than '(' at any prefix (maintains well-formedness).
// - We only place up to n '(' and n ')', so finished strings are balanced.
// RELATED PATTERNS
// --------------------------------------------------------------------------------
// - Balanced strings, bracket generation, valid path counting → often Catalan.
// - Backtracking with constraints to prune invalid partial states early.
// - DP decomposition with structure like G(n) = "(" + G(i) + ")" + G(n-1-i).
// --------------------------------------------------------------------------------
// */

//

using System.Collections.Generic;
using System.Text;
public class Solution
{
    public IList<string> GenerateParenthesis(int n)
    {
        var result = new List<string>();
        var sb = new StringBuilder();
        Dfs(n, 0, 0, sb, result);
        return result;
    }

    // open: number of '(' used so far
    // close: number of ')' used so far

    private void Dfs(int n, int open, int close, StringBuilder sb, List<string> result)
    {
        // Base case: if the current string is of length 2*n, it's a valid combination
        if (sb.Length == 2 * n)
        {
            result.Add(sb.ToString());
            return;
        }

        // If we can still add '(', do so and recurse
        if (open < n)
        {
            sb.Append('(');
            Dfs(n, open + 1, close, sb, result);
            sb.Length--; // backtrack
        }

        // If we can add ')', do so and recurse
        if (close < open)
        {
            sb.Append(')');
            Dfs(n, open, close + 1, sb, result);
            sb.Length--; // backtrack
        }
    }
}



