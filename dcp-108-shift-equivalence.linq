<Query Kind="Program" />

// This problem was asked by Google.
// 
// Given two strings A and B, return whether or not
// A can be shifted some number of times to get B.
// 
// For example, if A is abcde and B is cdeab, return
// true. If A is abc and B is acb, return false.

void Main()
{
	Equivalent("abcde", "cdeab").Dump("true");
	Equivalent("abc", "acb").Dump("false");
}

bool Equivalent(string a, string b)
{
	if (a.Length != b.Length)
		return false;
		
	for (int i = 0; i < a.Length; i++)
		if (Equals(a, b, i))
			return true;
	
	return false;
}

bool Equals(string a, string b, int offset)
{
	int n = a.Length;
	
	for (int i = 0; i < n; i++)
		if (a[i] != b[(i + offset) % n])
			return false;
	
	return true;
}
