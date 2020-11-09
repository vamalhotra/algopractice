---
layout: post
title: Lowest Common Ancestor Of A Binary Search Tree
preview: Easy  Given a binary search tree BST find the lowest common ancestor LCA of two given nodes in the BST  According to the defin
date: 2020-11-09 19:39:25 +0000
categories: tree
lcid: 235
---

# 235. Lowest Common Ancestor of a Binary Search Tree

Easy

Given a binary search tree (BST), find the lowest common ancestor (LCA) of two given nodes in the BST.

According to the [definition of LCA on Wikipedia](https://en.wikipedia.org/wiki/Lowest_common_ancestor): “The lowest common ancestor is defined between two nodes p and q as the lowest node in T that has both p and q as descendants (where we allow **a node to be a descendant of itself**).”

**Example 1:**

![img](https://assets.leetcode.com/uploads/2018/12/14/binarysearchtree_improved.png)

```
Input: root = [6,2,8,0,4,7,9,null,null,3,5], p = 2, q = 8
Output: 6
Explanation: The LCA of nodes 2 and 8 is 6.
```

**Example 2:**

![img](https://assets.leetcode.com/uploads/2018/12/14/binarysearchtree_improved.png)

```
Input: root = [6,2,8,0,4,7,9,null,null,3,5], p = 2, q = 4
Output: 2
Explanation: The LCA of nodes 2 and 4 is 2, since a node can be a descendant of itself according to the LCA definition.
```

**Example 3:**

```
Input: root = [2,1], p = 2, q = 1
Output: 2
```

**Constraints:**

- The number of nodes in the tree is in the range `[2, 105]`.
- `-109 <= Node.val <= 109`
- All `Node.val` are **unique**.
- `p != q`
- `p` and `q` will exist in the BST.

Accepted

444,176

Submissions

874,725

## Solution:

An ancestor of two nodes in BST is one whose value is smaller than one node and larger than the other node. Following code finds the common ancestor


​        
```c#
public class Solution {
    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q) {
        if(root == null)
        {
            return null;
        }
        var smallNode = p.val < q.val ? p : q;
        var largeNode = p.val < q.val ? q : p;
        if(root.val >= smallNode.val && root.val <= largeNode.val) return root;
        
        //if both values are lower, search in left subtree
        var res = LowestCommonAncestor(root.left, p, q);
        if(res != null) return res;
        
        //if both values are larger, search in right subtree
        res = LowestCommonAncestor(root.right, p, q);
        if(res != null) return res;
        return null;
    }
}
```
Runtime complexity O(n) if tree is unbalanced

Now, let's look at the same problem to find LCA but in a binary tree where there's no order between root, left and child.

# 236. Lowest Common Ancestor of a Binary Tree

Medium

Given a binary tree, find the lowest common ancestor (LCA) of two given nodes in the tree.

According to the [definition of LCA on Wikipedia](https://en.wikipedia.org/wiki/Lowest_common_ancestor): “The lowest common ancestor is defined between two nodes p and q as the lowest node in T that has both p and q as descendants (where we allow **a node to be a descendant of itself**).”

 

**Example 1:**

![img](https://assets.leetcode.com/uploads/2018/12/14/binarytree.png)

```
Input: root = [3,5,1,6,2,0,8,null,null,7,4], p = 5, q = 1
Output: 3
Explanation: The LCA of nodes 5 and 1 is 3.
```

**Example 2:**

![img](https://assets.leetcode.com/uploads/2018/12/14/binarytree.png)

```
Input: root = [3,5,1,6,2,0,8,null,null,7,4], p = 5, q = 4
Output: 5
Explanation: The LCA of nodes 5 and 4 is 5, since a node can be a descendant of itself according to the LCA definition.
```

**Example 3:**

```
Input: root = [1,2], p = 1, q = 2
Output: 1
```

 

**Constraints:**

- The number of nodes in the tree is in the range `[2, 105]`.
- `-109 <= Node.val <= 109`
- All `Node.val` are **unique**.
- `p != q`
- `p` and `q` will exist in the tree.

Accepted

538,554

Submissions

1,142,170

## Solution:

Algorithm 1:

Given a root node and two nodes to search, we will 

1. search both nodes in left and right subtree of this node using regular post order traversal.
2. If a node is found on left but not on right or one node is found on right but not on left, return the found node itself.
3. If one node is found on left and other on right, then return the current root node.
4. Final node returned from the tree root node is the answer.

```c#
 public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
 {
     if (root == null) return null;
     if (root.val == p.val) return root;
     if (root.val == q.val) return root;
     TreeNode leftNodeFound = LowestCommonAncestor(root.left, p, q);
     TreeNode rightNodeFound = LowestCommonAncestor(root.left, p, q);
     if(leftNodeFound != null && rightNodeFound != null)
     {
         return root;
     }
     else if(leftNodeFound != null)
     {
         return leftNodeFound;
     }
     else if(rightNodeFound != null)
     {
         return rightNodeFound;
     }
     return null;
 }
```
**Complexity Analysis**

- Time Complexity: O(N), as in the worst case we might be visiting all the nodes of the binary tree.
- Space Complexity: O(N) as the maximum amount of space utilized by the recursion stack would be N since the height of a skewed binary tree could be N.



#### Approach 2: Iterative using parent pointers

This is taken from official solution at LeetCode and demonstrate key idea on intersection of two sets and storing parent pointers in a dictionary.

**Intuition**

If we have parent pointers for each node we can traverse back from `p` and `q` to get their ancestors. The first common node we get during this traversal would be the LCA node. We can save the parent pointers in a dictionary as we traverse the tree.

**Algorithm**

1. Start from the root node and traverse the tree.
2. Until we find `p` and `q` both, keep storing the parent pointers in a dictionary.
3. Once we have found both `p` and `q`, we get all the ancestors for `p` using the parent dictionary and add to a set called `ancestors`.
4. Similarly, we traverse through ancestors for node `q`. If the ancestor is present in the ancestors set for `p`, this means this is the first ancestor common between `p` and `q` (while traversing upwards) and hence this is the LCA node.



```java
class Solution {

    public TreeNode lowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q) {

        // Stack for tree traversal
        Deque<TreeNode> stack = new ArrayDeque<>();

        // HashMap for parent pointers
        Map<TreeNode, TreeNode> parent = new HashMap<>();

        parent.put(root, null);
        stack.push(root);

        // Iterate until we find both the nodes p and q
        while (!parent.containsKey(p) || !parent.containsKey(q)) {

            TreeNode node = stack.pop();

            // While traversing the tree, keep saving the parent pointers.
            if (node.left != null) {
                parent.put(node.left, node);
                stack.push(node.left);
            }
            if (node.right != null) {
                parent.put(node.right, node);
                stack.push(node.right);
            }
        }

        // Ancestors set() for node p.
        Set<TreeNode> ancestors = new HashSet<>();

        // Process all ancestors for node p using parent pointers.
        while (p != null) {
            ancestors.add(p);
            p = parent.get(p);
        }

        // The first ancestor of q which appears in
        // p's ancestor set() is their lowest common ancestor.
        while (!ancestors.contains(q))
            q = parent.get(q);
        return q;
    }
}
```



**Complexity Analysis**

- Time Complexity : O(N)*O*(*N*), where N*N* is the number of nodes in the binary tree. In the worst case we might be visiting all the nodes of the binary tree.
- Space Complexity : O(N)*O*(*N*). In the worst case space utilized by the stack, the parent pointer dictionary and the ancestor set, would be N*N* each, since the height of a skewed binary tree could be N*N*.