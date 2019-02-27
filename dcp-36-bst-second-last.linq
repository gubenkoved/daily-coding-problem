<Query Kind="Program" />

// This problem was asked by Dropbox.
// 
// Given the root to a binary search tree, find the second largest node in the tree.

void Main()
{
	var root =
	
	new Node(10,
		new Node(5,
			new Node(1,
				null,
				new Node(4)),
			new Node(8)),
		new Node(20,
			new Node(15),
			new Node(50,
				new Node(40),
				new Node(60))));
				//null)));
				
//	root = new Node(10,
//		new Node(1),
//		new Node(100));

	root = new Node(10,
		null,
		new Node(11,
			null,
			new Node(12)));

	SecondLargest(root).Value.Dump();

}

public Node SecondLargest(Node root)
{
	if (root.Right == null)
	{
		if (root.Left != null)
			return root;
		else
			throw new Exception("Tree is too small!");
	}
	
	Node p = root;
	Node c = root.Right;
	
	while (c.Right != null)
	{
		c = c.Right;
		p = p.Right;
	}
	
	if (c.Left != null)
		return c.Left;
	
	return p;
}

public class Node
{
	public Node Left { get; set; }
	public Node Right { get; set; }
	public int Value { get; set; }
	
	public Node(int value, Node left = null, Node right = null)
	{
		Value = value;
		Left = left;
		Right = right;
	}
}
