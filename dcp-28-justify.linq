<Query Kind="Program" />

// This problem was asked by Palantir.
// 
// Write an algorithm to justify text. Given a sequence of words and an integer
// line length k, return a list of strings which represents each line, fully justified.
// 
// More specifically, you should have as many words as possible in each line. There
// should be at least one space between each word. Pad extra spaces when necessary
// so that each line has exactly length k. Spaces should be distributed as equally as
// possible, with the extra spaces, if any, distributed starting from the left.
// 
// If you can only fit one word on a line, then you should pad the right-hand side with spaces.
// 
// Each word is guaranteed not to be longer than k.
// 
// For example, given the list of words ["the", "quick", "brown", "fox", "jumps", "over",
// "the", "lazy", "dog"] and k = 16, you should return the following:
// 
// ["the  quick brown", # 1 extra space on the left
// "fox  jumps  over", # 2 extra spaces distributed evenly
// "the   lazy   dog"] # 4 extra spaces distributed evenly

void Main()
{
	string[] justified = Justify(new[] { "the", "quick", "brown", "fox", "jumps", "over", "the", "lazy", "dog" },
		k: 16);
	
	foreach (var x in justified)
		$"'{x}'".Dump();
}

string[] Justify(IEnumerable<string> words, int k)
{
	// for each line try to stick as many words as possible first
	// then calculate needed spaces
	
	List<string> justified = new List<string>();
	List<string> currentLine = new List<string>();
	
	foreach (string word in words)
	{
		int currentLineLen = currentLine.Count - 1 // at least 1 space for each word
			+ currentLine.Sum(x => x.Length);
		
		int tentativeLineLen = currentLineLen + 1 + word.Length; // space + word
		
		if (tentativeLineLen > k)
		{
			// line is filled, compute additional spaces if needed
			justified.Add(JustifyLine(currentLine.ToArray(), k));
			
			currentLine.Clear();
		}
		
		currentLine.Add(word);
	}
	
	if (currentLine.Any())
		justified.Add(JustifyLine(currentLine.ToArray(), k));
	
	return justified.ToArray();
}

string JustifyLine(string[] words, int k)
{
	if (words.Length == 1)
		return words[0];
	
	int currentLineLen = words.Length - 1 // at least 1 space for each word
		+ words.Sum(x => x.Length);

	int extraSpaces = k - currentLineLen;

	int spaceForEachWord = extraSpaces / (words.Length - 1); // all words have this amount of additional spaces
	int reminder = extraSpaces % (words.Length - 1); // first "reminder" words will have additional space

	StringBuilder lineBuilder = new StringBuilder();

	for (int wordNum = 0; wordNum < words.Length; wordNum++)
	{
		lineBuilder.Append(words[wordNum]);

		int spaceCount = 1 + spaceForEachWord;

		if (wordNum < reminder)
			spaceCount += 1;
			
		if (wordNum == words.Length - 1)
			spaceCount = 0;

		lineBuilder.Append(new string(' ', spaceCount));
	}
	
	return lineBuilder.ToString();
}