<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// Given a circular array, compute its maximum subarray sum in O(n) time.
// A subarray can be empty, and in this case the sum is 0.
// 
// For example, given [8, -1, 3, 4], return 15 as we choose the numbers
// 3, 4, and 8 where the 8 is obtained from wrapping around.
// 
// Given [-4, 5, 1, 0], return 6 as we choose the numbers 5 and 1.

void Main()
{
	MaxCircularSubarraySum(new[] { 8, -1, 3, 4 }).Dump("15");
	MaxCircularSubarraySum(new[] { -4, 5, 1, 0}).Dump("6");
	MaxCircularSubarraySum(new[] { 1, 1, 1, -2, 1, 1, 1, }).Dump("6");
	MaxCircularSubarraySum(new[] { 1, 1, 1, -2, 1, 1, 1, -2, }).Dump("4");
}

int MaxCircularSubarraySum(int[] a)
{
	// seems we can actually solve this problem using a solution for non circular problem
	// in we will find the offset equal to the position of the minimal negatiove element which we will
	// virtuall place to the very begining of the shifted cersion of the array, making it useless
	// to cross the boundries of the array, because it would mean that we are using the the smallest 
	// negative element which can not increase the sum
	
	int offset = 0;
	
	for (int i = 0; i < a.Length; i++)
	{
		if (a[i] < 0 && a[i] < a[offset])
			offset = i;
	}
	
	// okay, now solve for regualr case using the offset
	// for array starting at offset and having original number of the elements
	
	int n = a.Length;
	
	Func<int, int> f = x => (x + offset) % n;
	
	int[] best = new int[a.Length];
	
	for (int i = 0; i < a.Length; i++)
	{
		// DP based approach
		// best[i] shows the best sum for the subarray from 0 till the i (offset to be applied)
		
		int cur = a[f(i)];
		
		if (i >= 1)
			cur += best[i - 1];
		
		// we cut off at 0 as we do not have to take these into account
		best[i] = cur > 0 ? cur : 0;
	}
	
	return best.Max();
}