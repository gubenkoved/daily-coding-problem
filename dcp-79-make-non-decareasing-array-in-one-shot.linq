<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// Given an array of integers, write a function to
// determine whether the array could become non-decreasing
// by modifying at most 1 element.
// 
// For example, given the array [10, 5, 7], you should
// return true, since we can modify the 10 into a 1 to make
// the array non-decreasing.
// 
// Given the array [10, 5, 1], you should return false,
// since we can't modify any one element to get a non-decreasing array.

void Main()
{
	CanBeMadeNonDecreasingInOneShot(new[] { 10, 5, 7 }).Dump("true");
	CanBeMadeNonDecreasingInOneShot(new[] { 10, 5, 1 }).Dump("false");
	CanBeMadeNonDecreasingInOneShot(new[] { 10, 2, 3, 4, 5, 6, 7 }).Dump("ok");
	CanBeMadeNonDecreasingInOneShot(new[] { 3, 2, 3, 7, 5, 6, 70 }).Dump("should be false");
}

bool CanBeMadeNonDecreasingInOneShot(int[] a)
{
	int[] t = new int[a.Length];
	
	for (int i = 1; i < a.Length; i++)
	{
		if (a[i - 1] > a[i])
			t[i] = t[i - 1] + 1;
		else
			t[i] = t[i - 1];
	}
	
	// t.Dump();
	
	return t[a.Length - 1] <= 1;
}
