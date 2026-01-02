// ============================================================================
// Problem: 44. Wildcard Matching (LeetCode - Hard)
// Task: Given an input string (s) and a pattern (p), implement wildcard pattern
//       matching with support for '?' and '*' where:
//       - '?' Matches any single character.
//       - '*' Matches any sequence of characters (including the empty sequence).
//       The matching should cover the entire input string (not partial).
// 
// Examples:
//   s = "aa", p = "a"      -> false ("a" does not match entire "aa")
//   s = "aa", p = "*"      -> true ('*' matches any sequence)
//   s = "cb", p = "?a"     -> false ('?' matches 'c', but 'a' != 'b')
//   s = "adceb", p = "*a*b" -> true
//
// Constraints:
//   0 <= s.Length, p.Length <= 2000
//   s contains only lowercase English letters.
//   p contains only lowercase English letters, '?' or '*'.
// ============================================================================

/*
--------------------------------------------------------------------------------
THINKING PROCESS
--------------------------------------------------------------------------------
This is a classic 2D Dynamic Programming problem. We need to determine if
s[0..i-1] matches p[0..j-1] for all possible substrings.

Key insight: Build a DP table where dp[i][j] = true if s[0..i-1] matches
p[0..j-1]. We process both strings from left to right, building solutions
from smaller subproblems.

Three cases for pattern character p[j-1]:
1. '?' - matches any single character
   → dp[i][j] = dp[i-1][j-1] (both advance by 1)

2. '*' - matches any sequence (including empty)
   → dp[i][j] = dp[i][j-1] OR dp[i-1][j]
   - dp[i][j-1]: '*' matches empty (skip the '*')
   - dp[i-1][j]: '*' matches s[i-1] and can continue matching more

3. Regular character - must match exactly
   → dp[i][j] = (s[i-1] == p[j-1]) AND dp[i-1][j-1]

Base cases:
- dp[0][0] = true (empty string matches empty pattern)
- dp[i][0] = false for i > 0 (non-empty string can't match empty pattern)
- dp[0][j] = dp[0][j-1] if p[j-1] == '*', else false
  (empty string can only match pattern of all '*' characters)

Why DP works:
- Overlapping subproblems: checking if "abc" matches "a*c" requires checking
  if "ab" matches "a*", which is a smaller subproblem.
- Optimal substructure: if we know all smaller matches, we can determine
  larger matches by combining them.

--------------------------------------------------------------------------------
DATA STRUCTURES
--------------------------------------------------------------------------------
- bool[,] dp: 2D array of size (s.Length + 1) × (p.Length + 1)
  - dp[i][j] represents whether s[0..i-1] matches p[0..j-1]
  - Extra row/column for empty string/pattern cases
  - Space: O(m × n) where m = s.Length, n = p.Length

Alternative (space-optimized):
- Can reduce to O(min(m, n)) by only keeping previous row
- More complex to implement, but useful for large inputs

--------------------------------------------------------------------------------
ALGORITHM (2D Dynamic Programming - Bottom-up)
--------------------------------------------------------------------------------
1) Initialize dp[0][0] = true (empty matches empty)

2) Fill first row: dp[0][j] for j > 0
   - If p[j-1] == '*', then dp[0][j] = dp[0][j-1]
   - Otherwise, dp[0][j] = false

3) Fill first column: dp[i][0] = false for i > 0
   (non-empty string can't match empty pattern)

4) Fill remaining cells dp[i][j] for i > 0, j > 0:
   a) If p[j-1] == '?':
        dp[i][j] = dp[i-1][j-1]
   
   b) If p[j-1] == '*':
        dp[i][j] = dp[i][j-1] || dp[i-1][j]
        (match empty OR match current char and continue)
   
   c) Otherwise (regular char):
        dp[i][j] = (s[i-1] == p[j-1]) && dp[i-1][j-1]

5) Return dp[s.Length][p.Length]

Correctness:
- Base cases handle empty string/pattern correctly
- Transitions correctly model the matching rules
- Final answer at dp[s.Length][p.Length] represents full match

--------------------------------------------------------------------------------
COMPLEXITY
--------------------------------------------------------------------------------
Let m = s.Length, n = p.Length

Time Complexity: O(m × n)
- Each cell in DP table is computed exactly once
- Each computation is O(1) (just comparisons and boolean operations)

Space Complexity: O(m × n)
- DP table stores (m+1) × (n+1) boolean values
- Can be optimized to O(min(m, n)) with space optimization

--------------------------------------------------------------------------------
RELATED PATTERNS
--------------------------------------------------------------------------------
- Regular Expression Matching (LeetCode 10): Similar DP structure, but '*'
  means "zero or more of preceding element" (different semantics)
- Edit Distance: Uses similar 2D DP with character matching
- Longest Common Subsequence: Similar DP transitions
- String matching with special characters → often requires DP

--------------------------------------------------------------------------------
OPTIMIZATION NOTES
--------------------------------------------------------------------------------
1. Pattern preprocessing: Multiple consecutive '*' can be collapsed to single '*'
   Example: "a***b" → "a*b" (same matching behavior)

2. Space optimization: Only need previous row, can use 1D array
   - dp[j] represents current row
   - Update in-place from left to right

3. Early termination: If pattern has no '*' and lengths don't match, return false
   (but need to account for '?' which can match any single char)
--------------------------------------------------------------------------------
*/

namespace leetcode.Medium
{
    public class WildcardMatching
    {
        public bool IsMatch(string s, string p)
        {
            int m = s.Length;
            int n = p.Length;

            // dp[i][j] = true if s[0..i-1] matches p[0..j-1]
            bool[,] dp = new bool[m + 1, n + 1];

            // Base case: empty string matches empty pattern
            dp[0, 0] = true;

            // Fill first row: empty string can only match pattern of all '*'
            for (int j = 1; j <= n; j++)
            {
                if (p[j - 1] == '*')
                {
                    dp[0, j] = dp[0, j - 1];
                }
                // else dp[0, j] remains false (default)
            }

            // Fill the DP table
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (p[j - 1] == '?')
                    {
                        // '?' matches any single character
                        dp[i, j] = dp[i - 1, j - 1];
                    }
                    else if (p[j - 1] == '*')
                    {
                        // '*' can match:
                        // 1. Empty sequence: dp[i][j-1] (skip the '*')
                        // 2. One or more chars: dp[i-1][j] (match s[i-1], keep '*' for more)
                        dp[i, j] = dp[i, j - 1] || dp[i - 1, j];
                    }
                    else
                    {
                        // Regular character: must match exactly
                        dp[i, j] = (s[i - 1] == p[j - 1]) && dp[i - 1, j - 1];
                    }
                }
            }

            return dp[m, n];
        }
    }
}

