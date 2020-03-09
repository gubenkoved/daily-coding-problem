<Query Kind="Program" />

// This problem was asked by Spotify.
// 
// Write a function, throw_dice(N, faces, total), that determines how many
// ways it is possible to throw N dice with some number of faces each to
// get a specific total.
// 
// For example, throw_dice(3, 6, 7) should equal 15.

void Main()
{
	// there should be purely mathematical way to compute it, but
	// I forgot the formula...
	
	CountWays(2, 6, 6).Dump("5");
	CountWays(3, 6, 7).Dump("15");
	CountWays(10, 6, 30).Dump("2930455");
}

int CountWays(int n, int faces, int total)
{
	int count = 0;

	if (total <= 0)
		return 0;

	if (n == 1)
		return total <= faces ? 1 : 0;
		
	for (int k = 1; k <= faces; k++)
	{
		int subCount = CountWays(n - 1, faces, total - k);
		
		count += subCount;
	}
		
	return count;
}
