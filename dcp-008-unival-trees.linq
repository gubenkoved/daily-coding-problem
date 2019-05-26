<Query Kind="Program" />

// This problem was asked by Google.
// 
// A unival tree (which stands for "universal value") is a tree where all nodes under it have the same value.
// 
// Given the root to a binary tree, count the number of unival subtrees.
// 
// For example, the following tree has 5 unival subtrees:
// 
//    0
//   / \
//  1   0
//     / \
//    1   0
//   / \
//  1   1

// comment -- it looks like it's assumed that leaf node also is "subtree", so each leaf node contribute
// to the answer

public class Node
{
	public int Value { get;set; }
	public Node Left { get; set; }
	public Node Right { get; set; }
	
	public Node(int value, Node left = null, Node right = null)
	{
		Value = value;
		Left = left;
		Right = right;
	}
}

void Main()
{
	// 5
	var root = new Node(0, 
		new Node(1),
		new Node(0,
			new Node(1,
				new Node(1),
				new Node(1)),
			new Node(0)));

	// 3
	root = new Node(1,
		new Node(1),
		new Node(1));

	// 1 + 2 + 4 = 7
	root = new Node(1,
		new Node(1,
			new Node(1),
			new Node(1)),
		new Node(1,
			new Node(1),
			new Node(1)));

	// 1 + 2 + 4 + 8 = 15
	root = new Node(1,
		new Node(1,
			new Node(1,
				new Node(1),
				new Node(1)),
			new Node(1,
				new Node(1),
				new Node(1))),
		new Node(1,
			new Node(1,
				new Node(1),
				new Node(1)),
			new Node(1,
				new Node(1),
				new Node(1))));


	//	root = new Node(1,
//		new Node(2),
//		new Node(3,
//			new Node(4,
//				new Node(5),
//				new Node(6)),
//			new Node(7)));

	//BFS(root).Select(x => x.Value).Dump();
	
	CountUnivalTrees(root).Dump(5);
}

// time: O(n), space: O(n)
int CountUnivalTrees(Node root)
{
	// Queue<Node> queue = new Queue<Node>();
	
	// 1. bfs
	// 2. starting from the end nodes (leafs) check if node is leaf
	//    store unival in node, if not leaf check unival on childs
	//    if match store unival
	// 3. another pass counting nodes with unival set to some value
	
	// null will mean NO unival, absence will mean not yet calculated
	var univalMap = new Dictionary<Node, int?>();
	
	// time: O(n), space: O(n)
	List<Node> bfs = BFS(root);
	
	// time: O(n), space: O(n)
	for (int i = bfs.Count - 1; i >= 0; i--)
	{
		Node node = bfs[i];
		//int? unival = univalMap[node];
		
		int? unival = node.Value;
		
		if (node.Left != null)
		{
			int? leftUnival = univalMap[node.Left];
			
			if (leftUnival != unival)
				unival = null;
		}

		if (node.Right != null)
		{
			int? rightUnival = univalMap[node.Right];

			if (rightUnival != unival)
				unival = null;
		}

		univalMap[node] = unival;
	}
	
	int univalSubtrees = 0;
	
	// time: O(n), space: O(1)
	// counting pass 
	foreach (var kvp in univalMap)
	{
		if (kvp.Value != null)
			univalSubtrees += 1;
	}
	
	return univalSubtrees;
}

List<Node> BFS(Node node)
{
	List<Node> result = new List<Node>();
	Queue<Node> current = new Queue<Node>();
	
	current.Enqueue(node);
	
	do
	{
		// take node, add to result, then add adjacent to queue
		Node currentNode = current.Dequeue();

		result.Add(currentNode);
		
		if (currentNode.Left != null)
			current.Enqueue(currentNode.Left);

		if (currentNode.Right != null)
			current.Enqueue(currentNode.Right);

	} while (current.Count != 0);
	
	return result;
}