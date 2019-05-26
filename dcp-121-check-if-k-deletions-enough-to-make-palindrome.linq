<Query Kind="Program" />

// This problem was asked by Google.
// 
// Given a string which we can delete at most k,
// return whether you can make a palindrome.
// 
// For example, given 'waterrfetawx' and a k of 2,
// you could delete f and x to get 'waterretaw'.

void Main()
{
	PalindromePossible("waterrfetawx", 2).Dump("true");
	PalindromePossible("waterrfetawx", 1).Dump("false");
	PalindromePossible("ateset", 1).Dump("true");
	PalindromePossible("atetret", 1).Dump("false");
	PalindromePossible("atetret", 2).Dump("true");
	PalindromePossible("tetatet", 0).Dump("true");
	PalindromePossible("tetateta", 0).Dump("true");
}

bool PalindromePossible(string s, int k)
{
	return F(s, 0, s.Length - 1, k);
}

bool F(string s, int l, int r, int k)
{
	// checked till the end
	if (l >= r)
		return true;
	
	// symbols match up!
	if (s[l] == s[r])
		return F(s, l + 1, r - 1, k);
		
	// symbols do not match up
	
	if (k == 0)
		return false; // no capacity to accomodate the difference
	
	// try by removing the symbol from the left
	if (F(s, l + 1, r, k - 1))
		return true;
		
	// try removing the right one
	if (F(s, l, r - 1, k - 1))
		return true;
		
	return false;
}
