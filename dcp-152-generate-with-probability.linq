<Query Kind="Program" />

// This problem was asked by Triplebyte.
// 
// You are given n numbers as well as n probabilities that sum up to 1.
// Write a function to generate one of the numbers with its corresponding probability.
// 
// For example, given the numbers [1, 2, 3, 4] and probabilities [0.1, 0.5, 0.2, 0.2],
// your function should return 1 10% of the time, 2 50% of the time, and 3 and 4 20% of the time.
// 
// You can generate random numbers between 0 and 1 uniformly.

void Main()
{
	CheckDistribution(() => GenerateWithProbabilities(new[] { 1, 2, 3, 4 }, new[] { 0.1, 0.5, 0.2, 0.2 }));
}

public void CheckDistribution(Func<object> gen)
{
	Dictionary<object, int> freq = new Dictionary<object, int>();
	
	for (int i = 0; i < 1000000; i++)
	{
		var value = gen();
		
		if (!freq.ContainsKey(value))
			freq[value] = 0;
			
		freq[value] += 1;
	}
	
	freq.Chart(x => x.Key, x => x.Value, LINQPad.Util.SeriesType.Column).Dump();
}

public T GenerateWithProbabilities<T>(T[] values, double[] probabilities)
{
	double rnd = Rand();
	double cummulative = 0;
	
	for (int i = 0; i < values.Length; i++)
	{
		cummulative += probabilities[i];
		
		if (rnd <= cummulative)
			return values[i];
	}
	
	// should not happen really
	return values[values.Length - 1];
}

Random _rnd = new Random();

double Rand()
{
	return _rnd.NextDouble();
}
