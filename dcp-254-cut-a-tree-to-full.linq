<Query Kind="Program" />

// This problem was asked by Yahoo.
// 
// Recall that a full binary tree is one in which each node is either a leaf node,
// or has two children. Given a binary tree, convert it to a full one by removing
// nodes with only one child.
// 
// For example, given the following tree:
// 
//          0
//       /     \
//     1         2
//   /            \
// 3                 4
//   \             /   \
//     5          6     7
// You should convert it to:
// 
//      0
//   /     \
// 5         4
//         /   \
//        6     7


void Main()
{
	var head = new Node(0,
		new Node(1,
			new Node(3,
				null,
				new Node(5)),
			null),
		new Node(2,
			null,
			new Node(4,
				new Node(6),
				new Node(7))));
	
	Cut(head).Dump();		
}

public Node Cut(Node cur)
{
	if (cur.Left != null)
		cur.Left = Cut(cur.Left);

	if (cur.Right != null)
		cur.Right = Cut(cur.Right);

	// okay, so we proceessed all child, see if we have to do anything with current node

	if (cur.Left == null && cur.Right != null)
		return cur.Right; // cut current

	if (cur.Left != null && cur.Right == null)
		return cur.Left; // cut current

	// current node stays
	return cur;
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