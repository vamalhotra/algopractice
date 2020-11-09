# 102. Binary Tree Level Order Traversal

Medium

Given a binary tree, return the *level order* traversal of its nodes' values. (ie, from left to right, level by level).

For example:
Given binary tree `[3,9,20,null,null,15,7]`,

```
    3
   / \
  9  20
    /  \
   15   7
```

return its level order traversal as:

```
[
  [3],
  [9,20],
  [15,7]
]
```



Accepted

686,850

Submissions

1,240,710

## Solution:

This is seemingly innocent question but it can be little tricky if you're out of practice on similar problems. 

Just using a queue and adding elements in the order in which we encounter them will give us level order traversal. The complexity comes on how to determine end of current level and start of next level. Looking at binary tree, it's quite visually obvious but we need to track a level's start and end in code. 

We can solve it using BFS or DFS. For solving via BFS, we will use a queue. There are two ways to determine level. 

1) Use a separator

- Separator is the invisible last element of each level.
- Add root node and separator. 
- Every time we pop the separator, we insert it back to queue. It marks the end of current level.
- Take care of infinite loop and to not push it if queue is empty i.e. if we have already reached the end of last level. 
- Separator can be null but we need to take care not to insert null child nodes

2) Use count of elements at each level. 

- Process elements equal to this count. Any new element found while processing this level naturally belong to the next level. 
- This needs use of two loops instead of single loop as in 1). However, this gives neat solution and do not need special care of 'separator' element

3) Using DFS

- DFS, by definition, will go deeper in one direction and traverse all levels in one path before moving to next path. The idea is to keep track of level of node and add it at right row in our 2-d array. 

```c#
//Definition for a binary tree node.
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
    public IList<IList<int>> LevelOrderQueueUsingSeparator(TreeNode root)
    {
        var allLevels = new List<IList<int>>();
        if(root == null)
        {
            return allLevels;
        }

        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        queue.Enqueue(null); //separator null
        var curLevel = new List<int>();

        while(queue.Count > 0)
        {
            var top = queue.Dequeue();
            if(top == null)
            {
                //If we come across separator, we've reached end of current level and add it to queue again.
                if (queue.Count > 0)//do not add if at end of last level and queue is empty
                {
                    queue.Enqueue(null);
                }
                allLevels.Add(curLevel);

                //create new list. list is stored by reference. 
                //If you do curLevel.clear(), it will clear the reference 
                //added to allLevels
                curLevel = new List<int>();
                continue;
            }
            curLevel.Add(top.val);

            //since our separator is null, we should not push null child nodes
            if (top.left != null)
            {
                queue.Enqueue(top.left);
            }

            if (top.right != null)
            {
                queue.Enqueue(top.right);
            }
        }
        return allLevels;
    }

    public IList<IList<int>> LevelOrderQueueTwoLoops(TreeNode root)
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
            //We do not need any extra separator here
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
                allLevels.Add(curLevel);
            }
        }
        return allLevels;
    }
	
	public void LevelOrderDFSHelper(TreeNode root, List<IList<int>> allLevels, int level)
	{
		if (root == null) return;
		if (allLevels.Count == level)
		{
			allLevels.Add(new List<int>());
		}
		allLevels[level].Add(root.val);
		LevelOrderDFSHelper(root.left, allLevels, level + 1);
		LevelOrderDFSHelper(root.right, allLevels, level + 1);
	}

	public IList<IList<int>> LevelOrderDFS(TreeNode root)
	{
		var allLevels = new List<IList<int>>();
		LevelOrderDFSHelper(root, allLevels, 0);
		return allLevels;
	} 
}
```

Variations:

1) Print levels bottom to top

2) Print levels right to left

3) 

