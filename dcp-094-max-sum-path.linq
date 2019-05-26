<Query Kind="Program" />

// This problem was asked by Google.
// 
// Given a binary tree of integers, find the maximum path
// sum between two nodes. The path must go through at least
// one node, and does not need to go through the root.

void Main()
{
	Node root = new Node(1,
		new Node(2),
		new Node(3));
		
	MaxSumPath(root,
		a: root.Left,
		b: root.Right).Dump("6");

	Node root2 = new Node(1,
		new Node(2,
			new Node(4,
				new Node(5,
					new Node(6),
					new Node(7)),
				new Node(8)),
			new Node(10)),
		new Node(3));

	MaxSumPath(root2,
		a: root2.Left.Right,
		b: root2.Left.Left.Left.Left).Dump("27");
}

public class Node
{
	public int Value { get; set; }
	public Node Left { get; set; }
	public Node Right { get; set; }
	
	public Node(int value, Node left = null, Node right = null)
	{
		Value = value;
		Left = left;
		Right = right;
	}
}

public int MaxSumPath(Node root, Node a, Node b)
{
	// do postfix traversal maintaining
	// targetPathSum on the nodes corresponding to sum of the 
	// values of the nodes from the current to one of the target nodes
	// when we see a node with both subtrees targetPathSum filled
	// then it means that we found the root that connects both
	// and then result is sum of targetPathSum values + current.Value
	
	int? result = Traverse(root, a, b, new Dictionary<Node, int>());
	
	if (result == null)
		throw new Exception("Unable to find a path!");
	
	return result.Value;
}

public int? Traverse(Node current, Node a, Node b, Dictionary<Node, int> targetPathSumMap)
{
	if (current.Left != null)
	{
		int? result = Traverse(current.Left, a, b, targetPathSumMap);
		
		if (result != null)
			return result;
	}

	if (current.Right != null)
	{
		int? result = Traverse(current.Right, a, b, targetPathSumMap);

		if (result != null)
			return result;
	}

	// either leaf OR all child traversed
	
	if (current.Left != null && targetPathSumMap.ContainsKey(current.Left)
		&& current.Right != null && targetPathSumMap.ContainsKey(current.Right))
	{
		// return a result!
		return targetPathSumMap[current.Left] + targetPathSumMap[current.Right] + current.Value; 
	}

	if (current.Left != null && targetPathSumMap.ContainsKey(current.Left))
		targetPathSumMap[current] = targetPathSumMap[current.Left] + current.Value;

	if (current.Right != null && targetPathSumMap.ContainsKey(current.Right))
		targetPathSumMap[current] = targetPathSumMap[current.Right] + current.Value;

	if (current == a || current == b)
		targetPathSumMap[current] = current.Value;
	
	// continue looking, stop flag is not set
	return null;
}