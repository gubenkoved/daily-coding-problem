<Query Kind="Program" />

// This problem was asked by Zillow.
// 
// Let's define a "sevenish" number to be one which is either a power of 7, or the
// sum of unique powers of 7. The first few sevenish numbers
// are 1, 7, 8, 49, and so on. Create an algorithm to find the nth sevenish number.

void Main()
{
	for (int i = 1; i < 100; i++)
		$"{i} -> {SevenishNumber(i)}".Dump();
}

long SevenishNumber(int n)
{
	// we can make use of the binary code there!
	long number = 0;
	
	long pow = 1;
	
	while (n != 0)
	{
		if (n % 2 == 1)
			number += pow;
		
		pow *= 7;
		
		n /= 2;
	}
	
	return number;
}
