<Query Kind="Program" />

// his problem was asked by Palantir.
// 
// In academia, the h-index is a metric used to calculate the
// impact of a researcher's papers. It is calculated as follows:
// 
// A researcher has index h if at least h of her N papers have
// h citations each. If there are multiple h satisfying this
// formula, the maximum is chosen.
// 
// For example, suppose N = 5, and the respective citations of each
// paper are [4, 3, 0, 1, 5]. Then the h-index would be 3, since the
// researcher has 3 papers with at least 3 citations.
// 
// Given a list of paper citations of a researcher, calculate their
// h-index.

void Main()
{
	// https://leetcode.com/problems/h-index
	
	H(new[] { 4, 3, 0, 1, 5 }).Dump("3");
	H(new int[] { }).Dump("0");
	H(new [] { 0 }).Dump("0");
	H(new [] { 5, 5, 5, 5, 5, 0 }).Dump("5");
	H(new [] { 5, 5, 5, 5, 0 }).Dump("4");
	H(new[] { 3, 0, 6, 1, 5 }).Dump("3");
}

// O(n) time, O(n) space
int H(int[] a)
{
	int n = a.Length;
	int[] counts = new int[n + 1];
	
	for (int i = 0; i < n; i++)
	{
		int q = Math.Min(n, a[i]); // cut max to n, as it's the max we might get
		
		counts[q] += 1;
	}
	
	// now we cal compute cumulative counts
	int[] cumulative = new int[n + 1];
	
	for (int i = n; i >= 0; i--)
	{
		cumulative[i] += counts[i];
		
		if (i < n)
			cumulative[i] += cumulative[i + 1];

		if (cumulative[i] >= i)
			return i;
	}
	
	return 0; // should not happen if a is not empty
}