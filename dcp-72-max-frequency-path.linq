<Query Kind="Program" />

// This problem was asked by Google.
// 
// In a directed graph, each node is assigned an uppercase letter.
// We define a path's value as the number of most frequently-occurring
// letter along that path. For example, if a path in the graph goes
// through "ABACA", the value of the path is 3, since there are 3
// occurrences of 'A' on the path.
// 
// Given a graph with n nodes and m directed edges, return the largest
// value path of the graph. If the largest value is infinite, then return null.
// 
// The graph is represented with a string and an edge list. The i-th character
// represents the uppercase letter of the i-th node. Each tuple in the edge
// list (i, j) means there is a directed edge from the i-th node to the j-th
// node. Self-edges are possible, as well as multi-edges.
// 
// For example, the following input graph:
// 
// ABACA
// [(0, 1),
//  (0, 2),
//  (2, 3),
//  (3, 4)]
//
// Would have maximum value 3 using the path of vertices [0, 2, 3, 4], (A, A, C, A).
// 
// The following input graph:
// 
// A
// [(0, 0)]
// Should return null, since we have an infinite loop.

void Main()
{
	var graph0 = Graph.FromText(@"A");

	BiggestFrequencyPath(graph0).Dump("expected 1 (no edges)");

	var graph01 = Graph.FromText(@"A A
0 1");

	BiggestFrequencyPath(graph01).Dump("expected 2");

	var graph1 = Graph.FromText(@"A B A C A
0 1
0 2
2 3
3 4");

	BiggestFrequencyPath(graph1).Dump("expected 3");

	var graph2 = Graph.FromText(@"C A A B A C C A C A C
0 1
0 4
2 1
2 3
1 3
1 4
4 3
3 5
5 6
5 7
7 10
7 8
8 9");

	BiggestFrequencyPath(graph2).Dump("expected 5");

	// cycle case
	var graph3 = Graph.FromText(@"A
0 0");

	BiggestFrequencyPath(graph3).Dump("expected null (cycle)");

	var graph4 = Graph.FromText(@"A B C
0 1
1 2
2 0");

	BiggestFrequencyPath(graph3).Dump("expected null (big cycle)");
}

public class Node
{
	public object Value { get; set; }
	public List<Node> Reachable { get; set; } = new List<Node>();
	
	// node state needed for algorithm to work kept there for convinience
	public int? MaxReachableOfSameValue { get; set; } // this is what we want to calcualte for each node
	public bool Visited { get; set; }
}

public class Graph
{
	public List<Node> Nodes { get; set; }
	
	// format
	// A B C D E  <<< first line -- nodes values splitted by space
	// 0 1 << other lines -- from <space> to (zero based)
	// 1 2
	// 1 3
	public static Graph FromText(string text)
	{
		string[] lines = text.Split('\n').Select(x => x.Trim('\r')).ToArray();
		
		string[] nodeValues = lines[0].Split(' ').ToArray();

		Node[] nodes = nodeValues.Select(x => new Node() { Value = x }).ToArray();
		
		for (int line = 1; line < lines.Length; line++)
		{
			int fromIdx = int.Parse(lines[line].Split(' ')[0]);
			int toIdx = int.Parse(lines[line].Split(' ')[1]);
			
			nodes[fromIdx].Reachable.Add(nodes[toIdx]);
		}

		return new Graph()
		{
			Nodes = nodes.ToList(),
		};
	}
}

public class CycleFoundException : Exception
{
	public CycleFoundException(string message)
		:base (message)
	{
	}
}

// O(n * k) where n total amount of nodes and k is amount of unque values
public Node BiggestFrequencyPath(Graph graph)
{
	Node maxFreq = null;
	
	// process all the nodes -- calculate max reachable nodes count for every node
	foreach (var node in graph.Nodes)
	{
		if (node.MaxReachableOfSameValue != null)
			continue;
		
		try
		{	        
			node.MaxReachableOfSameValue = FindMaxReachable(node, node.Value);
		}
		catch (CycleFoundException)
		{
			return null;
		}
		
		if (maxFreq == null || maxFreq.MaxReachableOfSameValue < node.MaxReachableOfSameValue)
			maxFreq = node;
	}
	
	return maxFreq;
}

// fills max reachable counter for all reachable nodes from specified ones
// which have value that equals to target
public int FindMaxReachable(Node node, object targetValue)
{	
	int max = 0;

	// maintain IsVisited flag to detect cycles
	if (node.Value.Equals(targetValue))
		node.Visited = true;

	foreach (Node reachableNode in node.Reachable)
	{
		int subMax;

		// optimization: if we found node with value = target AND it's already calcualted, reuse!
		if (reachableNode.Value.Equals(targetValue) && reachableNode.MaxReachableOfSameValue != null)
		{
			subMax = reachableNode.MaxReachableOfSameValue.Value; // use already calculated value
		}
		else // okay calculate!
		{
			if (reachableNode.Visited && reachableNode.Value.Equals(targetValue) && reachableNode.MaxReachableOfSameValue == null)
				throw new CycleFoundException($"Node with value '{reachableNode.Value}' was already visited -- cycle!");
			
			subMax = FindMaxReachable(reachableNode, targetValue);
		}
		
		if (subMax > max)
			max = subMax;
	}

	// update the node's counter if it is matching the target
	if (node.Value.Equals(targetValue))
	{
		max += 1;
		node.MaxReachableOfSameValue = max;
		node.Visited = true;
	}

	return max;
}
