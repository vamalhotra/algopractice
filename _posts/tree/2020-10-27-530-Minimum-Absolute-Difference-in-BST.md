---
layout: post
title: Minimum Absolute Difference In BST
preview: Easy  Given a binary search tree with nonnegative values find the minimum absolute differencehttpsenwikipediaorgwikiAbsolut
date: 2020-10-27 00:06:51 +0000
categories: tree
lcid: 530
---

# 530. Minimum Absolute Difference in BST

Easy

Given a binary search tree with non-negative values, find the minimum [absolute difference](https://en.wikipedia.org/wiki/Absolute_difference) between values of any two nodes.

**Example:**

```
Input:

   1
    \
     3
    /
   2

Output:
1

Explanation:
The minimum absolute difference is 1, which is the difference between 2 and 1 (or between 2 and 3).
```

**Note:**

- There are at least two nodes in this BST.
- This question is the same as 783: https://leetcode.com/problems/minimum-distance-between-bst-nodes/

Accepted

98,236

Submissions

181,194

## Solution:

The concept involved here is that inorder traversal of bst would give sorted array in descending order. And we just need to find two consecutive elements with minimum difference. 

The prev. element is passed by reference and is nullable to indicate null value at start.

```c#
    /**
     * Definition for a binary tree node.
     * public class TreeNode {
         * public int val;
         * public TreeNode left;
         * public TreeNode right;
         * public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
         * this.val = val;
         * this.left = left;
         * this.right = right;
     	* }
     * }
     */
    public class Solution {
        //Inorder traversal - left, root, right
       public void GetMinimumDifferenceHelper(TreeNode root, ref int? prev, ref int minDiff) 
       {
           if(root == null) return;
       //left
        GetMinimumDifferenceHelper(root.left, ref prev, ref minDiff);
        if(prev.HasValue && root.val - prev < minDiff)
        {
            minDiff = root.val - (int)prev;
        }
        prev = root.val; //visit root node
        
        //visit right
        GetMinimumDifferenceHelper(root.right, ref prev, ref minDiff);
     }
 
     public int GetMinimumDifference(TreeNode root) 
     {
         int? prev = null;
         int minDiff = Int32.MaxValue;
         GetMinimumDifferenceHelper(root, ref prev, ref minDiff);
         return minDiff;
     }
 }
```
