<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Numerics.dll</Reference>
  <Namespace>System.Numerics</Namespace>
</Query>

// This problem was asked by Amazon.
// 
// There exists a staircase with N steps, and you can climb up either 1 or 2 steps
// at a time. Given N, write a function that returns the number of unique ways
// you can climb the staircase. The order of the steps matters.
// 
// For example, if N is 4, then there are 5 unique ways:
// 
// 1, 1, 1, 1
// 2, 1, 1
// 1, 2, 1
// 1, 1, 2
// 2, 2
// 
// What if, instead of being able to climb 1 or 2 steps at a time, you could climb
// any number from a set of positive integers X? For example,
// if X = { 1, 3, 5 }, you could climb 1, 3, or 5 steps at a time.


void Main()
{
	//f(4).Select(x => string.Join(", ", x)).Dump();
	//f(27).Select(x => string.Join(", ", x)).Dump(); // 2 sec
	//f_plus_wrapped(27).Select(x => string.Join(", ", x)).Dump(); // 0.25 sec
	
	//f_plus_wrapped(30).Select(x => string.Join(", ", x)).Dump(); // 0.25 sec
	
	//f_plus_plus(30).Dump(); // 0.05 sec
	//f_plus_plus(40).Dump(); // 7.5 sec
	
	//g(40).Dump(); // 0.001
	//g(50).Dump(); // 0.005
	//g(1000).Dump(); // 0.010
	
	//h(1000).Dump(); // 0 sec :)
	//h(10).Dump(); // 0 sec :)
	
	// AHA that's FIBBONACHI SEQUENCE
	
	for (int i = 1; i < 500; i++)
		h(i).Dump();
}

public List<List<int>> f(int n)
{
	int[] possible = new int[] { 1, 2, };
	
	List<List<int>> ways = new List<List<int>>();
	
	foreach (var step in possible)
	{
		if (step > n)
			continue; // too big of a step
			
		if (step == n)
		{
			ways.Add(new List<int>() { step });
			continue;
		}
		
		List<List<int>> subways = f(n - step);
		
		foreach (var subway in subways)
		{
			var copy = subway.ToList();
			copy.Add(step);
			ways.Add(copy);
		}
	}
	
	return ways;
}

public List<List<int>> f_plus_wrapped(int n)
{
	List<List<int>> results = new List<List<int>>();
	f_plus(n, new List<int>(), results);
	return results;
}

public void f_plus(int n, List<int> current, List<List<int>> results)
{
	int[] possible = new int[] { 1, 2, };
	
	foreach (var step in possible)
	{
		if (step > n)
			continue;

		if (step == n)
		{
			var copy = current.ToList();
			copy.Add(step);
			results.Add(copy);
			continue;
		}
		
		// recoursion dive
		current.Add(step);
		
		f_plus(n - step, current, results);
		
		current.RemoveAt(current.Count - 1);
	}
}

public int f_plus_plus(int n)
{
	int[] possible = new int[] { 1, 2, };

	int ways = 0;

	foreach (var step in possible)
	{
		if (step > n)
			continue;

		if (step == n)
		{
			ways += 1;
			continue;
		}

		// recoursion dive
		ways += f_plus_plus(n - step);
	}
	
	return ways;
}

public long g(int n, Dictionary<long, long> cache = null)
{
	// dynamic programming based, do not solve solved task over and over again, cache the results!

	if (cache == null)
		cache = new Dictionary<long, long>();

	if (cache.ContainsKey(n))
		return cache[n];

	int[] possible = new int[] { 1, 2, };

	long ways = 0;

	foreach (var step in possible)
	{
		if (step > n)
			continue;

		if (step == n)
		{
			ways += 1;
			continue;
		}

		// recoursion dive
		ways += g(n - step, cache);
	}
	
	cache[n] = ways;

	return ways;
}

public BigInteger h(int n)
{
	BigInteger[] r = new BigInteger[n + 2];
	
	r[1] = 1; // there is 1 single way to express 1
	r[2] = 2; // 2 and 1, 1
	
	for (int i = 3; i <= n; i++)
		r[i] = r[i - 1] + r[i - 2];
	
	return r[n];
}