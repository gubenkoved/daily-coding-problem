<Query Kind="Program" />

// This problem was asked by Nvidia.
// 
// Find the maximum of two numbers without using any if-else statements, branching, or direct comparisons.

void Main()
{
	// Max(1, 2);
	// Max(-10, -11);
	// Max(10, 11);
	
	var rnd = new Random();
	
	for (int i = 0; i < 1000; i++)
	{
		int a = rnd.Next() - rnd.Next();
		int b = rnd.Next() - rnd.Next();

		if (Max(a, b) != Math.Max(a, b))
			throw new Exception($"WA: {a}, {b}");
	}
	
	"looks good!".Dump();
}

int Max(int a, int b)
{
	//$"{a}, {b}".Dump("input");
	
	unchecked
	{
		int x = (int)((ulong)((long)a - (long)b) >> 63);

		// x will be 0 if a >= b, and 1 otherwise

		// it allows to leave 0 as is and create mask full of '1' if it was 1 as it's a binary representation of -1
		int mask = x * -1;
		
		//Convert.ToString(mask, 2).Dump("mask");
		
		// given the mask which is either 32 zero or 32 one we just apply it to source numbers so that only one
		// is left

		return (a & ~mask) | (b & mask);
	}
}
