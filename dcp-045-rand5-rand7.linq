<Query Kind="Program" />

// This problem was asked by Two Sigma.
// 
// Using a function rand5() that returns an integer from 1 to 5
// (inclusive) with uniform probability, implement a function
// rand7() that returns an integer from 1 to 7 (inclusive).

void Main()
{
	int n = 1000 * 1000;
	//TestDistribution(Rand5);
	TestDistribution(Rand7, n);
	
	((decimal)_loopCount / n).Dump("overhead rate");
}

private static Random _rnd = new Random();

public void TestDistribution(Func<int> gen, int n)
{
	_loopCount = 0;
	
	Dictionary<int, int> freqMap = new Dictionary<int, int>();
	
	for (int i = 0; i < n; i++)
	{
		int val = gen();
		
		if (!freqMap.ContainsKey(val))
			freqMap[val] = 0;
			
		freqMap[val] += 1;
	}
	
	freqMap.Dump();
}

public int Rand5()
{
	return _rnd.Next() % 5 + 1;
}

private int _loopCount = 0;

public int Rand7()
{
	// naive solution does not work, as distribution is not unifirom
	//return (Rand5() + Rand5()) % 7 + 1; 
	
	// t is uniformely distributed between 1 and 25
	int t;

	do
	{
		_loopCount += 1;
		t = (Rand5() - 1) * 5 + Rand5();
	} while (t > 21);
	
	return t % 7 + 1;
}