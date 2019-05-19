<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// Given a string and a set of delimiters, reverse the
// words in the string while maintaining the relative
// order of the delimiters. For example, given
// "hello/world:here", return "here/world:hello"
// 
// Follow-up: Does your solution work for the following
// cases: "hello/world:here/", "hello//world:here"

void Main()
{
	Reverse("hello/world:here").Dump("here/world:hello");
	Reverse("hello/world:here/").Dump("here/world:hello/");
	Reverse("hello//world:here").Dump("here//world:hello");
}

string Reverse(string s)
{
	List<string> words = new List<string>();
	List<string> delimiters = new List<string>();
	
	string buffer = null;
	bool bufferIsWord = false; // does not matter, just making the compiler happy
	
	for (int i = 0; i < s.Length; i++)
	{
		bool isWordChar = char.IsLetter(s[i]);
		
		bool processBuffer = i != 0 && bufferIsWord != isWordChar;
		
		if (processBuffer)
		{
			if (bufferIsWord)
				words.Add(buffer);
			else
				delimiters.Add(buffer);
				
			buffer = string.Empty;
		}
		
		buffer += s[i]; // just add char to the buffer
		bufferIsWord = isWordChar; // handles the edge case of the first char
	}

	if (bufferIsWord)
		words.Add(buffer);
	else
		delimiters.Add(buffer);

	//words.Dump("words");
	//delimiters.Dump("delimiters");
	
	words.Reverse(); // reverse the words!
	
	// join them back!
	
	var wordsEnumerator = words.GetEnumerator();
	var delimitersEnumerator = delimiters.GetEnumerator();
	
	// start with words right now, but we can look at the first char to handle delimtier-first cases
	
	string result = string.Empty;
	
	while (wordsEnumerator.MoveNext())
	{
		delimitersEnumerator.MoveNext();
		
		result += wordsEnumerator.Current;
		result += delimitersEnumerator.Current;
	}
	
	// edge case where it ends with delimiter
	if (delimitersEnumerator.MoveNext())
		result += delimitersEnumerator.Current;
	
	return result;
}
