<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Numerics.dll</Reference>
  <Namespace>System.Numerics</Namespace>
</Query>

// This problem was asked by Google.
// 
// Implement integer exponentiation. That is, implement the pow(x, y)
// function, where x and y are integers and returns x^y.
// 
// Do this faster than the naive method of repeated multiplication.
// 
// For example, pow(2, 10) should return 1024.

void Main()
{
	// 2^10 = 2 * 2 * 2 * 2 * 2 * 2 * 2 * 2 * 2 * 2
	// = (2 * 2) ^ 2
	
	//Pow(2, 100).Dump();
	PowV2(2, 200).Dump();
	BigInteger.Pow(2, 200).Dump();
}

// O(exp) anyways...
BigInteger Pow(int number, int exponent)
{
	BigInteger result = number;
	
	int curExp = 1;
	
	int step = 0;
	
	while (curExp < exponent)
	{
		$"Step #{++step}, current result is: {result} (exponent: {curExp})".Dump();
		
		if (curExp < exponent / 2)
		{
			result *= result;
			curExp *= 2;
		} else // approaching the target
		{
			result *= number;
			curExp += 1;
		}
	}
	
	return result;
}

// O(log(exp))
BigInteger PowV2(int number, int exponent)
{
	// we can represent exponent in a binary form b1, b2, b3, ..., bn (b1 is first bit)
	// and then represent result as the sum:
	// r = b1 * a + b2 * a^2 + b3 * a^4 + ... + bn * a^(2^(n-1))
	
	BigInteger result = 1;
	
	BigInteger rolling = number;
	
	int curExp = exponent;
	int step = 0;
	
	while (curExp != 0)
	{
		$"Step #{++step} current result is {result}, leftover exponent: {curExp}".Dump();
		
		if ((curExp & 1) == 1)
			result *= rolling;
		
		rolling *= rolling;
		curExp >>= 1;
	}
	
	return result;
}