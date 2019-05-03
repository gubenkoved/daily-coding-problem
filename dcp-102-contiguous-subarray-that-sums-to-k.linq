<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Numerics.dll</Reference>
  <Namespace>System.Numerics</Namespace>
</Query>

// This problem was asked by Lyft.
// 
// Given a list of integers and a number K, return
// which contiguous elements of the list sum to K.
// 
// For example, if the list is [1, 2, 3, 4, 5] and
// K is 9, then it should return [2, 3, 4], since 2 + 3 + 4 = 9.

void Main()
{
	// simplest way is just to enumerate via all subarrays
	// there are O(n*n) of them: 1 (of len n) + 2 (of len n-1) + 3 + ... + n (of len 1) = [(1 + n) / 2] * n
	// and naive sum caluclation takes ~n operations
	// so total is O(n*n*n)
	// but is there a faster and more elegant solution?
	// we can optimize calculating a sum and make it O(1) via preparing
	// helper partial sums
	// it makes it O(n*n)...
	// may be there is O(n) solution though...
	// postmortem:
	// (based on https://www.geeksforgeeks.org/find-subarray-with-given-sum/)
	// I've overcomplicated that, evenwithout partial sum trick it's easy
	// to make O(n*n) solution
	// and there O(n) solution I actually WAS thinking about
	// which will work only when numbers are all non-negative
	// in a nutshel it's an optimization that tries to keep the "window"
	// size as close to K as possible without blindly iterating all
	// the windows possible

	SubArrayWithSum(new[] { 1, 2, 3, 4, 5 }, 9).Dump();
	SubArrayWithSum(new[] { 1, 2, 3, 4, 5 }, 3).Dump();
	SubArrayWithSum(new[] { 1, 2, 3, 4, 5, 6 }, 11).Dump();
	SubArrayWithSum(new[] { 1, 2, 3, 4, 5, 6 }, 6).Dump();
	SubArrayWithSum(new[] { 1, 2, 3, 4, 5, 6 }, 666).Dump();
}

int[] SubArrayWithSum(int[] a, int k)
{
	int n = a.Length;

	BigInteger[] left = new BigInteger[n]; // left[i] = a[1] + a[2] + .. + a[i]
	BigInteger[] right = new BigInteger[n]; // right[i] = a[i] + a[i+1] + .. + a[n]

	// left partial sums
	left[0] = a[0];
	
	for (int i = 1; i < n; i++)
		left[i] = left[i - 1] + a[i];

	// precalcuate right partial sums
	right[n - 1] = a[n - 1];

	for (int i = n - 2; i >= 0; i--)
		right[i] = right[i + 1] + a[i];

	// get the total
	BigInteger total = 0;

	for (int i = 0; i < n; i++)
		total += a[i];

	// calculation pass
	for (int start = 0; start < n; start++)
	{
		for (int end = start; end < n; end++)
		{
			BigInteger sum = total;
			
			if (start > 0)
				sum -= left[start - 1];
				
			if (end <= n - 2)
				sum -= right[end + 1];
				
			if (sum == k)
			{
				int[] result = new int[end - start + 1];
				Array.Copy(a, start, result, 0, end - start + 1);
				return result;
			}
		}
	}
	
	// if we get there, we did not find an answer
	return null;
}
