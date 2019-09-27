<Query Kind="Program" />

// This problem was asked by Amazon.
// 
// Given n numbers, find the greatest common denominator between them.
// 
// For example, given the numbers [42, 56, 14], return 14.

void Main()
{
	GcdStupid(42, 56, 14).Dump("14");
	GcdStupid(42, 56, 13).Dump("1");
	GcdStupid(42, 56, 12).Dump("2");
	
	Gcd(42, 56).Dump("14");
	Gcd(42, 56, 14).Dump("14");
	
	Gcd(51, 204, 102).Dump("51");
}

int GcdStupid(params int[] numbers)
{
	for (int i = numbers.Min(); i >= 2; i--)
	{
		if (numbers.All(n => n % i == 0))
			return i;
		
	}
	
	return 1;
}

// euclid algorithm
int Gcd(int a, int b)
{
	if (b == 0)
		return a;
	
	return Gcd(b, a % b);
}

// looks like we chain it like that...
int Gcd(params int[] numbers)
{
	int gcd = Gcd(numbers[0], numbers[1]);
	
	for (int i = 2; i < numbers.Length; i++)
		gcd = Gcd(gcd, numbers[i]);
		
	return gcd;
}
