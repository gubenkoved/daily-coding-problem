<Query Kind="Program" />

// This problem was asked by Palantir.
// 
// Typically, an implementation of in-order traversal of a binary tree
// has O(h) space complexity, where h is the height of the tree. Write
// a program to compute the in-order traversal of a binary tree using O(1) space.

void Main()
{
	// i was searching for a hint about this one, and found that
	// the solution actually implies ability to change the tree structure
	// and this basically used as a means to get some more space and remember 
	// the positions algorithm should retract to
	
	var head = new Node("1",
		new Node("2",
			new Node("3")),
		new Node("4",
			new Node("5",
				new Node("6",
					new Node("7"),
					new Node("8"))),
			new Node("9")));
					
	MorrisTraversal(head).Dump();;

	

	var head2 = new Node(4,
		new Node(2,
			new Node(1),
			new Node(3)),
		new Node(6,
			new Node(5),
			new Node(7)));
			

	MorrisTraversal(head2).Dump();
}

public class Node
{
	public object Value { get; set; }
	public Node Left { get; set; }
	public Node Right { get; set; }
	
	public Node(object val, Node left = null, Node right = null)
	{
		Value = val;
		Left = left;
		Right = right;
	}
}

// it uses the O(height) space implicitly using the recoursion
void InOrderTraversal(Node node)
{
	if (node.Left != null)
		InOrderTraversal(node.Left);

	// visit node
	node.Value.Dump();
	
	if (node.Right != null)
		InOrderTraversal(node.Right);
}

IEnumerable<object> MorrisTraversal(Node head)
{
	// i did NOT figure this one out myself, had to watch morris traversal explanation...
	// https://www.youtube.com/watch?v=wGXB9OWhPTg
	
	Node cur = head;
	
	while (cur != null)
	{
		if (cur.Left != null)
		{
			bool cycle;
			Node predecessor = Predecessor(cur, out cycle);
			
			if (!cycle)
			{
				// store the way back to the current
				// note that predecessor.Right used to be NULL,
				// otherwise it won't be a prdecessor as it's rightmost one on the left subtree
				predecessor.Right = cur; 
				
				// go to the left!
				cur = cur.Left;
			} else // cycle found
			{
				predecessor.Right = null;
				
				yield return cur.Value; // visit!
				
				cur = cur.Right;
			}
		} else // cur.Left is null!
		{
			yield return cur.Value; // visit!
			
			cur = cur.Right;
		}
	}
}

Node Predecessor(Node node, out bool cycle)
{
	// go to the left
	Node ptr = node.Left;

	// and all to the right
	while (ptr.Right != null)
	{
		// we reached the starting point!
		if (ptr.Right == node)
		{
			cycle = true;
			return ptr;
		}

		ptr = ptr.Right;
	}
	
	cycle = false;
	
	return ptr;
}