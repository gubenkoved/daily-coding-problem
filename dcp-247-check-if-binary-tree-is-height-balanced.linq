<Query Kind="Program" />

// This problem was asked by PayPal.
// 
// Given a binary tree, determine whether or not it is height-balanced.
// A height-balanced binary tree can be defined as one in which the heights
// of the two subtrees of any node never differ by more than one.

void Main()
{
	var r = new Node("a",
		new Node("b"),
		new Node("c",
			new Node("d")));
			
	IsBalanced(r).Dump("true");

	var r2 = new Node("a",
		new Node("b"),
		new Node("c",
			new Node("d",
				new Node("e"))));
				
	IsBalanced(r2).Dump("false");
	
	IsBalanced(new Node("a", new Node("b"))).Dump("trues");
}

public bool IsBalanced(Node root)
{
	var map = new Dictionary<Node, int>();
	
	HeightCalculator(root, map);
	
	return IsBalancedRecoursive(root, map);
}

public bool IsBalancedRecoursive(Node root, Dictionary<Node, int> heightMap)
{
	int leftHeight = 0;
	int rightHeight = 0;
	
	if (root.Left != null)
		leftHeight = heightMap[root.Left];
		
	if (root.Right != null)
		rightHeight = heightMap[root.Right];
		
	if (Math.Abs(leftHeight - rightHeight) > 1)
		return false;
		
	if (root.Left != null && !IsBalancedRecoursive(root.Left, heightMap))
		return false;

	if (root.Right != null && !IsBalancedRecoursive(root.Right, heightMap))
		return false;

	// all good!
	return true;
}

public int HeightCalculator(Node cur, Dictionary<Node, int> heightMap)
{
	int height = 0;
	
	if (cur.Left != null)
		height = 1 + HeightCalculator(cur.Left, heightMap);
	
	if (cur.Right != null)
		height = Math.Max(height, 1 + HeightCalculator(cur.Right, heightMap));
	
	heightMap[cur] = height;
	
	return height;
}

public class Node
{
	public Node Left { get; set; }
	public Node Right { get; set; }
	public object Value { get; set; }
	
	public Node(object value, Node left = null, Node right = null)
	{
		Value = value;
		Left = left;
		Right = right;
	}
}
