<Query Kind="Program" />

// This problem was asked by LinkedIn.
// 
// Given a string, return whether it represents a number. Here are the different kinds of numbers:
// 
// "10", a positive integer
// "-10", a negative integer
// "10.1", a positive real number
// "-10.1", a negative real number
// "1e5", a number in scientific notation
// And here are examples of non-numbers:
// 
// "a"
// "x 1"
// "a -2"
// "-"

void Main()
{
	"should be true".Dump();
	
	IsNumber("10").Dump();
	IsNumber("-10").Dump();
	IsNumber("10.1").Dump();
	IsNumber("-10.1").Dump();
	IsNumber("1e5").Dump();
	IsNumber("1e-5").Dump();
	IsNumber("-1e5").Dump();
	IsNumber("-1e-5").Dump();
	IsNumber("1.5465e5").Dump();
	
	"should be false".Dump();
	
	IsNumber("a").Dump();
	IsNumber("x 1").Dump();
	IsNumber("a -2").Dump();
	IsNumber("-").Dump();
}

bool IsNumber(string s)
{
	// not sure if I could have used Regexes, but... it was not explicitly
	// forbidden by the problem statement
	
	if (Regex.IsMatch(s, @"^-?[0-9]+(\.[0-9]+)?$")) // positive/negative number incl. fractional part
		return true;

	// exp format
	if (Regex.IsMatch(s, @"^-?[0-9]+(\.[0-9]+)?(e-?[0-9]+)$"))
		return true;

	return false;
}
