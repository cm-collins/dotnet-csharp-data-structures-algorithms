// ============================================================================
// Problem: 21. Merge Two Sorted Lists (LeetCode - Easy)
// Task: Given heads of two sorted singly linked lists (list1, list2), merge them
//       into one sorted list by splicing existing nodes. Return the merged head.
//
// Examples:
//   Input: list1=[1,2,4], list2=[1,3,4]  -> Output: [1,1,2,3,4,4]
//   Input: list1=[], list2=[]            -> Output: []
//   Input: list1=[], list2=[0]           -> Output: [0]
//
// Constraints:
//   - Both lists are individually sorted in non-decreasing order.
//   - Node count: 0..50; values in [-100, 100]
// ============================================================================

/*
--------------------------------------------------------------------------------
DATA STRUCTURE CHOICE
--------------------------------------------------------------------------------
- Singly linked list nodes: ListNode { int val; ListNode next; }
- We only need a couple of pointers:
    - p1: cursor on list1
    - p2: cursor on list2
    - tail: the end of the merged list we’re building
- A dummy (sentinel) node simplifies edge cases (empty lists, first attach).

Memory characteristics:
- No new nodes created; we RE-LINK existing nodes.
- Auxiliary space: O(1).
--------------------------------------------------------------------------------

--------------------------------------------------------------------------------
THINKING PROCESS & ALGORITHM (Two-Pointer Merge)
--------------------------------------------------------------------------------
Intuition:
- Both inputs are already sorted. Like merging in merge sort:
  compare heads, take the smaller node, advance that list’s pointer, and repeat.

Steps:
1) Create a dummy node and set tail = dummy.
2) While p1 != null AND p2 != null:
   - If p1.val <= p2.val: tail.next = p1; p1 = p1.next;
     else:                tail.next = p2; p2 = p2.next;
   - Move tail to tail.next.
3) One list may have remaining nodes; link the remainder to tail.next.
4) Return dummy.next (skips the sentinel itself).

Correctness:
- Always attach the smallest available head, preserving sorted order.
- Each node is attached exactly once; stability is preserved (<=).

Edge cases:
- Either list is null → result is the other list.
- All nodes from one list smaller than the other → simple append at the end.
--------------------------------------------------------------------------------

--------------------------------------------------------------------------------
COMPLEXITY
--------------------------------------------------------------------------------
Let m = length(list1), n = length(list2)

Time Complexity: O(m + n)
- Each node is examined and linked exactly once.

Space Complexity: O(1) auxiliary
- Only a few pointers; no extra node allocations.
--------------------------------------------------------------------------------
*/



namespace leetcode.Easy;
public class ListNode
{
    public int val;
    public ListNode next;
    public ListNode(int val = 0, ListNode  next = null)
    {
        this.val = val;
        this.next = next;
    }
}


public class Solution
{
    public ListNode MergeTwoLists(ListNode list1, ListNode list2)
    {
        // Pointers to traverse the two lists
        ListNode p1 = list1;
        ListNode p2 = list2;

        // Sentinel to simplify head handling
        ListNode dummy = new ListNode(0);
        ListNode tail = dummy;

        // Merge by always taking the smaller current node
        while (p1 != null && p2 != null)
        {
            if (p1.val <= p2.val)
            {
                tail.next = p1;
                p1 = p1.next;
            }
            else
            {
                tail.next = p2;
                p2 = p2.next;
            }
            tail = tail.next; // advance the merged list tail
        }

        // Append any remaining nodes from either list
        tail.next = (p1 != null) ? p1 : p2;

        // The real head is after the dummy
        return dummy.next;
    }
}

/* LeetCode’s typical node definition for reference:
public class ListNode {
    public int val;
    public ListNode next;
    public ListNode(int val=0, ListNode next=null) {
        this.val = val;
        this.next = next;
    }
}
*/
