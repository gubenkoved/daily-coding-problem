<Query Kind="Program" />

// This problem was asked by Mozilla.
// 
// A bridge in a connected (undirected) graph is an edge that,
// if removed, causes the graph to become disconnected.
// Find all the bridges in a graph.

void Main()
{
	// edge is a bridge if one node can not be reached from another in this
	// graph if we were to remove it from the graph
	
	new Graph(new [] { (0, 1), (1, 2), (2, 0), }).GetAllBridges().Dump("none");
	
	new Graph(new [] { (0, 1), (1, 2), }).GetAllBridges().Dump("two");
	
	new Graph(new [] { (0, 1), (1, 2), (2, 0), (2, 3), (3, 5), (5, 4), (4, 3) }).GetAllBridges().Dump("2-3");
}

public class Graph
{
	private Dictionary<int, List<int>> _adjacency;
	
	public Graph(IEnumerable<(int a, int b)> adjacencyList)
		:this(adjacencyList.Select(x => Tuple.Create(x.a, x.b)))
	{
		
	}
	
	public Graph(IEnumerable<Tuple<int, int>> adjacencyList)
	{
		_adjacency = new Dictionary<int, List<int>>();
		
		foreach (var edge in adjacencyList)
		{
			if (!_adjacency.ContainsKey(edge.Item1))
				_adjacency.Add(edge.Item1, new List<int>());

			if (!_adjacency.ContainsKey(edge.Item2))
				_adjacency.Add(edge.Item2, new List<int>());
				
			// graph is undirectional
			_adjacency[edge.Item1].Add(edge.Item2);
			_adjacency[edge.Item2].Add(edge.Item1);
		}
	}
	
	public bool IsBridge(int a, int b)
	{
		return !IsReachableExcludingDirectPath(a, a, b, new HashSet<int>());
	}
	
	public bool IsReachableExcludingDirectPath(int source, int cur, int target, HashSet<int> visited)
	{
		if (!_adjacency.ContainsKey(cur))
			return false;
		
		foreach (int neighbor in _adjacency[cur])
		{
			// exclude direct path
			if (source == cur && neighbor == target || target == cur && neighbor == source)
				continue; 
			
			// base case, found it!
			if (neighbor == target)
				return true;
			
			if (visited.Contains(neighbor))
				continue;
				
			// dive in!
			visited.Add(neighbor);
			
			// repeat from neighbor
			bool innerReachable = IsReachableExcludingDirectPath(source, neighbor, target, visited);
			
			if (innerReachable)
				return true;
		}
		
		return false;
	}
	
	public IEnumerable<(int a, int b)> GetAllBridges()
	{
		foreach (int a in _adjacency.Keys)
		{
			foreach (int b in _adjacency[a])
			{
				// prevent duplications since graph is undirected
				if (a > b)
					continue;

				if (IsBridge(a, b))
					yield return (a, b);
			}
		}
	}
}
