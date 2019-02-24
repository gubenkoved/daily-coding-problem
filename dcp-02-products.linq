<Query Kind="Program" />

// This problem was asked by Uber.
// 
// Given an array of integers, return a new array such that each element at index i of the
//  new array is the product of all the numbers in the original array except the one at i.
// 
// For example, if our input was [1, 2, 3, 4, 5], the expected output would be
//  [120, 60, 40, 30, 24]. If our input was [3, 2, 1], the expected output would be [2, 3, 6].
// 
// Follow-up: what if you can't use division?

void Main()
{
	// using division problem can be solved with O(n) time
	// but what about case where we can not use division?
	// can we still solve it faster than O(n*n)?
	// Yes, see g, t: O(n), space: O(n)

	f(new long[] { 1, 2, 3, 4, 5 }).Dump();
	f(new long[] { 3, 2, 1 }).Dump();

	g(new long[] { 1, 2, 3, 4, 5 }).Dump();
	g(new long[] { 3, 2, 1 }).Dump();
}

long[] f(long[] a)
{
	long product = 1;
	
	for (int i = 0; i < a.Length; i++)
		product *= a[i];
		
	long[] result = new long[a.Length];
	
	for (int i = 0; i < a.Length; i++)
		result[i] = product / a[i];
	
	return result;
}

long[] g(long[] a)
{
	// low[i] = a[1] * a[2] ... * a[i]
	// hi[i] = a[n] * a[n - 1] ... * a[i]
	// result[i] = low[i - 1] * hi[i + 1]
	
	long[] low = new long[a.Length];
	long[] hi = new long[a.Length];
	
	low[0] = a[0];
	hi[a.Length - 1] = a[a.Length - 1];
	
	for (int i = 1; i < a.Length; i++)
		low[i] = low[i - 1] * a[i];

	for (int i = a.Length - 2; i >= 0; i--)
		hi[i] = hi[i + 1] * a[i];
		
	long[] result = new long[a.Length];
		
	for (int i = 0; i < a.Length; i++)
	{
		result[i] = 1;
		
		if (i > 0)
			result[i] *= low[i - 1];

		if (i < a.Length - 1)
			result[i] *= hi[i + 1];
	}
	
	return result;
}
