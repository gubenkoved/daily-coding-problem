<Query Kind="Program" />

// This problem was asked by Pandora.
// 
// Given an undirected graph, determine if it contains a cycle.

void Main()
{
	HasCycle(new [] { Tuple.Create(1, 2), Tuple.Create(1, 3), Tuple.Create(1, 4), }).Dump("false");
	HasCycle(new [] { Tuple.Create(1, 2), Tuple.Create(1, 3), Tuple.Create(1, 4), Tuple.Create(2, 3), }).Dump("true");
	HasCycle(new [] { Tuple.Create(1, 2), Tuple.Create(2, 3), Tuple.Create(3, 1), }).Dump("true");
}

bool HasCycle(IEnumerable<Tuple<int, int>> edges)
{
	// if we start from graph w/o any edges and will add
	// edges to the graph has a cycle if there is an edge
	// which when added connects already connected components

	int[] nodes = edges.SelectMany(x => new[] { x.Item1, x.Item2 }).Distinct().ToArray();
	
	// at the begining node is separate from others
	Dictionary<int, int> parents = nodes.ToDictionary(x => x, x => x);
	
	Func<int, int> ultimateParentFn = node =>
	{
		int cur = node;
		
		// while not the root node (which is self cycle)
		while (cur != parents[cur])
			cur = parents[cur];
			
		return cur;
	};
	
	foreach (Tuple<int, int> edge in edges)
	{
		// union edges' nodes
		int parent1 = ultimateParentFn(edge.Item1);
		int parent2 = ultimateParentFn(edge.Item2);
		
		// check if edges connects items of the same group already
		if (parent1 == parent2)
			return true;
			
		// okay, continue
		
		// merge second item into group with the first one
		parents[edge.Item2] = parent1;
	}
	
	return false;
}
