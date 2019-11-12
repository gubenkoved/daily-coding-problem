<Query Kind="Program" />

// This problem was asked by Google.
// 
// You are given an array of arrays of integers, where each array corresponds to a row
// in a triangle of numbers. For example, [[1], [2, 3], [1, 5, 1]] represents the triangle:
// 
//   1
//  2 3
// 1 5 1
// We define a path in the triangle to start at the top and go down one row at a time to an
// adjacent value, eventually ending with an entry on the bottom row. For example, 1 -> 3 -> 5.
// The weight of the path is the sum of the entries.
// 
// Write a program that returns the weight of the maximum weight path.

void Main()
{
	MaxPathWeight(new[] { new[] { 1 }, new[] { 2, 3, }, new[] { 1, 5, 1 } }).Dump("9");
	MaxPathWeight(new[] { new[] { 1 }, new[] { 2, 3, }, new[] { 4, 5, 6 } }).Dump("10");
	MaxPathWeight(new[] { new[] { 1 }, new[] { 1, 1, }, new[] { 1, 1, 1 } }).Dump("3");
	MaxPathWeight(new[] { new[] { 1 }, new[] { 1, 1, }, new[] { 1, 2, 1 } }).Dump("4");
	MaxPathWeight(new[] { new[] { 1 }, new[] { 10, 1, }, new[] { 1, 2, 100 } }).Dump("102");
}

int MaxPathWeight(int[][] a)
{
	// a[level][i] is adjacent with a[level + 1][i] and a[level + 1][i + 1]
	
	return MaxPathWeight(a, 0, 0);
}

int MaxPathWeight(int[][] a, int level, int i)
{
	if (a.Length == level + 1)
		return a[level][i]; // leaf!
		
	// can dive into it!
	
	int leftMax = MaxPathWeight(a, level + 1, i);
	int rightMax = MaxPathWeight(a, level + 1, i + 1);
	
	return Math.Max(leftMax, rightMax) + a[level][i];
}