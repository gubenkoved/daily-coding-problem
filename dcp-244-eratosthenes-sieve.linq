<Query Kind="Program" />

// This problem was asked by Square.
// 
// The Sieve of Eratosthenes is an algorithm used to generate all prime numbers
// smaller than N. The method is to take increasingly larger prime numbers, and
// mark their multiples as composite.
// 
// For example, to find all primes less than 100, we would first mark [4, 6, 8, ...]
// (multiples of two), then [6, 9, 12, ...] (multiples of three), and so on. Once we
// have done this for all primes less than N, the unmarked numbers that remain will
// be prime.
// 
// Implement this algorithm.
// 
// Bonus: Create a generator that produces primes indefinitely (that is, without taking N as an input).

void Main()
{
	Primes(smallerThan: 100).Dump();
	
	PrimesGenerator().Take(1000).Dump();
}

IEnumerable<int> Primes(int smallerThan)
{
	bool[] crossed = new bool[smallerThan];
	
	for (int i = 2; i < smallerThan; i++)
	{
		if (!crossed[i])
		{
			// prime!
			yield return i;
			
			for (int j = 2 * i; j < smallerThan; j += i)
				crossed[j] = true;
		}
	}
}

IEnumerable<int> PrimesGenerator()
{
	int x = 2;
	
	while (true)
	{
		if (IsPrime(x))
			yield return x;
		
		x += 1;
	}
}

bool IsPrime(int x)
{
	for (int d = 2; d <= Math.Sqrt(x); d++)
	{
		if (x % d == 0)
			return false;
	}
	
	return true;
}