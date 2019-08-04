<Query Kind="Program" />

// This problem was asked by Amazon.
// 
// Given a string, determine whether any permutation of it is a palindrome.
// 
// For example, carrace should return true, since it can be rearranged to
// form racecar, which is a palindrome. daily should return false, since
// there's no rearrangement that can form a palindrome.

void Main()
{
	CanBePalindromized("carrace").Dump("true");
	CanBePalindromized("daily").Dump("false");
}

bool CanBePalindromized(string s)
{
	Dictionary<char, int> counts = new Dictionary<char, int>();
	
	foreach (var c in s)
	{
		if (!counts.ContainsKey(c))
			counts[c] = 0;
		
		counts[c] += 1;
	}
	
	// okay now make sure that there is maximum 1 char with non-even count
	return counts.Values.Count(x => x % 2 != 0) <= 1;
}
