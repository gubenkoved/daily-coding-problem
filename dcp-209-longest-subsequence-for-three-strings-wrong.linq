<Query Kind="Program" />

// This problem was asked by YouTube.
// 
// Write a program that computes the length of the longest common subsequence of three
// given strings. For example, given "epidemiologist", "refrigeration", and
// "supercalifragilisticexpialodocious", it should return 5, since the longest common
// subsequence is "eieio".

void Main()
{
	// (1) an obvious way is to check every subseqnce of the shortest word
	// there are O(minLen ^ 2) such subsequences and every check is O(lenWord * lenString)
	// which gives us about O(n^4) for the worst case by time and O(1) memory
	//
	// (2) alernatively... we can tokenize each word into the n-gramms and they check the intersection
	// however, this will require a lot of memory
	// but, it should work for O(maxLen * maxLen) or simply O(n^2) where n is the longest work len
	// for time and  O(n^2) for memory
	//
	// (3) as an improvement of method 2 we can step by step consider longest n-gramms first of a given len
	// and then gradually decrease n-gram len to 1 char
	// time is the same - max n rounds for O(n) to tokenize into n-gramms, so O(n^2) in total
	// but maximum memory consumption is O(n) and we do not have to preserve memory between rounds
	//
	// whoopps... I got the problem wrong -- subsequence does not have to be consolidated...
	
	LongestSubsequence("epidemiologist", "refrigeration", "supercalifragilisticexpialodocious").Dump();
}

string LongestSubsequence(string a, string b, string c)
{
	string shortest = a;
	
	if (b.Length < shortest.Length)
		shortest = b;

	if (c.Length < shortest.Length)
		shortest = c;
		
	for (int n = shortest.Length; n >= 1; n--)
	{
		var aNGramms = SplitIntoNGrams(a, n);
		var bNGramms = SplitIntoNGrams(b, n);
		var cNGramms = SplitIntoNGrams(c, n);
		
		foreach (string ngram in aNGramms)
		{
			if (bNGramms.Contains(ngram) && cNGramms.Contains(ngram))
				return ngram;
		}
	}

	// no even single char in common!
	return null;
}

HashSet<string> SplitIntoNGrams(string s, int n)
{
	HashSet<string> ngramms = new HashSet<string>();
	
	for (int i = 0; i <= s.Length - n; i++)
		ngramms.Add(s.Substring(i, n));
	
	return ngramms;
}

