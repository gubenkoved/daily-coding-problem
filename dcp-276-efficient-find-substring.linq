<Query Kind="Program" />

// This problem was asked by Dropbox.
// 
// Implement an efficient string matching algorithm.
// 
// That is, given a string of length N and a pattern of length k, write
// a program that searches for the pattern in the string with less than
// O(N * k) worst-case time complexity.
// 
// If the pattern is found, return the start index of its location. If
// not, return False.

void Main()
{
	Func<string, string, int> f = IndexOf2;
	
	f("_test_", "test").Dump("1");
	f("thisistheteststring", "test").Dump("9");
	f("thisistheteststring", "test!").Dump("-1");
	f("aaabaaabaaabaaa", "aaaa").Dump("-1");
	f("aaabaaabaaabaaa", "aabaaaa").Dump("-1");
}

// worst-case: O(N * k)
int IndexOf(string s, string pattern)
{
	for (int i = 0; i < s.Length - pattern.Length + 1; i++)
	{
		bool matched = true;
		
		for (int j = i; j < i + pattern.Length; j++)
		{
			if (s[j] != pattern[j - i])
			{
				matched = false;
				break;
			}
		}
		
		if (matched)
			return i;
	}
	
	return -1;
}

// complexity is hard to tell... it won't do a good job on a very uniform type of the input
// like search 'aaaaabaaaa' when the input string is something like 'aaabaaaaaaaaabaaaaa'
// a lot of times hashes will be actually equal as they are position invariant
// and than makes then easily calculatable for the increment and it is equally useless for such cases...
// so, it probably does not improve the worst case complexity...
int IndexOf2(string s, string pattern)
{
	// the idea is to calculate window hash and check it against the 
	// target hash before doing the computationally expensive by-symbol matching
	
	int targetHash = Hash(pattern);
	int windowHash = Hash(s.Substring(0, pattern.Length));
	
	for (int i = 0; i < s.Length - pattern.Length + 1; i++)
	{
		// update window hash
		if (i > 0)
		{
			windowHash ^= s[i - 1]; // wipe out the effect of the prev char
			windowHash ^= s[i + pattern.Length - 1]; // add the last char in the new window
		}
		
		// if hashes are not equal -- no need to compare further
		if (targetHash != windowHash)
			continue;
		
		bool matched = true;
		
		for (int j = i; j < i + pattern.Length; j++)
		{
			if (s[j] != pattern[j - i])
			{
				matched = false;
				break;
			}
		}

		if (matched)
			return i;
	}

	return -1;
}

int Hash(string s)
{
	int h = 0;

	for (int i = 0; i < s.Length; i += 1)
		h ^= s[i];
		
	return h;
}
