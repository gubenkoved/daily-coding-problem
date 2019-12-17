<Query Kind="Program" />

// This problem was asked by Microsoft.
// 
// Given a string and a pattern, find the starting indices of all occurrences
// of the pattern in the string. For example, given the string "abracadabra"
// and the pattern "abr", you should return [0, 7].

void Main()
{
	FindAll("abracadabra", "abr").Dump();
}

IEnumerable<int> FindAll(string s, string pattern)
{
	for (int i = 0; i < s.Length - pattern.Length + 1; i++)
	{
		if (s.Substring(i, pattern.Length) == pattern)
			yield return i;
	}
}


