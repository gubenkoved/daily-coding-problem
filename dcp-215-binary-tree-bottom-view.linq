<Query Kind="Program" />

// This problem was asked by Yelp.
// 
// The horizontal distance of a binary tree node describes how far left or right the node
// will be when the tree is printed out.
// 
// More rigorously, we can define it as follows:
// 
// The horizontal distance of the root is 0.
// The horizontal distance of a left child is hd(parent) - 1.
// The horizontal distance of a right child is hd(parent) + 1.
// For example, for the following tree, hd(1) = -2, and hd(6) = 0.
// 
//              5
//           /     \
//         3         7
//       /  \      /   \
//     1     4    6     9
//    /                /
//   0                8
//
// The bottom view of a tree, then, consists of the lowest node at each horizontal distance.
// If there are two nodes at the same depth and horizontal distance, either is acceptable.
// 
// For this tree, for example, the bottom view could be [0, 1, 3, 6, 8, 9].
// 
// Given the root to a binary tree, return its bottom view.

void Main()
{
	BottomView(
		new Node(5,
			new Node(3,
				new Node(1,
					new Node(0)),
				new Node(4)),
			new Node(7,
				new Node(6),
				new Node(9,
					new Node(8))))).Dump();
}

class Node
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

IEnumerable<int> BottomView(Node root)
{
	var map = new Dictionary<int, int>();
	
	Traverse(0, root, map);
	
	return map.Keys.OrderBy(x => x).Select(x => map[x]).ToArray();
}

void Traverse(int horizontalDistance, Node cur, Dictionary<int, int> bottomView)
{
	// add current node
	bottomView[horizontalDistance] = cur.Value;
	
	if (cur.Left != null)
		Traverse(horizontalDistance - 1, cur.Left, bottomView);

	if (cur.Right != null)
		Traverse(horizontalDistance + 1, cur.Right, bottomView);
}