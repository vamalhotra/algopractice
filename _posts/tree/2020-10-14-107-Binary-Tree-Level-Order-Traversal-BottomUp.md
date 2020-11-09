---
layout: post
title: Binary Tree Level Order Traversal II
preview: Easy  Given a binary tree return the bottomup level order traversal of its nodes values ie from left to right level by level f
date: 2020-10-14 11:32:39 +0000
categories: tree
lcid: 107
---

# 107. Binary Tree Level Order Traversal II

Easy

Given a binary tree, return the *bottom-up level order* traversal of its nodes' values. (ie, from left to right, level by level from leaf to root).

For example:
Given binary tree `[3,9,20,null,null,15,7]`,

```
    3
   / \
  9  20
    /  \
   15   7
```



return its bottom-up level order traversal as:

```
[
  [15,7],
  [9,20],
  [3]
]
```



Accepted

377,248

Submissions

696,746

# Solution:

Somehow, LC marks this problem as easy. This is almost same as level order traversal which is marked as medium and you can just reverse the final level order traversal list to get the solution accepted. 

IMO, it requires touch of genius to do minor modification to level order traversal solution and eliminate the need to reverse the list at the end. 

You can come up with this modification only if you realize that DFS solution to level order traversal did pre-order traversal and converting it to post order traversal and inserting list at 0-th position instead of append will solve this problem. 

In BFS solution to level order solution, one need to just insert list at 0-th position instead of append. 

Both these 'touch of genius' will eliminate the need for reversing list at the end. See commented out lines and newly added lines in below code. 

```c#
public class TreeNode 
{
	public int val;
	public TreeNode left;
	public TreeNode right;
	public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
		this.val = val;
		this.left = left;
		this.right = right;
	}
}

public class Solution
{
    public IList<IList<int>> LevelOrderBottomBFS(TreeNode root)
    {
        var allLevels = new List<IList<int>>();
        if (root == null)
        {
            return allLevels;
        }
        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        while (queue.Count > 0)
        {
            var curLevel = new List<int>();

            //Element currently in list belong to current level
            //All new elements found will be in next level
            int levelCount = queue.Count;
            for (int i = 0; i < levelCount; ++i)
            {
                var ele = queue.Dequeue();
                if (ele != null)
                {
                    queue.Enqueue(ele.left);
                    queue.Enqueue(ele.right);
                    curLevel.Add(ele.val);
                }
            }
            if (curLevel.Count > 0)
            {
                //Remove this line from regular level order
                //allLevels.Add(curLevel);

                //Add this line
                allLevels.Insert(0, curLevel);
            }
        }
        return allLevels;
    }

    public void LevelOrderBottomDFSHelper(TreeNode root, List<IList<int>> allLevels, int level)
    {
        if (root == null) return;
        if (allLevels.Count == level)
        {
            //comment this line from level order traversal
            //allLevels.Add(new List<int>());

            //Add this line
            allLevels.Insert(0, new List<int>());
        }
        //comment this line from level order traversal
        //allLevels[level].Add(root.val);
        LevelOrderBottomDFSHelper(root.left, allLevels, level + 1);
        LevelOrderBottomDFSHelper(root.right, allLevels, level + 1);

        //Add this line
        allLevels[allLevels.Count-level-1].Add(root.val);
    }

    public IList<IList<int>> LevelOrderBottomDFS(TreeNode root)
    {
        var allLevels = new List<IList<int>>();
        LevelOrderBottomDFSHelper(root, allLevels, 0);
        return allLevels;
    }
}
```

