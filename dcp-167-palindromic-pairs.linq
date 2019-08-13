<Query Kind="Program" />

// This problem was asked by Airbnb.
// 
// Given a list of words, find all pairs of unique indices such
// that the concatenation of the two words is a palindrome.
// 
// For example, given the list ["code", "edoc", "da", "d"],
// return [(0, 1), (1, 0), (2, 3)].

void Main()
{
	GetPalindromicPairs(new[] { "code", "edoc", "da", "d" } ).Dump();
}

// O(n * n) where n is amount of words
IEnumerable<Tuple<int, int>> GetPalindromicPairs(string[] words)
{
	List<Tuple<int, int>> r = new List<System.Tuple<int, int>>();
	
	for (int i = 0; i < words.Length; i++)
	{
		for (int j = 0; j < words.Length; j++)
		{
			if (i == j)
				continue;
			
			if (IsPalindrom(words[i] + words[j]))
				r.Add(Tuple.Create(i, j));
		}
	}
	
	return r;
}

bool IsPalindrom(string w)
{
	for (int i = 0; i < w.Length / 2; i++)
	{
		if (w[i] != w[w.Length - 1 - i])
			return false;
	}
	
	return true;
}
