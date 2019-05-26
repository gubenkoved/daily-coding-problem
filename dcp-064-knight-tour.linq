<Query Kind="Program" />

// This problem was asked by Google.
// 
// A knight's tour is a sequence of moves by a knight
// on a chessboard such that all squares are visited once.
// 
// Given N, write a function to return the number of knight's
// tours on an N by N chessboard.

void Main()
{
	Func<int, long> f = KinghtTourV2;
	
	f(1).Dump("1x1"); // 1
	f(2).Dump("2x2"); // 0
	f(3).Dump("3x3"); // 0
	f(4).Dump("4x4"); // 0
	f(5).Dump("5x5"); // 1728
	//f(6).Dump("6x6"); // :(
	//f(7).Dump("7x7");
	//f(8).Dump("8x8");
}

// brute force solution -- only capable of solving 5x5 realistically
// as it already takes 15s to run...
long KinghtTour(int n)
{
	long count = 0;

	for (int x = 0; x < n; x++)
	{
		for (int y = 0; y < n; y++)
		{
			var visited = new bool[n, n];
			visited[x, y] = true;
			count += Traverse(visited, 1, x, y);
		}
	}
	
	return count;
}

long Traverse(bool[,] visited, int visitedCount, int x, int y)
{
	int n = visited.GetLength(0);
	
	if (visitedCount == n * n)
		return 1;

	var adjacent = GetAdjacent(n, x, y);
	
	long ways = 0;

	foreach (var point in adjacent)
	{
		if (visited[point.Item1, point.Item2])
			continue;
		
		visited[point.Item1, point.Item2] = true;
		
		ways += Traverse(visited, visitedCount + 1, point.Item1, point.Item2);
		
		visited[point.Item1, point.Item2] = false;
	}

	return ways; 
}

long KinghtTourV2(int n)
{
	long result = 0;

	int[,] degrees = new int[n, n];

	for (int x = 0; x < n; x++)
		for (int y = 0; y < n; y++)
			degrees[x, y] = GetAdjacent(n, x, y).Count();

	for (int x = 0; x < n; x++)
		for (int y = 0; y < n; y++)
			result += TraverseV2(new bool[n, n], degrees, 0, x, y);

	return result;
}

// optimization for naive traverse -- maintain an array that shows numbers of
// ways given cell is reachable potentially from not yet traversed cells
// if at new step some cell gets counter of 0 and not yet visited -- we can stop right away
long TraverseV2(bool[,] visited, int[,] degrees, int visitedCount, int x, int y)
{
	int n = visited.GetLength(0);

	// see if we visted them all
	if (visitedCount == n * n - 1)
		return 1;

	// marking current as visited
	visited[x, y] = true;
	
	var adjacent = GetAdjacent(n, x, y);
	
	long ways = 0;

	bool pruneMode = false;

	// update degrees
	foreach (var point in adjacent)
	{
		degrees[point.Item1, point.Item2] -= 1;
		
		if (!visited[point.Item1, point.Item2] && degrees[point.Item1, point.Item2] == 0)
			pruneMode = true;
	}

	//degrees.Dump($"visited {visitedCount}, ({x}, {y}), prune: {prune}");
	
	//if (prune) visited.Dump("visited cells");

	foreach (var point in adjacent)
	{
		if (visited[point.Item1, point.Item2])
			continue;

		// if we found some cell which has 0 reachables then
		// we do not need to traverse other ones, as this cell will be not reachable
		if (pruneMode && degrees[point.Item1, point.Item2] != 0)
			continue;

		ways += TraverseV2(visited, degrees, visitedCount + 1, point.Item1, point.Item2);
	}

	// restore state
	foreach (var point in adjacent)
	{
		degrees[point.Item1, point.Item2] += 1;
	}
		
	visited[x, y] = false;

	return ways;
}

Tuple<int, int>[] GetAdjacentFake(int n, int x, int y)
{
	return new[]
	{
		Tuple.Create(x - 1, y),
		Tuple.Create(x + 1, y),
		Tuple.Create(x, y - 1),
		Tuple.Create(x, y + 1),
	}.Where(p => p.Item1 >= 0 && p.Item1 < n && p.Item2 >= 0 && p.Item2 < n).ToArray();
}

Tuple<int, int>[] GetAdjacent(int n, int x, int y)
{
	return new []
	{
		Tuple.Create(x - 1, y - 2),
		Tuple.Create(x + 1, y - 2),
		Tuple.Create(x - 1, y + 2),
		Tuple.Create(x + 1, y + 2),
		Tuple.Create(x - 2, y - 1),
		Tuple.Create(x - 2, y + 1),
		Tuple.Create(x + 2, y - 1),
		Tuple.Create(x + 2, y + 1),
	}.Where(p => p.Item1 >= 0 && p.Item1 < n && p.Item2 >= 0 && p.Item2 < n).ToArray();
}