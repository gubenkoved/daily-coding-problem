<Query Kind="Program" />

// This problem was asked by Google.
// 
// Given a string of words delimited by spaces, reverse the
// words in string. For example, given "hello world here",
// return "here world hello"
// 
// Follow-up: given a mutable string representation, can you
// perform this operation in-place?

void Main()
{
	ReverseByWords("hello world here").Dump("here world hello");
	
	char[] s = "hello world here".ToArray();
	
	ReverseByWordsInPlace(s);
	
	new string(s).Dump("here world hello");
}

string ReverseByWords(string s)
{
	return string.Join(" ", s.Split(' ').Reverse());
}

void ReverseByWordsInPlace(char[] s)
{
	// reverse the whole thing, then reverse words inside
	
	Reverse(s, 0, s.Length - 1);
	
	int l = 0;
	int r = l;
	
	while (true)
	{
		// move r to the end of the word
		while (r < s.Length - 1 && s[r + 1] != ' ')
			r += 1;
			
		// process the word [l, r]
		Reverse(s, l, r);
		
		if (r == s.Length - 1)
			break;
		
		// move left & right pointers
		l = r + 1;
		
		while (s[l] == ' ')
			l += 1;
			
		r = l;
	}
}

void Reverse(char[] s, int l, int r)
{
	int i = l;
	int j = r;
	
	while (i < j)
	{
		char tmp = s[i];
		s[i] = s[j];
		s[j] = tmp;
		
		i += 1;
		j -= 1;
	}

	//Util.Metatext($"Reversed from {l} to {r}: {new string(s)}").Dump();
}