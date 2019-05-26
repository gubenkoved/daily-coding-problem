<Query Kind="Program" />

// This problem was asked by Amazon.
// 
// An sorted array of integers was rotated an unknown number
// of times.
// 
// Given such an array, find the index of the element in the
// array in faster than linear time. If the element doesn't
// exist in the array, return null.
// 
// For example, given the array [13, 18, 25, 2, 8, 10] and
// the element 8, return 4 (the index of 8 in the array).
//
// You can assume all the integers in the array are unique.

void Main()
{
	// we can try to find an offset for the array for O(logn)
	// then we can just do the binary search for O(logn)
	
	Offset(new int[] { 2, 8, 10, 13, 18, 25, }).Dump();
	Offset(new int[] { 13, 18, 25, 2, 8, 10 }).Dump();
	Offset(new int[] { 8, 10, 13, 18, 25, 2, }).Dump();
	
	BinarySerachWithOffset(new int[] { 13, 18, 25, 2, 8, 10 }, 25).Dump("2");
	BinarySerachWithOffset(new int[] { 13, 18, 25, 2, 8, 10 }, 18).Dump("1");
	BinarySerachWithOffset(new int[] { 13, 18, 25, 2, 8, 10 }, 10).Dump("5");
	
}

// O(logn)
int BinarySerachWithOffset(int[] a, int val)
{
	// O(logn)
	int offset = Offset(a);
	
	// O(logn)
	return BinarySearch(a, offset, val);
}

int Offset(int[] a)
{
	return Offset(a, 0, a.Length - 1);
}

int Offset(int[] a, int l, int r)
{
	// while item on the left side is larger than on the right
	// we will split range in two and find the one with same property
	
	if (r - l == 1)
		return l + 1;
		
	if (a[l] <= a[r])
		return l;
		
	int m = (r + l) / 2;
	
	if (a[l] > a[m])
		return Offset(a, l, m);
	else
		return Offset(a, m, r);
}

int BinarySearch(int[] a, int offset, int val)
{
	return BinarySearch(a, offset, val, 0, a.Length - 1);
}

int BinarySearch(int[] a, int offset, int val, int l, int r)
{
	// just like regular binary search, but when we go to the array do
	// conversion considering offset
	Func<int, int> t = x => (x - offset + a.Length) % a.Length;
	
	int m = (l + r) / 2;
	
	if (r - l == 1)
	{
		if (a[t(l)] == val)
			return t(l);
		else if (a[t(r)] == val)
			return t(r);
		else
			return -1;
	}
	
	if (val <= a[t(m)])
		return BinarySearch(a, offset, val, l, m);
	else
		return BinarySearch(a, offset, val, m, r);
}
