---
layout: post
title: Maximum Length Of Repeated Subarray
date: 2020-09-20 12:22:27 +0000
category: dp
lcid: 718
---

# 718. Maximum Length of Repeated Subarray

Medium

Given two integer arrays `A` and `B`, return the maximum length of an subarray that appears in both arrays.

**Example 1:**

```
Input:
A: [1,2,3,2,1]
B: [3,2,1,4,7]
Output: 3
Explanation: 
The repeated subarray with maximum length is [3, 2, 1].
```

 

**Note:**

1. 1 <= len(A), len(B) <= 1000
2. 0 <= A[i], B[i] < 100

 

Accepted

69,546

Submissions

140,253



## Solution:

```c#
/*
https://leetcode.com/problems/maximum-length-of-repeated-subarray/solution/

Since a common subarray of A and B must start at some A[i] and B[j], let dp[i][j] be the longest common prefix of A[i:] and B[j:]. Whenever A[i] == B[j], we know dp[i][j] = dp[i+1][j+1] + 1. Also, the answer is max(dp[i][j]) over all i, j.

We can perform bottom-up dynamic programming to find the answer based on this recurrence. Our loop invariant is that the answer is already calculated correctly and stored in dp for any larger i, j.
*/
//Time complexity: O(A.Length * B.Length) = O(M*N)
//Space complexity: O(M*N)
public int FindLength(int[] A, int[] B)
{
	int res = 0;
	int[][] lcp = new int[A.Length+1][];
	lcp[A.Length] = new int[B.Length+1];

	for(int i=A.Length-1; i>=0; --i)
	{
		lcp[i] = new int[B.Length+1];
		for(int j= B.Length-1; j>=0; --j)
		{
			if(A[i] == B[j])
			{
				lcp[i][j] = lcp[i + 1][j + 1] + 1;
				if(lcp[i][j] > res)
				{
					res = lcp[i][j];
				}
			}
		}
	}
	return res;
}
```

