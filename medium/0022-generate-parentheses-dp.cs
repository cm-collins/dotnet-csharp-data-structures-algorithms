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
//
// Approach: Dynamic Programming (Memoized Recursion)
// ============================================================================

/*
--------------------------------------------------------------------------------
THINKING PROCESS
--------------------------------------------------------------------------------
Catalan insight (DP view):
- The count of valid strings is the n-th **Catalan number** Cn.
- DP construction: 
  G(0) = {""}
  G(n) = ⋃_{i=0..n-1} "(" + G(i) + ")" + G(n-1-i)

The idea: For n pairs, we can place the first '(' and its matching ')' at
position i, where i ranges from 0 to n-1. Inside this outer pair, we have
G(i) valid combinations, and to the right we have G(n-1-i) valid combinations.

--------------------------------------------------------------------------------
DATA STRUCTURES
--------------------------------------------------------------------------------
- Dictionary<int, IList<string>>: memoization table
  - Key: number of pairs (k)
  - Value: list of all valid strings with k pairs

--------------------------------------------------------------------------------
ALGORITHM (Dynamic Programming with Memoization)
--------------------------------------------------------------------------------
1) Base case: G(0) = {""}
2) For G(k), iterate i from 0 to k-1:
   - lefts = G(i)      // strings inside the first outer "()"
   - rights = G(k-1-i) // strings to the right
   - For each combination (L, R), add "(" + L + ")" + R to result
3) Memoize G(k) to avoid recomputation

Time Complexity: O(4^n / n^(3/2)) - Catalan number enumeration
Space Complexity: O(4^n / n^(3/2)) - storing all results
--------------------------------------------------------------------------------
*/

using System.Collections.Generic;

namespace leetcode.Medium
{
    public class GenerateParenthesesDP
    {
        public IList<string> Solve(int n)
        {
            // memo[k] will store the full list G(k) of well-formed strings with k pairs
            var memo = new Dictionary<int, IList<string>>();
            memo[0] = new List<string> { "" }; // Base case: G(0) = { "" }
            return Gen(n, memo); // Compute G(n) using memoized recursion
        }

        private IList<string> Gen(int k, Dictionary<int, IList<string>> memo)
        {
            // If G(k) was computed before, reuse it
            if (memo.TryGetValue(k, out var cached)) return cached;

            var res = new List<string>();

            // Catalan recurrence:
            // G(k) = ⋃_{i=0..k-1} "(" + G(i) + ")" + G(k-1-i)
            for (int i = 0; i < k; i++)
            {
                var lefts = Gen(i, memo);           // G(i): strings to go inside the first outer "()"
                var rights = Gen(k - 1 - i, memo);  // G(k-1-i): strings to go to the right

                // Cross-product composition of left and right parts
                foreach (var L in lefts)
                    foreach (var R in rights)
                        res.Add("(" + L + ")" + R); // Build one valid string for this split i
            }

            memo[k] = res; // Cache G(k) for reuse by larger k
            return res;
        }
    }
}

