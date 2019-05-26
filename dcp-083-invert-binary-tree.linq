<Query Kind="Program" />

// This problem was asked by Google.
// 
// Invert a binary tree.
// 
// For example, given the following tree:
// 
//     a
//    / \
//   b   c
//  / \  /
// d   e f
// should become:
// 
//   a
//  / \
//  c  b
//  \  / \
//   f e  d

void Main()
{
	var root = new Node("a",
		new Node("b",
			new Node("d"),
			new Node("e")),
		new Node("c",
			new Node("f")));
			
	Invert(root);
	
	root.Dump();
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

public void Invert(Node current)
{
	Node tmp = current.Left;
	current.Left = current.Right;
	current.Right = tmp;
	
	if (current.Left != null)
		Invert(current.Left);
		
	if (current.Right != null)
		Invert(current.Right);
}
