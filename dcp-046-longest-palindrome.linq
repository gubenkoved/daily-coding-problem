<Query Kind="Program" />

// This problem was asked by Amazon.
// 
// Given a string, find the longest palindromic contiguous substring.
// If there are more than one with the maximum length, return any one.
// 
// For example, the longest palindromic substring of "aabcdcb" is
// "bcdcb". The longest palindromic substring of "bananas" is "anana".

void Main()
{
	LongestPalindrom("aabcdcb").Dump("bcdcb");
	LongestPalindrom("bananas").Dump("anana");
	LongestPalindrom("qwerty").Dump("q");
	LongestPalindrom("balabalav").Dump("alabala");
	LongestPalindrom("balaalav").Dump("alaala");
}

// time: O(n^2)
string LongestPalindrom(string s)
{
	// brute force
	// start at some index and extend substring to left and right while
	// chars are matching
	// we need to consider two cases: when we start with single char
	// and when we start at two adjacent chars
	// time complexity: O(n^2) - about 2*n start positions and for each position about n comparisons
	
	string longest = string.Empty;
	
	for (int idx = 0; idx < s.Length; idx++)
	{
		// case 1 - odd len
		string tmp = LongestPalindrom(s, idx, idx);
		
		if (tmp.Length > longest.Length)
			longest = tmp;

		// case 2 - even len
		if (idx >= 1 && s[idx - 1] == s[idx])
		{
			tmp = LongestPalindrom(s, idx - 1, idx);

			if (tmp.Length > longest.Length)
				longest = tmp;
		}
	}
	
	return longest;
}

string LongestPalindrom(string s, int l, int r)
{
	while (l >= 1 && r <= s.Length - 2 && s[l - 1] == s[r + 1])
	{
		l -= 1;
		r += 1;
	}
	
	return s.Substring(l, r - l + 1);
}
