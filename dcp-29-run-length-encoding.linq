<Query Kind="Program" />

// This problem was asked by Amazon.
// 
// Run-length encoding is a fast and simple method of encoding strings.
// The basic idea is to represent repeated successive characters as a
// single count and character. For example, the string "AAAABBBCCDAA"
// would be encoded as "4A3B2C1D2A".
// 
// Implement run-length encoding and decoding. You can assume the string
// to be encoded have no digits and consists solely of alphabetic
// characters. You can assume the string to be decoded is valid.

void Main()
{
	Encode("AAAABBBCCDAA").Dump();
	Decode("4A3B2C1D2A").Dump();
}

public string Encode(string s)
{
	char current = s[0];
	int count = 1;
	
	StringBuilder builder = new StringBuilder();
	
	for (int i = 1; i <= s.Length; i++)
	{
		if (i == s.Length || s[i] != current)
		{
			builder.Append($"{count}{current}");

			count = 1;
			
			if (i != s.Length)
				current = s[i];
		}
		else
			count += 1;
	}
	
	return builder.ToString();
}

public string Decode(string s)
{
	StringBuilder builder = new StringBuilder();
	
	int idx = 0;
	
	
	
	while (idx < s.Length)
	{
		string countString = "";
		string toRepeatString = "";

		// read count
		while (idx < s.Length && (s[idx] >= '0' && s[idx] <= '9'))
		{
			countString += s[idx];
			idx += 1;
		}

		// read string
		while (idx < s.Length && !(s[idx] >= '0' && s[idx] <= '9'))
		{
			toRepeatString += s[idx];
			idx += 1;
		}
		
		// add decoded string
		builder.Append(Times(toRepeatString, int.Parse(countString)));
	}
	
	return builder.ToString();
}

string Times(string s, int count)
{
	StringBuilder builder = new StringBuilder();
	
	for (int i = 0; i < count; i++)
		builder.Append(s);
	
	return builder.ToString();
}