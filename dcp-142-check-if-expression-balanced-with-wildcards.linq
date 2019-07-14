<Query Kind="Program" />

// This problem was asked by Google.
// 
// You're given a string consisting solely of (, ), and *. * can represent
// either a (, ), or an empty string. Determine whether the parentheses are balanced.
// 
// For example, (() * and (*) are balanced. )*( is not balanced.

void Main()
{
	// we can greedily check all the cases stopping when there is no way 
	// to balance the expression and branching when there are multiple ways to balance at asterisk
	// this is going to be computationally expensive solution though...
	// there must be some efficient strategy
	
	IsBalanced("(*").Dump("true");
	IsBalanced("(() *").Dump("true");
	IsBalanced("(*)").Dump("true");
	IsBalanced("(**)").Dump("true");
	IsBalanced("(**)(**)").Dump("true");
	IsBalanced("******)").Dump("true");
	IsBalanced(")*(").Dump("false");
	IsBalanced("((((((())****").Dump("false");
	IsBalanced("*****)))))").Dump("true");
	IsBalanced("*****))))))").Dump("false");
}

bool IsBalanced(string expression)
{
	return IsBalanced(new Stack<char>(), expression, 0);
}

bool IsBalanced(Stack<char> stack, string expression, int idx)
{
	while (idx < expression.Length)
	{
		char cur = expression[idx];

		// okay, let's consume 'cur'

		switch (cur)
		{
			case '(':
				stack.Push('(');
				break;
			case ')':
				if (stack.Count == 0)
					return false;
			
				char topOfStack = stack.Pop();

				if (topOfStack != '(')
					return false;

				break;
			case '*':
				// okay, now we branch
				// there are 3 possibilities there: (, ) or emptiness
				
				// let's imagine * being a '('
				var stack2 = Clone(stack);
				stack2.Push('(');
				
				if (IsBalanced(stack2, expression, idx + 1))
					return true;

				// let's imagine * being a ')' if that's possible
				if (stack.Count > 0 && stack.Peek() == '(')
				{
					stack2 = Clone(stack);
					stack2.Pop();

					if (IsBalanced(stack2, expression, idx + 1))
						return true;
				}

				// and finally let's suppose * was emptiness

				if (IsBalanced(Clone(stack), expression, idx + 1))
					return true;

				// not going further there as we checked all the ways!
				return false;
			case ' ':
				// just skip
				break;
			default:
				throw new Exception($"unepxected char: {cur}");
		}
		
		idx += 1;
	}

	return stack.Count == 0;
}

public static Stack<T> Clone<T>(Stack<T> stack)
{
	return new Stack<T>(stack.Reverse());
}