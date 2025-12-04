# LeetCode Problems - C# Solutions

A well-organized workspace for practicing data structures & algorithms in C# with .NET 8. Each problem includes detailed documentation, complexity analysis, and multiple solution approaches where applicable.

## 📁 Project Structure

```
.
├── easy/                          # Easy difficulty problems
│   ├── 0014-longest-common-prefix.cs
│   ├── 0020-valid-parentheses.cs
│   ├── 0021-merge-two-sorted-lists.cs
│   └── 0094-binary-inorder-dfs.cs
├── medium/                        # Medium difficulty problems
│   ├── 0022-generate-parentheses-backtracking.cs
│   └── 0022-generate-parentheses-dp.cs
├── Program.cs                     # Main entry point with test cases
├── leetcode-problems.csproj       # Project file
└── README.md                      # This file
```

## 🎯 Naming Convention

Files follow the pattern: `{problem-number}-{problem-name}.cs`

- Problem numbers are zero-padded (e.g., `0014`, `0020`)
- Problem names use kebab-case
- Multiple approaches for the same problem are suffixed (e.g., `-backtracking`, `-dp`)

## 📚 Problems Solved

### Easy

| # | Problem | File | Approach |
|---|---------|------|----------|
| 14 | [Longest Common Prefix](https://leetcode.com/problems/longest-common-prefix/) | `0014-longest-common-prefix.cs` | Vertical Scanning |
| 20 | [Valid Parentheses](https://leetcode.com/problems/valid-parentheses/) | `0020-valid-parentheses.cs` | Stack-based |
| 21 | [Merge Two Sorted Lists](https://leetcode.com/problems/merge-two-sorted-lists/) | `0021-merge-two-sorted-lists.cs` | Two-Pointer Merge |
| 94 | [Binary Tree Inorder Traversal](https://leetcode.com/problems/binary-tree-inorder-traversal/) | `0094-binary-inorder-dfs.cs` | Iterative Stack-based |

### Medium

| # | Problem | File | Approach |
|---|---------|------|----------|
| 22 | [Generate Parentheses](https://leetcode.com/problems/generate-parentheses/) | `0022-generate-parentheses-backtracking.cs` | Backtracking/DFS |
| 22 | [Generate Parentheses](https://leetcode.com/problems/generate-parentheses/) | `0022-generate-parentheses-dp.cs` | Dynamic Programming |

## 🚀 Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- Your favorite IDE (Visual Studio, VS Code, Rider, etc.)

### Building and Running

```bash
# Build the project
dotnet build

# Run the program (executes all test cases)
dotnet run
```

### Running Specific Problems

Edit `Program.cs` to comment/uncomment specific problem test functions, or modify the `Main` method to call only the problems you want to test.

## 📝 Adding New Problems

1. **Create a new file** in the appropriate difficulty folder:
   - Format: `{problem-number}-{problem-name}.cs`
   - Example: `0042-trapping-rain-water.cs`

2. **Use the standard template structure**:
   ```csharp
   // ============================================================================
   // Problem: {Number}. {Problem Name} (LeetCode - {Difficulty})
   // Task: {Description}
   //
   // Examples:
   //   Input: ... -> Output: ...
   //
   // Constraints:
   //   ...
   // ============================================================================
   
   /*
   --------------------------------------------------------------------------------
   DATA STRUCTURES
   --------------------------------------------------------------------------------
   ...
   --------------------------------------------------------------------------------
   
   --------------------------------------------------------------------------------
   ALGORITHM
   --------------------------------------------------------------------------------
   ...
   --------------------------------------------------------------------------------
   
   --------------------------------------------------------------------------------
   COMPLEXITY
   --------------------------------------------------------------------------------
   Time Complexity: O(...)
   Space Complexity: O(...)
   --------------------------------------------------------------------------------
   */
   
   namespace leetcode.{Difficulty}
   {
       public class {ProblemName}
       {
           public {ReturnType} Solve({Parameters})
           {
               // Your solution here
           }
       }
   }
   ```

3. **Add test cases** in `Program.cs`:
   ```csharp
   static void Run{ProblemName}()
   {
       Console.WriteLine("--- Problem {Number}: {Problem Name} ---");
       var solver = new {ProblemName}();
       
       // Add your test cases here
   }
   ```

4. **Update this README** with the new problem entry.

## 🏗️ Code Organization

### Namespace Structure

- `leetcode.Easy` - Easy difficulty problems
- `leetcode.Medium` - Medium difficulty problems
- `leetcode.Hard` - Hard difficulty problems (when added)

### Class Naming

- Class names use PascalCase and match the problem name
- Example: `LongestCommonPrefix`, `ValidParentheses`

### Method Naming

- Main solution method: `Solve()`
- Helper methods: descriptive names (e.g., `Dfs()`, `Merge()`)

## 📖 Documentation Standards

Each problem file includes:

1. **Problem Header**: LeetCode problem number, name, difficulty, task description, examples, and constraints
2. **Data Structures**: Explanation of data structures used and their purpose
3. **Algorithm**: Step-by-step explanation of the approach
4. **Complexity Analysis**: Time and space complexity with justification
5. **Thinking Process**: Interview-style narrative explaining the reasoning

## 🧪 Testing

Test cases are included in `Program.cs` for each problem. To add more test cases:

1. Add test data to the appropriate test function
2. Run `dotnet run` to verify correctness
3. Compare outputs with expected LeetCode results

## 📊 Progress Tracking

- ✅ Easy: 4 problems
- ✅ Medium: 1 problem (2 approaches)
- ⬜ Hard: 0 problems

## 🤝 Contributing

When adding new problems:

1. Follow the naming convention
2. Include comprehensive documentation
3. Add test cases to `Program.cs`
4. Update this README
5. Ensure code compiles without warnings

## 📝 Notes

- All solutions are designed to be readable and educational
- Multiple approaches are provided when applicable (e.g., backtracking vs DP)
- Code follows C# best practices and .NET 8 conventions
- Solutions are optimized for clarity first, then performance

## 🔗 Resources

- [LeetCode](https://leetcode.com/)
- [.NET Documentation](https://docs.microsoft.com/dotnet/)
- [C# Language Reference](https://docs.microsoft.com/dotnet/csharp/)

---

Happy coding! 🚀
