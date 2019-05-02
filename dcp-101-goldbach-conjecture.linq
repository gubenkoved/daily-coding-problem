<Query Kind="Program" />

// This problem was asked by Alibaba.
// 
// Given an even number (greater than 2), return two prime numbers whose sum will be equal to the given number.
// 
// A solution will always exist. See Goldbach’s conjecture.
// 
// Example:
// 
// Input: 4
// Output: 2 + 2 = 4
// If there are more than one solution possible, return the lexicographically smaller solution.
// 
// If [a, b] is one solution with a <= b, and [c, d] is another solution with c <= d, then
// 
// [a, b] < [c, d]
// If a < c OR a==c AND b < d.



void Main()
{
	for (int x = 4; x < 10000; x += 2)
	{
		var result = F(x);

		$"n={x}: ({result.a}, {result.b})".Dump();
	}
}

(int a, int b) F(int n)
{
	HashSet<int> primes = new HashSet<int>(PrimesTill(n));
	
	for (int a = 2; a <= n / 2; a++)
	{
		int b = n - a;
		
		if (primes.Contains(a) && primes.Contains(b))
			return (a, b);
	}
	
	throw new Exception("Not found!");
}

IEnumerable<int> PrimesTill(int n)
{
	List<int> primes = new List<int>() { 2 };
	
	for (int x = 3; x < n; x += 2)
	{
		bool prime = true;
		
		for (int t = 2; t <= Math.Sqrt(x); t++)
		{
			if (x % t == 0)
			{
				prime = false;
				continue;
			}
		}
		
		if (prime)
			primes.Add(x);
	}
	
	return primes;
}
