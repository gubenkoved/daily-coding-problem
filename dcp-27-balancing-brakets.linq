<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// Given a string of round, curly, and square open and closing brackets, return whether the brackets are balanced (well-formed).
// 
// For example, given the string "([])[]({})", you should return true.
// 
// Given the string "([)]" or "((()", you should return false.

void Main()
{
	CheckBrakets("([])[]({})").Dump();
	CheckBrakets("([)]").Dump();
	CheckBrakets("((()").Dump();
	CheckBrakets("(1 * (2 + 3) / (4 + 5))").Dump();
	CheckBrakets("(1 * (2 + 3) / (4 + 5)").Dump();
}

bool CheckBrakets(string s)
{
	Stack<char> brakets = new Stack<char>();
	
	for (int i = 0; i < s.Length; i++)
	{
		char c = s[i];
		
		switch (c)
		{
			case '(':
				brakets.Push(c);
				break;
			case ')':
				if (brakets.Peek() == '(')
					brakets.Pop();
				else
					return false;
				break;
			case '[':
				brakets.Push(c);
				break;
			case ']':
				if (brakets.Peek() == '[')
					brakets.Pop();
				else
					return false;
				break;
			default:
				break;
		}
	}
	
	return !brakets.Any();
}