<Query Kind="Program" />

// This problem was asked by Square.
// 
// Given a list of words, return the shortest unique prefix of each word.
// For example, given the list:
// 
// dog
// cat
// apple
// apricot
// fish
// Return the list:
// 
// d
// c
// app
// apr
// f


void Main()
{
	GetPrefixes(new[] { "dog", "cat", "apple", "apricot", "fish", }).Dump();
	GetPrefixes(new[] { "aaa", "aab", "aac", "baa", "bab", "bbb" }).Dump();
}

Dictionary<string, string> GetPrefixes(string[] strings)
{
	// obvious optimization is make search is somewhat local
	// by sorting the values first
	// then the only items any item should be different is are its
	// neighbors
	
	Dictionary<string, string> prefixies = new Dictionary<string, string>();
	
	string[] sorted = strings.OrderBy(x => x).ToArray();
	
	for (int i = 0; i < sorted.Length; i++)
	{
		bool notEnough;
		int l = 0;
		
		do
		{
			l += 1;
			
			notEnough = false;
			
			if (i >= 1 && sorted[i - 1].Length >= l)
				notEnough |= sorted[i - 1].Substring(0, l) == sorted[i].Substring(0, l);

			if (i < sorted.Length - 1 && sorted[i + 1].Length >= l)
				notEnough |= sorted[i + 1].Substring(0, l) == sorted[i].Substring(0, l);
			
		} while (notEnough && l < sorted[i].Length);
		
		prefixies[sorted[i]] = sorted[i].Substring(0, l);
	}
	
	return prefixies;
}
