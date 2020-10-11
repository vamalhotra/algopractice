---
layout: post
title: Split Array Into Fibonacci Sequence
preview: Medium  Given a string S of digits such as S  123456579 we can split it into a Fibonaccilike sequence 123 456 579
date: 2020-09-09 23:51:22 +0000
categories: search
lcid: 842
---

# 842. Split Array into Fibonacci Sequence

Medium

Given a string `S` of digits, such as `S = "123456579"`, we can split it into a *Fibonacci-like sequence* `[123, 456, 579].`

Formally, a Fibonacci-like sequence is a list `F` of non-negative integers such that:

- `0 <= F[i] <= 2^31 - 1`, (that is, each integer fits a 32-bit signed integer type);
- `F.length >= 3`;
- and` F[i] + F[i+1] = F[i+2] `for all `0 <= i < F.length - 2`.

Also, note that when splitting the string into pieces, each piece must not have extra leading zeroes, except if the piece is the number 0 itself.

Return any Fibonacci-like sequence split from `S`, or return `[]` if it cannot be done.

**Example 1:**

```
Input: "123456579"
Output: [123,456,579]
```

**Example 2:**

```
Input: "11235813"
Output: [1,1,2,3,5,8,13]
```

**Example 3:**

```
Input: "112358130"
Output: []
Explanation: The task is impossible.
```

**Example 4:**

```
Input: "0123"
Output: []
Explanation: Leading zeroes are not allowed, so "01", "2", "3" is not valid.
```

**Example 5:**

```
Input: "1101111"
Output: [110, 1, 111]
Explanation: The output [11, 0, 11, 11] would also be accepted.
```

**Note:**

1. `1 <= S.length <= 200`
2. `S` contains only digits.

Accepted

20,707

Submissions

56,948

## Solution:

```c#
/// <summary>
/// Split the string in such a way that sum of any i-th and i+1-th = i+2h segment
/// Make a choice and process, backtrack when necessary
/// The key idea is to use a list to keep track of collected numbers. 
/// Initial try to keep two numbers prev1 and prev2 made it messy.
/// </summary>
public class Solution
{
	/// <summary>
	/// If it starts with zero, you have no choice but to pick 0 as number
	/// </summary>
	/// <param name="str"></param>
	/// <param name="strIdx"></param>
	/// <param name="old1"></param>
	/// <param name="old2"></param>
	/// <returns></returns>
	private bool Split(string str, int strIdx, List<int> numbers)
	{
		if(strIdx >= str.Length)
		{
			return true;
		}

		//Number with leading zero is not allowed except taking it as zero
		int maxDigits = str[strIdx] == '0' ? 1 : Int32.MaxValue.ToString().Length;
		for(int i=strIdx; i< strIdx+maxDigits && i<str.Length; ++i)
		{
			var strNum = str.Substring(strIdx, i - strIdx + 1);
			int num;
			if(!int.TryParse(strNum, out num))
			{
				break;
			}
			//sum of last two numbers in the list should be equal to next num
			if(numbers.Count >= 2 && (numbers[numbers.Count-1] + numbers[numbers.Count-2]) != num)
			{
				continue;
			}
			numbers.Add(num);
			if(Split(str, i + 1, numbers))
			{
				if (numbers.Count > 2)
				{
					return true;
				}
			}
			numbers.RemoveAt(numbers.Count - 1);
		}
		return false;
	}

	//0112
	public IList<int> SplitIntoFibonacci(string S)
	{
		var res = new List<int>();
		Split(S, 0, res);
		return res;
	}
}
```