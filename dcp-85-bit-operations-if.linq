<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// Given three 32-bit integers x, y, and b, return x if b
// is 1 and y if b is 0, using only mathematical or bit
// operations. You can assume b can only be 1 or 0.

void Main()
{
	f(1234, 3456, 1).Dump("1234");
	f(1234, 3456, 0).Dump("3456");
}

int f(int x, int y, int b)
{
	//return b == 1 ? x : y;
	
	// we can turn b into mask: either all 1 OR all 0
	// and then go like X && mask || Y && !mask
	
	int mask = b;
	
	mask |= mask << 1;
	mask |= mask << 2;
	mask |= mask << 4;
	mask |= mask << 8;
	mask |= mask << 16;
	
	Util.Metatext("mask: " + Convert.ToString(mask, 2).PadLeft(32, '0')).Dump();
	
	return x & mask | y & ~mask;
}
