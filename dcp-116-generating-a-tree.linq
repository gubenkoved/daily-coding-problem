<Query Kind="Program" />

// This problem was asked by Jane Street.
// 
// Generate a finite, but an arbitrarily large binary tree quickly in O(1).
// 
// That is, generate() should return a tree whose size is unbounded but finite.

void Main()
{
	// I have to google what is meant there
	// and it turns out that arbitrary large is actually
	// just a tree with some predenifed math expectation of the size
	// that is why it it averge case is constant time
	
	var root = Generate();
	
	root.Dump();
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

private Random _rnd = new Random();

Node Generate()
{
	int idx = 0;
	
	Node root = new Node(++idx);
	
	Generate(root, ref idx);
	
	return root;
}

void Generate(Node cur, ref int idx)
{
	if (_rnd.NextDouble() < 0.45)
	{
		cur.Left = new Node(++idx);
		
		Generate(cur.Left, ref idx);
	}

	if (_rnd.NextDouble() < 0.45)
	{
		cur.Right = new Node(++idx);

		Generate(cur.Right, ref idx);
	}
}