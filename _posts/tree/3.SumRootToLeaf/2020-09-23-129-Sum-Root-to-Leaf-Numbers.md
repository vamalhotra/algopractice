---
layout: post
title: Sum Root To Leaf Numbers
preview: Medium  Given a binary tree containing digits from 09 only each roottoleaf path could represent a number  An example is the ro
date: 2020-09-23 00:37:33 +0000
categories: tree
lcid: 129
---

# 129. Sum Root to Leaf Numbers

Medium

Given a binary tree containing digits from `0-9` only, each root-to-leaf path could represent a number.

An example is the root-to-leaf path `1->2->3` which represents the number `123`.

Find the total sum of all root-to-leaf numbers.

**Note:** A leaf is a node with no children.

**Example:**

```
Input: [1,2,3]
    1
   / \
  2   3
Output: 25
Explanation:
The root-to-leaf path 1->2 represents the number 12.
The root-to-leaf path 1->3 represents the number 13.
Therefore, sum = 12 + 13 = 25.
```

**Example 2:**

```
Input: [4,9,0,5,1]
    4
   / \
  9   0
 / \
5   1
Output: 1026
Explanation:
The root-to-leaf path 4->9->5 represents the number 495.
The root-to-leaf path 4->9->1 represents the number 491.
The root-to-leaf path 4->0 represents the number 40.
Therefore, sum = 495 + 491 + 40 = 1026.
```

Accepted

290,396

Submissions

585,895

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
public class Solution 
{
    public int SumNumbers(TreeNode root, int parentSum)
    {
        if(root == null)
        {
            return 0;
        }
      if(root.left == null && root.right == null) //is leaf node
      {
          return parentSum*10 + root.val;
      }
      int leftSum = SumNumbers(root.left, parentSum*10 + root.val);
      int rightSum = SumNumbers(root.right, parentSum*10 + root.val);
        return leftSum + rightSum;
    }
    
    public int SumNumbers(TreeNode root)     {
        int totalSum = 0;
        return SumNumbers(root, 0);
    }
}
```