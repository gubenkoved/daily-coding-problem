<Query Kind="Program" />

// This problem was asked by Uber.
// 
// Suppose an array sorted in ascending order is rotated at some pivot
// unknown to you beforehand. Find the minimum element in O(log N) time.
// You may assume the array does not contain duplicates.
// 
// For example, given [5, 7, 10, 3, 4], return 3.

void Main()
{
	// we can find an offset using something like binary search

	FindMin(new[] { 5, 7, 10, 3, 4 }).Dump("3");
	FindMin(new[] { 1, 2, 3, 4, 5, 6, }).Dump("1");
	FindMin(new[] { 6, 1, 2, 3, 4, 5 }).Dump("1");
	FindMin(new[] { 2, 3, 4, 5, 6, 1 }).Dump("1");
	FindMin(new[] { 4, 5, 6, 1, 2, 3 }).Dump("1");
	FindMin(new[] { 2, 2, 2, 3, 3, 3 }).Dump("2");
	FindMin(new[] { 3, 3, 3, 1, 2 }).Dump("1");
}

int FindMin(int[] a)
{
	int l = 0;
	int r = a.Length - 1;
	
	int offset = 0;
	
	// edge case - offset is 0

	if (a[l] > a[r])
	{
		while (true)
		{
			if (r - l == 1)
			{
				offset = r;
				break;
			}

			// okay, moving along!
			int mid = (l + r) / 2;
			
			if (a[l] > a[mid])
				r = mid;
			else
				l = mid;
		}
	}
	
	return a[offset];
}
