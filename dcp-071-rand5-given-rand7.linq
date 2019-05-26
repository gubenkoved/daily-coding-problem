<Query Kind="Program" />

// This problem was asked by Two Sigma.
// 
// Using a function rand7() that returns an integer from 1 to 7 (inclusive)
// with uniform probability, implement a function rand5() that returns an
// integer from 1 to 5 (inclusive).

void Main()
{
	CheckDistribution(Rand7);
	CheckDistribution(Rand5);
}

void CheckDistribution(Func<int> f)
{
	Dictionary<int, int> freq = new Dictionary<int, int>();
	
	for (int i = 0; i < 1000000; i++)
	{
		int val = f();
		
		if (!freq.ContainsKey(val))
			freq[val] = 0;
			
		freq[val] += 1;
	}
	
	freq.Chart(x => x.Key, x => x.Value).Dump();
}

Random _rnd = new Random();

int Rand7()
{
	return _rnd.Next() % 7 + 1;
}

int Rand5()
{
	int rand;
	
	do
	{
		rand = Rand7();
	} while (rand > 5);
	
	return rand;
}