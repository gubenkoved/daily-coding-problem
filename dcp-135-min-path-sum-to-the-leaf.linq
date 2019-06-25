<Query Kind="Program" />

// This question was asked by Apple.
// 
// Given a binary tree, find a minimum path sum from root to a leaf.
// 
// For example, the minimum path in this tree is [10, 5, 1, -1], which has sum 15.
// 
//   10
//  /  \
// 5    5
//  \     \
//    2    1
//        /
//      -1
// 

void Main()
{
	var head = new Node(10,
		new Node(5,
			null,
			new Node(2)),
		new Node(5,
			null,
			new Node(1,
				new Node(-1),
				null)));
				
	MinSumToTheLeaf(head).Dump("15");
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

int MinSumToTheLeaf(Node head)
{
	int min = int.MaxValue;
	
	Traverse(head, 0, ref min);
	
	return min;
}

void Traverse(Node currentNode, int currentSum, ref int minToTheLeaf)
{
	int sum = currentNode.Value + currentSum;
	
	bool isLeaf = currentNode.Left == null && currentNode.Right == null;
	
	if (isLeaf)
	{
		if (sum < minToTheLeaf)
			minToTheLeaf = sum;
	} else
	{
		if (currentNode.Left != null)
			Traverse(currentNode.Left, sum, ref minToTheLeaf);

		if (currentNode.Right != null)
			Traverse(currentNode.Right, sum, ref minToTheLeaf);
	}
}
