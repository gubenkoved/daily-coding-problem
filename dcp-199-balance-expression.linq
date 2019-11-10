<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// Given a string of parentheses, find the balanced string that can be produced from
// it using the minimum number of insertions and deletions. If there are multiple
// solutions, return any of them.
// 
// For example, given "(()", you could return "(())". Given "))()(", you could return "()()()()".

void Main()
{
	BalanceNaive("(()").Dump();
	BalanceNaive("))()(").Dump();
}

string BalanceNaive(string s)
{
	// I'm wondering why simple greedy solution does not work?
	// if it did, then it would not be marked with "Hard"...
	// however, when there expression is unbalanced, and N parantheses are
	// missing, then exactly N chars should be added or removed...
	
	string balanced = string.Empty;
	
	Stack<char> stack = new Stack<char>();
	
	for (int i = 0; i < s.Length; i++)
	{
		char c = s[i];
		
		if (c == '(')
		{
			stack.Push(c);
		} else if (c == ')')
		{
			// if there is something to close -> okay, no additions
			if (stack.Any())
			{
				stack.Pop();
			} else
			{
				// uh-oh, we need add a '(' as there is non on stack
				balanced += '(';
			}
		}
		
		balanced += c;
	}
	
	// close all unclosed!
	balanced += new string(')', stack.Count());
	
	return balanced;
}
