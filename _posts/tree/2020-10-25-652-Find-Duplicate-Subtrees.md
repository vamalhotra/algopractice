---
layout: post
title: Find Duplicate Subtrees
preview: Medium  Given the root of a binary tree return all duplicate subtrees  For each kind of duplicate subtrees you only need to
date: 2020-10-25 18:25:47 +0000
categories: tree
lcid: 652
---

# 652. Find Duplicate Subtrees

Medium

Given the `root` of a binary tree, return all **duplicate subtrees**.

For each kind of duplicate subtrees, you only need to return the root node of any **one** of them.

Two trees are **duplicate** if they have the **same structure** with the **same node values**.

 

**Example 1:**

![img](https://assets.leetcode.com/uploads/2020/08/16/e1.jpg)

```
Input: root = [1,2,3,4,null,2,4,null,null,4]
Output: [[2,4],[4]]
```

**Example 2:**

![img](https://assets.leetcode.com/uploads/2020/08/16/e2.jpg)

```
Input: root = [2,1,1]
Output: [[1]]
```

**Example 3:**

![img](https://assets.leetcode.com/uploads/2020/08/16/e33.jpg)

```
Input: root = [2,2,2,3,null,3,null]
Output: [[2,3],[3]]
```

 

**Constraints:**

- The number of the nodes in the tree will be in the range `[1, 10^4]`
- `-200 <= Node.val <= 200`

Accepted

72,150

Submissions

141,407

## Solution:

See related problem [2020-10-25-297-Serialize-and-Deserialize-Binary-Tree.md](2020-10-25-297-Serialize-and-Deserialize-Binary-Tree.md)

Thinking tip: Given two root node pointers, how do you determine if two trees are same or not. Well, we can generate *-order traversal of both tree and see if the two arrays are same or not. 

Approach:

We will serialize every node and store the result in dictionary. Since, we only want to output once for every duplicate tree structure, we will maintain count of how many times we have seen this serialized tree structure before. When count == 2, it means we have seen this serialized version twice, so the tree rooted at current node is exactly same as we encountered before and we can add to results array.



```c#
private Dictionary<String, int> NodesSignature = new Dictionary<string, int>();

private string GetNodeSig(TreeNode root, List<TreeNode> dups)
{
    if(root == null)
    {
        return string.Empty;
    }
    var sigLeft = GetNodeSig(root.left, dups);
    var sigRight = GetNodeSig(root.right, dups);
    var sig = root.val.ToString();
    //if (sigLeft.Length > 0)
    {
        sig += "," + sigLeft;
    }
    //if (sigRight.Length > 0)
    {
        sig += "," + sigRight;
    }
    int val;
    if(!NodesSignature.TryGetValue(sig, out val))
    {
        NodesSignature.Add(sig, 1);
    }
    else
    {
        if(val == 1)
        {
            dups.Add(root);
        }
        NodesSignature[sig] = val + 1;
    }
    return sig;
}

public IList<TreeNode> FindDuplicateSubtrees(TreeNode root)
{
    List<TreeNode> dups = new List<TreeNode>();
    GetNodeSig(root, dups);
    return dups;
}
```
