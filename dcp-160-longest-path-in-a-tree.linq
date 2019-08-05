<Query Kind="Program" />

// This problem was asked by Uber.
// 
// Given a tree where each edge has a weight, compute the length
// of the longest path in the tree.
// 
// For example, given the following tree:
// 
//    a
//   /|\
//  b c d
//     / \
//    e   f
//   / \
//  g   h
// 
// and the weights: a-b: 3, a-c: 5, a-d: 8, d-e: 2, d-f: 4, e-g: 1, e-h: 1, the longest path would be c -> a -> d -> f, with a length of 17.
// 
// The path does not have to pass through the root, and each node can have any amount of children.

void Main()
{
	TestCase1();
	TestCase2();
}

public void TestCase1()
{
	var graph = new Graph();

	var a = new Node("a");
	var b = new Node("b");
	var c = new Node("c");
	var d = new Node("d");
	var e = new Node("e");
	var f = new Node("f");
	var g = new Node("g");
	var h = new Node("h");

	graph.Nodes.AddRange(new[] { a, b, c, d, e, f, g, h });

	graph.Edges.Add(new Edge(a, b, 3));
	graph.Edges.Add(new Edge(a, c, 5));
	graph.Edges.Add(new Edge(a, d, 8));
	graph.Edges.Add(new Edge(d, e, 2));
	graph.Edges.Add(new Edge(d, f, 4));
	graph.Edges.Add(new Edge(e, g, 1));
	graph.Edges.Add(new Edge(e, h, 1));

	//MaxDistanceToLeafFrom(graph, c).Dump("17");

	MaxDistanceInGraph(graph).Dump("17");
}

public void TestCase2()
{
	var graph = new Graph();

	var a = new Node("a");
	var b = new Node("b");
	var c = new Node("c");
	var d = new Node("d");
	var e = new Node("e");
	var f = new Node("f");
	var g = new Node("g");
	var h = new Node("h");

	graph.Nodes.AddRange(new[] { a, b, c, d, e, f, g, h });

	graph.Edges.Add(new Edge(a, b, 3));
	graph.Edges.Add(new Edge(a, c, 5));
	graph.Edges.Add(new Edge(a, d, 8));
	graph.Edges.Add(new Edge(d, e, 2));
	graph.Edges.Add(new Edge(d, f, 4));
	graph.Edges.Add(new Edge(e, g, 1));
	graph.Edges.Add(new Edge(e, h, 3));

	//MaxDistanceToLeafFrom(graph, c).Dump("17");

	MaxDistanceInGraph(graph).Dump("18");
}


public int MaxDistanceInGraph(Graph g)
{
	// O(n^2)
	// for all the nodes compute max distance to any other node
	
	int max = -1;
	
	foreach (var node in g.Nodes)
	{
		int cur = MaxDistanceToLeafFrom(g, node);
		
		if (cur > max)
			max = cur;
	}
	
	return max;
}

public int MaxDistanceToLeafFrom(Graph g, Node node)
{
	// okay so we just start at some node and expand till we traverse
	// all the nodes, we can do that in O(n)
	
	Dictionary<Node, int> distances = new Dictionary<UserQuery.Node, int>();
	
	distances[node] = 0; // distance to itself!
	
	HashSet<Node> traversed = new HashSet<UserQuery.Node>();

	Queue<Node> active = new Queue<UserQuery.Node>();
	
	active.Enqueue(node);
	
	while (active.Count > 0)
	{
		// take active node and calculate distances to all neigbors
		
		Node cur = active.Dequeue();
		
		var neighbors = g.GetNeighbors(cur);
		
		foreach (var neighbor in neighbors)
		{
			if (traversed.Contains(neighbor.n))
				continue;
			
			// calc the distance!
			distances[neighbor.n] = distances[cur] + neighbor.w;
			
			// repeat for neighbor!
			active.Enqueue(neighbor.n);
		}
		
		traversed.Add(cur);
	}
	
	//distances.Dump();
	
	return distances.Values.Max();
}

public class Edge
{
	public Node A { get; set; }
	public Node B { get; set; }
	public int Weight { get; set; }
	
	public Edge(Node a, Node b, int weight)
	{
		A = a;
		B = b;
		Weight = weight;
	}
}

public class Graph
{
	public List<Node> Nodes { get; set; }
	public List<Edge> Edges { get; set; }
	
	public Graph()
	{
		Nodes = new List<UserQuery.Node>();
		Edges = new List<UserQuery.Edge>();
	}
	
	public IEnumerable<(Node n, int w)> GetNeighbors(Node node)
	{
		// we can cache the result to get O(1) search there
		
		var result = new List<(UserQuery.Node n, int w)>();
		
		foreach (var edge in Edges)
		{
			if (edge.A == node)
				result.Add((edge.B, edge.Weight));
			else if (edge.B == node)
				result.Add((edge.A, edge.Weight));
		}
		
		return result;
	}
}

public class Node
{
	public string Label { get; set; }
	
	public Node(string label)
	{
		Label = label;
	}
}