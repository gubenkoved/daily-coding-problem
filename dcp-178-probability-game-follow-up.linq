<Query Kind="Program" />

void Main()
{
	PrintFreqMap(1000000, reuse: true);
	PrintFreqMap(1000000, reuse: false);
}

void PrintFreqMap(int n, bool reuse)
{
	int[] a = Generate(n);

	Dictionary<Tuple<int, int>, int> freq = new Dictionary<System.Tuple<int, int>, int>();

	for (int i = 1; i < a.Length; i++)
	{
		int prev = a[i - 1];
		int cur = a[i];

		var key = Tuple.Create(prev, cur);

		if (!freq.ContainsKey(key))
			freq[key] = 0;

		freq[key] += 1;

		if (!reuse && cur == prev) // both are equal!
			i += 1; // do not allow reuse
	}

	freq.OrderBy(x => x.Key).Select(x => new { a = x.Key.Item1, b = x.Key.Item2, n = x.Value }).Dump();
}

int[] Generate(int n)
{
	return Enumerable.Range(0, n).Select(x => Dice()).ToArray();
}

Random _rnd = new Random();

int Dice()
{
	return _rnd.Next() % 6 + 1;
}