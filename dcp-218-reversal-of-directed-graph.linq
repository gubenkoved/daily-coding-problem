<Query Kind="Program" />

// This problem was asked by Yahoo.
// 
// Write an algorithm that computes the reversal of a directed graph.
// For example, if a graph consists of A -> B -> C, it should become A <- B <- C.

void Main()
{
	// what the hell?
	
	var graph = new Graph();
	
	var a = new Node("a");
	var b = new Node("b");
	var c = new Node("c");
	
	graph.AddNode(a);
	graph.AddNode(b);
	graph.AddNode(c);
	
	graph.AddEdge(a, b);
	graph.AddEdge(b, c);
	
	graph.Print();
	
	graph.Reverse();
	
	graph.Print();
}

public class Node
{
	public string Value { get; set; }
	
	public Node(string val)
	{
		Value = val;
	}
}

public class Edge
{
	public Node From { get; }
	public Node To { get; }
	
	public Edge(Node from, Node to)
	{
		From = from;
		To = to;
	}
}

public class Graph
{
	public HashSet<Node> Nodes { get; private set; }
	public HashSet<Edge> Edges { get; private set; }
	
	public Graph()
	{
		Nodes = new HashSet<Node>();
		Edges = new HashSet<Edge>();
	}
	
	public void AddNode(Node node)
	{
		Nodes.Add(node);
	}
	
	public void AddEdge(Node from, Node to)
	{
		if (Edges.Any(x => x.From == from && x.To == to))
			return;
			
		Edges.Add(new Edge(from, to));
	}
	
	public void Reverse()
	{
		HashSet<Edge> newEdges = new HashSet<Edge>();
		
		foreach (Edge edge in Edges)
		{
			newEdges.Add(new Edge(edge.To, edge.From));
		}
		
		// replace the edges
		Edges = newEdges;
	}
	
	public void Print()
	{
		this.Dump();
	}
}