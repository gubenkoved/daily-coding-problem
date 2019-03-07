<Query Kind="Program" />

// This problem was asked by Two Sigma.
// 
// Using a function rand5() that returns an integer from 1 to 5
// (inclusive) with uniform probability, implement a function
// rand7() that returns an integer from 1 to 7 (inclusive).

void Main()
{
	//TestDistribution(Rand5);
	TestDistribution(Rand7);
}

private static Random _rnd = new Random();

public void TestDistribution(Func<int> gen)
{
	Dictionary<int, int> freqMap = new Dictionary<int, int>();
	
	for (int i = 0; i < 1000000; i++)
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

public int Rand7()
{
	//return (Rand5() + Rand5()) % 7 + 1;
	
	// t is uniformely distributed between 1 and 25
	int t;

	do
	{
		t = (Rand5() - 1) * 5 + Rand5();
	} while (t > 21);
	
	return t % 7 + 1;
}