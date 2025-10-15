using System.Collections.Generic;
public class Solution1
{
    public IList<string> GenerateParenthesis(int n)
    {
        // memo[k] will store the full list G(k) of well-formed strings with k pairs
        var memo = new System.Collections.Generic.Dictionary<int, System.Collections.Generic.IList<string>>();
        memo[0] = new System.Collections.Generic.List<string> { "" }; // Base case: G(0) = { "" }
        return Gen(n, memo); // Compute G(n) using memoized recursion
    }

    private IList<string> Gen(
        int k,
        Dictionary<int,
        IList<string>> memo)
    {
        // If G(k) was computed before, reuse it
        if (memo.TryGetValue(k, out var cached)) return cached;

        var res = new System.Collections.Generic.List<string>();

        // Catalan recurrence:
        // G(k) = ⋃_{i=0..k-1} "(" + G(i) + ")" + G(k-1-i)
        for (int i = 0; i < k; i++)
        {
            var lefts = Gen(i, memo);           // G(i): strings to go inside the first outer "()"
            var rights = Gen(k - 1 - i, memo);  // G(k-1-i): strings to go to the right

            // Cross-product composition of left and right parts
            foreach (var L in lefts)
                foreach (var R in rights)
                    res.Add("(" + L + ")" + R); // Build one valid string for this split i
        }

        memo[k] = res; // Cache G(k) for reuse by larger k
        return res;
    }
}
