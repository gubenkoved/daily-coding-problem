<Query Kind="Program" />

// This problem was asked by Google.
// 
// Given a set of closed intervals, find the smallest
// set of numbers that covers all the intervals. If
// there are multiple smallest sets, return any of them.
// 
// For example, given the intervals [0, 3], [2, 6], [3, 4],
// [6, 9], one set of numbers that covers all these
// intervals is {3, 6}.

void Main()
{
	F(new[] { (0, 3), (2, 6), (3, 4), (6, 9) }).Dump();
	F(new[] { (0, 3), (0, 2), (0, 1), (4, 7), (5, 7), (6, 7) }).Dump();
}

IEnumerable<int> F(IEnumerable<(int l, int r)> d)
{
	// simple greedy algorithm should work out there...
	
	var sorted = d.OrderBy(x => x.l).ToArray();
	var covered = new HashSet<(int l, int r)>();
	var result = new List<int>();
	
	for (int i = 0; i < sorted.Length; i++)
	{
		var range = sorted[i];
		
		if (covered.Contains(range))
			continue;
			
		// find the best spot
		var bestCoverage = new List<(int l, int r)>();
		int bestPosition = range.l;
		
		for (int j = i; j < sorted.Length; j++)
		{
			if (sorted[j].l > range.r)
				break; // does not suit
				
			// test this point
			int position = sorted[j].l;
			
			var coverage = sorted.Where(r => InRange(r, position)).ToList();

			if (coverage.Count > bestCoverage.Count)
			{
				bestCoverage = coverage;
				bestPosition = position;
			}
		}
		
		// okay, now update the state
		result.Add(bestPosition);
		
		foreach (var coveredRange in bestCoverage)
			covered.Add(coveredRange);
	}
	
	return result;
}

bool InRange((int l, int r) range, int x)
{
	return x >= range.l && x <= range.r;
}