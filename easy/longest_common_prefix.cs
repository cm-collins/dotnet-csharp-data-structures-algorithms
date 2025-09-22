// ============================================================================
// Problem: 14. Longest Common Prefix (LeetCode - Easy)
// Task: Write a function to find the longest common prefix string amongst
//       an array of strings. If there is no common prefix, return "".
//
// Examples:
//   Input: ["flower","flow","flight"]  -> Output: "fl"
//   Input: ["dog","racecar","car"]     -> Output: ""
//
// Constraints:
//   1 <= strs.length <= 200
//   0 <= strs[i].length <= 200
//   strs[i] consists of only lowercase English letters if it is non-empty.
// ============================================================================

/*
--------------------------------------------------------------------------------
DATA STRUCTURES
--------------------------------------------------------------------------------
- Input: string[] (array of strings). We do not create any additional complex
  structures; we only read from this array.
- Working variables:
    - string 'first'   : reference to strs[0] for vertical comparison.
    - char 'c'         : current character at column 'j' to compare across all
                         strings.
    - int indices 'j', 'i': loop counters.
- Output: string (a substring of 'first'), which is the longest common prefix.

Memory characteristics: all auxiliary data is O(1). We don’t allocate
collections, tries, or buffers; the result is derived directly from the input.
The returned substring is the answer, not "working space".
--------------------------------------------------------------------------------

--------------------------------------------------------------------------------
ALGORITHM (Vertical Scanning)
--------------------------------------------------------------------------------
Intuition:
- A common *prefix* must agree character-by-character from the start of
  each string. The first mismatch (or the shortest string ending) determines
  where the prefix stops.

Steps:
1) Handle edge cases:
   - If 'strs' is null or empty -> return "".
   - Let 'first' = strs[0]; if it's empty -> return "" immediately.
2) For each column index j in [0 .. first.Length-1]:
   - Let c = first[j].
   - Compare c with strs[i][j] for every other string i in [1 .. N-1].
   - If any string is too short (j >= strs[i].Length) OR strs[i][j] != c:
       -> we found the first mismatch; return first.Substring(0, j).
3) If we finish the loop with no mismatches, the entire 'first' is common
   -> return 'first'.

Why this works:
- By scanning column-by-column, we stop exactly at the earliest position where
  the strings disagree or one string ends—precisely where a common prefix must end.
--------------------------------------------------------------------------------

--------------------------------------------------------------------------------
COMPLEXITY
--------------------------------------------------------------------------------
Let:
  N = number of strings (<= 200)
  L = length of the shortest string (<= 200)
  P = length of the final longest common prefix (P <= L)

Time Complexity: O(N * P)
- For each prefix position j (at most P), we compare the character across up to N
  strings. We stop as soon as a mismatch happens, so the work is proportional to
  the actual prefix length, not the total string lengths.

Space Complexity (auxiliary): O(1)
- We use only a few scalar variables (indices and a char).
- No extra arrays/tries/buffers are allocated.

Note: The returned substring is output, not auxiliary memory.

--------------------------------------------------------------------------------
THINKING PROCESS / INTERVIEW NARRATIVE
--------------------------------------------------------------------------------
- Identify the structure: This is a "shared prefix" problem; it’s naturally
  tackled column-wise (vertical scan) or by shrinking a candidate prefix
  (horizontal scan). Both are O(N*L).
- Choose clarity: Vertical scanning is simple and stops at the first mismatch,
  making it efficient and easy to explain.
- Consider edge cases early: empty input, empty string present, no shared first
  character, all strings identical.
- Validate stopping criteria: the first mismatch or length overrun ends the
  prefix exactly before that position.
- Complexity check: With N, L <= 200, O(N*P) is easily within limits and
  memory is constant.
--------------------------------------------------------------------------------
*/

namespace leetcode.Easy
{
    public class LongestCommon
    {
    
        public string LongestCommonPrefix(string[] strs)
        {
            // Edge case: null or empty array -> no prefix.
            if (strs == null || strs.Length == 0) return string.Empty;

            // Use the first string as the reference template for vertical comparison.
            string first = strs[0];

            // If the first string is empty, no common prefix is possible.
            if (first.Length == 0) return string.Empty;

            // For each character position j in the first string...
            for (int j = 0; j < first.Length; j++)
            {
                // Reference character to match in all other strings.
                char c = first[j];

                // Compare against every other string at the same column j.
                for (int i = 1; i < strs.Length; i++)
                {
                    // Mismatch if current string is shorter than j+1 or its j-th char differs.
                    if (j >= strs[i].Length || strs[i][j] != c)
                    {
                        // Longest common prefix ends right before j.
                        return first.Substring(0, j);
                    }
                }
            }

            // If no mismatches were found, the entire first string is the common prefix.
            return first;
        }
    }
}
