<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Numerics.dll</Reference>
  <Namespace>System.Numerics</Namespace>
</Query>

// This problem was asked by Google.
// 
// A regular number in mathematics is defined as one which evenly divides
// some power of 60. Equivalently, we can say that a regular number is one
// whose only prime divisors are 2, 3, and 5.
// 
// These numbers have had many applications, from helping ancient Babylonians
// keep time to tuning instruments according to the diatonic scale.
// 
// Given an integer N, write a program that returns, in order, the first N regular
// numbers.

void Main()
{
	// note -- did not get the problem right first, fixed the implementation
	// after checking with wiki
	
	int idx = 0;
	
	foreach (var n in RegularNumbers().Take(10000))
		$"{++idx}: {n}".Dump();
}

IEnumerable<long> RegularNumbers()
{
	// we basically find all numbers of form
	// 2^a * 3^b * 5^c where a, b, c >= 0
	// the only problem is to get the order right

	HashSet<long> pool = new HashSet<long>();
	
	pool.Add(1);
	
	while (true)
	{
		// take the smallest one from the pool,
		// return it and add other items which are
		// 2x, 3x and 5x of that number
		
		long cur = pool.Min();
		
		pool.Remove(cur);
		
		pool.Add(cur * 2);
		pool.Add(cur * 3);
		pool.Add(cur * 5);
		
		yield return cur;
	}
}
