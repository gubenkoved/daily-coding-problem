<Query Kind="Program" />

// This problem was asked by Yext.
// 
// Two nodes in a binary tree can be called cousins if they are on the same level
// of the tree but have different parents. For example, in the following diagram 4 and 6 are cousins.
// 
//     1
//    / \
//   2   3
//  / \   \
// 4   5   6
//
// Given a binary tree and a particular node, find all cousins of that node.

void Main()
{
	var root = new Node(1,
		new Node(2,
			new Node(4),
			new Node(5)),
		new Node(3,
			null,
			new Node(6)));
		
	FindCousins(root, root.Left.Left).Dump("6");
	FindCousins(root, root.Left.Right).Dump("6");
	FindCousins(root, root.Right.Right).Dump("4 and 5");
		
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

public IEnumerable<Node> FindCousins(Node root, Node target)
{
	// it can be BFS based as it's the easiest way to perform level-based traversal
	// we would need two queues though so that levels can be separated
	
	Queue<Node> queue = new Queue<Node>();
	
	queue.Enqueue(root);
	
	Queue<Node> next = new Queue<Node>();
	
	Dictionary<Node, Node> parentsMap = new Dictionary<Node, Node>();

	do
	{
		while (queue.Count > 0)
		{
			Node cur = queue.Dequeue();

			// add the children
			if (cur.Left != null)
			{
				parentsMap[cur.Left] = cur;
				next.Enqueue(cur.Left);
			}

			if (cur.Right != null)
			{
				parentsMap[cur.Right] = cur;
				next.Enqueue(cur.Right);
			}
		}
	
		// next level is prepared, see if we got the target node
		if (next.Contains(target))
		{
			// cousins can be only on this level!
			return next.Where(x => parentsMap[x] != parentsMap[target]).ToArray();
		}
		
		queue = next;
		next = new Queue<Node>();
		
	} while (queue.Count > 0);
	
	throw new Exception("Target node was not found!");
}