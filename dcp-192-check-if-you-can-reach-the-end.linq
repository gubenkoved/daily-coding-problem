<Query Kind="Program" />

// This problem was asked by Google.
// 
// You are given an array of nonnegative integers. Let's say you start at the beginning of the array and are trying to advance to the end. You can advance at most, the number of steps that you're currently on. Determine whether you can get to the end of the array.
// 
// For example, given the array [1, 3, 1, 2, 0, 1], we can go from indices 0 -> 1 -> 3 -> 5, so return true.
// 
// Given the array [1, 2, 1, 0, 0], we can't reach the end, so return false.

void Main()
{
	Reachable(new[] { 1, 3, 1, 2, 0, 1 }).Dump("true");
	Reachable(new[] { 1, 2, 1, 0, 0 }).Dump("false");
}

bool Reachable(int[] a)
{
	bool[] r = new bool[a.Length];
	
	r[0] = true;
	
	for (int i = 0; i < a.Length; i++)
	{
		if (!r[i])
			continue;
			
		for (int d = 0; d <= a[i]; d++)
			if (i + d < a.Length)
				r[i + d] = true;
	}
	
	return r[a.Length - 1];
}
