---
layout: post
title: Longest Palindromic Substring
preview: Medium  Given a string s find the longest palindromic substring in s You may assume that the maximum length of s is 1000
date: 2020-09-20 14:38:06 +0000
categories: dp
lcid: 5
---

# 5. Longest Palindromic Substring

Medium

Given a string **s**, find the longest palindromic substring in **s**. You may assume that the maximum length of **s** is 1000.

**Example 1:**

```
Input: "babad"
Output: "bab"
Note: "aba" is also a valid answer.
```

**Example 2:**

```
Input: "cbbd"
Output: "bb"
```

Accepted

1,034,296

Submissions

3,490,797

```c#
//https://leetcode.com/problems/longest-palindromic-substring/
public class Solution
{
	//O(n)
	public bool IsPalindrome(string s)
	{
		int len = s.Length;
		int i = 0, j = len - 1;
		while (i <= j && s[i++] == s[j--]) ;
		return (j < i);
	}

	//O(n^3)
	public List<string> AllPalindromeBrute(string s)
	{
		int len = s.Length;
		var allPalindromes = new List<string>();
		for(int i=0; i<len; ++i)
		{
			for(int j=i+1; j<len; ++j)
			{
				var substr = s.Substring(i, j - i + 1);
				if(IsPalindrome(substr))
				{
					allPalindromes.Add(substr);
				}
			}
		}
		return allPalindromes;
	}

	//O(n^2)
	public List<string> AllPalindromeDP(string s, bool onlyLongest = true)
	{
		int len = s.Length;
		string longest = string.Empty;

		//dp[i,j] = true if S[i..j] is palindrome
		//dp[i,j] = dp[i+1, j-1] && S[i] = S[j]
		bool[][] dp = new bool[len + 1][];
		for (int i = 0; i < len; ++i)
		{
			dp[i] = new bool[len + 1];
		}

		//string len= step size.
		//we start with one letter palindromes, then two letter and so on
		for (int step = 0; step < len; ++step)
		{
			for (int i = 0; i < len; ++i)
			{
				int j = i + step;
				if (j >= len) break;

				if (step == 0)
				{
					dp[i][j] = true;
				}
				else if (step == 1)
				{
					dp[i][j] = s[i] == s[j];
				}
				else
				{
					dp[i][j] = dp[i + 1][j - 1] && s[i] == s[j];
				}
				if (onlyLongest && dp[i][j] && j - i + 1 > longest.Length)
				{
					longest = s.Substring(i, j - i + 1);
				}
			}
		}

		var allPalindromes = new List<string>();

		if (onlyLongest)
		{
			allPalindromes.Add(longest);
		}
		else
		{

			for (int i = 0; i < len; ++i)
			{
				for (int j = 0; j < len; ++j)
				{
					if (dp[i][j])
					{
						allPalindromes.Add(s.Substring(i, j - i + 1));
					}
				}
			}
		}
		return allPalindromes;
	}


	public string LongestPalindromeBruteAC(string s)
	{
		int len = s.Length;
		if (len == 0) return s;
		string longest = s.Substring(0, 1);
		for (int i = 0; i < len; ++i)
		{
			for (int j = len - 1; j > i; --j)
			{
				if (j - i + 1 < longest.Length) continue;
				var substr = s.Substring(i, j - i + 1);
				if (IsPalindrome(substr) && longest.Length < substr.Length)
				{
					longest = substr;
					break;
				}
			}
		}
		return longest;
	}
}
```