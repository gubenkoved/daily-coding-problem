<Query Kind="Program" />

// This problem was asked by Twitter.
// 
// Implement an autocomplete system. That is, given a query string s and a set of all possible query strings,
// return all strings in the set that have s as a prefix.
// 
// For example, given the query string de and the set of strings[dog, deer, deal], return [deer, deal].
// 
// Hint: Try preprocessing the dictionary into a more efficient data structure to speed up queries.

void Main()
{
	// naive -- single run optimized:
	// just search for O(n)
	
	// query time optimized (where index is created once/rare):
	// sort O(nlogn) and then binary search -- O(logn)

	//Autocomplete("de", new[] { "auto", "dog", "deer", "home", "deal", "door"}).Dump();
	
	// there is a problem when handling "ignorable chars"
	Autocomplete("de", new[] {"auto", "dog", "deer", "home", "deal", "d-elo", "de", "door"}).Dump();
}

// initial sort is O(n logn)
// query is O(logn)
IEnumerable<string> Autocomplete(string prefix, IEnumerable<string> all)
{
	// https://stackoverflow.com/questions/44675770/character-after-hyphen-affects-string-compare
	List<string> sorted = all.OrderBy(x => x, StringComparer.OrdinalIgnoreCase).ToList();
	
	Util.Metatext(string.Join(", ", sorted)).Dump();
	
	// binary search first word that matches, then yeild while matching
	int idx = BinarySearchByPrefix(sorted, prefix);
	
	Util.Metatext($"first idx: {idx}").Dump();
	
	if (idx == -1)
		return new string[] {};
	
	List<string> result = new List<string>();
	
	while (sorted[idx].StartsWith(prefix))
	{
		result.Add(sorted[idx]);
		idx += 1;
	}
	
	return result;
}

// uses binary search return first index of element that starts with given prefix
int BinarySearchByPrefix(IList<string> sorted, string prefix)
{
	int low = 0;
	int hi = sorted.Count - 1;

	var comparer = StringComparer.OrdinalIgnoreCase;

	while (true)
	{
		// edge case
		if (hi - low == 1 && comparer.Compare(prefix, sorted[low]) >= 0)
		{
			low = hi;
			break;
		}
		else if (hi - low == 0)
		{
			break;
		}

		int midIdx = (hi + low) / 2;
		string midElement = sorted[midIdx];
		
		// prefix is lexicographically bigger
		bool prefixIsBiggerThanMid = comparer.Compare(prefix, midElement) >= 0;

		Util.Metatext($"\tlow={low} ({sorted[low]}), hi={hi} ({sorted[hi]}) '{prefix}' is bigger than mid={midIdx} element '{midElement}'? {prefixIsBiggerThanMid}").Dump();
		
		if (prefixIsBiggerThanMid)
			low = midIdx; // search [midIdx, hi]
		else
			hi = midIdx; // search [low, midIdx]
			
		
	}
	
	int idx = -1;
	
	if (sorted[low].StartsWith(prefix))
	{
		// found, rewind to the first element that fulfils the prefix predicate
		idx = low;
		
		while (idx >= 1 && sorted[idx - 1].StartsWith(prefix))
			idx -= 1;
	}
	
	return idx;
}