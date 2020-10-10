---
layout: post
title: Balanced Binary Tree
date: 2020-09-22 23:12:02 +0000
category: tree
lcid: 110
---

# 110. Balanced Binary Tree

Easy

Given a binary tree, determine if it is height-balanced.

For this problem, a height-balanced binary tree is defined as:

> a binary tree in which the left and right subtrees of *every* node differ in height by no more than 1.

 

**Example 1:**

Given the following tree `[3,9,20,null,null,15,7]`:

```
    3
   / \
  9  20
    /  \
   15   7
```

Return true.

**Example 2:**

Given the following tree `[1,2,2,3,3,null,null,4,4]`:

```
       1
      / \
     2   2
    / \
   3   3
  / \
 4   4
```

Return false.

Accepted

476,929

Submissions

1,089,979

## Solution:

```c#
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
public class Solution {
    
    private int GetHeight(TreeNode node, ref bool isBalanced)
    {
        if(node == null || !isBalanced)
        {
            return 0;
        }
        
        int leftHeight = GetHeight(node.left, ref isBalanced);
        int rightHeight = GetHeight(node.right, ref isBalanced);
        if(Math.Abs(leftHeight - rightHeight) > 1)
        {
            isBalanced = false;
        }
        return Math.Max(leftHeight, rightHeight)+1;
    }
    
    public bool IsBalanced(TreeNode root)     {
        bool isBalanced = true;
        GetHeight(root, ref isBalanced);
        return isBalanced;
    }
}
```