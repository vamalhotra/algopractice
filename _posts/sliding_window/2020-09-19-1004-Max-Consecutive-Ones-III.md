---
layout: post
title: Max Consecutive Ones III
preview: Medium  Given an array A of 0s and 1s we may change up to K values from 0 to 1  Return the length of the longest contiguous s
date: 2020-09-19 15:30:49 +0000
categories: sliding_window
lcid: 1004
---

# 1004. Max Consecutive Ones III

Medium

Given an array `A` of 0s and 1s, we may change up to `K` values from 0 to 1.

Return the length of the longest (contiguous) subarray that contains only 1s. 

 

**Example 1:**

```
Input: A = [1,1,1,0,0,0,1,1,1,1,0], K = 2
Output: 6
Explanation: 
[1,1,1,0,0,1,1,1,1,1,1]
Bolded numbers were flipped from 0 to 1.  The longest subarray is underlined.
```

**Example 2:**

```
Input: A = [0,0,1,1,0,0,1,1,1,0,1,1,0,0,0,1,1,1,1], K = 3
Output: 10
Explanation: 
[0,0,1,1,1,1,1,1,1,1,1,1,0,0,0,1,1,1,1]
Bolded numbers were flipped from 0 to 1.  The longest subarray is underlined.
```

 

**Note:**

1. `1 <= A.length <= 20000`
2. `0 <= K <= A.length`
3. `A[i]` is `0` or `1` 

Accepted

66,352

Submissions

111,358

## Solution:

```c#
public class Solution
{
	//K 0's can be flipped to 1
	//Find longest streak of 1's in A
	public int LongestOnes(int[] A, int K)
	{
		int maxLen = 0;
		int windowStart = 0, windowEnd = 0;
		for(int i=0; i<A.Length; ++i, ++windowEnd)
		{
			if(A[i] == 0)
			{
				if(K > 0)
				{
					--K;
				}
				else
				{
					//window contains (k+1)zeros currently including both windowStart and windowEnd
					maxLen = Math.Max(maxLen, windowEnd-windowStart);

					//move windowStart pointer until it skips a zero
					while (windowStart < A.Length && 
						A[windowStart++] == 1) ;
				}
			}
		}
		maxLen = Math.Max(maxLen, windowEnd - windowStart);
		return maxLen;
	}

	public int LongestOnesTLE(int[] A, int K)
	{
		int longest = 0;
		for (int i = 0; i < A.Length; ++i)
		{
			int count = K;
			int oneLen = 0;
			for (int j = i; j < A.Length; ++j)
			{
				if (A[j] == 0)
				{
					if (count == 0)
						break;
					--count;
				}
				++oneLen;
			}
			longest = Math.Max(longest, oneLen);
		}
		return longest;
	}
}
```