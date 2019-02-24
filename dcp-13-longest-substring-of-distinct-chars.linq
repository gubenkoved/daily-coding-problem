<Query Kind="Program" />

// This problem was asked by Amazon.
// 
// Given an integer k and a string s, find the length of the longest substring that contains at most k distinct characters.
// 
// For example, given s = "abcba" and k = 2, the longest substring with k distinct characters is "bcb".

void Main()
{
	f("abcba", 2).Dump();
	f("abcqqqqweqqqqba", 3).Dump();
	f("acde", 3).Dump();
	f("acde", 5).Dump();
	f("acde", 1).Dump();
}

// O(n*k) where n len of string
string f(string s, int k)
{
	string longest = "";
	
	for (int i = 0; i < Math.Max(1, s.Length - k); i++)
	{
		HashSet<char> used = new HashSet<char>();
		
		int idx = i;
		
		while (used.Count <= k && idx < s.Length)
		{
			if (used.Count == k && !used.Contains(s[idx]))
				break; // game over
			
			used.Add(s[idx++]);
		}
		
		string cur = s.Substring(i, idx - i);
		
		if (cur.Length > longest.Length)
			longest = cur;
	}
	
	return longest;
}
