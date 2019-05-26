<Query Kind="Program" />

// This problem was asked by Google.
// 
// Given the root of a binary search tree, and a target K,
// return two nodes in the tree whose sum equals K.
// 
// For example, given the following tree and K of 20
// 
//     10
//    /   \
//  5      15
//        /  \
//      11    15
//
// Return the nodes 5 and 15.

void Main()
{
	var root = new Node(10,
		new Node(5),
		new Node(15,
			new Node(11),
			new Node(15)));
			
	Find(root, 20).Dump();
	Find(root, 22).Dump();
	Find(root, 21).Dump();
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

(Node a, Node b) Find(Node root, int k)
{
	Dictionary<int, List<Node>> map = new Dictionary<int, List<Node>>();
	
	// preparation pass O(n)
	Traverse(root, node =>
	{
		if (!map.ContainsKey(node.Value))
			map[node.Value] = new List<Node>();
			
		map[node.Value].Add(node);
	});
	
	// evaluation pass O(n)
	
	Node a = null;
	Node b = null;
	
	Traverse(root, node =>
	{
		int complement = k - node.Value;
		
		if (map.ContainsKey(complement)
			&& map[complement].Any(x => x != node)) // handle edge case where complement is of the same value
		{
			a = node;
			b = map[complement].First(x => x != node);
		}
	});
	
	return (a, b);
}

void Traverse(Node cur, Action<Node> fn)
{
	fn(cur);
	
	if (cur.Left != null)
		Traverse(cur.Left, fn);

	if (cur.Right != null)
		Traverse(cur.Right, fn);
}