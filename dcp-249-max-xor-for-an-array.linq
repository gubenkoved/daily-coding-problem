<Query Kind="Program" />

// This problem was asked by Salesforce.
// 
// Given an array of integers, find the maximum XOR of any two elements.

void Main()
{
	// task probably asks to find something better than obvious O(n^2)
	// but it's silent about it
	
	MaxXor(new[] { 1, }).Dump();
	MaxXor(new[] { 1, 2, }).Dump();
	MaxXor(new[] { 1, 2, 3, 4, }).Dump();
	MaxXor(new[] { 1, 2, 3, 4, 5, 6 }).Dump();
}

// O(n^2)
int MaxXor(int[] a)
{
	int maxXor = int.MinValue;
	
	foreach (int x in a)
	{
		foreach (int y in a)
		{
			if ((x ^ y) > maxXor)
				maxXor = x ^ y;
		}
	}
	
	return maxXor;
}
