<Query Kind="Program" />

// This problem was asked by LinkedIn.
// 
// Determine whether a tree is a valid binary search tree.
// 
// A binary search tree is a tree with two children, left
// and right, and satisfies the constraint that the key in
// the left child must be less than or equal to the root
// and the key in the right child must be greater than or
// equal to the root.

void Main()
{
	var root = new Node(10,
		new Node(5,
			new Node(3),
			new Node(6)),
		new Node(15,
			new Node(11),
			new Node(20)));
			
	IsBinarySearchTree(root).Dump();
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

bool IsBinarySearchTree(Node cur, int max = int.MaxValue, int min = int.MinValue)
{
	Util.Metatext($"{cur.Value} ({min}, {max})").Dump();
	
	if (cur.Value > max || cur.Value < min)
		return false;
	
	if (cur.Left != null)
	{
		if (!IsBinarySearchTree(cur.Left, max: cur.Value, min: min))
			return false;
	}
	
	if (cur.Right != null) 
	{
		if (!IsBinarySearchTree(cur.Right, max: max, min: cur.Value))
			return false;
	}
	
	return true;
}


