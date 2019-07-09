<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// Given an array of integers in which two elements appear exactly
// once and all other elements appear exactly twice, find the two
// elements that appear only once.
// 
// For example, given the array [2, 4, 6, 8, 10, 2, 6, 10], return
// 4 and 8. The order does not matter.
// 
// Follow-up: Can you do this in linear time and constant space?

void Main()
{
	// it must be some kind of bitwise operation related problem
	// like XOR all the items, but we will end up with XOR of two elements

	Find(new[] { 2, 4, 6, 8, 10, 2, 6, 10 }).Dump("(4, 8)");
	Find(new[] { 1, 2, 3, 3, 4, 4, 5, 5 }).Dump("(1, 2)");
	Find(new[] { 1, 1, 2, 2, 3, 3, 9, 8 }).Dump("(9, 8)");
	Find(new[] { 1, 2, 3, 4, 8, 9, 4, 3, 2, 1 }).Dump("(8, 9)");
}

(int a, int b) Find(int[] d)
{
	int xor = 0;

	for (int i = 0; i < d.Length; i++)
		xor ^= d[i];
		
	// okay, we basically got the result of
	// a xor b
	// and what it really shows is whether or not a and b bit-by-bit are equal
	// so let's take one of the bit where xor = 1 and use it to split initial set in two
	// so that one of the numbers is on one side and another is on another
	
	int diffBit = 0;
	
	while (!GetBit(xor, diffBit))
		diffBit += 1;
		
	// there must be at least one bit which is different! (see problem statement)
	
	int xor1 = 0;
	int xor2 = 0;
	
	for (int i = 0; i < d.Length; i++)
	{
		if (GetBit(d[i], diffBit))
			xor1 ^= d[i];
		else
			xor2 ^= d[i];
	}

	return (xor1, xor2);
}

bool GetBit(int x, int num)
{
	return ((x >> num) & 1) == 1;
}