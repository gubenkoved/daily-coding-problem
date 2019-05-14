<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// Given a binary tree, return all paths from the root to leaves.
// 
// For example, given the tree:
// 
//    1
//   / \
//  2   3
//     / \
//    4   5
// Return [[1, 2], [1, 3, 4], [1, 3, 5]].

void Main()
{
	var root = new Node(1,
		new Node(2),
		new Node(3,
			new Node(4),
			new Node(5)));
			
	AllPathsToTheLeaves(root).Select(path => path.Select(x => x.Value)).Dump();

	var root2 = new Node(1,
		new Node(2),
		new Node(3,
			new Node(4),
			new Node(5,
				new Node(6))));

	AllPathsToTheLeaves(root2).Select(path => path.Select(x => x.Value)).Dump();
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

IEnumerable<Node[]> AllPathsToTheLeaves(Node root)
{
	List<Node[]> result = new List<Node[]>();
	
	Walker(new Stack<Node>(), root, result);

	return result;
}

void Walker(Stack<Node> pathToRoot, Node current, List<Node[]> paths)
{
	if (current.Left == null && current.Right == null)
	{
		// leaf
		List<Node> path = new List<Node>(pathToRoot.Reverse());
		path.Add(current);
		
		// new path found!
		paths.Add(path.ToArray());
	}
	
	pathToRoot.Push(current);
	
	if (current.Left != null)
		Walker(pathToRoot, current.Left, paths);

	if (current.Right != null)
		Walker(pathToRoot, current.Right, paths);
		
	pathToRoot.Pop();
}
