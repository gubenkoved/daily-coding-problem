<Query Kind="Program" />

// This problem was asked by Google.
// 
// Given an undirected graph represented as an adjacency
// matrix and an integer k, write a function to determine
// whether each vertex in the graph can be colored such
// that no two adjacent vertices share the same color
// using at most k colors.

void Main()
{
	// 1. looks like finding the minimum amount of colors
	// is a hard(er) problem, checking any given k
	// can be solved with simple backtracing search
	// 2. also quite obvious that if N is amount of nodes then
	// the maximum amount of colors we could need is N
	
	// post-mortem
	// coloring the graph task is NP-complete
	// https://en.wikipedia.org/wiki/Graph_coloring
	// and backtracking is actually the good way to solve it!
	// https://www.geeksforgeeks.org/m-coloring-problem-backtracking-5/

	int[,] adjacency0 = new int[,]
	{
		{ 0, }
	};

	CanColor(adjacency0, 1).Dump("yes");

	// answer is 2
	int[,] adjacency1 = new int[,]
	{
		{ 0, 0, 1, 0, 0, },
		{ 0, 0, 1, 0, 0, },
		{ 1, 1, 0, 1, 1, },
		{ 0, 0, 1, 0, 0, },
		{ 0, 0, 1, 0, 0, },
	};

	CanColor(adjacency1, 1).Dump("no");
	CanColor(adjacency1, 2).Dump("yes");

	// 3
	int[,] adjacency2 = new int[,]
	{
		{ 0, 1, 1, 1, 0, },
		{ 1, 0, 1, 0, 1, },
		{ 1, 1, 0, 1, 1, },
		{ 1, 0, 1, 0, 1, },
		{ 0, 1, 1, 1, 0, },
	};

	CanColor(adjacency2, 2).Dump("no");
	CanColor(adjacency2, 3).Dump("yes");
}

bool CanColor(int[,] adjacency, int k)
{
	Dictionary<int, int> colorMap = new Dictionary<int, int>();
	
	bool result = CanColor(adjacency, k, colorMap);
	
	if (result)
		colorMap.Dump();
	
	return result;
}

bool CanColor(int[,] adjacency, int k, Dictionary<int, int> colorMap)
{
	// color map contains index -> color mapping
	// colors are the values in [1, k] range
	
	int n = adjacency.GetLength(0);
	
	// pick not yet colored node
	for (int idx = 0; idx < n; idx++)
	{
		if (colorMap.ContainsKey(idx))
			continue;
		
		HashSet<int> usedColors = new HashSet<int>();
		
		// populate already used colors
		foreach (int adjacentIndx in GetAdjacentIndexes(adjacency, idx))
		{
			if (colorMap.ContainsKey(adjacentIndx))
				usedColors.Add(colorMap[adjacentIndx]);
		}
		
		// okay if we got there we found not yet colored node
		// try to color it
		for (int color = 1; color <= k; color++)
		{
			if (usedColors.Contains(color))
				continue;
				
			// color the node!
			colorMap[idx] = color;
			
			// dive!
			if (CanColor(adjacency, k, colorMap))
				return true;
			
			// restore state
			colorMap.Remove(idx);
		}
		
		// if we got there it means we were unable to solve the problem recoursively
		return false;
	}
	
	// if we got there it means that all nodes are colored 
	// AND no more than k colors was required
	return true;
}

IEnumerable<int> GetAdjacentIndexes(int[,] adjacency, int nodeIndx)
{
	List<int> result = new List<int>();
	
	for (int nodeIdx2 = 0; nodeIdx2 < adjacency.GetLength(1); nodeIdx2++)
	{
		if (adjacency[nodeIndx, nodeIdx2] != 0)
			result.Add(nodeIdx2);
	}
	
	return result;
}




