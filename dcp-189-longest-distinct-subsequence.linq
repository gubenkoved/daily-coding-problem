<Query Kind="Program" />

// This problem was asked by Google.
// 
// Given an array of elements, return the length of the longest subarray where all its elements are distinct.
// 
// For example, given the array [5, 1, 3, 5, 2, 3, 4, 1], return 5 as the longest subarray of distinct elements is [5, 2, 3, 4, 1].

void Main()
{
	LongestDistinctSubArray(new[] { 5, 1, 3, 5, 2, 3, 4, 1 }).Dump();
	LongestDistinctSubArray(new[] { 1, 2, 3, 4, 5, 6 }).Dump();
	LongestDistinctSubArray(new[] { 1, 2, 3, 3, 4, 5, 6 }).Dump();
}

int[] LongestDistinctSubArray(int[] a)
{
	int l = 0;
	int r = 0;
	
	int bestL = 0;
	int bestR = 0;
	
	HashSet<int> current = new HashSet<int>();
	
	current.Add(a[0]);

	while (true)
	{

		// expand to the right as far as we can
		while (r < a.Length - 1 && !current.Contains(a[r + 1]))
		{
			current.Add(a[r + 1]);
			r += 1;
		}
		
		if (r - l > bestR - bestL)
		{
			bestR = r;
			bestL = l;
		}
		
		if (r == a.Length - 1)
		{
			// we are done!
			break;
		}
		
		// move left pointer until we get rid of the next char we need to consume
		while (current.Contains(a[r + 1]))
		{
			current.Remove(a[l]);
			l += 1;
		}
	}
	
	var result = new int[bestR - bestL + 1];
	
	Array.Copy(a, bestL, result, 0, bestR - bestL + 1);
	
	return result;
}
