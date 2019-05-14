<Query Kind="Program" />

// This problem was asked by Pinterest.
// 
// Given an integer list where each number represents the
// number of hops you can make, determine whether you can
// reach to the last index starting at index 0.
// 
// For example, [2, 0, 1, 0] returns True while [1, 1, 0, 1]
// returns False.

void Main()
{
	f(new [] { 2, 0, 1, 0 }).Dump("true");
	f(new [] { 1, 1, 0, 1 }).Dump("false");
	f(new [] { 4, 4, 0, 0, 0, 2, 2, 0, 0 }).Dump("true");
	f(new [] { 3, 0, 0, 0, }).Dump("true");
	f(new [] { 2, 0, 0, 0, }).Dump("false");
	f(new [] { 0, }).Dump("true");
	f(new [] { 0, 0, }).Dump("false");
}

bool f(int[] d)
{
	return f(d, 0);
}

// backtracking search
bool f(int[] d, int idx)
{
	int n = d.Length;
	int c = d[idx];
	
	if (idx + c >= n - 1)
		return true; // the end is reachable
	
	// recoursively try to reach out from reachable from current cell
	for (int i = idx + 1; i <= idx + c; i++)
		if (f(d, i))
			return true;
	
	// did not find it
	return false;
}
