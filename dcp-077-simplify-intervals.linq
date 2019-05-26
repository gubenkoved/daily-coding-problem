<Query Kind="Program" />

// This problem was asked by Snapchat.
// 
// Given a list of possibly overlapping intervals, return
// a new list of intervals where all overlapping intervals
// have been merged.
// 
// The input list is not necessarily ordered in any way.
// 
// For example, given [(1, 3), (5, 8), (4, 10), (20, 25)],
// you should return [(1, 3), (4, 10), (20, 25)].

void Main()
{
	Simplify(new[]
	{
		new Interval(1, 3),
		new Interval(5, 8),
		new Interval(4, 10),
		new Interval(20, 25),
	}).Dump();
}

public struct Interval
{
	public int Start;
	public int End;
	
	public Interval(int start, int end)
	{
		Start = start;
		End = end;
	}
}

IEnumerable<Interval> Simplify(IEnumerable<Interval> intervals)
{
	Interval[] ordered = intervals.OrderBy(x => x.Start).ToArray();
	
	List<Interval> simplified = new List<UserQuery.Interval>();

	Interval lastInverval = ordered.First();
	
	foreach (Interval interval in ordered.Skip(1))
	{
		if (interval.Start <= lastInverval.End)
		{
			// extend the last one
			lastInverval = new Interval(lastInverval.Start, Math.Max(lastInverval.End, interval.End));
		} else
		{
			// add last interval to the list
			simplified.Add(lastInverval);
			
			lastInverval = interval;
		}
	}
	
	simplified.Add(lastInverval);
	
	return simplified;
}
