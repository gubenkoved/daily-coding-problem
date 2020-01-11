<Query Kind="Program" />

// This problem was asked by Amazon.
// 
// A tree is symmetric if its data and shape remain unchanged when it is reflected about the root node.The following tree is an example:
// 
//         4
//       / | \
//     3   5   3
//   /           \
// 9              9
// Given a k-ary tree, determine whether it is symmetric.

void Main()
{
	var root = new Node("4",
		new Node(3,
			new Node(9), null, null),
		new Node(5),
		new Node(3,
			null, null, new Node(9)));
			
	IsSymmetric(root).Dump();

	var root2 = new Node("4",
		new Node(3,
			new Node(9), new Node(7), new Node(8)),
		new Node(5),
		new Node(3,
			new Node(8), new Node(7), new Node(9)));
			
	IsSymmetric(root2).Dump();
}

public class Node
{
	public object Value { get; set; }
	public Node[] Children { get; set; }
	
	public Node(object value, params Node[] children)
	{
		Value = value;
		Children = children;
	}
}

public bool IsSymmetric(Node root)
{
	return IsSymmetric(root, root);
}

public bool IsSymmetric(Node left, Node right)
{
	if (left == null && right == null)
		return true;
	
	// if we got there, then at least one is NOT NULL, then if one is NULL it means we matching empty node against non empty
	if (left == null || right == null)
		return false;
	
	if (!left.Children.Any() && !right.Children.Any())
		return left.Value.Equals(right.Value);
		
	// there are children
	
	if (left.Children.Length != right.Children.Length)
		return false;
	
	int k = left.Children.Length;
	
	for (int i = 0; i < k; i++)
	{
		if (!IsSymmetric(left.Children[i], right.Children[k - i - 1]))
			return false;
	}
	
	return true;
}

