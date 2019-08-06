<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// Given a 32-bit integer, return the number with its bits reversed.
// 
// For example, given the binary number
// 			1111 0000 1111 0000 1111 0000 1111 0000,
// return   0000 1111 0000 1111 0000 1111 0000 1111.

void Main()
{
	ToBitsString(Reverse(0b1111_0000_1111_0000_1111_0000_1111_0000)).Dump();
	ToBitsString(Reverse(0b0000_0000_1111_0000_1111_0000_1111_0000)).Dump();
}

string ToBitsString(uint x)
{
	var ba = new BitArray(new int[] { (int)x });

	return string.Join("", ba.OfType<bool>().Select(b => b ? "1" : "0").Reverse());
}

uint Reverse(uint x)
{
	return ~x;
}
