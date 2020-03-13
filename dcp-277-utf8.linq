<Query Kind="Program">
  <Namespace>System.Globalization</Namespace>
</Query>

// This problem was asked by Google.
// 
// UTF-8 is a character encoding that maps each symbol to one, two, three, or four bytes.
// 
// For example, the Euro sign, €, corresponds to the three bytes 11100010 10000010 10101100.
// The rules for mapping characters are as follows:
// 
// For a single-byte character, the first bit must be zero.
// For an n-byte character, the first byte starts with n ones and a zero. The other n - 1
// bytes all start with 10.
//
// Visually, this can be represented as follows.
// 
//  Bytes   |           Byte format
// -----------------------------------------------
//    1     | 0xxxxxxx
//    2     | 110xxxxx 10xxxxxx
//    3     | 1110xxxx 10xxxxxx 10xxxxxx
//    4     | 11110xxx 10xxxxxx 10xxxxxx 10xxxxxx
//
// Write a program that takes in an array of integers representing byte values, and returns
// whether it is a valid UTF-8 encoding.

void Main()
{
	// 4 bytes = 32 bits -> ~ 4 * 10*9
	// usable  for utf - depends on the characters being used:
	// 2**7 + 2 ** 11 + 2**16 + 2**21
	
	byte[] b = Encoding.UTF8.GetBytes("€");
	
	ToBinString(b).Dump();

	IsValidUTF8(b).Dump("true");
	IsValidUTF8(new byte[] { 0b00000000, 0b00000000 }).Dump("true");
	IsValidUTF8(new byte[] { 0b10000000, 0b00000000 }).Dump("false");
	IsValidUTF8(new byte[] { 0b11000000, 0b00000000 }).Dump("false");
	IsValidUTF8(new byte[] { 0b11000000, 0b10000000 }).Dump("true");
}

bool IsValidUTF8(byte[] b)
{
	int i = 0;
	
	while (i < b.Length)
	{
		int len;
		
		byte x = b[i];
		
		if ((x >> 7) == 0b0)
			len = 1;
		else if (x >> 5 == 0b110)
			len = 2;
		else if (x >> 4 == 0b1110)
			len = 3;
		else if (x >> 3 == 0b11110)
			len = 4;
		else
			return false; // invalid
		
		// go to the next one
		i += 1;
		
		for (int k = 0; k < len - 1; k++, i++)
		{
			if (i >= b.Length)
				return false;
			
			x = b[i];
			
			if ((x >> 6) != 0b10)
				return false;
		}
	}
	
	return true;
}

string ToBinString(byte[] bytes)
{
	return string.Join(" ", bytes.Select(x => Convert.ToString(x, 2).PadLeft(8, '0' )));
}