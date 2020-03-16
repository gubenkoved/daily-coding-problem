<Query Kind="Program" />

// This problem was asked by LinkedIn.
// 
// A wall consists of several rows of bricks of various integer lengths and uniform height.
// Your goal is to find a vertical line going from the top to the bottom of the wall that
// cuts through the fewest number of bricks. If the line goes through the edge between two
// bricks, this does not count as a cut.
// 
// For example, suppose the input is as follows, where values in each row represent the
// lengths of bricks in that row:
// 
// [[3, 5, 1, 1],
//  [2, 3, 3, 2],
//  [5, 5],
//  [4, 4, 2],
//  [1, 3, 3, 3],
//  [1, 1, 6, 1, 1]]
//
// The best we can we do here is to draw a line after the eighth brick, which will only
// require cutting through the bricks in the third and fifth row.
// 
// Given an input consisting of brick lengths for each row such as the one above, return
// the fewest number of bricks that must be cut to create a vertical line.

void Main()
{
	CutLocation(
		new[]
		{
			new [] { 1, 1, 1, 1 },
			new [] { 2, 2, },
			new [] { 4, },
		}).Dump("2");

	CutLocation(
		new[]
		{
			new [] { 1, 2, 1 },
			new [] { 2, 2, },
			new [] { 4, },
		}).Dump("1");

	CutLocation(
		new[]
		{
			new [] { 3, 5, 1, 1 },
			new [] { 2, 3, 3, 2 },
			new [] { 5, 5 },
			new [] { 4, 4, 2 },
			new [] { 1, 3, 3, 3 },
			new [] { 1, 1, 6, 1, 1 },
		}).Dump("8");
}

int CutLocation(int[][] wall)
{
	// find a partial sum value of the maximal frequency
	// because it means that this particular sum has the biggest amount 
	// of coincidences and so we cut minimal amount of blocks

	Dictionary<int, int> sumFreq = new Dictionary<int, int>();
	
	for (int row = 0; row < wall.Length; row++)
	{
		int sum = 0;
		
		foreach (int w in wall[row])
		{
			if (sum != 0)
			{
				if (!sumFreq.ContainsKey(sum))
					sumFreq[sum] = 0;
				
				sumFreq[sum] += 1;
			}
				
			sum += w;
		}
	}
	
	int maxFreq = int.MinValue;
	int cutAt = -1;
	
	foreach (int sum in sumFreq.Keys)
	{
		if (sumFreq[sum] > maxFreq)
		{
			maxFreq = sumFreq[sum];
			cutAt = sum;
		}
	}
	
	Util.Metatext($"cut through {wall.Length - maxFreq} bricks").Dump();
	
	return cutAt;
}
