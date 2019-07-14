<Query Kind="Program" />

// This problem was asked by Google.
// 
// Given an array of numbers and an index i, return the index of the nearest
// larger number of the number at index i, where distance is measured in array indices.
// 
// For example, given [4, 1, 3, 5, 6] and index 0, you should return 3.
// 
// If two distances to larger numbers are the equal, then return any one of them.
// If the array at i doesn't have a nearest larger integer, then return null.
// 
// Follow-up: If you can preprocess the array, can you do this in constant time?

void Main()
{
	NearestLargerNumber(new[] { 4, 1, 3, 5, 6 }, 0).Dump("3");

	// answer to the follow-up:
	// we can find an answer for original question in O(n)
	// so the most straightforward way is to solve for each index
	// and prepare the map for all the indexes for O(n*n)
	// but it feels like they are talking about more sophisticated solution
	// and something tells me it is going to be O(n)
	
	// Naive preprocessing -- O(n*n)
	// int[] a = new[] { 4, 1, 3, 5, 6};
	// 
	// var map = Enumerable.Range(0, a.Length).ToDictionary(idx => idx, idx => NearestLargerNumber(a, idx));
	// 
	// map.Dump();
}

// O(n)
int? NearestLargerNumber(int[] a, int idx)
{
	for (int dist = 0; dist < a.Length - 1; dist++)
	{
		int l = idx - dist;
		int r = idx + dist;
		
		if (l > 0 && a[l] > a[idx])
			return l;

		if (r < a.Length && a[r] > a[idx])
			return r;
	}
	
	return null;
}