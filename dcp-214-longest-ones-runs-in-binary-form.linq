<Query Kind="Program" />

// This problem was asked by Stripe.
// 
// Given an integer n, return the length of the longest consecutive
// run of 1s in its binary representation.
// 
// For example, given 156, you should return 3.

void Main()
{
	LongestOnesRunInBinaryForm(156).Dump("3");
	LongestOnesRunInBinaryForm(0).Dump("0");
	LongestOnesRunInBinaryForm(1).Dump("1");
	LongestOnesRunInBinaryForm(3).Dump("2");
	LongestOnesRunInBinaryForm(7).Dump("3");
	LongestOnesRunInBinaryForm(15).Dump("4");
	LongestOnesRunInBinaryForm(14).Dump("3");
}

int LongestOnesRunInBinaryForm(int n)
{
	List<bool> bits = new List<bool>();
	
	while (n >= 2)
	{
		bits.Add(n % 2 == 1);
		
		n = n / 2;
	}
	
	bits.Add(n % 2 == 1);
	
	bits.Reverse();
	
	string binaryForm = new string(bits.Select(x => x ? '1' : '0').ToArray());
	
	return binaryForm.Split('0').Select(x => x.Count()).Max();
}