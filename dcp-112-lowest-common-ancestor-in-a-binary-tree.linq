<Query Kind="Program" />

// This problem was asked by Twitter.
// 
// Given a binary tree, find the lowest common ancestor (LCA)
// of two given nodes in the tree. Assume that each node in
// the tree also has a pointer to its parent.
// 
// According to the definition of LCA on Wikipedia: “The lowest
// common ancestor is defined between two nodes v and w as the
// lowest node in T that has both v and w as descendants (where
// we allow a node to be a descendant of itself).”

void Main()
{
	var root = new Node(1,
		new Node(2,
			new Node(4,
				new Node(7)),
			new Node(5,
				new Node(8,
					new Node(10)),
				new Node(9))),
		new Node(3,
			null,
			new Node(6)));
	
	LCA(root, root.Left.Left.Left, root.Left.Right.Left.Left).Value.Dump("2");
	LCA(root, root.Left.Left.Left, root.Left.Left.Left).Value.Dump("4");
	LCA(root, root.Left.Left.Left, root.Right.Right).Value.Dump("1");
	LCA(root, root.Left, root.Right.Right).Value.Dump("1");
}

Node LCA(Node root, Node a, Node b)
{
	IEnumerable<Node> aPath = PathTo(root, a);
	IEnumerable<Node> bPath = PathTo(root, b);
	
	Node lca = root;
	
	var aEnumerator = aPath.GetEnumerator();
	var bEnumerator = bPath.GetEnumerator();
	
	aEnumerator.MoveNext();
	bEnumerator.MoveNext();
	
	while (aEnumerator.Current == bEnumerator.Current)
	{
		lca = aEnumerator.Current;
		
		if (!aEnumerator.MoveNext())
			break;

		if (!bEnumerator.MoveNext())
			break;
	}
	
	return lca;
}

IEnumerable<Node> PathTo(Node root, Node target)
{
	return PathTo(new Stack<Node>(new[] { root }), root, target);
}

IEnumerable<Node> PathTo(Stack<Node> path, Node cur, Node target)
{
	if (target == cur)
		return path.Reverse().ToArray();
	
	if (cur.Left != null)
	{
		path.Push(cur.Left);
		
		var inner = PathTo(path, cur.Left, target);
		
		path.Pop();
		
		if (inner != null)
			return inner;
	}

	if (cur.Right != null)
	{
		path.Push(cur.Right);

		var inner = PathTo(path, cur.Right, target);

		path.Pop();

		if (inner != null)
			return inner;
	}
	
	return null;
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