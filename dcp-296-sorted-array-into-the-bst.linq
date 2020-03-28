<Query Kind="Program" />

// This problem was asked by Etsy.
// 
// Given a sorted array, convert it into a height-balanced binary search tree.

void Main()
{
	var tree = Convert(Enumerable.Range(1, 32).ToArray());
	
	IsBalanced(tree).Dump("is balanced");
	
	tree.Dump();
}

Node Convert(int[] sorted)
{
	return Convert(sorted, 0, sorted.Length - 1);
}

Node Convert(int[] sorted, int left, int right)
{
	if (right - left == 0)
		return new Node(sorted[left]);
	
//	if (right - left == 1)
//		return new Node(
	
	int med = (left + right) / 2;
	
	var node = new Node(sorted[med]);
	
	if (med > left)
		node.Left = Convert(sorted, left, med - 1);
		
	if (med < right)
		node.Right = Convert(sorted, med + 1, right);
	
	return node;
}

int Height(Node root)
{
	if (root == null)
		return 0;
	
	int lh = 0;
	int rh = 0;
	
	if (root.Left != null)
		lh = Height(root.Left);
		
	if (root.Right != null)
		rh = Height(root.Right);
	
	return 1 + Math.Max(lh, rh);
}

bool IsBalanced(Node root)
{
	if (root == null)
		return true;
	
	if (Math.Abs(Height(root.Left) - Height(root.Right)) > 1)
		return false;
		
	if (!IsBalanced(root.Left) || !IsBalanced(root.Right))
		return false;
		
	return true;
}

public class Node
{
	public int Value { get; set; }
	public Node Left { get; set; }
	public Node Right { get; set; }
	
	public Node(int val, Node left = null, Node right = null)
	{
		Value = val;
		Left = left;
		Right = right;
	}
}