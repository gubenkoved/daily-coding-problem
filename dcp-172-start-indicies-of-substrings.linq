<Query Kind="Program" />

// This problem was asked by Dropbox.
// 
// Given a string s and a list of words words, where each word is
// the same length, find all starting indices of substrings in s
// that is a concatenation of every word in words exactly once.
// 
// For example, given s = "dogcatcatcodecatdog" and words = ["cat",
// "dog"], return [0, 13], since "dogcat" starts at index 0 and
// "catdog" starts at index 13.
// 
// Given s = "barfoobazbitbyte" and words = ["dog", "cat"], return
// [] since there are no substrings composed of "dog" and "cat" in s.
// 
// The order of the indices does not matter.

void Main()
{
	GetStartingIndexes("dogcatcatcodecatdog", new[] { "dog", "cat" }).Dump();
	GetStartingIndexes("barfoobazbitbyte", new[] { "dog", "cat" }).Dump();
}

IEnumerable<int> GetStartingIndexes(string s, string[] words)
{
	List<int> result = new List<int>();
	
	for (int idx = 0; idx < s.Length; idx++)
	{
		bool first = true;
		bool found;
		
		do
		{
			found = false;
			
			foreach (var word in words)
			{
				if (IsThere(s, idx, word))
				{
					if (first)
					{
						first = false;
						result.Add(idx);
					}
					
					idx += word.Length;
					found = true;
				}
			}
		} while (found);
	}
	
	return result;
}

bool IsThere(string s, int idx, string word)
{
	for (int i = idx; i < idx + word.Length; i++)
	{
		if (i >= s.Length || s[i] != word[i - idx])
			return false;
	}
	
	return true;
}