<Query Kind="Program" />

// This problem was asked by Twitter.
// 
// Given a list of numbers, create an algorithm that arranges
// them in order to form the largest possible integer. For
// example, given [10, 7, 76, 415], you should return 77641510.

void Main()
{
	// https://leetcode.com/problems/largest-number/
	
	LargestPossibleInteger(new[] { 0, 0 }).Dump("0");
	LargestPossibleInteger(new[] { 121, 12 }).Dump("12121");
	LargestPossibleInteger(new[] { 10, 7, 76, 415 }).Dump("77641510");
	LargestPossibleInteger(new[] { 10, 77, 76, 415 }).Dump("777641510");
	LargestPossibleInteger(new[] { 10, 7, 78, 415 }).Dump("78741510");
	LargestPossibleInteger(new[] { 10, 12, 13, 14 }).Dump("14131210");
}

string LargestPossibleInteger(int[] nums)
{
	string result = string.Join("", nums.Select(x => x.ToString()).OrderByDescending(x => x, MyComparer.Instance));
	
	while (result.StartsWith("00"))
		result = result.Substring(1);
	
	return result;
}

public class MyComparer : IComparer<string>
{
	public static MyComparer Instance = new MyComparer();
	
	public int Compare(string x, string y)
	{
		return (x + y).CompareTo(y + x);
	}
}