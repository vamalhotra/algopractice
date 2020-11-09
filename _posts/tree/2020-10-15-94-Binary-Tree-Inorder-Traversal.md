# 94. Binary Tree Inorder Traversal

Medium

Given the `root` of a binary tree, return *the inorder traversal of its nodes' values*.

**Example 1:**

![img](https://assets.leetcode.com/uploads/2020/09/15/inorder_1.jpg)

```
Input: root = [1,null,2,3]
Output: [1,3,2]
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

![img](https://assets.leetcode.com/uploads/2020/09/15/inorder_5.jpg)

```
Input: root = [1,2]
Output: [2,1]
```

**Example 5:**

![img](https://assets.leetcode.com/uploads/2020/09/15/inorder_4.jpg)

```
Input: root = [1,null,2]
Output: [1,2]
```

**Constraints:**

- The number of nodes in the tree is in the range `[0, 100]`.
- `-100 <= Node.val <= 100`

 

**Follow up:**

Recursive solution is trivial, could you do it iteratively?

Accepted

822,215

Submissions

1,277,054

## Solution

This is a very good example of why we need practice. Skills alone are not sufficient as these seemingly trivial problem can confuse when implementing on whiteboard in a stressful situation like interview. While the recursive solution is widely popular and familiar to most, the iterative solution is trickier to implement. That's why this problem is marked as medium-hard instead of easy. The only reason why it is not hard is because approach is quite clear to everyone and the trap is in implementation only. Hard problems usually need optimal approach.

------

### Approach 1: Recursive Approach

The first method to solve this problem is using recursion. This is the classical method and is straightforward. We can define a helper function to implement recursion.

```c#
public void InorderTraversalHelper(TreeNode root, List<int> nodes)
{
	InorderTraversalHelper(root.left);
	nodes.Add(root.val);
	InorderTraversalHelper(root.right);
}

public IList<int> InorderTraversal(TreeNode root)
{
	InorderTraversal(root.left);

	InorderTraversal(root.right);
}
```
**Complexity Analysis**

- Time complexity : O(n)*O*(*n*). The time complexity is O(n)*O*(*n*) because the recursive function is 
  - T(n) = 2 . T(n/2) + 1
  - *T*(*n*)=2 ⋅ *T*(*n*/2) + 1
- Space complexity : The worst case space required is O(n), and in the average case it's O*(log* n) where n is number of nodes.

### Approach 2: Iterative approach

```c#
public class Solution
{
	public void InorderTraversalHelper(TreeNode root, List<int> nodes)
	{
		InorderTraversalHelper(root.left);
		nodes.Add(root.val);
		InorderTraversalHelper(root.right);
	}

	public IList<int> InorderTraversal(TreeNode root)
	{
		InorderTraversal(root.left);

		InorderTraversal(root.right);
	}

	/// <summary>
	/// This is incorrect approach to inorder traversal.
	/// Use it to check your debugging skills and see why it's broken
	/// </summary>
	public IList<int> InorderTraversalIterativeWrongTry1(TreeNode root)
	{
		var nodes = new List<int>();
		if(root == null)
		{
			return nodes;
		}
		
		var stack = new Stack<TreeNode>();
		stack.Push(root);

		while(stack.Count > 0)
		{
			//we peek a node (say X), add all its left children and finally pop the
			//most recent left child.
			//the problem is at some point we are again going to peek the same node X
			//and repeat adding all its left children and end up with a infinite loop
			//Study carefully this wrong and correct solution and see clear thinking
			//is important while implementing it.
			var top = stack.Peek();
			while(top.left != null)
			{
				stack.Push(top.left);
				top = top.left;
			}
			nodes.Add(top.val);
			stack.Pop();
			if(top.right != null)
			{
				stack.Push(top.right);
			}
		}
		return nodes;
	}

	/// <summary>
	/// This program teaches you the basics of pointer movement in binary tree. Do not underestimate this problem and approach even though it's the most basic textbook question on trees.
	/// </summary>
	public IList<int> InorderTraversalIterative(TreeNode root)
	{
		var nodes = new List<int>();
		if (root == null)
		{
			return nodes;
		}

		var res = new List<int>();
		var stack = new Stack<TreeNode>();
		TreeNode curr = root;
		while (curr != null || stack.Count > 0)
		{
			//move left as much as you can and keep pushing all nodes to stack
			while (curr != null)
			{
				stack.Push(curr);
				curr = curr.left;
			}

			//get leaf leftmost node of binary tree rooted at actual 'curr' before start of move-left loop
			curr = stack.Pop();
			res.Add(curr.val);

			//Repeat for the binary tree rooted at right node
			curr = curr.right;
		}
		return res;
	}
}
```
**Complexity Analysis**

- Time complexity : O(n).
- Space complexity : O(n).

### Approach 3: Morris Method

Once upon a time, a guy called Morris really struggled long and hard with this and came up with solution that breaks the original tree. Didn't we also want to do same thing when stuck with infinite loop? The good part is he also published his approach on Wikipedia.

In this method, we have to use a new data structure-Threaded Binary Tree, and the strategy is as follows:

> Step 1: Initialize current as root
>
> Step 2: While current is not NULL,
>
> ```
> If current does not have left child
> 
>     a. Add current’s value
> 
>     b. Go to the right, i.e., current = current.right
> 
> Else
> 
>     a. In current's left subtree, make current the right child of the rightmost node
> 
>     b. Go to this left child, i.e., current = current.left
> ```

For example:

```
          1
        /   \
       2     3
      / \   /
     4   5 6
```

First, 1 is the root, so initialize 1 as current, 1 has left child which is 2, the current's left subtree is

```
         2
        / \
       4   5
```

So in this subtree, the rightmost node is 5, then make the current(1) as the right child of 5. Set current = cuurent.left (current = 2). The tree now looks like:

```
         2
        / \
       4   5
            \
             1
              \
               3
              /
             6
```

For current 2, which has left child 4, we can continue with thesame process as we did above

```
        4
         \
          2
           \
            5
             \
              1
               \
                3
               /
              6
```

then add 4 because it has no left child, then add 2, 5, 1, 3 one by one, for node 3 which has left child 6, do the same as above. Finally, the inorder taversal is [4,2,5,1,6,3].

For more details, please check [Threaded binary tree](https://en.wikipedia.org/wiki/Threaded_binary_tree) and [Explaination of Morris Method](https://stackoverflow.com/questions/5502916/explain-morris-inorder-tree-traversal-without-using-stacks-or-recursion)

Java solution at leetcode

```java
class Solution {
    public List < Integer > inorderTraversal(TreeNode root) {
        List < Integer > res = new ArrayList < > ();
        TreeNode curr = root;
        TreeNode pre;
        while (curr != null) {
            if (curr.left == null) {
                res.add(curr.val);
                curr = curr.right; // move to next right node
            } else { // has a left subtree
                pre = curr.left;
                while (pre.right != null) { // find rightmost
                    pre = pre.right;
                }
                pre.right = curr; // put cur after the pre node
                TreeNode temp = curr; // store cur node
                curr = curr.left; // move cur to the top of the new tree
                temp.left = null; // original cur left be null, avoid infinite loops
            }
        }
        return res;
    }
}
```

**Complexity Analysis**

- Time complexity : O*(*n*). To prove that the time complexity is O(n)*, the biggest problem lies in finding the time complexity of finding the predecessor nodes of all the nodes in the binary tree. Intuitively, the complexity is O*(*n*log*n*), because to find the predecessor node for a single node related to the height of the tree. But in fact, finding the predecessor nodes for all nodes only needs O(n)* time. Because a binary Tree with n nodes has n-1 edges, the whole processing for each edges up to 2 times, one is to locate a node, and the other is to find the predecessor node. So the complexity is O*(*n).
- Space complexity : O(n). Arraylist of size n is used.