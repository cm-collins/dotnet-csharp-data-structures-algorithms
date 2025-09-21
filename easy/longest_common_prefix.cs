//problem 
// 14. Longest Common Prefix
// Solved
// Easy
// Topics
// premium lock icon
// Companies
// Write a function to find the longest common prefix string amongst an array of strings.

// If there is no common prefix, return an empty string "".



// Example 1:

// Input: strs = ["flower","flow","flight"]
// Output: "fl"
// Example 2:

// Input: strs = ["dog","racecar","car"]
// Output: ""
// Explanation: There is no common prefix among the input strings.


// Constraints:

// 1 <= strs.length <= 200
// 0 <= strs[i].length <= 200
// strs[i] consists of only lowercase English letters if it is non-empty.

// solution  

namespace leetcode.Easy
{
    public class Solution
    {
        public string LongestCommonPrefix(string[] strs)
        {
            if (strs == null || strs.Length == 0) return string.Empty;

            string first = strs[0];
            if (first.Length == 0) return string.Empty;

            for (int j = 0; j < first.Length; j++)
            {
                char c = first[j];
                for (int i = 1; i < strs.Length; i++)
                {
                    // Mismatch if current string is too short or char differs
                    if (j >= strs[i].Length || strs[i][j] != c)
                    {
                        return first.Substring(0, j);
                    }
                }
            }

            // All characters of the first string matched
            return first;
        }
    }



}



