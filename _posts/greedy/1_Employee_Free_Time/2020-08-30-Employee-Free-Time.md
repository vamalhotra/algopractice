---
layout: post
title: Employee Free Time
preview: We are given a list schedule of employees which represents the working time for each employee  Each employee has a list of nonover
date: 2020-08-30 21:23:57 +0000
categories: greedy
lcid: 759
---

## 759. Employee Free Time

We are given a list `schedule` of employees, which represents the working time for each employee.

Each employee has a list of non-overlapping `Intervals`, and these intervals are in sorted order.

Return the list of finite intervals representing **common, positive-length free time** for *all* employees, also in sorted order.

**Example 1:**

```
Input: schedule = [[[1,2],[5,6]],[[1,3]],[[4,10]]]
Output: [[3,4]]
Explanation:
There are a total of three employees, and all common
free time intervals would be [-inf, 1], [3, 4], [10, inf].
We discard any intervals that contain inf as they aren't finite.
```



**Example 2:**

```
Input: schedule = [[[1,3],[6,7]],[[2,4]],[[2,5],[9,12]]]
Output: [[5,6],[7,9]]
```



(Even though we are representing `Intervals` in the form `[x, y]`, the objects inside are `Intervals`, not lists or arrays. For example, `schedule[0][0].start = 1, schedule[0][0].end = 2`, and `schedule[0][0][0]` is not defined.)

Also, we wouldn't include intervals like [5, 5] in our answer, as they have zero length.

**Note:**

1. `schedule` and `schedule[i]` are lists with lengths in range `[1, 50]`.
2. `0 <= schedule[i].start < schedule[i].end <= 10^8`.

## Solution:

```c#
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Practice
{
	
	public class Interval
	{
		public int start;
		public int end;
		public Interval() { start = 0; end = 0; }
		public Interval(int s, int e) { start = s; end = e; }
	}

	class Solution
	{
		/// <summary>
        /// Add all given intervals on a timeline. 
        /// We are looking for gaps between intervals. 
        /// To find the gaps, we will sort by start-time and traverse the list
        /// </summary>
		public List<Interval> employeeFreeTime(List<List<Interval>> avails)
		{
			var timeline = avails.SelectMany(x => x).OrderBy(x => x.start).ToList();
			var freeIntervals = new List<Interval>();
			var prev = timeline[0];
			for(int i=0; i<timeline.Count; ++i)
			{
				var cur = timeline[i];
				if(prev.end < cur.start)
				{
					freeIntervals.Add(new Interval(prev.end, cur.start));
				}
				prev = cur.end > prev.end ? cur : prev;
			}
			return freeIntervals;
		}
	}
}
```