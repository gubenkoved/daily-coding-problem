<Query Kind="Program" />

// This problem was asked by Apple.
// 
// Given a tree, find the largest tree/subtree that is a BST.
// 
// Given a tree, return the size of the largest tree/subtree that is a BST.

void Main()
{
	// interesting observation
	// if two subtrees are BST being merged into a single tree
	// it will be BST when:
	// a. left.value <= root <= right.value
	// b. biggest (rightmost) value in left subtree <= smallest (leftmost) value in right subtree
	
	Node root = new Node(1);
	
	LargestBstSubtree(root).Dump("1");
	
	Node root2 =
	
	new Node(10,
		new Node(8,
			new Node(6,
				new Node(2),
				new Node(7)),
			new Node(40,
				new Node(10),
				new Node(42))),
		new Node(12,
			new Node(11),
			new Node(30,
				new Node(20),
				new Node(40))));
	
	LargestBstSubtree(root2).Dump("7");
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

public class NodeMetadata
{
	public int SubtreeSize { get; set; }
	public bool IsBstSubtree { get; set; }
	public int BstMaxValue { get; set; } // only applicable it BST!
	public int BstMinValue { get; set; } // only applicable it BST!
}

public int LargestBstSubtree(Node root)
{
	var metadataMap = new Dictionary<Node, NodeMetadata>();
	
	Traverse(root, metadataMap);
	
	//metadataMap.Dump();
	
	return metadataMap.Where(x => x.Value.IsBstSubtree).Max(x => x.Value.SubtreeSize);
}

public void Traverse(Node current, Dictionary<Node, NodeMetadata> metadataMap)
{
	if (current.Left != null)
		Traverse(current.Left, metadataMap);

	if (current.Right != null)
		Traverse(current.Right, metadataMap);

	// okay when we got there we are either at leaf OR
	// we already calculated ALL the child, so we able to make
	// an informed decision about this subtree
	
	int subtreeSize = 1; // current node
	
	if (current.Left != null)
		subtreeSize += metadataMap[current.Left].SubtreeSize;

	if (current.Right != null)
		subtreeSize += metadataMap[current.Right].SubtreeSize;

	bool isBst = true;
	
	if (current.Left != null && (current.Left.Value > current.Value || !metadataMap[current.Left].IsBstSubtree))
		isBst = false;

	if (current.Right != null && (current.Right.Value < current.Value || !metadataMap[current.Right].IsBstSubtree))
		isBst = false;

	// okay the last property to be checked to quilify for BST
	if (current.Left != null && current.Right != null && metadataMap[current.Left].BstMaxValue > metadataMap[current.Right].BstMinValue)
		isBst = false;

	int bstMinValue = current.Value;
	int bstMaxValue = current.Value;

	if (isBst)
	{
		if (current.Left != null)
			bstMinValue = metadataMap[current.Left].BstMinValue;

		if (current.Right != null)
			bstMaxValue = metadataMap[current.Right].BstMaxValue;
	}

	metadataMap[current] = new NodeMetadata()
	{
		SubtreeSize = subtreeSize,
		IsBstSubtree = isBst,
		BstMaxValue = bstMaxValue,
		BstMinValue = bstMinValue,
	};
}