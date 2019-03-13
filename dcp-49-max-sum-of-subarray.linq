<Query Kind="Program" />

// This problem was asked by Amazon.
// 
// Given an array of numbers, find the maximum sum of any
// contiguous subarray of the array.
// 
// For example, given the array [34, -50, 42, 14, -5, 86],
// the maximum sum would be 137, since we would take
// elements 42, 14, -5, and 86.
// 
// Given the array [-5, -1, -8, -9], the maximum sum would
// be 0, since we would not take any elements.
// 
// Do this in O(N) time.

void Main()
{
	MaxSum(new[] { 34, -50, 42, 14, -5, 86 }).Dump("137");
	MaxSum(new[] { -5, -1, -8, -9 }).Dump("0");
	MaxSum(new[] { 5, 10, -100, 15, 5, -100, 20, 30, -100, 40 }).Dump("50");
	MaxSum(new[] { 9, 5, 8, -20, 5, -10, 5, 4 }).Dump("22");
	MaxSum(new[] { -1, -2, -3, 5, 1, -4, -5 }).Dump("6");
	MaxSum(new[] { 1, 2, 3, -5, 3, 2, 1 }).Dump("7");
}

int MaxSum(int[] a)
{
	// algorithm is simple go through the array
	// add current element to running max
	// if it becomes less than zero, zero it out
	// maintain max of all running max values
	// time O(n), space O(1)
	
	// it looks like what I ended up with is called "Kadane's algorithm"
	// https://en.wikipedia.org/wiki/Maximum_subarray_problem#Kadane's_algorithm
	
	int overallMax = 0;
	int runningMax = 0;
	
	for (int i = 0; i < a.Length; i++)
	{
		runningMax += a[i];
		
		runningMax = Math.Max(0, runningMax);
		overallMax = Math.Max(runningMax, overallMax);
	}
	
	return overallMax;
}