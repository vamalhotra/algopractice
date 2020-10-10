---
layout: post
title: Longest Common Subsequence
date: 2020-09-22 01:07:43 +0000
category: dp
lcid: 1143
---

# 1143. Longest Common Subsequence

Medium

Given two strings `text1` and `text2`, return the length of their longest common subsequence.

A *subsequence* of a string is a new string generated from the original string with some characters(can be none) deleted without changing the relative order of the remaining characters. (eg, "ace" is a subsequence of "abcde" while "aec" is not). A *common subsequence* of two strings is a subsequence that is common to both strings.

 

If there is no common subsequence, return 0.

 

**Example 1:**

```
Input: text1 = "abcde", text2 = "ace" 
Output: 3  
Explanation: The longest common subsequence is "ace" and its length is 3.
```

**Example 2:**

```
Input: text1 = "abc", text2 = "abc"
Output: 3
Explanation: The longest common subsequence is "abc" and its length is 3.
```

**Example 3:**

```
Input: text1 = "abc", text2 = "def"
Output: 0
Explanation: There is no such common subsequence, so the result is 0.
```

 

**Constraints:**

- `1 <= text1.length <= 1000`
- `1 <= text2.length <= 1000`
- The input strings consist of lowercase English characters only.

Accepted

138,532

Submissions

237,197

## Solution:

```c#
//https://emre.me/coding-patterns/longest-common-substring-subsequence/
public class Solution
{
	/*
	 * Two pointer i and j. 
	 * If text1[i] = text2[j], then LCS[i][j] = 1 + LCS[i-1][j-1]
	 * Else LCS[i][j] = max(LCS[i-1][j], LCS[i][j-1])
	 */
	public int LongestCommonSubsequence(string text1, string text2)
	{
		var len1 = text1.Length;
		var len2 = text2.Length;
		int[][] lcs = new int[len1][];
		for(int i=0; i<len1; ++i)
		{
			lcs[i] = new int[len2];
		}

		for(int i=0; i<len1; ++i)
		{
			for(int j=0; j<len2; ++j)
			{
				if(text1[i] == text2[j])
				{
					lcs[i][j] = 1 + 
						((i > 0 && j > 0) ? lcs[i - 1][j - 1] : 0);
				}
				else
				{
					var l1 = j > 0 ? lcs[i][j - 1] : 0;
					var l2 = i > 0 ? lcs[i - 1][j] : 0;
					lcs[i][j] = Math.Max(l1, l2);
				}
			}
		}
		return lcs[len1 - 1][len2 - 1];
	}
}
```

