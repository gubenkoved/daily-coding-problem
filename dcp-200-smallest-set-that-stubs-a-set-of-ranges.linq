<Query Kind="Program" />

// This problem was asked by Microsoft.
// 
// Let X be a set of n intervals on the real line. We say that a set of points P
// "stabs" X if every interval in X contains at least one point in P. Compute the smallest set of points that stabs X.
// 
// For example, given the intervals [(1, 4), (4, 5), (7, 9), (9, 12)], you should return [4, 9].

void Main()
{
	// it seems to me that the greeedy algorithm should actually work out in this case
	// however, it does not align well with he fact that this problem is marked with
	// "hard" complexity level
	// here is the algothims as it first comes to my mind
	// 1. take all the ranges and sort by the start of the range in increasing order
	// 2. given that each segment should be covered it if the optimal point location skips
	//    some range entierly, it automatically means that we should go back and put the point on the
	//    skipped range, which basically contradicts the original premise as we might as well just
	//    put the point there from the beggining, so given the contradiction it should be possible to
	//    select a point location which does not skip any intervals, but rather greedely covers some prefix of the intervals
	// 3. after selecting one point we ended up with the same problem with decreased amount of ranges, so 
	//    we should just repeat (2) until we crossed all the ranges

	SmallestSetThatStabs(new[] { (1, 4), (4, 5), (7, 9), (9, 12) }).Dump();
	SmallestSetThatStabs(new[] { (1, 2), (2, 3), (4, 5), (5, 6) }).Dump();
	SmallestSetThatStabs(new[] { (1, 5), (2, 5), (3, 5), (4, 5) }).Dump();
	SmallestSetThatStabs(new[] { (1, 2), (3, 4), (5, 6), (7, 8) }).Dump();
}

IEnumerable<int> SmallestSetThatStabs((int start, int end)[] intervals)
{
	List<int> points = new List<int>();
	
	var sorted = intervals.OrderBy(item => item.start).ToArray();
	
	HashSet<(int x, int y)> covered = new HashSet<(int x, int y)>();
	
	foreach (var interval in sorted)
	{
		if (covered.Count == intervals.Length)
			break;
		
		int next = sorted.Where(x => !covered.Contains(x)).Min(x => x.end);
		
		foreach (var interval2 in intervals)
			if (!covered.Contains(interval2) && interval2.start <= next && interval2.end >= next)
				covered.Add(interval2);
				
		points.Add(next);
	}
	
	return points;
}
