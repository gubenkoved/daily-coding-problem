<Query Kind="Program" />

// This problem was asked by WhatsApp.
// 
// Given an array of integers out of order, determine the bounds of the smallest
// window that must be sorted in order for the entire array to be sorted. For
// example, given [3, 7, 5, 6, 9], you should return (1, 3).

void Main()
{
	SmallestWindowToBeSorted(new [] { 3, 7, 5, 6, 9 }).Dump();
	SmallestWindowToBeSorted(new [] { 1, 2, 3, 4, 5 }).Dump();
	SmallestWindowToBeSorted(new [] { 1, 2, 3, 5, 4 }).Dump();
	SmallestWindowToBeSorted(new [] { 5, 4, 3, 2, 1 }).Dump();
}

(int from, int to) SmallestWindowToBeSorted(int[] a)
{
	int[] minToRight = new int[a.Length];
	int runningMin = a.Length;
	
	for (int i = a.Length - 1; i >= 0; i--)
	{
		runningMin = Math.Min(runningMin, a[i]);
		
		minToRight[i] = runningMin;
	}

	int[] maxToLeft = new int[a.Length];
	int runningMax = 0;

	for (int i = 0; i < a.Length; i++)
	{
		runningMax = Math.Max(runningMax, a[i]);

		maxToLeft[i] = runningMax;
	}

	// okay, now we srink boundries of array to be sorted

	int from = 0;
	int to = a.Length - 1;
	
	while (a[from] <= minToRight[from] && from < a.Length - 1)
		from += 1;

	while (a[to] >= maxToLeft[to] && to > 0)
		to -= 1;
		
	if (from > to)
		return (-1, -1); // already sorted!

	return (from, to);
}
