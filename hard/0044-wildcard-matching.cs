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
//
// Approach: Dynamic Programming (Space-Optimized 2D DP)
// ============================================================================

/*
--------------------------------------------------------------------------------
UNDERSTANDING DYNAMIC PROGRAMMING (DP) - A LEARNING GUIDE
--------------------------------------------------------------------------------

WHAT IS DYNAMIC PROGRAMMING?
Dynamic Programming is a problem-solving technique that solves complex problems
by breaking them down into simpler subproblems. It stores the results of
subproblems to avoid recomputing them, making it more efficient than naive
recursion.

KEY CHARACTERISTICS OF DP PROBLEMS:
1. Overlapping Subproblems: The same subproblems are solved multiple times
2. Optimal Substructure: The solution to a problem can be built from solutions
   to its subproblems
3. Memoization/Tabulation: Store results of subproblems to reuse them

WHY IS THIS A DP PROBLEM?
- We need to check if s[0..i] matches p[0..j] for all possible substrings
- These subproblems overlap: checking "abc" matches "a*c" requires checking
  if "ab" matches "a*", which is a smaller subproblem
- We can build a table where each cell represents a subproblem solution

DP APPROACHES:
1. Top-Down (Memoization): Recursive with caching
   - Start with full problem, recursively solve subproblems
   - Cache results to avoid recomputation
   - More intuitive but has recursion overhead

2. Bottom-Up (Tabulation): Iterative with table
   - Start with smallest subproblems, build up to full problem
   - Fill a table systematically
   - More efficient, no recursion overhead
   - This solution uses bottom-up approach

--------------------------------------------------------------------------------
THINKING PROCESS - HOW TO APPROACH THIS PROBLEM
--------------------------------------------------------------------------------

STEP 1: IDENTIFY THE SUBPROBLEM
Question: "Does s[0..i-1] match p[0..j-1]?"
- This is our fundamental subproblem
- We'll build a DP table where dp[i][j] answers this question

STEP 2: IDENTIFY BASE CASES
- Empty string matches empty pattern: dp[0][0] = true
- Non-empty string can't match empty pattern: dp[i][0] = false (for i > 0)
- Empty string can only match pattern of all '*': dp[0][j] depends on pattern

STEP 3: IDENTIFY STATE TRANSITIONS
For each position (i, j), we need to determine how to fill dp[i][j] based on:
- Current character in string: s[i-1]
- Current character in pattern: p[j-1]

Three cases:
1. Pattern is '?' → matches any single char → dp[i][j] = dp[i-1][j-1]
2. Pattern is '*' → matches any sequence → dp[i][j] = dp[i][j-1] || dp[i-1][j]
3. Pattern is regular char → must match exactly → dp[i][j] = (s[i-1] == p[j-1]) && dp[i-1][j-1]

STEP 4: BUILD THE SOLUTION
- Start from base cases
- Fill table row by row, column by column
- Final answer is at dp[m][n] where m = s.Length, n = p.Length

STEP 5: OPTIMIZE SPACE
- Notice: we only need the previous row to compute current row
- Instead of 2D table, use 1D array and update in-place
- Reduces space from O(m×n) to O(n)

--------------------------------------------------------------------------------
DATA STRUCTURES
--------------------------------------------------------------------------------

PRIMARY DATA STRUCTURE:
- bool[] dp: 1D array of size (p.Length + 1)
  - dp[j] represents whether s[0..i-1] matches p[0..j-1] for current row i
  - We process row by row, updating dp in-place
  - Extra element (index 0) handles empty pattern case

WHY 1D INSTEAD OF 2D?
In the 2D DP approach, we would use:
  bool[,] dp = new bool[m + 1, n + 1];
  // dp[i, j] = true if s[0..i-1] matches p[0..j-1]

However, when computing dp[i, j], we only need:
  - dp[i-1, j-1] (diagonal - previous row, previous column)
  - dp[i, j-1] (left - current row, previous column)
  - dp[i-1, j] (top - previous row, current column)

Since we process row by row, we can:
  - Use dp[j] to represent current row (dp[i, j])
  - Use a temporary variable to store previous row's value before overwriting
  - This reduces space from O(m×n) to O(n)

AUXILIARY VARIABLES:
- int m: length of input string s
- int n: length of pattern p
- bool prev: stores dp[i-1][j-1] (diagonal value) before it's overwritten
- bool temp: stores dp[i-1][j] (top value) before it's overwritten

Space Complexity: O(n) where n = p.Length
  - The dp array is the only data structure that grows with input size
  - All other variables are O(1)

--------------------------------------------------------------------------------
ALGORITHM - STEP BY STEP EXPLANATION
--------------------------------------------------------------------------------

PHASE 1: INITIALIZATION
1. Get lengths: m = s.Length, n = p.Length
2. Create dp array: bool[] dp = new bool[n + 1]
   - Size is n+1 to include empty pattern case (index 0)
3. Set base case: dp[0] = true
   - Empty string matches empty pattern

PHASE 2: FILL FIRST ROW (Empty String Cases)
For j from 1 to n:
  - If p[j-1] == '*': dp[j] = dp[j-1]
    (Empty string can match pattern of all '*')
  - Otherwise: dp[j] = false
    (Empty string can't match non-'*' patterns)

This handles the case where s is empty but p is not.

PHASE 3: FILL REMAINING ROWS (Process Each Character in String)
For i from 1 to m (each character in string s):
  
  a) PRESERVE PREVIOUS ROW'S STATE:
     - Store dp[0] in prev (this is dp[i-1][0] before update)
     - Set dp[0] = false (non-empty string can't match empty pattern)
  
  b) PROCESS EACH COLUMN (each character in pattern p):
     For j from 1 to n:
     
     - Store current dp[j] in temp (this is dp[i-1][j] before overwriting)
     
     - CASE 1: Pattern character is '?'
       dp[j] = prev
       Explanation: '?' matches any single character, so we advance both
       string and pattern by 1. The result depends on whether the previous
       characters matched (stored in prev = dp[i-1][j-1]).
     
     - CASE 2: Pattern character is '*'
       dp[j] = dp[j-1] || temp
       Explanation: '*' can match:
       1. Empty sequence: dp[j-1] (skip the '*', pattern advances but string doesn't)
       2. One or more characters: temp (match current char, keep '*' for more)
       We use OR because either option can lead to a match.
     
     - CASE 3: Pattern character is regular letter
       dp[j] = (s[i-1] == p[j-1]) && prev
       Explanation: Characters must match exactly AND previous characters
       must have matched. Both conditions must be true.
     
     - Update prev = temp for next iteration
       (prev becomes the diagonal value for j+1)

PHASE 4: RETURN RESULT
Return dp[n]
- This represents whether s[0..m-1] matches p[0..n-1]
- The entire string matches the entire pattern

--------------------------------------------------------------------------------
VISUAL EXAMPLE - TRACING THROUGH "aa" AND "*"
--------------------------------------------------------------------------------

String s = "aa", Pattern p = "*"

Initialization:
  m = 2, n = 1
  dp = [true, false]  (size 2: index 0 for empty pattern, index 1 for '*')

After Phase 2 (Fill first row):
  dp = [true, true]   (empty string matches '*')

Row 1 (i=1, processing 'a' from string):
  prev = dp[0] = true
  dp[0] = false
  
  j=1 (processing '*' from pattern):
    temp = dp[1] = true
    dp[1] = dp[0] || temp = false || true = true
    prev = temp = true
  
  Result: dp = [false, true]

Row 2 (i=2, processing 'a' from string):
  prev = dp[0] = false
  dp[0] = false
  
  j=1 (processing '*' from pattern):
    temp = dp[1] = true
    dp[1] = dp[0] || temp = false || true = true
    prev = temp = true
  
  Result: dp = [false, true]

Final: dp[1] = true → "aa" matches "*" ✓

--------------------------------------------------------------------------------
COMPLEXITY ANALYSIS
--------------------------------------------------------------------------------

TIME COMPLEXITY: O(m × n)
- We iterate through each character in string s: O(m) iterations
- For each character, we iterate through each character in pattern p: O(n) iterations
- Each iteration performs O(1) operations (comparisons, boolean operations)
- Total: O(m × n) where m = s.Length, n = p.Length

SPACE COMPLEXITY: O(n)
- The dp array has size (n + 1): O(n)
- Auxiliary variables (m, n, prev, temp, i, j): O(1)
- Total: O(n)
- Note: This is optimized from O(m × n) in the 2D DP approach

COMPARISON WITH 2D DP:
- 2D DP: Time O(m×n), Space O(m×n)
- This solution: Time O(m×n), Space O(n)
- Trade-off: Slightly more complex code for significant space savings

--------------------------------------------------------------------------------
KEY DP CONCEPTS DEMONSTRATED
--------------------------------------------------------------------------------

1. STATE DEFINITION:
   State: dp[j] = "Does s[0..i-1] match p[0..j-1]?"
   - Clear, unambiguous definition is crucial for DP

2. STATE TRANSITIONS:
   - How to compute current state from previous states
   - Three distinct cases based on pattern character type
   - Each transition is O(1)

3. BASE CASES:
   - Handle edge cases (empty string, empty pattern)
   - Foundation for all other computations
   - Must be correct or entire solution fails

4. SPACE OPTIMIZATION:
   - Identify that only previous row is needed
   - Use 1D array with careful state management
   - Maintain correctness while reducing space

5. ITERATIVE BUILD-UP:
   - Start with smallest subproblems (base cases)
   - Build up to larger subproblems
   - Final answer is the last computed state

--------------------------------------------------------------------------------
COMMON PITFALLS AND HOW TO AVOID THEM
--------------------------------------------------------------------------------

PITFALL 1: Incorrect base case for empty string
  Wrong: dp[0][j] = false for all j > 0
  Correct: dp[0][j] = dp[0][j-1] if p[j-1] == '*'
  Why: Empty string CAN match pattern of all '*'

PITFALL 2: Wrong state transition for '*'
  Wrong: dp[i][j] = dp[i-1][j-1] (treating '*' like '?')
  Correct: dp[i][j] = dp[i][j-1] || dp[i-1][j]
  Why: '*' can match empty OR one or more characters

PITFALL 3: Forgetting to preserve previous state in space optimization
  Wrong: Not storing prev and temp before overwriting
  Correct: Store dp[j] in temp, dp[j-1] in prev before updates
  Why: We need previous row's values for current row's computation

PITFALL 4: Off-by-one errors with indices
  Wrong: Using s[i] and p[j] directly
  Correct: Use s[i-1] and p[j-1] (dp[i][j] represents s[0..i-1] and p[0..j-1])
  Why: dp array has extra row/column for empty cases

--------------------------------------------------------------------------------
RELATED DP PROBLEMS TO PRACTICE
--------------------------------------------------------------------------------

Similar Pattern Matching Problems:
- LeetCode 10: Regular Expression Matching (similar DP structure)
- LeetCode 72: Edit Distance (similar 2D DP with character matching)
- LeetCode 115: Distinct Subsequences (DP with string matching)

General DP Practice:
- LeetCode 70: Climbing Stairs (simplest DP)
- LeetCode 198: House Robber (1D DP)
- LeetCode 300: Longest Increasing Subsequence (1D DP)
- LeetCode 1143: Longest Common Subsequence (2D DP)

These problems help build intuition for:
- State definition
- State transitions
- Base cases
- Space optimization

--------------------------------------------------------------------------------
C# BEST PRACTICES USED IN THIS SOLUTION
--------------------------------------------------------------------------------

1. MEANINGFUL VARIABLE NAMES:
   - m, n: Standard notation for string lengths
   - dp: Clear abbreviation for "dynamic programming"
   - prev, temp: Descriptive names for temporary values

2. COMMENTS EXPLAINING LOGIC:
   - Inline comments explain WHY, not just WHAT
   - Complex logic is broken down with comments
   - Each phase is clearly marked

3. PROPER ARRAY INITIALIZATION:
   - bool[] dp = new bool[n + 1] - explicit size
   - Arrays are zero-initialized in C# (all false by default)

4. EFFICIENT MEMORY USAGE:
   - Reusing dp array instead of creating new ones
   - Minimal temporary variables
   - Space-optimized approach

5. READABLE CODE STRUCTURE:
   - Clear separation of phases
   - Logical flow from initialization to result
   - Easy to trace through execution

6. TYPE SAFETY:
   - Explicit type declarations where helpful
   - Using bool for boolean values (not int)
   - Proper array indexing with bounds checking

--------------------------------------------------------------------------------
*/

namespace leetcode.Hard
{
    /// <summary>
    /// Solution for LeetCode 44: Wildcard Matching
    /// Uses space-optimized Dynamic Programming approach
    /// </summary>
    public class WildcardMatching
    {
        /// <summary>
        /// Determines if the input string matches the wildcard pattern.
        /// </summary>
        /// <param name="s">Input string containing only lowercase English letters</param>
        /// <param name="p">Pattern string containing lowercase English letters, '?', or '*'</param>
        /// <returns>True if the entire string matches the pattern, false otherwise</returns>
        /// <remarks>
        /// Time Complexity: O(m × n) where m = s.Length, n = p.Length
        /// Space Complexity: O(n) - optimized from O(m × n)
        /// </remarks>
        public bool IsMatch(string s, string p)
        {
            int m = s.Length;
            int n = p.Length;

            // dp[j] represents whether s[0..i-1] matches p[0..j-1] for current row i
            // Size is n+1 to include empty pattern case (index 0)
            bool[] dp = new bool[n + 1];

            // Base case: empty string matches empty pattern
            dp[0] = true;

            // Fill first row: empty string can only match pattern of all '*'
            // This handles the case where s is empty but p is not
            for (int j = 1; j <= n; j++)
            {
                if (p[j - 1] == '*')
                {
                    // '*' can match empty sequence, so if previous pattern matched,
                    // adding '*' still matches
                    dp[j] = dp[j - 1];
                }
                // else dp[j] remains false (default value)
                // Empty string cannot match non-'*' patterns
            }

            // Process each row (each character in string s)
            // We build the solution row by row, reusing the dp array
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
                    // This represents: "does s[0..i-2] match p[0..j-1]?"
                    bool temp = dp[j];

                    if (p[j - 1] == '?')
                    {
                        // '?' matches any single character
                        // Both string and pattern advance by 1
                        // Result depends on whether previous characters matched
                        dp[j] = prev;
                    }
                    else if (p[j - 1] == '*')
                    {
                        // '*' can match:
                        // 1. Empty sequence: dp[j-1] (skip the '*', pattern advances)
                        // 2. One or more chars: temp (match current char, keep '*' for more)
                        // We use OR because either option can lead to a match
                        dp[j] = dp[j - 1] || temp;
                    }
                    else
                    {
                        // Regular character: must match exactly
                        // Characters must match AND previous characters must have matched
                        dp[j] = (s[i - 1] == p[j - 1]) && prev;
                    }

                    // Update prev for next iteration
                    // prev becomes the diagonal value for j+1
                    prev = temp;
                }
            }

            // Final answer: does entire string match entire pattern?
            return dp[n];
        }
    }
}

