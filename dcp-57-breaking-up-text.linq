<Query Kind="Program" />

// This problem was asked by Amazon.
// 
// Given a string s and an integer k, break up the string into
// multiple texts such that each text has a length of k or less.
// You must break it up so that words don't break across lines.
// If there's no way to break the text up, then return null.
// 
// You can assume that there are no spaces at the ends of the
// string and that there is exactly one space between each word.
// 
// For example, given the string "the quick brown fox jumps over
// the lazy dog" and k = 10, you should return: ["the quick",
// "brown fox", "jumps over", "the lazy", "dog"]. No string in
// the list has a length of more than 10.

// UPDATE.
// + "each line has to have the maximum possible amount of words"

void Main()
{
	// may be statement misses an optimization restriction that
	// we must do such split in a fashion that minimizes amount of chunks?
	// will the greedy algorithm work there?
	// it might give suboptimal solution though
	
	// got a response from founders -- they said that it's just about each line
	// having maximum possible amount of words
	// seems fishy... what if to maximize one line content I have to sacrifice 
	// another line len...
	// e.g. len = 8
	// ########
	// aaa aa
	// aaaa aaa <--- we can move "aaa" to the next line
	// a        <--- what is more optimal breakdown?...
	// aaaaaaa
	// aaa aaaa
	// aa
	
	// anyways, let's solve by simple greed algorithm
	
	Split("aaa aa aaaa aaa a aaaaaaa aaa aaaa aa", 8).Dump();
	Split("the quick brown fox jumps over the lazy dog", 10).Dump();
}

string[] Split(string text, int k)
{
	string[] words = text.Split(' ');
	
	List<string> lines = new List<string>();
	
	string line = string.Empty;
	
	foreach (string word in words)
	{
		if (word.Length > k)
			return null; // unable to split!
		
		if (line.Length + word.Length + 1 > k)
		{
			// flush!
			lines.Add(line);
			line = string.Empty;
		}
		
		if (line.Length != 0)
			line += " ";
			
		line += word;
	}
	
	if (!string.IsNullOrEmpty(line))
		lines.Add(line);
	
	return lines.ToArray();
}