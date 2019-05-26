<Query Kind="Program" />

// This problem was asked by Google.
// 
// Given the root of a binary tree, return a deepest
// node. For example, in the following tree, return d.
// 
//     a
//    / \
//   b   c
//  /
// d

void Main()
{
	var root = new Node("a",
		new Node("b",
			new Node("d")),
		new Node("c"));
	
	GetDeepest(root).Dump();
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

public Node GetDeepest(Node root)
{
	var state = new SearchState();
	
	Search(root, 1, state);
	
	return state.Deepest;
}

public class SearchState
{
	public int Level { get; set; }
	public Node Deepest { get; set; }
}

public void Search(Node cur, int level, SearchState state)
{
	if (cur == null)
		return;
		
	if (state.Level < level)
	{
		state.Deepest = cur;
		state.Level = level;
	}
	
	Search(cur.Left, level + 1, state);
	Search(cur.Right, level + 1, state);
}
