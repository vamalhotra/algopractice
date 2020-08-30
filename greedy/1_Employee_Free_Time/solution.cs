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