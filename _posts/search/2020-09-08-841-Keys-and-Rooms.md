---
layout: post
title: Keys And Rooms
preview: Medium  There are N rooms and you start in room 0 Each room has a distinct number in 0 1 2  N1 and each room may have s
date: 2020-09-08 00:06:14 +0000
categories: search
lcid: 841
---

# 841. Keys and Rooms

Medium

There are `N` rooms and you start in room `0`. Each room has a distinct number in `0, 1, 2, ..., N-1`, and each room may have some keys to access the next room. 

Formally, each room `i` has a list of keys `rooms[i]`, and each key `rooms[i][j]` is an integer in `[0, 1, ..., N-1]` where `N = rooms.length`. A key `rooms[i][j] = v` opens the room with number `v`.

Initially, all the rooms start locked (except for room `0`). 

You can walk back and forth between rooms freely.

Return `true` if and only if you can enter every room.



**Example 1:**

```
Input: [[1],[2],[3],[]]
Output: true
Explanation:  
We start in room 0, and pick up key 1.
We then go to room 1, and pick up key 2.
We then go to room 2, and pick up key 3.
We then go to room 3.  Since we were able to go to every room, we return true.
```

**Example 2:**

```
Input: [[1,3],[3,0,1],[2],[0]]
Output: false
Explanation: We can't enter the room with number 2.
```

**Note:**

1. `1 <= rooms.length <= 1000`
2. `0 <= rooms[i].length <= 1000`
3. The number of keys in all rooms combined is at most `3000`.

Accepted

82,676

Submissions

128,057

## Solution:

```c#
 public class Solution
{
	public bool CanVisitAllRooms(IList<IList<int>> rooms)
	{
		//maintain a queue of rooms which we can open
		var opened = new bool[rooms.Count];
		var queue = new Queue<int>();
		queue.Enqueue(0);
		
		while(queue.Count > 0)
		{
			var top = queue.Dequeue();
			if (opened[top]) continue; //already opened
			opened[top] = true;
			foreach (var key in rooms[top])
			{
				queue.Enqueue(key);
			}
		}

		return !opened.Any(x => x == false);
	}
}
```