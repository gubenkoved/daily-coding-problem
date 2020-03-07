<Query Kind="Program" />

// This problem was asked by Atlassian.
// 
// MegaCorp wants to give bonuses to its employees based on how many lines of
// codes they have written. They would like to give the smallest positive amount
// to each worker consistent with the constraint that if a developer has written
// more lines of code than their neighbor, they should receive more money.
// 
// Given an array representing a line of seats of employees at MegaCorp, determine
// how much each one should get paid.
// 
// For example, given [10, 40, 200, 1000, 60, 30], you should return [1, 2, 3, 4, 2, 1].

void Main()
{
	F(new[] { 10, 40, 200, 1000, 60, 30 }).Dump();
	F(new[] { 10, 40, 200, 1000, 100, 60, 30 }).Dump();
	F(new[] { 10, 40, 200, 1000, 500, 100, 60, 30 }).Dump();
	F(new[] { 10, 9, 8, 7, 6, 5 }).Dump();
	F(new[] { 10, 20, 30, 7, 40, 10, 5 }).Dump();
}

int[] F(int[] a)
{
	var r = new int[a.Length];

	// start with all ones (min amount)
	for (int i = 0; i < a.Length; i++)
		r[i] = 1;
	
	// fix each error by increasing the bonus if amount of lines
	// increasing
	for (int i = 1; i < a.Length; i++)
		if (a[i] > a[i - 1])
			r[i] = r[i - 1] + 1;

	// now handle the down slopes
	for (int i = a.Length - 2; i >= 0; i--)
		if (a[i] > a[i + 1] && r[i] <= r[i + 1])
			r[i] = r[i + 1] + 1;

	return r;
}
