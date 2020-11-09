---
layout: post
title: Construct Binary Tree From Preorder And Inorder Traversal
preview: Medium  Given preorder and inorder traversal of a tree construct the binary tree  Note You may assume that duplicates do not
date: 2020-10-15 23:53:22 +0000
categories: tree
lcid: 105
---

# 105. Construct Binary Tree from Preorder and Inorder Traversal

Medium

Given preorder and inorder traversal of a tree, construct the binary tree.

**Note:**
You may assume that duplicates do not exist in the tree.

For example, given

```
preorder = [3,9,20,15,7]
inorder = [9,3,15,20,7]
```

Return the following binary tree:

```
    3
   / \
  9  20
    /  \
   15   7
```

Accepted

405,895

Submissions

812,093

# 106. Construct Binary Tree from Inorder and Postorder Traversal

Medium

211739Add to ListShare

Given inorder and postorder traversal of a tree, construct the binary tree.

**Note:**
You may assume that duplicates do not exist in the tree.

For example, given

```
inorder = [9,3,15,20,7]
postorder = [9,15,7,20,3]
```

Return the following binary tree:

```
    3
   / \
  9  20
    /  \
   15   7
```

Accepted

258,477

Submissions

536,827

## Solution

I have merged both questions because they are similar. You can construct binary tree given inorder traversal and either preorder or post-order traversal.

Remember, 

Inorder Traversal: Left -> root -> right

Preorder: root -> Left -> right

Post order: left -> right -> root

### Idea

- In case of preorder, first element will give the root and in case of postorder, last element will give the root. 
- Inorder will help partition the array into left and right and also give the count of elements in each partition. 
- We can use that count to partition preorder or postorder array and recurse to create binary tree rooted at left node and binary tree rooted at right node. 

```c#
public class TreeNode
{
	public int val;
	public TreeNode left;
	public TreeNode right;
	public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
	{
		this.val = val;
		this.left = left;
		this.right = right;
	}

	public static TreeNode BuildTreePreorder(int[] preorder, int[] inorder)
	{
		if (preorder.Length == 0 || inorder.Length == 0) return null;
        
        var rootVal = preorder.First();
		var root = new TreeNode(val: rootVal);
		var rootIdx = Array.IndexOf(inorder, rootVal);

		root.left = rootIdx == 0 ? null : BuildTreePreorder(
			inorder.Take(rootIdx).ToArray(), 
			preorder.Skip(1).Take(rootIdx).ToArray());

		root.right = (rootIdx == inorder.Length-1) ? null : BuildTreePreorder(
			inorder.Skip(rootIdx + 1).ToArray(), 
			preorder.Skip(rootIdx+1).ToArray());

		return root;
	}

	public static TreeNode BuildTreePostorder(int[] inorder, int[] postorder)
	{
		if (postorder.Length == 0 || inorder.Length == 0) return null;

		var rootVal = postorder.Last();
		var root = new TreeNode(val: rootVal);
		var rootIdx = Array.IndexOf(inorder, rootVal);

		root.left = rootIdx == 0 ? null : BuildTreePostorder(
			inorder.Take(rootIdx).ToArray(),
			postorder.Take(rootIdx).ToArray());

		root.right = (rootIdx == inorder.Length - 1) ? null : BuildTreePostorder(
			inorder.Skip(rootIdx + 1).ToArray(),
			postorder.Skip(rootIdx).Take(postorder.Length-1-rootIdx).ToArray());

		return root;
	}
}
```