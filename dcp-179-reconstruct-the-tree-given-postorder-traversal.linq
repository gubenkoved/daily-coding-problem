<Query Kind="Program" />

// This problem was asked by Google.
// 
// Given the sequence of keys visited by a postorder traversal
// of a binary search tree, reconstruct the tree.
// 
// For example, given the sequence 2, 4, 3, 8, 7, 5, you should
// construct the following tree:
// 
//     5
//    / \
//   3   7
//  / \   \
// 2   4   8

void Main()
{
	//Restore(new[] { 1, 3, 2 }).Dump();
	//Restore(new[] { 2, 4, 3, 8, 7, 5 }).Dump();
	Restore(new[] { 1, 7, 9, 8, 6, 13, 30, 20, 10}).Dump();
}

public class Node
{
	public int Value { get; set; }
	public Node Left { get; set; }
	public Node Right { get; set; }
	
	public Node(int value)
	{
		Value = value;
	}
}

Node Restore(int[] postorder)
{
	return Restore(postorder, 0, postorder.Length - 1);
}

Node Restore(int[] postorder, int l, int r)
{
	// start at the very last node, that's our root
	// then we can split the items into the two collections
	// in the first one items are less than root, in the second ones
	// items are bigger than the root
	// recoursively reconstruct the left subtree from the first collection
	// and the right from the second and wire it up to the root
	
	int root = postorder[r];
	
	Node head = new Node(root);
	
	if (r == l)
		return head; // needed?
	
	if (postorder[l] < root)
	{
		// there is a left subtree, let's find out where it ends
		int lr = l;
		
		while (lr + 1 < r && postorder[lr + 1] < root)
			lr += 1;
			
		head.Left = Restore(postorder, l, lr);
	}
	
	if (r - 1 >= l && postorder[r - 1] > root)
	{
		// there is a right subtree
		int rl = r - 1;
		
		while (rl - 1 >= l && postorder[rl - 1] > root)
			rl -= 1;
			
		head.Right = Restore(postorder, rl, r - 1);
	}
	
	return head;
}