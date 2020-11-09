---
layout: post
title: Serialize And Deserialize Binary Tree
preview: Hard  Serialization is the process of converting a data structure or object into a sequence of bits so that it can be stored in a file
date: 2020-10-25 18:15:41 +0000
categories: tree
lcid: 297
---

# 297. Serialize and Deserialize Binary Tree

Hard

Serialization is the process of converting a data structure or object into a sequence of bits so that it can be stored in a file or memory buffer, or transmitted across a network connection link to be reconstructed later in the same or another computer environment.

Design an algorithm to serialize and deserialize a binary tree. There is no restriction on how your serialization/deserialization algorithm should work. You just need to ensure that a binary tree can be serialized to a string and this string can be deserialized to the original tree structure.

**Clarification:** The input/output format is the same as [how LeetCode serializes a binary tree](https://leetcode.com/faq/#binary-tree). You do not necessarily need to follow this format, so please be creative and come up with different approaches yourself.

 

**Example 1:**

![img](https://assets.leetcode.com/uploads/2020/09/15/serdeser.jpg)

```
Input: root = [1,2,3,null,null,4,5]
Output: [1,2,3,null,null,4,5]
```

**Example 2:**

```
Input: root = []
Output: []
```

**Example 3:**

```
Input: root = [1]
Output: [1]
```

**Example 4:**

```
Input: root = [1,2]
Output: [1,2]
```

 

**Constraints:**

- The number of nodes in the tree is in the range `[0, 104]`.
- `-1000 <= Node.val <= 1000`

Accepted

371,191

Submissions

765,419

## Solution:

There are multiple ways of solving this. 

1) Doing inorder traversal combined with either of pre-order or post-order traversal. Both these traversals can then be used to reconstruct the tree.

2) Store null pointers also and just do pre-order traversal of the tree. The resulting string is level order traversal as a single array without any level boundaries. 

```c#
public string Serialize(TreeNode root)
{
    if (root == null)
    {
        return string.Empty;
    }
    var sigLeft = Serialize(root.left);
    var sigRight = Serialize(root.right);
    var sig = root.val.ToString();
    sig += "," + sigLeft;
    sig += "," + sigRight;
    return sig;
}

//We are keeping cur as pointer to current position in string 
//so that we do not have to create new string objects
public TreeNode Deserialize(string strTree, ref int cur)
{
    if (strTree.Length == 0 || strTree.Length <= cur)
    {
        return null;
    }
    var ix = strTree.IndexOf(',', cur);
    //null pointer is stored as empty string i.e. 
    //9,, represent single node tree with null left and right subtree
    if(ix == cur) 
    {
        cur = ix + 1;
        return null;
    }
    var node = new TreeNode(Int32.Parse(strTree.Substring(cur, ix-cur)));
    cur = ix + 1;
    node.left = Deserialize(strTree, ref cur);
    node.right = Deserialize(strTree, ref cur);
    return node;
}

```

Can we do better if we are given a BST instead of binary tree?

# 449. Serialize and Deserialize BST

Medium

Serialization is converting a data structure or object into a sequence of bits so that it can be stored in a file or memory buffer, or transmitted across a network connection link to be reconstructed later in the same or another computer environment.

Design an algorithm to serialize and deserialize a **binary search tree**. There is no restriction on how your serialization/deserialization algorithm should work. You need to ensure that a binary search tree can be serialized to a string, and this string can be deserialized to the original tree structure.

**The encoded string should be as compact as possible.**

 

**Example 1:**

```
Input: root = [2,1,3]
Output: [2,1,3]
```

**Example 2:**

```
Input: root = []
Output: []
```

 

**Constraints:**

- The number of nodes in the tree is in the range `[0, 104]`.
- `0 <= Node.val <= 104`
- The input tree is **guaranteed** to be a binary search tree.

Accepted

133,962

Submissions

250,741

## Solution:

We don't need to store null values. Any value less than root will go to left and any value greater than root will go to right.

```c#
public class Codec
{
    // Encodes a tree to a single string.
    public String serialize(TreeNode root)
    {
        StringBuilder sb = new StringBuilder();
        dfs(root, sb);
        return sb.ToString();
    }
    private void dfs(TreeNode root, StringBuilder sb)
    {
        if (root == null)
        {
            return;
        }
        sb.Append(root.val + ",");
        dfs(root.left, sb);
        dfs(root.right, sb);
        return;
    }
    
    // Decodes your encoded data to tree.
    public TreeNode deserialize(String data)
    {
        String[] arr = data.Split(',');
        TreeNode root = null;
        foreach (String s in arr)
        {
            if (s.Length > 0)
            {
                root = buildBST(root, Int32.Parse(s));
            }
        }
        return root;
    }

    public TreeNode buildBST(TreeNode root, int v)
    {
        if (root == null) return new TreeNode(v);
        if (v < root.val)
        {
            root.left = buildBST(root.left, v);
        }
        else
        {
            root.right = buildBST(root.right, v);
        }
        return root;
    }
}
```