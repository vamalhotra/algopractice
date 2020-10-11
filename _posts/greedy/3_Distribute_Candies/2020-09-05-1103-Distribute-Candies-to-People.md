---
layout: post
title: Distribute Candies To People
preview: Easy  We distribute some number of candies to a row of n  numpeople people in the following way  We then give 1 candy to
date: 2020-09-05 23:57:52 +0000
categories: greedy
lcid: 1103
---

# 1103. Distribute Candies to People

Easy

We distribute some number of `candies`, to a row of **`n = num_people`** people in the following way:

We then give 1 candy to the first person, 2 candies to the second person, and so on until we give `n` candies to the last person.

Then, we go back to the start of the row, giving `n + 1` candies to the first person, `n + 2` candies to the second person, and so on until we give `2 * n` candies to the last person.

This process repeats (with us giving one more candy each time, and moving to the start of the row after we reach the end) until we run out of candies. The last person will receive all of our remaining candies (not necessarily one more than the previous gift).

Return an array (of length `num_people` and sum `candies`) that represents the final distribution of candies.

 

**Example 1:**

```
Input: candies = 7, num_people = 4
Output: [1,2,3,1]
Explanation:
On the first turn, ans[0] += 1, and the array is [1,0,0,0].
On the second turn, ans[1] += 2, and the array is [1,2,0,0].
On the third turn, ans[2] += 3, and the array is [1,2,3,0].
On the fourth turn, ans[3] += 1 (because there is only one candy left), and the final array is [1,2,3,1].
```

**Example 2:**

```
Input: candies = 10, num_people = 3
Output: [5,2,3]
Explanation: 
On the first turn, ans[0] += 1, and the array is [1,0,0].
On the second turn, ans[1] += 2, and the array is [1,2,0].
On the third turn, ans[2] += 3, and the array is [1,2,3].
On the fourth turn, ans[0] += 4, and the final array is [5,2,3].
```

 

**Constraints:**

- 1 <= candies <= 10^9
- 1 <= num_people <= 1000

Accepted

50,515

Submissions

79,409

## Solution:
```c#
public class Solution
{
	/*
	Time complexity:
	
	Calculate total iterations. 
	Let n be total iterations in which i-candies are given in i-th iteration. Let's find upper bound on n in terms of input 'candies'
	
	1 + 2 + 3 + ... n >= candies
n(n+1)/2 >= candies
(n^2 + n)/2 >= candies

n^2/2 < candies <  (n+1)^2/2

n^2 < 2*candies
n < sqrt(2*candies) ~ O(sqrt(candies))

	*/
	public int[] DistributeCandiesApproach1(int candies, int num_people)
	{
		var arrCandies = new int[num_people];
		for(int i=0; i<num_people; ++i)
		{
			arrCandies[i] = 0;
		}

		int bucketSize = 1;
		
		while(candies > 0)
		{
			for(int i=0; i<num_people; ++i)
			{
				if(bucketSize > candies)
				{
					bucketSize = candies;
				}
				arrCandies[i] += bucketSize;
				candies -= bucketSize;
				++bucketSize;
			}
		}
		return arrCandies;
	}

	public int[] DistributeCandiesApproach2(int candies, int num_people)
	{
		var arrCandies = new int[num_people];
		for (int i = 0; i < num_people; ++i)
		{
			arrCandies[i] = 0;
		}

		//Each person receives 1 more candy than previous person
		int candyIndex = 0, candyBucketSize = 1;
		for (; 
			candyIndex < candies; 
			candyIndex += candyBucketSize, ++candyBucketSize)
		{
			arrCandies[(candyBucketSize-1) % num_people] += candyBucketSize;
		}

		//Last person might have received more candies than actual.
		//We can either do Min(candyBucketSize, candies-candyIndex) and avoid alloting more or 
		//we can adjust later outside the loop as below or
		//loop condition can be candyIndex < (candies - candyBucketSize) and then assign remaining candies to last person
		if(candyIndex > candies)
		{
			var lastPersonIdx = (candyBucketSize - 1 - 1) % num_people;
			arrCandies[lastPersonIdx] -= (candyIndex - candies);
		}

		return arrCandies;
	}

	/// <summary>
	/// Sum of AP [a, a+d, a+2d, ..., a+(n-1)*d] = n/2(2*a + (n-1)*d)
	/// </summary>
	/// <param name="candies"></param>
	/// <param name="num_people"></param>
	/// <returns></returns>
	public int[] DistributeCandiesApproach3(int candies, int num_people)
	{
		var arrCandies = new int[num_people];

		//Let's calculate total_iters. In all but last iteration, everyone will receive their full share. In last iteration, only some people will receive their full share.

		//We distribute these many candies in first iter
		int candiesInFirstIter = (num_people) * (num_people + 1) / 2;

		//Candy distribution in each iter forms an AP with difference as:
		int differenceInEachIter = num_people * num_people;

		//We will make at least 1 iter
		int totalIters = 1;

		//We will distribute at most these candies
		int temp = candiesInFirstIter;

		//temp is count of total candies distributed.
		//We add next term given by (a+n-1*d) for nth iter
		while(temp < candies)
		{
			++totalIters;
			temp += (candiesInFirstIter + (totalIters - 1) * differenceInEachIter);
		}

		//Candies distributed till now
		int totalCandies = 0;
		
		//O(num_people)
		for (int i = 0; i < num_people; ++i)
		{
			var a = i+1;
			var d = num_people;
			int n = totalIters-1;
			arrCandies[i] = n * (2 * a + (n - 1) * d) / 2;
			totalCandies += arrCandies[i];
		}

		//last person received in last iter these many candies
		//This is just last element in AP series for last person
		//a+(n-1)*d where a = num_people for last person
		//n = totalIter-1
		//d = num_people
		int lastBucketSize = num_people + (totalIters-2)*num_people;

		//Let's perform the last iter
		for (int i = 0; i < num_people; ++i)
		{
			++lastBucketSize; //next person to receive one more than prev.

			//running out of candies?
			if(lastBucketSize+totalCandies >= candies)
			{
				arrCandies[i] += candies - totalCandies;
				break;
			}
			else
			{
				arrCandies[i] += lastBucketSize;
				totalCandies += lastBucketSize;
			}
		}
		
		return arrCandies;
	}
}

```