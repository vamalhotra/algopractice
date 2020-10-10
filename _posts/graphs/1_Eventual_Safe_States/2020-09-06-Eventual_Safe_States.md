---
layout: post
title: Find Eventual Safe States
date: 2020-09-06 01:09:26 +0000
category: graphs
lcid: 802
---

# 802. Find Eventual Safe States

Medium

In a directed graph, we start at some node and every turn, walk along a directed edge of the graph. If we reach a node that is terminal (that is, it has no outgoing directed edges), we stop.

Now, say our starting node is *eventually safe* if and only if we must eventually walk to a terminal node. More specifically, there exists a natural number `K` so that for any choice of where to walk, we must have stopped at a terminal node in less than `K` steps.

Which nodes are eventually safe? Return them as an array in sorted order.

The directed graph has `N` nodes with labels `0, 1, ..., N-1`, where `N` is the length of `graph`. The graph is given in the following form: `graph[i]` is a list of labels `j` such that `(i, j)` is a directed edge of the graph.

```
Example:
Input: graph = [[1,2],[2,3],[5],[0],[5],[],[]]
Output: [2,4,5,6]
Here is a diagram of the above graph.
```

![Illustration of graph](https://s3-lc-upload.s3.amazonaws.com/uploads/2018/03/17/picture1.png)

**Note:**

- `graph` will have length at most `10000`.
- The number of edges in the graph will not exceed `32000`.
- Each `graph[i]` will be a sorted list of different integers, chosen within the range `[0, graph.length - 1]`.

Accepted

41,447

Submissions

84,476

#### Approach #1: Reverse Edges [Accepted]

**Intuition**

The crux of the problem is whether you can reach a cycle from the node you start in. If you can, then there is a way to avoid stopping indefinitely; and if you can't, then after some finite number of steps you'll stop.

Thinking about this property more, a node is eventually safe if all it's outgoing edges are to nodes that are eventually safe.

This gives us the following idea: we start with nodes that have no outgoing edges - those are eventually safe. Now, we can update any nodes which only point to eventually safe nodes - those are also eventually safe. Then, we can update again, and so on.

However, we'll need a good algorithm to make sure our updates are efficient.

**Algorithm**

We'll keep track of `graph`, a way to know for some node `i`, what the outgoing edges `(i, j)` are. We'll also keep track of `rgraph`, a way to know for some node `j`, what the incoming edges `(i, j)` are.

Now for every node `j` which was declared eventually safe, we'll process them in a queue. We'll look at all parents `i = rgraph[j]` and remove the edge `(i, j)` from the graph (from `graph`). If this causes the `graph` to have no outgoing edges `graph[i]`, then we'll declare it eventually safe and add it to our queue.

Also, we'll keep track of everything we ever added to the queue, so we can read off the answer in sorted order later.

<iframe src="https://leetcode.com/playground/U4VgbRzv/shared" frameborder="0" width="100%" height="500" name="U4VgbRzv" style="box-sizing: border-box; margin: 20px 0px; color: rgba(0, 0, 0, 0.65); font-family: -apple-system, BlinkMacSystemFont, &quot;Segoe UI&quot;, &quot;PingFang SC&quot;, &quot;Hiragino Sans GB&quot;, &quot;Microsoft YaHei&quot;, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial;"></iframe>



**Complexity Analysis**

- Time Complexity: O(N + E)*O*(*N*+*E*), where N*N* is the number of nodes in the given graph, and E*E* is the total number of edges.
- Space Complexity: O(N)*O*(*N*) in additional space complexity.

------

#### Approach #2: Depth-First Search [Accepted]

**Intuition**

As in *Approach #1*, the crux of the problem is whether you reach a cycle or not.

Let us perform a "brute force": a cycle-finding DFS algorithm on each node individually. This is a classic "white-gray-black" DFS algorithm that would be part of any textbook on DFS. We mark a node gray on entry, and black on exit. If we see a gray node during our DFS, it must be part of a cycle. In a naive view, we'll clear the colors between each search.

**Algorithm**

We can improve this approach, by noticing that we don't need to clear the colors between each search.

When we visit a node, the only possibilities are that we've marked the entire subtree black (which must be eventually safe), or it has a cycle and we have only marked the members of that cycle gray. So indeed, the invariant that gray nodes are always part of a cycle, and black nodes are always eventually safe is maintained.

In order to exit our search quickly when we find a cycle (and not paint other nodes erroneously), we'll say the result of visiting a node is `true` if it is eventually safe, otherwise `false`. This allows information that we've reached a cycle to propagate up the call stack so that we can terminate our search early.

<iframe src="https://leetcode.com/playground/fBBucNrE/shared" frameborder="0" width="100%" height="500" name="fBBucNrE" style="box-sizing: border-box; margin: 20px 0px; color: rgba(0, 0, 0, 0.65); font-family: -apple-system, BlinkMacSystemFont, &quot;Segoe UI&quot;, &quot;PingFang SC&quot;, &quot;Hiragino Sans GB&quot;, &quot;Microsoft YaHei&quot;, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;, &quot;Segoe UI Symbol&quot;; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial;"></iframe>



**Complexity Analysis**

- Time Complexity: O(N + E)*O*(*N*+*E*), where N*N* is the number of nodes in the given graph, and E*E* is the total number of edges.
- Space Complexity: O(N)*O*(*N*) in additional space complexity.

## Solution:

```c#

/// <summary>
/// The input is adjacency list.
/// The idea behind Approach1 and Approach2 is same and nothing but 
/// dfs "white-gray-black" or "unknown-good-bad" approach of 
/// marking each node. Approach1 is poor code but working, approach 2
/// is clean code. 
/// Approach 3 is altogether different idea to not do dfs
/// but to create reverse graph i.e. list of nodes pointing to the node. 
/// and traverse the graph in topological sort fashion
/// See https://www.geeksforgeeks.org/topological-sorting/ for refresher on topological sort
///(Official solution) https://leetcode.com/problems/find-eventual-safe-states/solution/
/// </summary>
public class Solution
{

	private bool DoDfsApproach1(int[][] graph, int start, 
		HashSet<int> visitedNodes,
		ref HashSet<int> safeNodes)
	{
		bool isSafe = true;
		//Do dfs from a node. A node is unsafe if we find a repeated node while doing dfs from it. If a node is found unsafe, then all nodes in current dfs-path are also unsafe. 
		//It's necessary to remember which nodes were found safe or unsafe to barely pass the system tests.
		var ngbrs = graph[start];
		foreach (var nbr in ngbrs)
		{
			if(visitedNodes.Contains(nbr))
			{
				return false;
			}
			if (safeNodes.Contains(nbr)) continue;

			visitedNodes.Add(nbr);
			isSafe = isSafe & DoDfsApproach1(graph, nbr, visitedNodes, ref safeNodes);
			
			if (!isSafe)
			{
				break;
			}
			else
			{
				safeNodes.Add(nbr);
			}
			visitedNodes.Remove(nbr);
		}
		if(isSafe)
		{
			safeNodes.Add(start);
		}
		return isSafe;
	}

	public IList<int> EventualSafeNodesApproach1(int[][] graph)
	{
		var nodeCount = graph == null ? 0 : graph.Count();
		var safeNodes = new HashSet<int>();
		var unsafeNodes = new HashSet<int>();
						
		for (int i = 0; i < nodeCount; ++i)
		{
			if (safeNodes.Contains(i) || unsafeNodes.Contains(i)) continue;

			var unsafeNodesTemp = new HashSet<int>(unsafeNodes);
			if (!DoDfsApproach1(graph, i, unsafeNodesTemp, ref safeNodes))
			{
				unsafeNodes.UnionWith(unsafeNodesTemp);
			}
		}
		return safeNodes.OrderBy(x => x).ToList();
	}

	private bool DoDfsApproach2(int[][] graph, ref int[] color, int curNode)
	{
		if (color[curNode] > 0) return color[curNode] == 1;

		color[curNode] = 2; //mark as unsafe
		foreach(var neighbor in graph[curNode])
		{
			if(!DoDfsApproach2(graph, ref color, neighbor))
			{
				return false;
			}
		}
		color[curNode] = 1; //mark as safe;
		return true;
	}

	public IList<int> EventualSafeNodesApproach2(int[][] graph)
	{
		var nodeCount = graph == null ? 0 : graph.Count();

		var safeNodes = new List<int>();
		int[] color = new int[nodeCount];

		for (int i = 0; i < nodeCount; ++i)
		{
			if(DoDfsApproach2(graph, ref color, i))
			{
				safeNodes.Add(i);
			}
		}
		return safeNodes.OrderBy(x => x).ToList();
	}

	public List<int> EventualSafeNodesApproach3(int[][] G)
	{
		int N = G.Length;
		var safe = new bool[N];

		var graph = new List<HashSet<int>>();
		var rgraph = new List<HashSet<int>>();
		for (int i = 0; i < N; ++i)
		{
			graph.Add(new HashSet<int>());
			rgraph.Add(new HashSet<int>());
		}

		Queue<int> queue = new Queue<int>();

		for (int i = 0; i < N; ++i)
		{
			//Collect the terminal nodes as safe nodes
			if (G[i].Length == 0)
				queue.Enqueue(i);

			foreach (int j in G[i])
			{
				graph[i].Add(j);
				rgraph[j].Add(i);
			}
		}

		while (queue.Count() > 0) //safe nodes queue
		{
			int j = queue.Dequeue();
			safe[j] = true;
			foreach (int i in rgraph[j])
			{
				graph[i].Remove(j); //remove safe node as outlink
				if (!graph[i].Any()) //if no further outlink, add to safe node queue
					queue.Enqueue(i);
			}
		}
		//all nodes with outlink in graph are ones with no contact with terminal nodes. 
		//Terminal nodes were removed one-by-one from outlink list of each node.
		//If some nodes still remain, those are not touching any terminal node.
		
		//Finally collect your answer
		List<int> ans = new List<int>();
		for (int i = 0; i < N; ++i)
			if (safe[i])
				ans.Add(i);

		return ans;
	}
}

static void Main(string[] args)
{

	var array2d = new int[][]{
		new int[] { 1, 2 },
		new int[] { 2, 3 },
		new int[] { 5 },
		new int[] { 0 },
		new int[] { 5 },
		new int[] { },
		new int[] { }
	};

	var matrix = new int[,]{
		{ 1, 3 },
		{ 2, 3},
	};

	var safeNodes = new Solution().eventualSafeNodesApproach3(array2d);
	Console.WriteLine(string.Join(" ", safeNodes));
}
```