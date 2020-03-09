<Query Kind="Program" />

// This problem was asked by Indeed.
// 
// Given a 32-bit positive integer N, determine whether it is a power of four in faster than O(log N) time.

void Main()
{
	for (int i = 0; i < 100_000_000; i++)
	{
		if (PowerOfFour(i))
			$"{i}".Dump();
	}
}

bool PowerOfFour(int a)
{
	int exp = (int)Math.Round(Math.Log(a, 4));
	
	return IntPower(4, exp) == a;
}

int IntPower(int x, int exp)
{
	int result = 1;
	
	for (int i = 0; i < exp; i++)
		result *= x;
	
	return result;
}
