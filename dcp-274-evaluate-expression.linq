<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// Given a string consisting of parentheses, single digits, and positive
// and negative signs, convert the string into a mathematical expression to
// obtain the answer.
// 
// Don't use eval or a similar built-in parser.
// 
// For example, given '-1 + (2 + 3)', you should return 4.

void Main()
{
	// EXP ::= EXP ("+" | "-" | "*" | "/") EXP
	// EXP ::= "(" EXP ")"
	// EXP ::= "-" NUMBER
	// EXP ::= "+" NUMBER
	// NUMBER ::= DIGIT // single only by definition
	// DIGIT ::= "0" | "1" | "2" | ... | "9"
	
	Eval("-1 + (2 + 3)").Dump("4");
	Eval("-1 - (2 - 3)").Dump("0");
	Eval("2 * (2 + 2)").Dump("8");
	Eval("2 * (3 - 6)").Dump("-6");
	Eval("2").Dump("2");
	Eval("((2))").Dump("2");
	Eval("2+2*2").Dump("6");
	Eval("2*2+2").Dump("6 -- DOES NOT HANDLE THE PRIORITY OF OPERATORS W/O PARENTHESES");
	Eval("(2+2)*2").Dump("8");
	Eval("1*2*3*4*5").Dump("120");
	Eval("(1*2*3*4*5)/12").Dump("10");
	
}

int Eval(string expression)
{
	expression = expression.Replace(" ", "");
	
	return Eval(expression, 0, expression.Length - 1);
}

int Eval(string exp, int l, int r)
{
	int? arg1 = null;
	int idx = l;
	
	if (exp[l] == '(')
	{
		// find the end of expression
		int total = 0;
		int start = l;
		int end = r;
		
		for (int i = l; i <= r; i++)
		{
			if (exp[i] == '(')
				total += 1;
			else if (exp[i] == ')')
				total -= 1;

			// see if we balanced out
			if (total == 0)
			{
				end = i;
				break;
			}
		}
		
		arg1 = Eval(exp, start + 1, end - 1);
		idx = end + 1; // continue there

		// see if we consumed it all
		if (idx > r)
			return arg1.Value;
	}

	if (exp[idx] == '-' || IsDigit(exp[l])) // numbers
	{
		string buffer = string.Empty;

		while (idx <= r && (IsDigit(exp[idx]) || (exp[idx] == '-' && buffer.Length == 0))) // parse '-' sign only if it's the first char in the number
		{
			buffer += exp[idx];
			idx += 1;
		}

		arg1 = int.Parse(buffer);

		// see if we consumed it all
		if (idx > r)
			return arg1.Value;
	}
	
	if (IsOperator(exp[idx]))
	{
		if (arg1 == null)
			throw new Exception("expected to see argument before the operator");
		
		char op = exp[idx];
		
		int arg2 = Eval(exp, idx + 1, r);
		
		if (op == '+')
			return arg1.Value + arg2;
		else if (op == '-')
			return arg1.Value - arg2;
		else if (op == '*')
			return arg1.Value * arg2;
		else if (op == '/')
			return arg1.Value / arg2;
		else
			throw new Exception($"unexpected operator '{op}'");
	}

	throw new Exception($"unexpected input, unable to parse: '{exp.Substring(l, r - l + 1)}'");
}

bool IsDigit(char c)
{
	return char.IsDigit(c);
}

bool IsOperator(char c)
{
	return c == '*' || c == '/' || c == '+' || c == '-';
}

