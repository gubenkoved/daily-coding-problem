<Query Kind="Program" />

// Find an efficient algorithm to find the smallest distance (measured in
// number of words) between any two given words in a string.
// 
// For example, given words "hello", and "world" and a text content of
// "dog cat hello cat dog dog hello cat world", return 1 because there's
// only one word "cat" in between the two words.

void Main()
{
	MinDistance("dog cat hello cat dog dog hello cat world", "hello", "world").Dump("1");
	MinDistance("dog cat hello cat dog dog hello cat world", "dog", "cat").Dump("0");
	MinDistance("dog cat hello cat dog dog hello cat world", "dog", "world").Dump("2");
	MinDistance("dog cat hello cat dog dog hello cat world", "dog", "qwe").Dump("-1");
}

// O(n) time, O(1) space, what can be more efficient?
public int MinDistance(string sentence, string word1, string word2)
{
	int lastWord1Index = -1;
	int lastWord2Index = -1;
	
	int minDistance = int.MaxValue;
	
	int wordIndex = 0;
	foreach (var word in Tokenize(sentence))
	{
		if (word == word1)
		{
			lastWord1Index = wordIndex;
			
			// check for word 2 before
			if (lastWord2Index != -1)
				minDistance = Math.Min(minDistance, lastWord1Index - lastWord2Index);
		} else if (word == word2)
		{
			lastWord2Index = wordIndex;

			// check for word 1 before
			if (lastWord1Index != -1)
				minDistance = Math.Min(minDistance, lastWord2Index - lastWord1Index);
		}
		
		wordIndex += 1;
	}
	
	if (minDistance == int.MaxValue)
		return -1; // either word1 or word2 does not exist
	
	return minDistance - 1;
}

public IEnumerable<string> Tokenize(string sentence)
{
	int idx = 0;
	
	string buffer = string.Empty;
	
	while (idx < sentence.Length)
	{
		char c = sentence[idx];
		idx += 1;
		
		if (c == ' ')
		{
			// word edge --> emit
			
			if (!string.IsNullOrEmpty(buffer))
				yield return buffer;
			
			buffer = string.Empty;
		} else
		{
			buffer += c;
		}
	}
	
	if (!string.IsNullOrEmpty(buffer))
		yield return buffer;
}