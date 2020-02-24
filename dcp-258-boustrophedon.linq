<Query Kind="Program" />

// This problem was asked by Morgan Stanley.
// 
// In Ancient Greece, it was common to write text with the first line going left to right,
// the second line going right to left, and continuing to go back and forth. This style
// was called "boustrophedon".
// 
// Given a binary tree, write an algorithm to print the nodes in boustrophedon order.
// 
// For example, given the following tree:
// 
//        1
//     /     \
//   2         3
//  / \       / \
// 4   5     6   7
//
// You should return [1, 3, 2, 4, 5, 6, 7].

void Main()
{
	BoustrophedonPrint(new Node(1,
		new Node(2,
			new Node(4),
			new Node(5)),
		new Node(3,
			new Node(6),
			new Node(7))));

	BoustrophedonPrint(new Node(1,
		new Node(2,
			new Node(4,
				new Node(8),
				new Node(9)),
			new Node(5,
				new Node(10),
				new Node(11))),
		new Node(3,
			new Node(6),
			new Node(7))));
}

void BoustrophedonPrint(Node head)
{
	// it's almost like BFS, but we have to have additional information
	// so that we can distinguish one layer from another one

	Stack<Node> currentLayer = new Stack<Node>();
	Stack<Node> nextLayer = new Stack<Node>();

	currentLayer.Push(head);
	
	bool alternate = false;

	do
	{
		while (currentLayer.Any())
		{
			Node current = currentLayer.Pop();

			// visit!
			Console.Write(current.Value.ToString() + ", ");
	
			List<Node> children = new List<Node>();
			
			if (current.Left != null)
				children.Add(current.Left);

			if (current.Right != null)
				children.Add(current.Right);

			if (alternate)
				children.Reverse();

			foreach (var child in children)
				nextLayer.Push(child);
		}

		// we exausted this layer, go to the next one!
		currentLayer = nextLayer;
		nextLayer = new Stack<Node>();
		alternate = !alternate;
		
	} while (currentLayer.Any());
	
	Console.WriteLine();
}

public class Node
{
	public object Value { get; set; }
	public Node Left { get; set; }
	public Node Right { get; set; }
	
	public Node(object value, Node left = null, Node right = null)
	{
		Value = value;
		Left = left;
		Right = right;
	}
}
