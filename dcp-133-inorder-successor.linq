<Query Kind="Program" />

// This problem was asked by Amazon.
// 
// Given a node in a binary search tree, return the next bigger element,
// also known as the inorder successor.
// 
// For example, the inorder successor of 22 is 30.
// 
//    10
//   /  \
//  5    30
//      /  \
//    22    35
//
// You can assume each node has a parent pointer.

void Main()
{
	var head = new Node(10,
		new Node(5),
		new Node(30,
			new Node(22),
			new Node(35)));
			
	//head.Dump();
	InOrderTraverse(head);
	
	// check!
	InOrderTraverseViaSuccessor(head.Left);

	var head2 = new Node(2,
		new Node(1),
		new Node(6,
			new Node(4,
				new Node(3),
				new Node(5)),
			new Node(7)));
	
	InOrderTraverse(head2);
	
	// check!
	InOrderTraverseViaSuccessor(head2.Left);
}

void InOrderTraverse(Node head, bool top = true)
{
	if (head.Left != null)
		InOrderTraverse(head.Left, false);

	// process!
	Console.Write($"{head.Value} ");
	
	if (head.Right != null)
		InOrderTraverse(head.Right, false);
		
	if (top)
		Console.WriteLine();
}

void InOrderTraverseViaSuccessor(Node head)
{
	var cur = head;

	while (cur != null)
	{
		Console.Write($"{cur.Value} ");
		cur = InOrderSuccessor(cur);
	}
	
	Console.WriteLine();
}

Node InOrderSuccessor(Node node)
{
	// if node has Right child, go there and then dive to left most node
	
	// if node has no Right child
	// go to the parent node as long as we going up to parent as a
	// right child (so navigating up left in a sense) we will need to continue 
	// so that we making exactly 1 up-right move
	
	Node cur = null;
	
	if (node.Right != null)
	{
		 cur = node.Right;
		 
		 while (cur.Left != null)
		 	cur = cur.Left;
	} else // Right == null
	{
		// go up to end up with exactly 1 movement to right
		cur = node;

		bool traversedToRight = false;
		
		while (!traversedToRight)
		{
			Node parent = cur.Parent;
			
			if (parent == null)
				return null; // right-most element!
		
			traversedToRight = parent.Left == cur;
			
			cur = parent;
		}
	}
	
	return cur;
}

public class Node
{
	public int Value { get; set; }
	
	public Node Left { get; set; }
	public Node Right { get; set; }
	public Node Parent { get; set; }
	
	public Node(int value, Node left = null, Node right = null)
	{
		Value = value;
		
		Left = left;
		
		if (Left != null)
			Left.Parent = this;
		
		Right = right;
		
		if (Right != null)
			Right.Parent = this;
	}
}


