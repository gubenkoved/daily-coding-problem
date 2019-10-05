<Query Kind="Program" />

// This problem was asked by Stripe.
// 
// Given a collection of intervals, find the minimum number of
// intervals you need to remove to make the rest of the intervals
// non-overlapping.
// 
// Intervals can "touch", such as [0, 1] and [1, 2], but they won't
// be considered overlapping.
// 
// For example, given the intervals (7, 9), (2, 4), (5, 8), return 1
// as the last interval can be removed and the first two won't overlap.
// 
// The intervals are not necessarily sorted in any order.

void Main()
{
	EraseOverlapIntervals(new[]
	{
		new [] { 0, 10 },
		new [] { 1, 2 },
		new [] { 3, 4 },
		new [] { 5, 6 },
	}).Dump("1");

	EraseOverlapIntervals(new[]
	{
		new [] { 0, 1 },
		new [] { 1, 2 },
	}).Dump("0");

	EraseOverlapIntervals(new[]
	{
		new [] { 7, 9 },
		new [] { 2, 4 },
		new [] { 5, 8 },
	}).Dump("1");

	EraseOverlapIntervals(new[]
	{
		new [] { 10, 12 },
		new [] { 2, 4 },
		new [] { 5, 8},
	}).Dump("0");

	EraseOverlapIntervals(new[]
	{
		new [] { 1, 2 },
		new [] { 3, 4 },
		new [] { 5, 6 },
		new [] { 0, 10 },
	}).Dump("1");

	EraseOverlapIntervals(new[]
	{
		new [] { 1, 2 },
		new [] { 1, 3 },
		new [] { 1, 4 },
		new [] { 1, 5 },
	}).Dump("3");
}

public int EraseOverlapIntervalsDoesNotWork(int[][] intervals)
{

	// looks like greedy algorithm will work there
	// we just need to compute amount of intervals each interval
	// intersects with and start removing one by one startign the interval
	// that crosses the biggest amount
	// we would need to maintain counters

	Dictionary<int, List<int>> intersections = new Dictionary<int, List<int>>();


	for (int i = 0; i < intervals.Length; i += 1)
	{
		intersections[i] = new List<int>();

		for (int j = 0; j < intervals.Length; j += 1)
		{
			if (i == j)
				continue;

			if (Intersects(intervals[i][0], intervals[i][1], intervals[j][0], intervals[j][1]))
				intersections[i].Add(j);
		}
	}

	int toDelete = 0;

	while (true)
	{
		int? candidate = null;

		for (int i = 0; i < intervals.Length; i += 1)
		{
			if (!intersections.ContainsKey(i) || !intersections[i].Any())
				continue;

			if (candidate == null || intersections[i].Count > intersections[candidate.Value].Count)
				candidate = i;
		}

		if (candidate == null)
			break;

		toDelete += 1;

		foreach (var j in intersections[candidate.Value])
			intersections[j].Remove(candidate.Value);

		intersections.Remove(candidate.Value);
	}

	return toDelete;
}

public bool Intersects(int x1, int x2, int y1, int y2)
{
	return x1 < y2 && y1 < x2;
}


// working solution that I had to steal as I was unable to figure this one out till the end...
public int EraseOverlapIntervals(int[][] intervals)
{
	intervals = intervals.OrderBy(x => x[0]).ToArray();

	int end = intervals[0][1];
	int min = 0;

	for (int i = 1; i < intervals.Length; i++)
	{
		if (intervals[i][0] < end)
		{
			end = Math.Min(end, intervals[i][1]);
			min++;
		}
		else
		{
			end = intervals[i][1];
		}
	}

	return min;
}