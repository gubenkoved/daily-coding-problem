<Query Kind="Program" />

// This problem was asked by Google.
// 
// Given a string, return the first recurring character in it,
// or null if there is no recurring character.
// 
// For example, given the string "acbbac", return "b". Given
// the string "abcdef", return null.

void Main()
{
	FirstRecurring("acbbac").Dump("b");
	FirstRecurring("abcdef").Dump("null");
}

char? FirstRecurring(string s)
{
	char prev = s[0];
	
	for (int i = 1; i < s.Length; i++)
	{
		if (s[i] == prev)
			return s[i];
			
		prev = s[i];
	}
	
	return null;
}
