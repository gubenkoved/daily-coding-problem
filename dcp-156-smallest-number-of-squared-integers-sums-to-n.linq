<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// Given a positive integer n, find the smallest number of squared
// integers which sum to n.
// 
// For example, given n = 13, return 2 since 13 = 3^2 + 2^2 = 9 + 4.
// 
// Given n = 27, return 3 since 27 = 3^2 + 3^2 + 3^2 = 9 + 9 + 9.

void Main()
{
	// we can apply some sort of dynamic programming
	// as to find the optimal solution for n
	// we can just go via each k less then or equal to sqrt(n)
	// and map problem for n into problem for n - k^2
	
	MinNumberOfSquaredSumsToN(4).Dump("1");
	MinNumberOfSquaredSumsToN(13).Dump("2");
	MinNumberOfSquaredSumsToN(25).Dump("1");
	MinNumberOfSquaredSumsToN(27).Dump("3");
}

Dictionary<int, int> _cache = new Dictionary<int, int>();

int MinNumberOfSquaredSumsToN(int n)
{
	// todo - memorize the calculations
	
	if (n == 0)
		return 0;
	
	if (n == 1)
		return 1; // stop!
		
	if (_cache.ContainsKey(n))
		return _cache[n];
		
	int maxK = (int) Math.Sqrt(n);
	
	int best = int.MaxValue;
	
	for (int k = maxK; k >= 1; k--)
	{
		int reminder = n - k * k;
		int subNumber = MinNumberOfSquaredSumsToN(reminder) + 1;
		
		if (subNumber < best)
			best = subNumber;
	}
	
	_cache[n] = best;
	
	return best;
}
