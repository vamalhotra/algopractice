# 700. Search in a Binary Search Tree

Easy

Given the root node of a binary search tree (BST) and a value. You need to find the node in the BST that the node's value equals the given value. Return the subtree rooted with that node. If such node doesn't exist, you should return NULL.

For example, 

```
Given the tree:
        4
       / \
      2   7
     / \
    1   3

And the value to search: 2
```

You should return this subtree:

```
      2     
     / \   
    1   3
```

In the example above, if we want to search the value `5`, since there is no node with value `5`, we should return `NULL`.

Note that an empty tree is represented by `NULL`, therefore you would see the expected output (serialized tree format) as `[]`, not `null`.

Accepted

220,560

Submissions

300,964

## Solution:

```c#
public class Solution {
    public TreeNode SearchBST(TreeNode root, int val) {
        if(root == null) return null;
        if(root.val == val) return root;
        if(root.val > val) return SearchBST(root.left, val);
        if(root.val < val) return SearchBST(root.right, val);
        return null;
    }
}
```

There are two related questions: 

### 1) Closest Binary Search Tree Value 

Given a non-empty binary search tree and a target value, find the value in the BST that is closest to the target.

Given target value is a floating point. You are guaranteed to have only one unique value in the BST that is closest to the target.

### **Example**

**Example1**

```
Input: root = {5,4,9,2,#,8,10} and target = 6.124780
Output: 5
Explanation：
Binary tree {5,4,9,2,#,8,10},  denote the following structure:
        5
       / \
     4    9
    /    / \
   2    8  10
```

**Example2**

```c#
Input: root = {3,2,4,1} and target = 4.142857
Output: 4
Explanation：
Binary tree {3,2,4,1},  denote the following structure:
     3
    / \
  2    4
 /
1
```

### Solution:

The idea is to do regular BST search by traversing left when target is smaller than current node and traverse right when target is greater than current node.



```c#
public void ClosestValueHelper(TreeNode root, double target, ref int closest)
{
    if (root == null) return;
    if (Math.Abs(closest - target) > Math.Abs(root.val - target))
    {
        closest = root.val;
    }
    //If this node's value is larger than target, then going to
    //right side will only give even larger values and greater
    //delta. So, only check on left side.
    if (root.val > target)
    {
        ClosestValueHelper(root.left, target, ref closest);
    }
    else
    {
        ClosestValueHelper(root.right, target, ref closest);
    }
}

public int ClosestValue(TreeNode root, double target)
{
    int closest = Int32.MaxValue;
    ClosestValueHelper(root, target, ref closest);
    return closest;
}
```
### 2) K-closest values to given value

Given a non-empty binary search tree and a target value, find `k` values in the BST that are closest to the target.

Given target value is a floating point. You may assume `k` is always valid, that is: `k ≤ total` nodes. You are guaranteed to have only one `unique` set of k values in the BST that are closest to the target.

### **Example**

**Example 1:**

```
Input:
{1}
0.000000
1
Output:
[1]
Explanation：
Binary tree {1},  denote the following structure:
 1
```

**Example 2:**

```
Input:
{3,1,4,#,2}
0.275000
2
Output:
[1,2]
Explanation：
Binary tree {3,1,4,#,2},  denote the following structure:
  3
 /  \
1    4
 \
  2
```

### **Challenge**

Assume that the BST is balanced, could you solve it in less than O(n) runtime (where n = total nodes)?

### Solution:

#### Approach 1

Approach 1 is to do similar to closest number. Here, we will keep K numbers in a list and keep the list sorted based on their distance from target number so that farthest number is at end. For every new number, we will see if distance between (target-current) < (target-farthest_number) . If so, remove the farthest number and add 'current' at right place in list. 

The only disadvantage of this approach is that it needs sorting of list everytime a new number is sorted. This gives O(n) = n*(k log k)

```c#
public void KClosestValueHelper(TreeNode root, double target, int k, List<int> closest)
{
    if (root == null) return;
    if (closest.Count < k)
    {
        closest.Add(root.val);
        closest = closest.OrderBy(x => Math.Abs(x - target)).ToList();
    }
    else
    {
        var lastVal = closest[k - 1];
        if (Math.Abs(lastVal - target) > Math.Abs(root.val - target))
        {
            //insert root.val at right place
            closest[k - 1] = root.val;
            closest = closest.OrderBy(x => Math.Abs(x - target)).ToList();
        }
    }
    KClosestValueHelper(root.left, target, k, closest);
    KClosestValueHelper(root.right, target, k, closest);
}

public List<int> KClosestValues(TreeNode root, double target, int k)
{
    var closest = new List<int>(k);
    KClosestValueHelper(root, target, k, closest);
    return closest;
}
```
#### Approach 2:

The basic idea is to do inorder traversal and start with adding first K elements to our queue. We look for a crossover node which is the node with value less than target and next node having value greater than the target. 

We are initially approaching target until we reach this 'crossover node' and we start moving away from the target once we cross the crossover node. Our k elements are around this crossover node. We just keep last K elements found in inorder traversal until we cross crossover node. After that, we traverse until the distance between (target-current) < (target-farthest_node_in_queue). farthest node is the 0th node in our queue. We keep dequeuing and adding current until this condition holds. We stop after that.

This solution is O(n) since we will traverse all n-nodes in worst case.



```c#
public void KClosestValueHelper2(TreeNode root, double target, int k, Queue<int> closest, ref bool ascending)
{
    if (root == null) return;
    KClosestValueHelper2(root.left, target, k, closest, ref ascending);

    if (closest.Count < k)
    {
        closest.Enqueue(root.val);
    }
    else
    {
        if (root.val > target)
        {
            ascending = false;
        }
        if (ascending && root.val != target)
        {
            closest.Dequeue();
            closest.Enqueue(root.val);
        }
        else if (!ascending && root.val != target)
        {
            var farthestVal = closest.Peek();
            if (Math.Abs(farthestVal - target) > Math.Abs(root.val - target))
            {
                closest.Dequeue();
                closest.Enqueue(root.val);
            }
            else //end of search
            {
                return;
            }
        }
    }

    KClosestValueHelper2(root.right, target, k, closest, ref ascending);
}

public List<int> KClosestValues2(TreeNode root, double target, int k)
{
    bool ascending = true;
    var closest = new Queue<int>(k);
    KClosestValueHelper2(root, target, k, closest, ref ascending);
    return closest.ToList();
}
```
Approach 3:

Use two stacks. 

Read the solution [here](https://kennyzhuang.gitbooks.io/leetcode-lock/content/270_closest_binary_search_tree_value_ii.html)

Also, consider solving problem same problem where you're given a sorted array instead of BST [here](https://www.geeksforgeeks.org/find-k-closest-elements-given-value/)