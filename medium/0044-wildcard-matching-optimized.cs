// ============================================================================
// Problem: 44. Wildcard Matching (LeetCode - Hard) - Space Optimized
// Task: Same as 0044-wildcard-matching.cs but with O(min(m, n)) space complexity
// 
// This version optimizes space from O(m × n) to O(min(m, n)) by using only
// a 1D array instead of a 2D table, since we only need the previous row to
// compute the current row.
// ============================================================================

/*
--------------------------------------------------------------------------------
SPACE OPTIMIZATION TECHNIQUE
--------------------------------------------------------------------------------
Key insight: In the 2D DP solution, when computing dp[i][j], we only need:
- dp[i-1][j-1] (diagonal)
- dp[i][j-1] (left)
- dp[i-1][j] (top)

Since we process row by row, we can use a 1D array where:
- dp[j] represents the current row (dp[i][j])
- We need to keep track of the previous value (dp[i-1][j-1]) before overwriting

Strategy:
1. Use bool[] dp of size (n + 1) where n = p.Length
2. Process each row (each character in string s)
3. For each row, update dp from left to right
4. Keep track of dp[j-1] before updating dp[j] (for diagonal value)

Important: We need to process columns from left to right, but we need the
previous row's value at position j-1 before we overwrite it. So we store
dp[j-1] in a temporary variable before updating.

--------------------------------------------------------------------------------
ALGORITHM (Space-Optimized DP)
--------------------------------------------------------------------------------
1) Initialize dp[0] = true (empty string matches empty pattern)

2) Fill first row: dp[j] for j > 0
   - If p[j-1] == '*', then dp[j] = dp[j-1]
   - Otherwise, dp[j] = false

3) For each row i (each char in s):
   a) Store dp[0] in prev (previous row's value at column 0)
   b) Set dp[0] = false (non-empty string can't match empty pattern)
   
   c) For each column j > 0:
      - Store dp[j] in temp (this is dp[i-1][j] before update)
      - Compute new dp[j] based on pattern character:
        * '?' → dp[j] = prev (diagonal value)
        * '*' → dp[j] = dp[j-1] || temp (left OR top)
        * Regular → dp[j] = (s[i-1] == p[j-1]) && prev
      - Update prev = temp (for next iteration's diagonal)

4) Return dp[n]

Complexity:
- Time: O(m × n) - same as 2D version
- Space: O(n) where n = p.Length (can be further optimized to O(min(m, n)))

Note: We could also optimize by using the smaller dimension, but typically
the pattern is shorter than the string, so O(n) is already optimal.
--------------------------------------------------------------------------------
*/

namespace leetcode.Medium
{
    public class WildcardMatchingOptimized
    {
        public bool IsMatch(string s, string p)
        {
            int m = s.Length;
            int n = p.Length;

            // dp[j] represents whether s[0..i-1] matches p[0..j-1] for current row i
            bool[] dp = new bool[n + 1];

            // Base case: empty string matches empty pattern
            dp[0] = true;

            // Fill first row: empty string can only match pattern of all '*'
            for (int j = 1; j <= n; j++)
            {
                if (p[j - 1] == '*')
                {
                    dp[j] = dp[j - 1];
                }
                // else dp[j] remains false (default)
            }

            // Process each row (each character in string s)
            for (int i = 1; i <= m; i++)
            {
                // prev stores dp[i-1][j-1] (diagonal value from previous row)
                // We need to preserve it before overwriting dp[j]
                bool prev = dp[0];
                
                // First column: non-empty string can't match empty pattern
                dp[0] = false;

                // Process each column (each character in pattern p)
                for (int j = 1; j <= n; j++)
                {
                    // temp stores dp[i-1][j] (top value) before we overwrite it
                    bool temp = dp[j];

                    if (p[j - 1] == '?')
                    {
                        // '?' matches any single character
                        // dp[j] = dp[i-1][j-1] = prev
                        dp[j] = prev;
                    }
                    else if (p[j - 1] == '*')
                    {
                        // '*' can match:
                        // 1. Empty sequence: dp[i][j-1] = dp[j-1] (left)
                        // 2. One or more chars: dp[i-1][j] = temp (top)
                        dp[j] = dp[j - 1] || temp;
                    }
                    else
                    {
                        // Regular character: must match exactly
                        // dp[j] = (s[i-1] == p[j-1]) && dp[i-1][j-1]
                        dp[j] = (s[i - 1] == p[j - 1]) && prev;
                    }

                    // Update prev for next iteration (becomes diagonal for j+1)
                    prev = temp;
                }
            }

            return dp[n];
        }
    }
}

