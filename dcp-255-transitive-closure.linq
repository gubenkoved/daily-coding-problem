<Query Kind="Program" />

// This problem was asked by Microsoft.
// 
// The transitive closure of a graph is a measure of which vertices are reachable
// from other vertices. It can be represented as a matrix M, where M[i][j] == 1 if
// there is a path between vertices i and j, and otherwise 0.
// 
// For example, suppose we are given the following graph in adjacency list form:
// 
// graph = [
//     [0, 1, 3],
//     [1, 2],
//     [2],
//     [3]
// ]
//
// The transitive closure of this graph would be:
// 
// [1, 1, 1, 1]
// [0, 1, 1, 0]
// [0, 0, 1, 0]
// [0, 0, 0, 1]
//
// Given a graph, find its transitive closure.

void Main()
{
	TransitiveCloure(new []
	{
		new [] { 0, 1, 3 },
		new [] { 1, 2 },
		new [] { 2 },
		new [] { 3 },
	}).Dump();

	// cycles should be no problem as well
	TransitiveCloure(new[]
	{
		new [] { 0, 1, 3 },
		new [] { 1, 2 },
		new [] { 2 },
		new [] { 3, 0 },
	}).Dump();
}

int[,] TransitiveCloure(int[][] adjacency)
{
	int n = adjacency.Length;
	
	var transitiveClosure = new int[n, n];
	
	for (int i = 0; i < n; i++)
		for (int j = 0; j < n; j++)
			transitiveClosure[i, j] = IsReachable(adjacency, i, j, new HashSet<int>() { i }) ? 1 : 0;
	
	return transitiveClosure;
}

bool IsReachable(int[][] adjacency, int from, int to, HashSet<int> visited)
{
	int[] adjacent = adjacency[from];
	
	if (adjacent.Contains(to))
		return true;
		
	foreach (int neighboor in adjacent)
	{
		if (visited.Contains(neighboor))
			continue;
		
		visited.Add(neighboor);
		
		if (IsReachable(adjacency, neighboor, to, visited))
			return true;
	}
	
	return false;
}