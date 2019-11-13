<Query Kind="Program" />

// This problem was asked by Palantir.
// 
// Write a program that checks whether an integer is a palindrome.
// For example, 121 is a palindrome, as well as 888. 678 is not a
// palindrome. Do not convert the integer into a string.

void Main()
{
	IsPalindrome(121).Dump("true");
	IsPalindrome(888).Dump("true");
	IsPalindrome(678).Dump("false");
}

bool IsPalindrome(long x)
{
	int n = Len(x);
	
	for (int i = 0; i < n / 2; i++)
		if (Digit(x, i) != Digit(x, n - i - 1))
			return false;
			
	return true;
}

int Digit(long x, int k)
{
	if (k == 0)
		return (int)(x % 10);
	else
		return Digit(x / 10, k - 1);
}

int Len(long x)
{
	int len = 1;
	
	while (x >= 10)
	{
		x = x / 10;
		len += 1;
	}
	
	return len;
}
