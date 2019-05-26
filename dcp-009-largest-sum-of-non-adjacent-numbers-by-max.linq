<Query Kind="Program" />

// This problem was asked by Airbnb.
// 
// Given a list of integers, write a function that returns the largest sum of non-adjacent numbers. Numbers can be 0 or negative.
// 
// For example, [2, 4, 6, 2, 5] should return 13, since we pick 2, 6, and 5. [5, 1, 1, 5] should return 10, since we pick 5 and 5.
// 
// Follow-up: Can you do this in O(N) time and constant space?

void Main()
{
	Func<int[], int> solver = GetMaxSum;
	
	solver(new[] { 2, 5, 6, }).Dump("8");
	solver(new[] { 2, 5, 4, }).Dump("6"); // fuck!
	solver(new[] { 2, 4, 6, 2, 5}).Dump("13");
	solver(new[] { 1, 0, 1, 0, 0, 1, 0, 1, 0 }).Dump("4");
	solver(new[] { 5, 1, 1, 5 }).Dump("10");
	solver(new[] { 1, 5, 5, 1 }).Dump("6");
	solver(new[] { 1, 5, 1, 5 }).Dump("10");
	solver(new[] { 1, 5, 1, 1, 5, 1, 1, 5 }).Dump("15");
	solver(new[] { -1, -1, 7, -1, -1, }).Dump("7");
	solver(new[] { 2, 1, 1, 1, 1, 1 }).Dump("4");
	solver(new[] { 2, 1, 1, 1, 1, 3 }).Dump("6");
	solver(new[] { 2, 1, 1, 1, 1, 1, 1, 1}).Dump("5");
	solver(new[] { 2, 1, 1, 1, 1, 1, 1, 3 }).Dump("7");
	solver(new[] { 2, 1, 1, 1, 1, 1, 1, 1, 3 }).Dump("8");
	solver(new[] { 3, 1, 1, 3, 1, 1, 3, 1 }).Dump("9");
	solver(new[] { -1, -1, -1, -1, -1, 1, 2, -1 }).Dump("2");
	solver(new[] { -1, -1, -1, -1, -1, -1, 1, 2, -1 }).Dump("2");
	solver(new[] { -1, -1, -1, -1, -1, 1, 1, 2, -1 }).Dump("3");
	solver(new[] { -1, -1, -1, -1, -1, 2, 1, 2, -1 }).Dump("4");
	solver(new[] { -1, -1, -1, -1, 2, 1, 2, -1 }).Dump("4");
}

int GetMaxSum(int[] a)
{
	return GetMaxSum(a, 0).currentSum;
}

(int currentSum, int oneStepAheadSum) GetMaxSum(int[] a, int i)
{
	if (i >= a.Length) return (0, 0);

	if (i == a.Length - 1) return (Math.Max(0, a[i]), 0);

	var (oneStepAheadSum, twoStepsAheadSum) = GetMaxSum(a, i + 1);

	// compare two options: we either use oneStepAheadSum (and skip i-th item) 
	// or use twoStepsAheadSum but in this case we can include i-th item (because it's not adjacent)
	var maxSum = Math.Max(oneStepAheadSum, a[i] + twoStepsAheadSum);

	return (maxSum, oneStepAheadSum);
}