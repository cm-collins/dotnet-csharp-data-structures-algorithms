# LeetCode 44: Wildcard Matching - Key Concepts for Learning

## Problem Overview
Given an input string `s` and a pattern `p`, implement wildcard pattern matching with:
- `'?'` matches any single character
- `'*'` matches any sequence of characters (including empty sequence)
- Matching must cover the **entire** input string (not partial)

---

## Core Concepts Needed

### 1. **Dynamic Programming (2D DP)**
This problem is a classic **2D Dynamic Programming** problem where we build a solution table.

**Why DP?**
- We need to check if `s[0..i]` matches `p[0..j]` for all substrings
- These subproblems overlap: matching `s[0..5]` with `p[0..3]` might reuse results from `s[0..4]` with `p[0..3]`
- DP avoids recalculating the same subproblems repeatedly

**DP Table Structure:**
```
dp[i][j] = true if s[0..i-1] matches p[0..j-1]
           false otherwise

Dimensions: (s.Length + 1) × (p.Length + 1)
- Extra row/column for empty string/pattern cases
```

**Key Insight:**
- `dp[0][0]` = true (empty string matches empty pattern)
- `dp[i][0]` = false for i > 0 (non-empty string can't match empty pattern)
- `dp[0][j]` = depends on whether pattern has only `'*'` characters

---

### 2. **State Transitions (The Heart of DP)**

For each position `(i, j)` in the DP table, we need to determine how to fill it based on:
- Current character in string: `s[i-1]`
- Current character in pattern: `p[j-1]`

**Three Cases:**

#### Case 1: Pattern character is `'?'`
```
If p[j-1] == '?'
  → dp[i][j] = dp[i-1][j-1]
  (Matches any single char, so both advance by 1)
```

#### Case 2: Pattern character is `'*'`
```
If p[j-1] == '*'
  → dp[i][j] = dp[i][j-1] OR dp[i-1][j]
  
  Two possibilities:
  a) dp[i][j-1]: '*' matches empty sequence (skip the '*')
  b) dp[i-1][j]: '*' matches one or more characters (use '*' to match s[i-1], keep '*' for more)
```

**Why both options?**
- `dp[i][j-1]`: The `'*'` doesn't match anything (empty match)
- `dp[i-1][j]`: The `'*'` matches `s[i-1]` and can continue matching more

#### Case 3: Pattern character is a regular letter
```
If p[j-1] == regular char
  → dp[i][j] = (s[i-1] == p[j-1]) AND dp[i-1][j-1]
  (Characters must match exactly, then both advance)
```

---

### 3. **Base Cases (Critical for Correctness)**

**Empty String Cases:**
```
dp[0][0] = true
  → Empty string matches empty pattern

dp[i][0] = false for all i > 0
  → Non-empty string cannot match empty pattern

dp[0][j] = depends on pattern
  → Empty string can only match pattern of all '*' characters
  → dp[0][j] = dp[0][j-1] if p[j-1] == '*', else false
```

**Why these matter:**
- Without proper base cases, the DP transitions won't work correctly
- The empty string/pattern cases are the foundation for all other calculations

---

### 4. **Memoization (Alternative to Tabulation)**

Instead of building a 2D table bottom-up, you can use **top-down recursion with memoization**:

**Concept:**
- Start with the full problem: `match(s, 0, p, 0)`
- Recursively check subproblems: `match(s, i, p, j)`
- Cache results in a dictionary/matrix to avoid recomputation

**Advantages:**
- More intuitive recursive thinking
- Only computes needed subproblems (lazy evaluation)
- Same time complexity as tabulation

**Trade-offs:**
- Slight overhead from recursion stack
- May need to handle stack overflow for very large inputs

---

### 5. **Two-Pointer / Greedy Approach (Advanced Optimization)**

**Key Insight:**
- When you encounter `'*'`, you don't need to try all possible matches
- You can use a "greedy" approach: match `'*'` to as few characters as possible, but backtrack if needed

**Concept:**
- Maintain two pointers: `i` for string, `j` for pattern
- When you see `'*'`, remember the position and try matching it to nothing first
- If that fails, backtrack and try matching it to one more character

**Why it works:**
- `'*'` can match any sequence, so you can be greedy
- If matching `'*'` to nothing works, great!
- If not, you can always extend the match incrementally

**Space Optimization:**
- Can reduce space from O(m×n) to O(1) or O(min(m,n))
- More complex to implement correctly

---

### 6. **String Matching Patterns**

This problem belongs to a family of **pattern matching** problems:

**Related Problems:**
- **Regular Expression Matching (LeetCode 10)**: Similar but `'*'` means "zero or more of preceding element"
- **Edit Distance**: Uses similar 2D DP structure
- **Longest Common Subsequence**: Similar DP transitions

**Common Pattern:**
- Both strings processed left-to-right
- DP table tracks "how much of each string has been matched"
- Transitions depend on character comparison and special rules

---

### 7. **Edge Cases to Consider**

**Important edge cases:**
1. **Multiple consecutive `'*'`**: `"***"` is equivalent to `"*"` (can optimize pattern first)
2. **`'*'` at the end**: `"abc"` matches `"a*"` (must match entire string)
3. **Empty string with pattern**: `""` matches `"*"` but not `"a"`
4. **Pattern longer than string**: `"a"` doesn't match `"ab*"` (unless `'*'` can match nothing)
5. **All `'*'` pattern**: `"abc"` matches `"***"`

---

## Algorithmic Thinking Process

### Step 1: Identify the Problem Type
- Pattern matching with special characters → DP or backtracking
- Need to check all possible matches → DP is efficient

### Step 2: Define Subproblems
- "Does `s[0..i]` match `p[0..j]`?" → `dp[i][j]`

### Step 3: Find Recurrence Relations
- How does `dp[i][j]` relate to smaller subproblems?
- Consider each pattern character type separately

### Step 4: Determine Base Cases
- What are the smallest subproblems?
- Empty string/pattern cases

### Step 5: Choose Implementation
- Bottom-up (tabulation): Build table from base cases
- Top-down (memoization): Recursive with caching

---

## Complexity Analysis (What to Expect)

**Time Complexity:**
- O(m × n) where m = s.Length, n = p.Length
- Each cell in DP table computed once
- Each computation is O(1)

**Space Complexity:**
- O(m × n) for full DP table
- Can optimize to O(min(m, n)) with space optimization
- O(m × n) for memoization (recursion stack + cache)

---

## Key Takeaways for Learning

1. **DP is about building solutions from smaller subproblems**
   - Each cell represents a subproblem
   - Transitions show how subproblems relate

2. **Special characters need special handling**
   - `'?'` is straightforward (match one, advance both)
   - `'*'` is complex (can match 0 or more, requires OR logic)

3. **Base cases are critical**
   - Empty string/pattern cases are the foundation
   - Get these wrong, and everything else fails

4. **State transitions capture the logic**
   - The recurrence relation IS the algorithm
   - Understanding transitions = understanding the solution

5. **Optimization comes after correctness**
   - First get the O(m×n) solution working
   - Then optimize space if needed

---

## Practice Questions to Build Understanding

Before tackling this problem, make sure you understand:
1. **2D DP basics**: Longest Common Subsequence, Edit Distance
2. **String matching**: Basic string comparison, substring matching
3. **Recursion with memoization**: Fibonacci with memo, climbing stairs
4. **Greedy algorithms**: When to be greedy vs. when to try all options

---

## Next Steps

Once you understand these concepts:
1. Try implementing the 2D DP solution
2. Trace through examples manually with the DP table
3. Consider space optimization
4. Compare with memoization approach
5. Explore the two-pointer greedy solution

