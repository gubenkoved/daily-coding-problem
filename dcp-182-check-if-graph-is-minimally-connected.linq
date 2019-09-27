<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// A graph is minimally-connected if it is connected and there is no edge
// that can be removed while still leaving the graph connected. For example,
// any binary tree is minimally-connected.
// 
// Given an undirected graph, check if the graph is minimally-connected. You
// can choose to represent the graph as either an adjacency matrix or adjacency list.

void Main()
{
	IsMinimallyConnected(Graph.Create(new[] { "a", "b", "c" }, new[] { new[] { "a", "b"}, new[] { "b", "c" }, new[] { "c", "a"}})).Dump("false");
	IsMinimallyConnected(Graph.Create(new[] { "a", "b", "c" }, new[] { new[] { "a", "b"}, new[] { "b", "c" }, })).Dump("true");
	
	IsMinimallyConnected(Graph.Create(new[] { "a", "b", "c", "d", "e" }, new[] { new[] { "a", "b" }, new[] { "b", "c" }, new[] { "a", "d" }, new[] { "b", "d" }, new[] { "c", "d" }, new[] { "d", "e" }, })).Dump("false");
	IsMinimallyConnected(Graph.Create(new[] { "a", "b", "c", "d", "e" }, new[] { new[] { "a", "d" }, new[] { "b", "d" }, new[] { "c", "d" }, new[] { "d", "e" }, })).Dump("true");
}

// inspired by kruskal algorithm...
bool IsMinimallyConnected(Graph graph)
{
	HashSet<Node> covered = new HashSet<UserQuery.Node>();
	
	foreach (Edge edge in graph.Edges)
	{
		if (covered.Contains(edge.A) && covered.Contains(edge.B))
			return false;
			
		// that's okay if we already had it there
		covered.Add(edge.A);
		covered.Add(edge.B);
	}
	
	return true;
}

public class Graph
{
	public IEnumerable<Node> Nodes { get; set; }
	public IEnumerable<Edge> Edges { get; set; }
	
	public static Graph Create(string[] nodes, string[][] edges)
	{
		var nodesObj = nodes.Select(x => new Node(x)).ToArray();
		
		List<Edge> edgesObj = new List<UserQuery.Edge>();
		
		for (int i = 0; i < edges.Length; i++)
		{
			edgesObj.Add(new Edge(
				nodesObj.First(x => x.Value == edges[i][0]),
				nodesObj.First(x => x.Value == edges[i][1])));
		}
		
		return new Graph()
		{
			Nodes = nodesObj,
			Edges = edgesObj,
		};
	}
}

public class Node
{
	public string Value { get; set; }
	
	public Node(string value)
	{
		Value = value;
	}
}

public class Edge
{
	public Node A { get; set; }
	public Node B { get; set; }
	
	public Edge(Node a, Node b)
	{
		A = a;
		B = b;
	}
}