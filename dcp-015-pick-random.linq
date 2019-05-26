<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// Given a stream of elements too large to store in memory, pick a random element from the stream with uniform probability.

void Main()
{
	// Method 1
	// let's suppose we can only fit K elements in memory
	// then we can go grab K elements and pick random from there
	// repeat until we consume all N elements
	// then pick the random accross all these winners of round 1
	// the problem is that it still might be too much -- amount of winners
	// is N / K which can be arbitrary big number
	
	// Method 2
	// Count amount of numbers, then pick a nubmer i from 1 to N, pick i-th number
	// will not work if we can not rewind stream
	
	// Method 3
	// No rewind. Each element should be picked with 1/n probability.
	// Iteratively, one by one we can run random generator and always
	// have "running random"; if previos value is aggregation over k elements
	// then it should stay with probability k/(k+1), otherwise take new number as "running random"
	// we should probably have buffer of Q elements in order to decrease amount of rounds
	// after we picked winning buffer we take a random from there
	// Ha! Looks like I came up with something that is called "Reservoir sampling"
	// https://en.wikipedia.org/wiki/Reservoir_sampling
	
	//var source = Generator(1000 * 1000 * 150);
	var source = Generator(1000);
	
	//PickRandom(source).Dump();
	
	TestDistribution(() => PickRandom(source), 100000);
}

// simulates stream of numbers too large to fit into memory
IEnumerable<long> Generator(long n)
{
	for (long i = 0; i < n; i++)
		yield return i;
}

void TestDistribution<T>(Func<T> producer, int repetitions)
{
	Dictionary<T, int> freq = new Dictionary<T, int>();
	
	for (int i = 0; i < repetitions; i++)
	{
		T val = producer();
		
		if (!freq.ContainsKey(val))
			freq[val] = 0;
			
		freq[val] += 1;
	}
	
	freq.Chart(x => x.Key, x => x.Value, LINQPad.Util.SeriesType.Point).Dump();
}

Random _rnd = new Random(Guid.NewGuid().GetHashCode());

T PickRandom<T>(IEnumerable<T> stream)
{
	int blockSize = 100;
	long readCounter = 0;
	
	List<T> winnerBlock = new List<T>();
	List<T> currentBlock = new List<T>();
	
	var enumerator = stream.GetEnumerator();
	
	while (enumerator.MoveNext())
	{
		currentBlock.Add(enumerator.Current);
		readCounter += 1;
		
		if (currentBlock.Count == blockSize)
		{
			// pick randomly (weighted) against winner block
			// with probability block size / read counter we should pick new block
			double targetRand = currentBlock.Count / (double) readCounter;
			
			if (_rnd.NextDouble() <= targetRand || winnerBlock == null)
				winnerBlock = currentBlock.ToList();
				
			currentBlock = new List<T>();
		}
	}
	
	// handle not completed current block
	if (currentBlock.Any())
	{
		double targetRand = currentBlock.Count / (double)readCounter;

		if (_rnd.NextDouble() <= targetRand || winnerBlock == null)
			winnerBlock = currentBlock.ToList();
	}
	
	// pick randomly from winner block
	return winnerBlock[_rnd.Next() % winnerBlock.Count];
}