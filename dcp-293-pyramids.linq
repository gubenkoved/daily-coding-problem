<Query Kind="Program" />

// This problem was asked by Uber.
// 
// You have N stones in a row, and would like to create from them a pyramid.
// This pyramid should be constructed such that the height of each stone
// increases by one until reaching the tallest stone, after which the heights
// decrease by one. In addition, the start and end stones of the pyramid should
// each be one stone high.
// 
// You can change the height of any stone by paying a cost of 1 unit to lower
// its height by 1, as many times as necessary. Given this information, determine
// the lowest cost method to produce this pyramid.
// 
// For example, given the stones [1, 1, 3, 3, 2, 1], the optimal solution is
// to pay 2 to create [0, 1, 2, 3, 2, 1].

void Main()
{
	// seems like that should be a dynamic programming problem
	// also we have an uncertainty in the position where we should start to have
	// a down slope, sounds like we should by assuming all possible down slop start
	// indexies
	
	Pyramidize(new[] { 1, 2, 1, }).Dump("0");
	Pyramidize(new[] { 1, 3, 1, }).Dump("1");
	Pyramidize(new[] { 3, 3, 3, }).Dump("5");
	Pyramidize(new[] { 3, 3, 3, 3, 3, }).Dump("6");
	Pyramidize(new[] { 1, 1, 3, 3, 2, 1, }).Dump("2");
	Pyramidize(new[] { 2, 2, 4, 4, 3, 2, }).Dump("4");
	Pyramidize(new[] { 3, 3, 5, 5, 4, 3, }).Dump("9");
}

// O(n * n)
int Pyramidize(int[] a)
{
	if (a.Length < 3)
		throw new Exception("Too small!");
	
	int bestCost = int.MaxValue;
	int[] bestResult = null;
	
	for (int climaxIdx = 1; climaxIdx < a.Length; climaxIdx++)
	{
		int[] b;
		int curCost = Pyramidize(a, climaxIdx, out b);
		
		if (curCost == -1)
			continue;

		if (curCost < bestCost)
		{
			bestCost = curCost;
			bestResult = b;
		}
	}
	
	Util.Metatext(string.Join(", ", bestResult.Select(x => x.ToString()))).Dump();
	
	return bestCost;
}

// O(n)
int Pyramidize(int[] a, int climaxIdx, out int[] b)
{
	// normilize the downslope, as it's tighly specified
	
	b = a.ToArray();
	
	for (int idx = a.Length - 1; idx >= climaxIdx; idx--)
	{
		int expected = a.Length - idx;
		
		if (b[idx] < expected)
			return -1;
		
		// cut!
		b[idx] = expected;
	}
	
	for (int idx = climaxIdx - 1; idx >= 0; idx--)
	{
		if (b[idx] >= b[idx + 1])
		{
			// cut so that it's just barely less than the following one to comply
			b[idx] = b[idx + 1] - 1;
		}
	}
	
	// final pass - enforce that first (non zero) stone is 1
	
	for (int idx = 0; idx < climaxIdx; idx++)
	{
		if (b[idx] != 0)
		{
			b[idx] = 1;
			break;
		}
	}
	
	
	return a.Sum() - b.Sum();
}