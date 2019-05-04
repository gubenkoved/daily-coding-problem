<Query Kind="Program" />

// This problem was asked by Square.
// 
// Given a string and a set of characters, return the
// shortest substring containing all the characters in
// the set.
// 
// For example, given the string "figehaeci" and the
// set of characters {a, e, i}, you should return "aeci".
// 
// If there is no substring containing all the characters
// in the set, return null.

void Main()
{
	MinSubstringContainingAllChars("figehaeci", new[] { 'a', 'e', 'i' }).Dump("aeci");
	MinSubstringContainingAllChars("abcd", new[] { 'a', 'b', 'c', 'd' }).Dump("abcd");
	MinSubstringContainingAllChars("a", new[] { 'a', }).Dump("a");
	MinSubstringContainingAllChars("accccbcccaccbccacb", new[] { 'a', 'b' }).Dump("acb");
}

string MinSubstringContainingAllChars(string s, char[] chars)
{
	string result = null;
	
	int n = s.Length;
	
	Dictionary<char, int> counter = new Dictionary<char, int>();
	
	// edge case: count the first char
	counter[s[0]] = 1;

	int r = 0;
	int l = 0;

	// cycle overall is O(n * k) as on each iteration we move at least one char
	while (l < n)
	{
		while (!AllPresent(chars, counter) && r < n - 1) // O(k)
		{
			// more r to the right
			r += 1;
			
			if (!counter.ContainsKey(s[r]))
				counter[s[r]] = 1;
			else
				counter[s[r]] += 1;
		}
		
		// if we got there it is either match or we reached the end
		if (AllPresent(chars, counter)) // O(k)
		{
			// move l pointer while it still contains all the items
			while (AllPresent(chars, counter, excludeChar: s[l])) // O(k)
				l += 1;
			
			// okay, now we found a solution, remember it if it's better
			if (result == null || r - l + 1 < result.Length)
			{
				string current = s.Substring(l, r - l + 1);
				result = current; // found better result
			}
		} else if (r == n - 1)
		{
			break; // nothing to look for
		}
		
		// move left pointer, decrease available counter
		counter[s[l]] -= 1;
		l += 1;
	}
	
	return result;
}

// O(k) where k is amount if chars
public bool AllPresent(char[] chars, Dictionary<char, int> counter, char? excludeChar = null)
{
	foreach (var c in chars)
	{
		if (!counter.ContainsKey(c))
			return false;
			
		int available = counter[c];
		
		if (c == excludeChar)
			available -= 1;
			
		if (available < 1)
			return false;
	}
	
	return true;
}
