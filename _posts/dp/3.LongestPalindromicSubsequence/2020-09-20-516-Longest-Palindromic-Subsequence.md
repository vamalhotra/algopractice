---
layout: post
title: Longest Palindromic Subsequence
date: 2020-09-20 23:46:42 +0000
category: dp
lcid: 516
---

# 516. Longest Palindromic Subsequence

Medium

Given a string s, find the longest palindromic subsequence's length in s. You may assume that the maximum length of s is 1000.

**Example 1:**
Input:

```
"bbbab"
```

Output:

```
4
```

One possible longest palindromic subsequence is "bbbb".

 

**Example 2:**
Input:

```
"cbbd"
```

Output:

```
2
```

One possible longest palindromic subsequence is "bb".

 

**Constraints:**

- `1 <= s.length <= 1000`
- `s` consists only of lowercase English letters.

Accepted

131,682

Submissions

244,651

## Solution:
```c#
//https://emre.me/coding-patterns/palindromes/
public class Solution
{
	public List<string> GenerateAllSubseqs(string s, int curIdx)
	{
		if (curIdx >= s.Length)
		{
			return new List<string>();
		}

		var seqs = new List<string>();
		seqs.Add(s[curIdx].ToString());

		var subseqs = GenerateAllSubseqs(s, curIdx + 1);
		foreach (var seq in subseqs)
		{
			seqs.Add(s[curIdx] + seq);
		}
		seqs.AddRange(subseqs);

		return seqs;
	}

	public string LongestPalindromeSubsequenceBrute(string s, int start, int end)
	{
		if (start < 0 || end >= s.Length)
		{
			return string.Empty;
		}

		string longest;
		if (s[start] == s[end]) //let's pick these two
		{
			longest = LongestPalindromeSubsequenceBrute(s, start + 1, end - 1);
			longest = s[start] + longest + s[end];
		}
		else
		{
			var s1 = LongestPalindromeSubsequenceBrute(s, start + 1, end);
			var s2 = LongestPalindromeSubsequenceBrute(s, start, end - 1);
			if (s1.Length > s2.Length)
			{
				longest = s1;
			}
			else
			{
				longest = s2;
			}
		}
		return longest;
	}

	public string[][] memo;
	public string LongestPalindromeSubsequenceMemo(string s, int start, int end)
	{
		if (start < 0 || end >= s.Length || start > end)
		{
			return string.Empty;
		}

		if (start == end)
		{
			return s[start].ToString();
		}

		if (memo[start][end] != null)
		{
			return memo[start][end];
		}

		string longest;
		//case 1: elements at the beginning and the end are the same
		if (s[start] == s[end])
		{
			longest = LongestPalindromeSubsequenceMemo(s, start + 1, end - 1);
			longest = s[start] + longest + s[end];
		}
		else
		{
			//case 2: skip one element either from the beginning or the end
			var s1 = LongestPalindromeSubsequenceMemo(s, start + 1, end);
			var s2 = LongestPalindromeSubsequenceMemo(s, start, end - 1);
			if (s1.Length > s2.Length)
			{
				longest = s1;
			}
			else
			{
				longest = s2;
			}
		}
		memo[start][end] = longest;
		return longest;
	}

	public int LongestPalindromeSubseq(string s)
	{
		if (s == null) return 0;
		if (s.Length <= 1) return s.Length;

		memo = new string[s.Length][];
		for (int i = 0; i < s.Length; ++i)
		{
			memo[i] = new string[s.Length];
		}
		return LongestPalindromeSubsequenceMemo(s, 0, s.Length - 1).Length;
	}

	/*
	 * We can build our two-dimensional memoization array in a bottom-up fashion, adding one element at a time.
	   If the element at the start and the end is matching, the length of Longest Palindromic Substring (LPS) is 2 plus the length of LPS till start+1 and end-1.
	   If the element at the start does not match the element at the end, we will take the max of LPS by either skipping the element at start or end
	   So the overall algorithm will be;
	   if s[start] == s[end]:
memo[start][end] = 2 + memo[start + 1][end - 1]
else:
memo[start][end] = max(memo[start + 1][end], memo[start][end - 1])
	*/

	public int LongestPalindromeSubsequenceBottomUp(string s)
	{
		int len = s.Length;
		memo = new string[s.Length][];
		for (int i = 0; i < s.Length; ++i)
		{
			memo[i] = new string[s.Length];
			memo[i][i] = s[i].ToString();
		}

		for (int start = len - 1; start >= 0; --start)
		{
			for (int end = start + 1; end < len; ++end)
			{
				if (s[start] == s[end])
				{
					memo[start][end] = s[start] + memo[start + 1][end - 1] + s[end];
				}
				else
				{
					var l1 = memo[start][end - 1];
					var l2 = memo[start + 1][end];
					memo[start][end] = l1.Length > l2.Length ? l1 : l2;
				}
			}
		}
		return memo[0][len-1].Length;
	}
}
```