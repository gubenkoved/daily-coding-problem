<Query Kind="Program" />

// This problem was asked by Microsoft.
// 
// Suppose an arithmetic expression is given as a binary tree.
// 
// Each leaf is an integer and each internal node is one of
// '+', '−', '∗', or '/'.
//
// Given the root to such a tree, write a function to evaluate it.
//
// For example, given the following tree:
//     *
//    / \
//   +    +
//  / \  / \
// 3  2  4  5


void Main()
{
	var	root = new Node("*",
		new Node("+",
			new Node("3"),
			new Node("2")),
		new Node("+",
			new Node("4"),
			new Node("5")));
			
	root.Evaluate().Dump();
}

public class Node
{
	public string Value { get; set; }
	
	public Node Left { get; set; }
	public Node Right { get; set; }
	
	public Node(string value, Node left = null, Node right = null)
	{
		Value = value;
		Left = left;
		Right = right;
	}
	
	public int Evaluate()
	{
		if (Value == "+")
			return Left.Evaluate() + Right.Evaluate();
		else if (Value == "-")
			return Left.Evaluate() - Right.Evaluate();
		else if (Value == "/")
			return Left.Evaluate() / Right.Evaluate();
		else if (Value == "*")
			return Left.Evaluate() * Right.Evaluate();
		else if (Regex.IsMatch(Value, "[0-9]+"))
			return int.Parse(Value);
		else
			throw new Exception($"Unexpected value: {Value}");
	}
}
