<Query Kind="Program" />

// This problem was asked by Epic.
// 
// The "look and say" sequence is defined as follows: beginning with the term 1, each subsequent
// term visually describes the digits appearing in the previous term. The first few terms are
// as follows:
// 
// 1
// 11
// 21
// 1211
// 111221
//
// As an example, the fourth term is 1211, since the third term consists of one 2 and one 1.
// 
// Given an integer N, print the Nth term of this sequence

void Main()
{
	LookAndSay().Take(10).Dump();
}

string LookAndSay(int n)
{
	return LookAndSay().Skip(n - 1).First();
}

IEnumerable<string> LookAndSay()
{
	string s = "1";
	
	yield return s;

	while (true)
	{
		string next = string.Empty;
		
		int count = 1;
		
		for (int i = 1; i < s.Length; i++)
		{
			if (s[i] == s[i - 1])
			{
				count += 1;
			} else
			{
				// flush
				next += $"{count}{s[i - 1]}";
				count = 1;
			}
		}
		
		// flush the last one
		next += $"{count}{s[s.Length - 1]}";
		
		s = next;
		
		yield return s;
	}
}
