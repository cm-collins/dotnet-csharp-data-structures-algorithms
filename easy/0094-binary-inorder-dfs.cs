// ============================================================================
// Problem: 94. Binary Tree Inorder Traversal (LeetCode - Easy)
// Task: Given the root of a binary tree, return the inorder traversal of its
//       nodes' values.
//
// Examples:
//   Input: root = [1,null,2,3]              -> Output: [1,3,2]
//   Input: root = [1,2,3,4,5,null,8,null,null,6,7,9] -> Output: [4,2,6,5,7,1,3,9,8]
//   Input: root = []                         -> Output: []
//   Input: root = [1]                        -> Output: [1]
//
// Constraints:
//   The number of nodes in the tree is in the range [0, 100].
//   -100 <= Node.val <= 100
// ============================================================================

/*
--------------------------------------------------------------------------------
DATA STRUCTURES
--------------------------------------------------------------------------------
- TreeNode: Binary tree node with:
    - int val: the node's value
    - TreeNode? left: reference to left child (null if absent)
    - TreeNode? right: reference to right child (null if absent)

- List<int>: Stores the inorder traversal result. We use IList<int> as the
  return type for generality, but instantiate it as List<int> for efficient
  appending.

- Stack<TreeNode>: LIFO (Last-In-First-Out) data structure used to simulate
  the call stack of recursion. This allows us to implement inorder traversal
  iteratively without recursion.

- TreeNode current: Pointer to track the current node being processed as we
  traverse the tree.

Memory characteristics:
- Stack can grow up to O(h) where h is the height of the tree (worst case O(n)
  for a skewed tree, O(log n) for a balanced tree).
- Result list is O(n) to store all node values.
--------------------------------------------------------------------------------

--------------------------------------------------------------------------------
ALGORITHM (Iterative Inorder Traversal using Stack)
--------------------------------------------------------------------------------
Intuition:
- Inorder traversal visits nodes in the order: left subtree → node → right subtree.
- Recursively, this is: traverse left, visit node, traverse right.
- We simulate this iteratively using a stack to remember nodes we've encountered
  but haven't processed yet.

Steps:
1) Initialize an empty result list and an empty stack. Set current = root.
2) While current is not null OR stack is not empty:
   a) GO LEFT AS FAR AS POSSIBLE:
      - While current is not null:
        * Push current onto the stack (we'll process it after exploring left)
        * Move current to current.left
      - At this point, current is null (we've gone as left as possible)
   
   b) PROCESS NODE FROM STACK:
      - Pop the top node from the stack (this is the leftmost unprocessed node)
      - Add its value to the result (this is the "visit" step)
   
   c) GO RIGHT:
      - Set current = current.right
      - The outer loop will then explore the right subtree (going left as far as
        possible from this right child, repeating the process)

Why this works:
- The stack preserves the order of nodes we've seen but haven't visited yet.
- By going left first, we ensure we process nodes in the correct inorder sequence.
- After visiting a node, we move to its right subtree, which will be processed
  in the same left-first manner.
--------------------------------------------------------------------------------

--------------------------------------------------------------------------------
COMPLEXITY
--------------------------------------------------------------------------------
Let n = number of nodes in the tree.

Time Complexity: O(n)
- Each node is pushed onto the stack exactly once and popped exactly once.
- Each node is visited exactly once to add its value to the result.
- Total operations are linear in the number of nodes.

Space Complexity: O(h) where h is the height of the tree
- The stack stores at most h nodes (the path from root to the deepest leftmost
  node we're currently exploring).
- Worst case: O(n) for a skewed tree (all nodes on one side).
- Best case: O(log n) for a balanced tree.
- Result list is O(n) but is required output, not auxiliary space.
--------------------------------------------------------------------------------

--------------------------------------------------------------------------------
THINKING PROCESS / INTERVIEW NARRATIVE
--------------------------------------------------------------------------------
- Recognize the traversal pattern: inorder means left → node → right.
- Consider approaches:
  * Recursive: Simple but uses O(h) call stack space implicitly.
  * Iterative with stack: Explicit control, same space complexity, avoids
    recursion overhead and potential stack overflow for deep trees.
- Choose iterative for clarity and to demonstrate understanding of the underlying
  mechanism.
- Key insight: Use a stack to defer processing nodes until after their left
  subtrees are fully explored.
- Handle edge cases: empty tree (root is null) is naturally handled by the
  while loop condition.
- Validate the algorithm: trace through examples to ensure correct ordering.
--------------------------------------------------------------------------------
*/

namespace leetcode.Easy
{
    public class TreeNode
    {
        public int val;
        public TreeNode? left;
        public TreeNode? right;
        public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    public class BinaryInorderDfs
    {
        public IList<int> Solve(TreeNode? root)
        {
            var result = new List<int>();
            var stack = new Stack<TreeNode>();
            TreeNode? current = root;

            // Continue as long as:
            // 1. current is not null (we are still exploring nodes), OR
            // 2. the stack is not empty (there are nodes we have visited but not processed yet)
            while (current != null || stack.Count > 0)
            {
                // Go left as far as possible
                // In inorder traversal, we first visit the entire left subtree.
                // This loop keeps going left until there is no more left child.
                while (current != null)
                {
                    // Push the current node onto the stack.
                    // We will come back to it after exploring its left subtree.
                    stack.Push(current);
                    // Move to the left child.
                    current = current.left;
                }

                // At this point, current is null (we went as left as we could).
                // So we pop the last node we pushed onto the stack.
                current = stack.Pop();

                // This is the "visit" step in inorder traversal:
                // left subtree (already done), then the node itself.
                result.Add(current.val);

                // After visiting the node, we now need to visit its right subtree.
                // So we move current to its right child.
                // The outer while loop will then:
                // - go left as far as possible from this right child, and
                // - repeat the process.
                current = current.right;
            }

            return result;
        }
    }
}
