# 508. Most Frequent Subtree Sum

Medium

Given the root of a tree, you are asked to find the most frequent subtree sum. The subtree sum of a node is defined as the sum of all the node values formed by the subtree rooted at that node (including the node itself). So what is the most frequent subtree sum value? If there is a tie, return all the values with the highest frequency in any order.

**Examples 1**
Input:

```
  5
 /  \
2   -3
```

return [2, -3, 4], since all the values happen only once, return all of them in any order.



**Examples 2**
Input:

```
  5
 /  \
2   -5
```

return [2], since 2 happens twice, however -5 only occur once.



**Note:** You may assume the sum of values in any subtree is in the range of 32-bit signed integer.

Accepted

76,683

Submissions

130,925

Reference: https://leetcode.com/problems/most-frequent-subtree-sum/

## Solution:

Do dfs post-order traversal, calculate sum of left subtree and right subtree and store it in a dict

```c#
    private Dictionary<int, int> SumDict = new Dictionary<int, int>();
    
    public int SumHelper(TreeNode root)
    {
        if (root == null)
        {
            return 0;
        }
        var subtreeSumLeft = SumHelper(root.left);
        var subtreeSumRight = SumHelper(root.right);
        int sum = root.val + subtreeSumLeft + subtreeSumRight;
        if (SumDict.ContainsKey(sum))
        {
            SumDict[sum] = SumDict[sum] + 1;
        }
        else
        {
            SumDict.Add(sum, 1);
        }
        return sum;
    }

    public int[] FindFrequentTreeSum(TreeNode root)
    {
        if(root == null)
        {
            return new int[] { };
        }

        var dict = SumDict.OrderByDescending(x => x.Value).ToList();
        int freq = dict.First().Value;
        return dict.TakeWhile(x => x.Value == freq).Select(x => x.Key).ToArray();
    }
```
