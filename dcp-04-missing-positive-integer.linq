<Query Kind="Program" />

// This problem was asked by Stripe.
// Given an array of integers, find the first missing positive integer
// in linear time and constant space. In other words, find the lowest 
// positive integer that does not exist in the array. The array can contain duplicates and negative numbers as well.
// 
// For example, the input [3, 4, -1, 1] should give 2. The input [1, 2, 0] should give 3.
// You can modify the input array in-place.


void Main()
{
	f2(new[] { 3, 4, -1, 1 }).Dump("2");
	f2(new[] { 1, 2, 0 }).Dump("3");
	f2(new[] { 5, 3, 2, 1, }).Dump("4");
	f2(new[] { 5, 4, 2, 1, }).Dump("3");
	f2(new[] { 5, 4, 3, 2, }).Dump("1");
	f2(new[] { 4, 3, 2, 1, }).Dump("5");
	f2(new[] { -4, -3, -2, -1, }).Dump("1");
}

public int f(int[] a)
{
	// problem -- space: O(n)
	HashSet<int> set = new HashSet<int>();
	
	// time: O(n)
	foreach (var x in a)
		set.Add(x); // O(1)
	
	// time: O(n)
	for (int i = 1; true; i++)
		if (!set.Contains(i)) // O(1)
			return i;
}

public int f2(int[] a)
{
	// when n is amount of elements, answer is less than or equal (n + 1)
	// given we can use an array as memory, let use sign of the number to encode what we want to store
	// let say that negative number at index i means that (i + 1) is present
	// since we do not care about about not positive numbers, let's replace them with something bigger than n first

	int n = a.Length;

	// zero pass - get rid of non positive
	for (int i = 0; i < n; i++)
		if (a[i] <= 0)
			a[i] = n + 1;
	
	// first pass - counting
	for (int i = 0; i < n; i++)
	{
		int val = Math.Abs(a[i]);
		
		if (val <= n)
		{
			// store that "val" is present as a "-" sign (if not yet negative)
			
			if (a[val - 1] > 0)
				a[val - 1] *= -1;
		}
	}
	
	//a.Dump();
	
	// second pass - figure out missing
	for (int i = 0; i < n; i++)
		if (a[i] > 0)
			return i + 1;
	
	return n + 1;
}