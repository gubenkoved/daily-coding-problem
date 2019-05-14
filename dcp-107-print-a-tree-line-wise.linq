<Query Kind="Program" />

// This problem was asked by Microsoft.
// 
// Print the nodes in a binary tree level-wise.
// For example, the following should print 1, 2, 3, 4, 5.
// 
//   1
//  / \
// 2   3
//    / \
//   4   5

void Main()
{
	var root = new Node(1,
		new Node(2),
		new Node(3,
			new Node(4),
			new Node(5)));
			
	Print(root);
}

void Print(Node root)
{
	// basically that's is breadth-first search which is implemented via the queue	
	
	Queue<Node> q = new Queue<Node>();
	
	q.Enqueue(root);
	
	while (q.Count != 0)
	{
		Node current = q.Dequeue();
		
		Console.Write(current.Value);
		
		if (current.Left != null)
			q.Enqueue(current.Left);

		if (current.Right != null)
			q.Enqueue(current.Right);

		if (q.Count != 0)
			Console.Write(", ");
	}
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
