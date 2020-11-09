# 1008. Construct Binary Search Tree from Preorder Traversal

Medium

Return the root node of a binary **search** tree that matches the given `preorder` traversal.

*(Recall that a binary search tree is a binary tree where for every node, any descendant of `node.left` has a value `<` `node.val`, and any descendant of `node.right` has a value `>` `node.val`. Also recall that a preorder traversal displays the value of the `node` first, then traverses `node.left`, then traverses `node.right`.)*

It's guaranteed that for the given test cases there is always possible to find a binary search tree with the given requirements.

**Example 1:**

```
Input: [8,5,1,7,10,12]
Output: [8,5,10,1,7,null,12]
```

 

**Constraints:**

- `1 <= preorder.length <= 100`
- `1 <= preorder[i] <= 10^8`
- The values of `preorder` are distinct.

Accepted

143,036

Submissions

181,907

## Solution:

For binary trees, we need inorder traversal along with either of preorder or postorder traversal. inorder traversal gives us left and right subtree while preorder/postorder gives us crucial root node information.

For BSTs, we can reconstruct tree using just preorder traversal because left subtree has all values < root node value and right subtree has all values > root node value.



```c#
public TreeNode BstFromPreorderHelper(int[] preorder, int start, int end)
{
    if (start >= preorder.Length || end >= preorder.Length)
        return null;
    if (end < start) return null;
    var node = new TreeNode(preorder[start]);
    var lStart = start + 1;
    var lEnd = lStart;
    while (lEnd <= end && preorder[lEnd] < preorder[start]) ++lEnd;
    node.left = BstFromPreorderHelper(preorder, lStart, lEnd - 1);
    node.right = BstFromPreorderHelper(preorder, lEnd, end);
    return node;
}

public TreeNode BstFromPreorder(int[] preorder)
{
    return BstFromPreorderHelper(preorder, 0, preorder.Length - 1);
}
```