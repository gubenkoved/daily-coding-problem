<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// Implement regular expression matching with the following special characters:
// 
// . (period) which matches any single character
// * (asterisk) which matches zero or more of the preceding element
// That is, implement a function that takes in a string and a valid regular
// expression and returns whether or not the string matches the regular expression.
// 
// For example, given the regular expression "ra." and the string "ray",
// your function should return true. The same regular expression on the string "raymond" should return false.
// 
// Given the regular expression ".*at" and the string "chat", your function should return true.
// The same regular expression on the string "chats" should return false.

void Main()
{
	Match("ra.", "ray").Dump();
	Match("ra.", "raymond").Dump();
	Match(".*at", "chat").Dump();
	Match(".*at", "chats").Dump();
	
	"***".Dump();
	
	Match("abc", "abc").Dump();
	Match("abc", "abd").Dump();
	Match("a.c", "abc").Dump();
	Match("a.c", "abd").Dump();
	Match("a.c.d", "abcqd").Dump();
	Match("a.c.d", "abcq").Dump();
}

bool Match(string regex, string s)
{
	// uses backtracking technique (https://en.wikipedia.org/wiki/Backtracking)
	
	if (regex.Length == 0 && s.Length == 0)
		return true;
	
	if (regex.Length == 0 && s.Length > 0)
		return false;
	
	char regexChar = regex[0];
	
	if (regexChar == '.')
	{
		if (s.Length == 0)
			return false;
		
		return Match(regex.Substring(1), s.Substring(1));
	} else if (regexChar == '*')
	{
		// * can consume from 0 chars to all the chars, so try all the cases
		for (int consume = 0; consume <= s.Length; consume++)
		{
			if (Match(regex.Substring(1), s.Substring(consume)))
				return true;
		}
		
	} else // regular char
	{
		if (s.Length == 0 || s[0] != regexChar)
			return false;
			
		return Match(regex.Substring(1), s.Substring(1));
	}
	
	return false;
}