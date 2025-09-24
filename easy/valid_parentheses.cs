// ============================================================================
// Problem: 20. Valid Parentheses (LeetCode - Easy)
// Task: Given a string s containing only '(', ')', '{', '}', '[' and ']', determine
//       if the input string is valid.
// A string is valid if:
//   - Open brackets must be closed by the same type of brackets.
//   - Open brackets must be closed in the correct order (LIFO).
//   - Every closing bracket has a corresponding opening bracket of the same type.
//
// Examples:
//   Input: "()"       -> Output: true
//   Input: "()[]{}"   -> Output: true
//   Input: "(]"       -> Output: false
//   Input: "([])"     -> Output: true
//   Input: "([)]"     -> Output: false
//
// Constraints:
//   1 <= s.Length <= 10^4
//   s consists of parentheses only: '()[]{}'
// ============================================================================

/*
--------------------------------------------------------------------------------
DATA STRUCTURES
--------------------------------------------------------------------------------
- Stack<char>:
    - Models the nesting/ordering constraint (LIFO): the last opening bracket
      encountered must be the first one to be matched/closed.
    - O(1) push/pop and top operations.

- Dictionary<char, char> (closing -> opening):
    - Enables O(1) expected-time checks that a closing bracket matches the
      correct opening type:
        ')' -> '('
        ']' -> '['
        '}' -> '{'

Memory characteristics: O(n) in the worst case when the string starts with many
openings (e.g., "((([[{{"). No other collections are used.
--------------------------------------------------------------------------------

--------------------------------------------------------------------------------
ALGORITHM (Stack-based validation)
--------------------------------------------------------------------------------
Intuition:
- The validity rules imply a Last-In-First-Out matching between openers and
  closers. A stack naturally enforces this order.

Steps:
1) Initialize a dictionary from closing to opening and an empty stack.
2) Scan characters left-to-right:
   - If ch is an opening bracket, push it on the stack.
   - Else ch is a closing bracket:
       * If the stack is empty -> invalid (no opener to match).
       * Pop the top. If popped != dictionary[ch] -> invalid (wrong type/order).
3) After processing all characters:
   - Valid iff the stack is empty (no unclosed openings remain).

Why this works:
- Every closing bracket is matched against the most recent unmatched opening
  bracket; mismatches or premature closings are detected immediately, and any
  leftover openings invalidate the string at the end.
--------------------------------------------------------------------------------

--------------------------------------------------------------------------------
COMPLEXITY
--------------------------------------------------------------------------------
Let n = s.Length.

Time Complexity: O(n)
- Each character is processed exactly once; each opening is pushed once and
  popped at most once. Dictionary lookups are O(1) expected.

Space Complexity: O(n)
- In the worst case (all openings before any closings), the stack grows to n.
--------------------------------------------------------------------------------

--------------------------------------------------------------------------------
THINKING PROCESS / INTERVIEW NARRATIVE
--------------------------------------------------------------------------------
- Recognize the LIFO constraint from "correct order" -> choose a Stack.
- Need constant-time type checking -> small Dictionary (closing -> opening).
- Fail fast on:
    * Closing with empty stack (no opener),
    * Type/order mismatch when comparing top of stack to required opener.
- Ensure completeness by requiring the stack be empty at the end.
--------------------------------------------------------------------------------
*/

namespace leetcode.Easy
{
    public class Solution
    {
        public bool IsValid(string s)
        {
            var match = new Dictionary<char, char>
            {
                { ')', '(' },
                { ']', '[' },
                { '}', '{' }
            };

            var stack = new Stack<char>();

            foreach (char ch in s)
            {
                if (ch == '(' || ch == '[' || ch == '{')
                {
                    stack.Push(ch);
                }
                else
                {
                    if (stack.Count == 0) return false;

                    char top = stack.Pop();
                    if (top != match[ch]) return false;
                }
            }

            return stack.Count == 0;
        }
    }
}
