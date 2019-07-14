<Query Kind="Program" />

// This question was asked by BufferBox.
// 
// Given a binary tree where all nodes are either 0 or 1, prune the tree so that subtrees containing all 0s are removed.
// 
// For example, given the following tree:
// 
//    0
//   / \
//  1   0
//     / \
//    1   0
//   / \
//  0   0
// should be pruned to:
// 
//    0
//   / \
//  1   0
//     /
//    1
// We do not remove the tree at the root or its left child because it still has a 1 as a descendant.

void Main()
{
	var head = new Node(0,
		new Node(1),
		new Node(0,
			new Node(1,
				new Node(0),
				new Node(0)),
			new Node(0)));
	
	Prune(head);
	
	head.Dump();
}

void Prune(Node head)
{
	PruneTraverser(head);
}

bool PruneTraverser(Node cur)
{
	// returns true if all of the child nodes are 0
	
	bool subTreeOfZeros = cur.Value == 0;

	var childs = new Node[] { cur.Left, cur.Right };
	
	if (cur.Left != null)
	{
		bool leftSubTreeOfZeros = PruneTraverser(cur.Left);

		if (leftSubTreeOfZeros)
		{
			cur.Left = null; // prune
			Util.Metatext("prune left").Dump();
		}
		else
		{
			subTreeOfZeros = false;
		}
	}

	if (cur.Right != null)
	{
		bool rightSubTreeOfZeros = PruneTraverser(cur.Right);

		if (rightSubTreeOfZeros)
		{
			cur.Right = null; // prune
			Util.Metatext("prune right").Dump();
		}
		else
		{
			subTreeOfZeros = false;
		}
	}
	
	return subTreeOfZeros;
}

public class Node
{
	public Node Left { get; set; }
	public Node Right { get; set; }
	public int Value { get; set; }
	
	public Node(int value, Node left = null, Node right = null)
	{
		Value = value;
		Left = left;
		Right = right;
	}
}
