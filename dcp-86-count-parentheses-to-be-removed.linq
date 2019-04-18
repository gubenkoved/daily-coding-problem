<Query Kind="Program" />

// This problem was asked by Google.
// 
// Given a string of parentheses, write a function
// to compute the minimum number of parentheses to be
// removed to make the string valid (i.e. each open
// parenthesis is eventually closed).
// 
// For example, given the string "()())()", you should
// return 1. Given the string ")(", you should return
// 2, since we must remove all of them.

void Main()
{
	CountToBeRemoved("()())()").Dump();
	CountToBeRemoved(")(").Dump();
	CountToBeRemoved("(()())()").Dump();
	CountToBeRemoved("))").Dump();
	CountToBeRemoved("((").Dump();
	CountToBeRemoved("(())").Dump();
	CountToBeRemoved("))((").Dump();
}

int CountToBeRemoved(string s)
{
	int toBeRemoved = 0;
	
	Stack<char> active = new Stack<char>();
	
	foreach (char c in s)
	{
		if (c == '(')
		{
			active.Push(c);
		}
		else if (c == ')')
		{
			if (active.Count > 0)
				active.Pop();
			else
				toBeRemoved += 1;
		}
	}
	
	return toBeRemoved + active.Count;
}
