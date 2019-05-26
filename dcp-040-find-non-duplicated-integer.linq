<Query Kind="Program" />

// This problem was asked by Google.
// 
// Given an array of integers where every integer occurs three times
// except for one integer, which only occurs once, find and return the non-duplicated integer.
// 
// For example, given [6, 1, 3, 3, 3, 6, 6], return 1. Given [13, 19, 13, 13], return 19.
// 
// Do this in O(N) time and O(1) space.

void Main()
{
	// good thing is that in array of n elements there will be
	// only n/3 + 1 unique values
	// but still we can not count them separately as it will require
	// linear amount of space
	
	// did not solve this one myself...
	// found the solution there: https://www.geeksforgeeks.org/find-the-element-that-appears-once/
	
	FindNonDuplicated(new[] { 1, 1, 1, 0 }).Dump("0");
	FindNonDuplicated(new[] { 1, 1, 1, 2 }).Dump("2");
	FindNonDuplicated(new[] { 6, 1, 3, 3, 3, 6, 6 }).Dump("1");
	FindNonDuplicated(new[] { 13, 19, 13, 13 }).Dump("19");
}

int FindNonDuplicated(int[] a)
{
	// idea is to sum all the numbers bitwise (!)
	// and then compute mod 3 to get a resulting bit

	int result = 0;

	for (int bit = 0; bit < 32; bit++)
	{
		int mask = 1 << bit;

		int mod3Sum = 0;

		for (int i = 0; i < a.Length; i++)
			mod3Sum = (mod3Sum + ((a[i] & mask) >> bit)) % 3;
		
		result |= mod3Sum << bit;
	}
	
	return result;
}