<Query Kind="Program" />

// This question was asked by ContextLogic.
// 
// Implement division of two positive integers without
// using the division, multiplication, or modulus operators.
// Return the quotient as an integer, ignoring the remainder.

void Main()
{
	IntegerDiv(20, 2).Dump("10");
	IntegerDiv(21, 2).Dump("10");
	IntegerDiv(22, 2).Dump("11");
	IntegerDiv(21, 3).Dump("7");
	IntegerDiv(25, 5).Dump("5");
	IntegerDiv(25, 7).Dump("3");
}

int IntegerDiv(int number, int divider)
{
	int result = 0;
	int sum = 0;
	
	do
	{
		sum += divider;
		result += 1;
		
	}  while (sum <= number);
	
	return result - 1;
}
