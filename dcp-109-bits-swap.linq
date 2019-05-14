<Query Kind="Program" />

// This problem was asked by Cisco.
// 
// Given an unsigned 8-bit integer, swap its even and odd bits.
// The 1st and 2nd bit should be swapped, the 3rd and 4th bit
// should be swapped, and so on.
// 
// For example, 10101010 should be 01010101. 11100010 should
// be 11010001.
// 
// Bonus: Can you do this in one line?

void Main()
{
	// wow, C# 7 supports binary literals!

	$"{Convert.ToString(0b_1010_1010, 2)} -> {Convert.ToString(SwapBits(0b_1010_1010), 2).PadLeft(8, '0')}".Dump("01010101");
	$"{Convert.ToString(0b_1110_0010, 2)} -> {Convert.ToString(SwapBits(0b_1110_0010), 2).PadLeft(8, '0')}".Dump("11010001");
	$"{Convert.ToString(0b_1001_0110, 2)} -> {Convert.ToString(SwapBits(0b_1001_0110), 2).PadLeft(8, '0')}".Dump("01101001");
}

uint SwapBits(uint a)
{
	// you can see why the below is the case just by writing down
	// numbers of bits before and after swap
	return ((a >> 1) & 0b_0101_0101) | ((a << 1) & 0b_1010_1010);
}
