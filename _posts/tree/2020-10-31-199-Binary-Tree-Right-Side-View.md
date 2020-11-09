# 199. Binary Tree Right Side View

Medium

Given a binary tree, imagine yourself standing on the *right* side of it, return the values of the nodes you can see ordered from top to bottom.

**Example:**

```
Input: [1,2,3,null,5,null,4]
Output: [1, 3, 4]
Explanation:

   1            <---
 /   \
2     3         <---
 \     \
  5     4       <---
```

Accepted

347,161

Submissions

629,731

## Solution:



```c#
public IList<int> RightSideView(TreeNode root)
{
    var result = new List<int>();
    RightViewHelper(root, result, 0);
    return result;
}

public void RightViewHelper(TreeNode curr, List<int> result, int currDepth)
{
    if (curr == null)
    {
        return;
    }

    //start of new level, the first element is the rightmost element sinc we traverse right side first
    if (currDepth == result.Count)
    {
        result.Add(curr.val);
    }

    RightViewHelper(curr.right, result, currDepth + 1);
    RightViewHelper(curr.left, result, currDepth + 1);
}
```