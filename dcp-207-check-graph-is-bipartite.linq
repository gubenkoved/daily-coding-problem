<Query Kind="Program" />

// This problem was asked by Dropbox.
// 
// Given an undirected graph G, check whether it is bipartite. Recall that a graph
// is bipartite if its vertices can be divided into two independent sets, U and V,
// such that no edge connects vertices of the same set.


void Main()
{
	IsBipartite(Graph.Build(new[] { "a", "b", "c", }, new[] { ("a", "b"), ("b", "c"), ("c", "a"), })).Dump("false");
	IsBipartite(Graph.Build(new[] { "a", "b", "c", "d" }, new[] { ("a", "b"), ("b", "c"), ("c", "d"), ("d", "a") })).Dump("true");
	IsBipartite(Graph.Build(new[] { "a", "b", "c", "d" }, new[] { ("a", "b"), ("b", "c"), ("c", "d"), ("d", "a"), ("a", "c") })).Dump("false");
	IsBipartite(Graph.Build(new[] { "a", "b", "c", "d", "e", "f" }, new[] { ("a", "b"), ("b", "c"), ("c", "d"), ("d", "e"), ("e", "f") })).Dump("true");
	IsBipartite(Graph.Build(new[] { "a", "b", "c", "d", "e", "f" }, new[] { ("a", "b"), ("b", "c"), ("c", "d"), ("d", "e"), ("e", "f"), ("f", "a") })).Dump("true");
	IsBipartite(Graph.Build(new[] { "a", "b", "c", "d", "e", "f" }, new[] { ("a", "b"), ("b", "c"), ("c", "d"), ("d", "e"), ("e", "f"), ("f", "a"), ("a", "c") })).Dump("false");
	IsBipartite(Graph.Build(new[] { "a", "b", "c", "d", "e", "f" }, new[] { ("a", "b"), ("b", "c"), ("c", "d"), ("d", "e"), ("e", "f"), ("f", "a"), ("a", "d") })).Dump("true");
}

public bool IsBipartite(Graph graph)
{
	// algorithm:
	// start with arbitrary vertex, assign it class a
	// then find all accessible vertexes, assign it class b
	// then repeat the process starting with marked labels
	// until we assigned all the labels OR there is a conflict
	// when same node being already "colored" with one "color"
	// should be recolored with another
	
	Dictionary<Vertex, int> colorMap = new Dictionary<UserQuery.Vertex, int>();

	// pick uncoloroed
	Vertex start = graph.Vertexes.First();
	
	bool conflict = ColorRecoursive(graph, start, 0, colorMap);
	
	if (colorMap.Count != graph.Vertexes.Count())
		throw new Exception("graph is not connected");
	
	if (conflict)
		return false;

	string.Join(", ", colorMap.Select(x => $"{x.Key.Label} is color {x.Value}")).Dump();
		
	return true;
}

private bool ColorRecoursive(Graph graph, Vertex current, int color, Dictionary<Vertex, int> colorMap)
{
	colorMap[current] = color;
	
	int nextColor = (color + 1) % 2;
	
	var neighbors = graph.Edges
		.Where(x => x.A == current || x.B == current)
		.SelectMany(x => new[] { x.A, x.B })
		.Where(x => x != current)
		.ToArray();
	
	foreach (var neighbor in neighbors)
	{
		if (colorMap.ContainsKey(neighbor))
		{
			if (colorMap[neighbor] != nextColor)
			{
				$"unable to color on {neighbor.Label}".Dump();
				return true; // conflict!
			}
			else
			{
				continue; // already colored with the "right" color
			}
		}
			
		// not yet colored!
		bool innerConflict = ColorRecoursive(graph, neighbor, nextColor, colorMap);
		
		if (innerConflict)
			return true; // found a conflict
	}
	
	return false; // no conflict
}

public class Graph
{
	public IEnumerable<Vertex> Vertexes { get; } = new List<Vertex>();
	public IEnumerable<Edge> Edges { get; } = new List<Edge>();
	
	private Graph(IEnumerable<Vertex> vertexes, IEnumerable<Edge> edges)
	{
		Vertexes = vertexes;
		Edges = edges;
	}
	
	public static Graph Build(string[] labels, (string a, string b)[] edges)
	{
		var vertexesMap = labels.Select(x => new Vertex(x)).ToDictionary(x => x.Label);
		
		var edges2 = new List<Edge>();
		
		foreach (var edge in edges)
			edges2.Add(new Edge(vertexesMap[edge.a], vertexesMap[edge.b]));
		
		return new Graph(vertexesMap.Values, edges2);
	}
}

public class Vertex
{
	public string Label { get; }
	
	public Vertex(string label)
	{
		Label = label;
	}
}

public class Edge
{
	public Vertex A { get; }
	public Vertex B { get; }
	
	public Edge(Vertex a, Vertex b)
	{
		A = a;
		B = b;
	}
}