<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// Given a function that generates perfectly random
// numbers between 1 and k (inclusive), where k is
// an input, write a function that shuffles a deck
// of cards represented as an array using only swaps.
// 
// It should run in O(N) time.
// 
// Hint: Make sure each one of the 52! permutations
// of the deck is equally likely.

private Random _rnd = new Random();

void Main()
{
	//FreqCheck(100000, 52, Shuffle);
	//FreqCheck(1000000, 10, Shuffle);
	//FreqCheck(100000, 10, Shuffle);
	//FreqCheck(100000, 10, ShuffleV2);
	//FreqCheck(100000, 10, ShuffleV3);
	FreqCheck(600000, 52, ShuffleV3);
}

void FreqCheck(int times, int size, Action<int[]> shuffle)
{
	// we will check frequency of given number depending on position
	for (int num = 1; num <= size; num++)
	{
		Dictionary<int, int> freqMap = new Dictionary<int, int>();

		for (int i = 0; i < times; i++)
		{
			int[] deck = Generate(size);

			shuffle(deck);

			// search num
			int idx = Array.IndexOf(deck, num);
			
			if (!freqMap.ContainsKey(idx))
				freqMap[idx] = 0;
				
			freqMap[idx] += 1;
		}


		freqMap.Chart(d => d.Key, d => d.Value, LINQPad.Util.SeriesType.Column).Dump($"{num} freq.");
	}
}

int[] Generate(int n)
{
	int[] deck = new int[n];
	
	for (int i = 0; i < n; i++)
		deck[i] = i + 1;
		
	return deck;
}

void Shuffle(int[] deck)
{
	for (int i = 0; i < deck.Length; i++)
	{
		int j = Random(deck.Length);
		
		Swap(deck, i, j);
	}
}

void ShuffleV2(int[] deck)
{
	// memorize and ensure each element is swapped only once
	HashSet<int> swappedIndexes = new HashSet<int>();
	
	for (int i = 0; i < deck.Length; i++)
	{
		if (swappedIndexes.Contains(i))
			continue;
		
		int j = Random(deck.Length);

		Swap(deck, i, j);
		
		swappedIndexes.Add(j);
	}
}

void ShuffleV3(int[] deck)
{
	// can not even explain why this one works flawlessly...
	// just decided to try it out...
	
	for (int i = 0; i < deck.Length; i++)
	{
		int j = Random(deck.Length - i) + i;

		Swap(deck, i, j);
	}
}

void Swap(int[] deck, int i, int j)
{
	if (i == j)
		return;
	
	int tmp = deck[i];
	deck[i] = deck[j];
	deck[j] = tmp;
}

int Random(int k)
{
	return _rnd.Next() % k;
}
