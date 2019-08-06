<Query Kind="Program" />

// This problem was asked by Jane Street.
// 
// Given an arithmetic expression in Reverse Polish Notation,
// write a program to evaluate it.
// 
// The expression is given as a list of numbers and operands. 
//
// For example: [5, 3, '+'] should return 5 + 3 = 8.
// 
// For example, [15, 7, 1, 1, '+', '-', '/', 3, '*', 2, 1, 1, '+', '+', '-']
// should return 5, since it is equivalent to ((15 / (7 - (1 + 1))) * 3) - (2 + (1 + 1)) = 5.
// 
// You can assume the given expression is always valid.

void Main()
{
	Evaluate(new[] { "5", "3", "+" }).Dump("8");
	Evaluate(new[] { "15", "7", "1", "1", "+", "-", "/", "3", "*", "2", "1", "1", "+", "+", "-" }).Dump("5");
	Evaluate(new[] { "2", "2", "2", "*", "+" }).Dump("6");
}

int Evaluate(string[] rpn)
{
	Stack<int> stack = new Stack<int>();
	
	foreach (string s in rpn)
	{
		if (s == "+")
		{
			int b = stack.Pop();
			int a = stack.Pop();
			
			stack.Push(a + b);
		} else if (s == "-")
		{
			int b = stack.Pop();
			int a = stack.Pop();

			stack.Push(a - b);
		} else if (s == "*")
		{
			int b = stack.Pop();
			int a = stack.Pop();

			stack.Push(a * b);
		} else if (s == "/")
		{
			int b = stack.Pop();
			int a = stack.Pop();

			stack.Push(a / b);
		} else
		{
			stack.Push(int.Parse(s));
		}
	}
	
	return stack.Pop();
}
