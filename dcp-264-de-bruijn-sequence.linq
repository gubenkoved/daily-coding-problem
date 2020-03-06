<Query Kind="Program" />

// This problem was asked by LinkedIn.
// 
// Given a set of characters C and an integer k, a De Bruijn sequence
// is a cyclic sequence in which every possible k-length string of
// characters in C occurs exactly once.
// 
// For example, suppose C = {0, 1} and k = 3. Then our sequence should
// contain the substrings
// {'000', '001', '010', '011', '100', '101', '110', '111'},
// and one possible solution would be 00010111.
// 
// Create an algorithm that finds a De Bruijn sequence.

void Main()
{
	// 00101100

	// 111 000001010011100101110111 000

	// 00 01 10 11

	DeBruijn(new[] { '0', '1' }, 2).Dump("4");
	DeBruijn(new[] { '0', '1' }, 3).Dump("8");
	DeBruijn(new[] { '0', '1' }, 4).Dump("16");
	DeBruijn(new[] { '0', '1' }, 6).Dump("64");
	DeBruijn(new[] { '0', '1', '2', '3', '4', '5' }, 2).Dump("36");
	DeBruijn(new[] { '0', '1', '2', '3', '4', }, 3).Dump("125");
	DeBruijn(new[] { '0', '1', '2', '3' }, 5).Dump("1024");
	DeBruijn(new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }, 4).Dump("10000");
}

string DeBruijn(char[] alphabet, int k)
{
	// as per wikipedia, optimal DeBruijn sequence can be built as a Hamiltonian Path in the graph
	// where nodes are individual seqences and edges are formed by the adding either '0' or '1' to the
	// end of sequences (removing the first one each time, e.g. 010 -> 101 and 010 -> 100)
	
	string[] sequences = GenerateSubsequences(alphabet, k);

	Graph graph = CreateSearchGraph(sequences, alphabet);

	List<Node> path = FindHamiltonialPath2(graph.Nodes.Count, graph.Nodes.First());

	//List<Node> path = new List<Node>() { graph.Nodes.First() };
	//
	//bool found = FindHamiltonialPath(graph.Nodes.Count, path);
	//
	//if (!found)
	//	throw new Exception("Unable to find the path");

	string result = new string(path.Select(x => x.Value.Last()).ToArray());

	Util.Metatext($"len={result.Length}").Dump();
	
	return result;
}

// generates all sequences
string[] GenerateSubsequences(char[] set, int k)
{
	List<string> sequences = new List<string>();
	
	Generator(set, new char[k], k, 0, sequences);
	
	return sequences.ToArray();
}

void Generator(char[] set, char[] cur, int k, int idx, List<string> sequences)
{
	if (idx == k)
	{
		sequences.Add(new string(cur));
		return;
	}
	
	foreach (char c in set)
	{
		cur[idx] = c;
		
		Generator(set, cur, k, idx + 1, sequences);
	}
}

// graphs part
public Graph CreateSearchGraph(string[] sequences, char[] alphabet)
{
	Dictionary<string, Node> nodesMap = sequences.Select(x => new Node(x)).ToDictionary(x => x.Value, x => x);
	
	foreach (string sequence in sequences)
	{
		Node node = nodesMap[sequence];
		
		// there should be n other reachable nodes if we were to add one of the symbol from
		// alphabet to the right and remove 1 character from the left
		foreach (var c in alphabet)
		{
			string nextSequence = sequence.Substring(1) + c;
			
			node.Edges.Add(new Edge(c, node, nodesMap[nextSequence]));
		}
	}
	
	var graph = new Graph();
	
	graph.Nodes.AddRange(nodesMap.Values);
	
	return graph;
}

// returns true when found
public bool FindHamiltonialPath(int targetLen, List<Node> currentPath)
{
	if (currentPath.Count == targetLen)
		return true; // looks like we walked all the nodes
	
	Node current = currentPath.Last();
	
	foreach (var edges in current.Edges)
	{
		if (currentPath.Contains(edges.Target))
			continue;
		
		// add as the last
		currentPath.Add(edges.Target);
			
		// not yet walked
		bool found = FindHamiltonialPath(targetLen, currentPath);
		
		if (found)
			return true;
		
		// remove the last
		currentPath.RemoveAt(currentPath.Count - 1);
	}
	
	// nope, dead end
	return false;
}

public List<Node> FindHamiltonialPath2(int targetLen, Node start)
{
	Stack<Edge> stack = new Stack<Edge>();
	
	foreach (Edge edge in start.Edges)
		stack.Push(edge);

	List<Node> path = new List<Node>() { start };
	
	while (stack.Count > 0)
	{
		Edge edge = stack.Pop();
		
		// each time we pop edge we might be switching to another track and discard
		// current path (partially) imagine we went almost all the way to the dead-end
		// we will need to return
		while (path[path.Count - 1] != edge.Source)
			path.RemoveAt(path.Count - 1);
		
		// try to add it as a next in path if not yet visited
		if (path.Contains(edge.Target))
			continue;
			
		// add node as the next one
		path.Add(edge.Target);
		
		// stop condition -- we found a path with a target len
		if (path.Count == targetLen)
			return path;
		
		// start looking at childs immidiately
		foreach (Edge childEdge in edge.Target.Edges)
			stack.Push(childEdge);
	}
	
	// not found
	
	return null;
}

public class Graph
{
	public List<Node> Nodes { get; set; } = new List<Node>();
	
	public Graph()
	{
		
	}
}

public class Edge
{
	public char Value { get; set; }
	
	public Node Source { get; set; }
	public Node Target { get; set; }
	
	public Edge(char value, Node source, Node target)
	{
		Value = value;
		Source = source;
		Target = target;
	}
}

public class Node
{
	public string Value { get; set; }
	
	public List<Edge> Edges { get; set; } = new List<Edge>();
	
	public Node(string value)
	{
		Value = value;
	}
}
