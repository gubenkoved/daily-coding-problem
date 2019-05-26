<Query Kind="Program" />

// This question was asked by Google.
// 
// Given an integer n and a list of integers l, write a
// function that randomly generates a number from 0 to
// n-1 that isn't in l (uniform).

void Main()
{
	int n = 10;
	
	int[] counts = new int[n];
	
	for (int i = 0; i < 1000000; i++)
	{
		int r = Rand(n, new[] { 1, 2, 5, 8, 12 });
		
		counts[r] += 1;
	}
	
	counts.Dump();
}

private static Random _rnd = new Random();

public int Rand(int n, int[] l)
{
	int k = l.Count(x => x < n);
	int t = _rnd.Next() % (n - k); // t in uniform for [0, n-k)

	// now we just need to remap t into the resulting value
	int r = 0;
	
	// move r to the first available number
	while (l.Contains(r))
		r += 1;

	while (t != 0)
	{
		r += 1; // move to the next one
		
		// then move till we find free number
		while (l.Contains(r))
			r += 1;
		
		t -= 1;
	}
	
	return r;
}
