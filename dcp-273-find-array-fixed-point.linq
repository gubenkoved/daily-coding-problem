<Query Kind="Program" />

// This problem was asked by Apple.
// 
// A fixed point in an array is an element whose value is equal to its index. Given a sorted array of distinct elements, return a fixed point, if one exists. Otherwise, return False.
// 
// For example, given [-6, 0, 2, 40], you should return 2. Given [1, 5, 7, 8], you should return False.

void Main()
{
	FixedPoint(new[] { -6, 0, 2, 40 }).Dump("2");
	FixedPoint(new[] { 1, 5, 7, 8 }).Dump("does not exist");
}

// O(n)
// but can be improved with a binry search to O(logn)
// start with the center and see if the value greater or smaller than the index and
// the dive into the corresponding half
int FixedPoint(int[] a)
{
	for (int i = 0; i < a.Length; i++)
	{
		if (a[i] == i)
			return i;
	}
	
	return -1;
}
