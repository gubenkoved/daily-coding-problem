<Query Kind="Program" />

// This problem was asked by Google.
// 
// Given two non-empty binary trees s and t, check
// whether tree t has exactly the same structure and
// node values with a subtree of s. A subtree of s is
// a tree consists of a node in s and all of this node's
// descendants. The tree s could also be considered as
// a subtree of itself.

void Main()
{
	var a = new Node(1,
		new Node(2),
		new Node(3));
	
	var b = new Node(1,
		new Node(2),
		new Node(3));
	
	AreEquivalent(a, b).Dump("true");
	
	var c = new Node(0,
		new Node(1,
			new Node(2),
			new Node(3)),
		new Node(4,
			new Node(5,
				null,
				new Node(6))));
				
	var d = new Node(1,
		new Node(2));
				
	EquivalentWithSubtree(c, b).Dump("true");
	EquivalentWithSubtree(c, d).Dump("false");
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

// O(n * n) I think...
bool EquivalentWithSubtree(Node s, Node t)
{
	// enumerate all the nodes on s and see if any subtree that starts
	// at some node is equivalent to t
	
	if (AreEquivalent(s, t)) // O(n)
		return true;
		
	if (s.Left != null)
	{
		if (EquivalentWithSubtree(s.Left, t))
			return true;
	}

	if (s.Right != null)
	{
		if (EquivalentWithSubtree(s.Right, t))
			return true;
	}

	return false;
}

// O(n) where n is minimal amount of nodes in either tree
bool AreEquivalent(Node a, Node b)
{
	if (a.Value != b.Value)
		return false;
	
	if (a.Left == null && b.Left != null || a.Left != null && b.Left == null)
		return false;

	if (a.Right == null && b.Right != null || a.Right != null && b.Right == null)
		return false;

	// recoursively check other
	if (a.Left != null)
		if (!AreEquivalent(a.Left, b.Left))
			return false;

	if (a.Right != null)
		if (!AreEquivalent(a.Right, b.Right))
			return false;

	return true;
}