<Query Kind="Program" />

// This problem was asked by YouTube.
// 
// Write a program that computes the length of the longest common subsequence of three
// given strings. For example, given "epidemiologist", "refrigeration", and
// "supercalifragilisticexpialodocious", it should return 5, since the longest common
// subsequence is "eieio".

void Main()
{
	LongestSubsequence("epidemiologist", "refrigeration", "supercalifragilisticexpialodocious").Dump("eieio");
	LongestSubsequence("supermegatester", "supermega", "megatester").Dump("mega");
}

string LongestSubsequence(string a, string b, string c)
{
	// brute force...
	
	var state = new SearchState();
	
	F(string.Empty, a, b, c, -1, -1, -1, state);
	
	return state.Longest;
}

class SearchState
{
	public string Longest { get; set; }
}

// tries to add one more letter to common subsequence
// searching to the right of the pointers
void F(string currentSubsequence, string a, string b, string c, int aIdx, int bIdx, int cIdx, SearchState state)
{
	//$"F({currentSubsequence}, {aIdx}, {bIdx}, {cIdx})".Dump();
	
	for (int aIdxNew = aIdx + 1; aIdxNew < a.Length; aIdxNew++)
	{
		// try it!
		char q = a[aIdxNew];
		
		// is there such char in others?
		int bIdxNew = FindToTheRight(b, q, bIdx);
		int cIdxNew = FindToTheRight(c, q, cIdx);
		
		// found in both?! okay! dive further
		if (bIdxNew != -1 && cIdxNew != -1)
		{
			string newSubsequence = currentSubsequence + a[aIdxNew];
			
			F(newSubsequence, a, b, c, aIdxNew, bIdxNew, cIdxNew, state);
			
			if (state.Longest == null || newSubsequence.Length > state.Longest.Length)
				state.Longest = newSubsequence;
		}
	}
}

int FindToTheRight(string s, char c, int start)
{
	for (int idx = start + 1; idx < s.Length; idx++)
		if (s[idx] == c)
			return idx;
			
	return -1;
}