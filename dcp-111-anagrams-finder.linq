<Query Kind="Program" />

// This problem was asked by Google.
// 
// Given a word W and a string S, find all starting
// indices in S which are anagrams of W.
// 
// For example, given that W is "ab", and S is "abxaba",
// return 0, 3, and 4.

void Main()
{
	AnagramsIndexes("abxaba", "ab").Dump();
	AnagramsIndexes("abababab", "ab").Dump();
	AnagramsIndexes("abcde", "c").Dump();
	AnagramsIndexes("abcde", "qwe").Dump();
	AnagramsIndexes("ewqwe", "qwe").Dump();
}

IEnumerable<int> AnagramsIndexes(string s, string w)
{
	Dictionary<char, int> balance = new Dictionary<char, int>();
	
	foreach (char c in w)
	{
		if (!balance.ContainsKey(c))
			balance[c] = 0;
			
		balance[c] += 1;
	}
	
	// we found an anagram when the balance is 0
	Func<bool> found = () => balance.All(x => x.Value == 0);
	
	// consider first len(w) chars in the balance
	for (int i = 0; i < w.Length; i++)
	{
		if (balance.ContainsKey(s[i]))
			balance[s[i]] -= 1;
	}
	
	List<int> result = new List<int>();
	
	// O(len(s) * len(w))
	for (int index = 0; index <= s.Length - w.Length; index++) // O(len(s))
	{
		if (found()) // O(len(w))
			result.Add(index);
			
		// prepare balance for the window
		// we lose char at index and get the char at index + len(w)
		if (balance.ContainsKey(s[index]))
			balance[s[index]] += 1;
		
		if (index + w.Length < s.Length && balance.ContainsKey(s[index + w.Length]))
			balance[s[index + w.Length]] -= 1;
	}
	
	return result;
}
