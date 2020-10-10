---
layout: post
title: Flower Planting With No Adjacent
date: 2020-09-10 00:42:59 +0000
category: search
lcid: 1042
---

# 1042. Flower Planting With No Adjacent

Easy

You have `N` gardens, labelled `1` to `N`. In each garden, you want to plant one of 4 types of flowers.

`paths[i] = [x, y]` describes the existence of a bidirectional path from garden `x` to garden `y`.

Also, there is no garden that has more than 3 paths coming into or leaving it.

Your task is to choose a flower type for each garden such that, for any two gardens connected by a path, they have different types of flowers.

Return **any** such a choice as an array `answer`, where `answer[i]` is the type of flower planted in the `(i+1)`-th garden. The flower types are denoted 1, 2, 3, or 4. It is guaranteed an answer exists.

 **Example 1:**

```
Input: N = 3, paths = [[1,2],[2,3],[3,1]]
Output: [1,2,3]
```

**Example 2:**

```
Input: N = 4, paths = [[1,2],[3,4]]
Output: [1,2,1,2]
```

**Example 3:**

```
Input: N = 4, paths = [[1,2],[2,3],[3,4],[4,1],[1,3],[2,4]]
Output: [1,2,3,4]
```

 

**Note:**

- `1 <= N <= 10000`
- `0 <= paths.size <= 20000`
- No garden has 4 or more paths coming into or leaving it.
- It is guaranteed an answer exists.

Accepted

30,420

Submissions

62,929

## Solution:

```c#
public class Solution
{
	/*
	We can just pick any color for a node that does not conflict and continue; we don't need to backtrack here.
	A simpler solution suffice... to prove that, you can actually throw exception as below
	*/
	private bool ColorBacktracking(List<int>[] paths, int cur, int[] colors)
	{
		if (cur >= paths.Length) return true;
		var neighbors = paths[cur];

		var alreadyColored = neighbors.Select(x => colors[x]).Where(x => x > 0).ToList();
		for (int i = 1; i <= 4; ++i)
		{
			if (alreadyColored.Contains(i)) continue;
			colors[cur] = i;
			if (ColorBacktracking(paths, cur + 1, colors))
			{
				return true;
			}
			colors[cur] = -1;
			throw new Exception("Code never reaches here. Backtracking is not needed");
		}
		return false;
	}

	public int[] GardenNoAdjBacktracking(int N, int[][] paths)
	{
		List<int>[] connections = new List<int>[N];
		for (int i = 0; i < N; ++i)
		{
			connections[i] = new List<int>();
		}

		for (int i = 0; i < paths.Length; ++i)
		{

			connections[paths[i][0] - 1].Add(paths[i][1] - 1);
			connections[paths[i][1] - 1].Add(paths[i][0] - 1);
		}

		var colors = new int[N];
		ColorBacktracking(connections, 0, colors);
		return colors;
	}
	
	public int[] GardenNoAdj(int N, int[][] paths)
	{
		var connections = new Dictionary<int, HashSet<int>>();
		for (int i = 0; i < N; i++)
		{
			connections.Add(i, new HashSet<int>());
		}

		foreach (int[] p in paths)
		{
			connections[p[0] - 1].Add(p[1] - 1);
			connections[p[1] - 1].Add(p[0] - 1);
		}

		int[] res = new int[N];
		for (int i = 0; i < N; i++)
		{
			int[] colors = new int[5];
			foreach (int j in connections[i])
			{
				colors[res[j]] = 1;
			}

			for (int c = 1; c <= 4; ++c)
			{
				if (colors[c] == 0)
				{
					res[i] = c;
					break;
				}
			}
		}
		return res;
	}
}
```