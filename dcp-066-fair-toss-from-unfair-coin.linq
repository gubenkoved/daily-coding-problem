<Query Kind="Program" />

// This problem was asked by Square.
// 
// Assume you have access to a function toss_biased() which
// returns 0 or 1 with a probability that's not 50-50
// (but also not 0-100 or 100-0). You do not know the bias
// of the coin.
// 
// Write a function to simulate an unbiased coin toss.

Random _random = new Random();

void Main()
{
	PrintDistribution(UnfairToss, "unfair");
	PrintDistribution(FairToss, "fair");
}

void PrintDistribution(Func<int> f, string label)
{
	Dictionary<int, int> freqMap = new Dictionary<int, int>();
	
	for (int i = 0; i < 1000000; i++)
	{
		var val = f();
		
		if (!freqMap.ContainsKey(val))
			freqMap[val] = 0;
			
		freqMap[val] += 1;
	}
	
	freqMap.Chart(x => x.Key, x => x.Value, LINQPad.Util.SeriesType.BevelledPie).Dump(label);
}

int UnfairToss()
{
	if (_random.NextDouble() > 0.9)
		return 0;
	else
		return 1;		
}

int FairToss()
{
	// P(0 then 1) = P(1 then 0) even if freqs of 1/0 are different!
	
	do
	{
		int toss1 = UnfairToss();
		int toss2 = UnfairToss();
		
		if (toss1 == toss2)
			continue;
			
		return toss1;
	} while (true);
	
}
