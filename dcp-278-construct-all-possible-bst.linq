<Query Kind="Program" />

// This problem was asked by Amazon.
// 
// Given an integer N, construct all possible binary search
// trees with N nodes.

void Main()
{
	// BST is a binary tree where for each node left child has a value
	// which is less than the root's value and the right child value is 
	// more or equal to the root's value
	// as far as I understood the goal is to generate all BST of different
	// topologies
	
	//GenerateTreesV2(3);
	
	for (int n = 1; n <= 5; n++)
	{
		var roots = GenerateTreesV2(n);
		
		roots.Count().Dump($"n={n}");
		
		foreach (var root in roots)
			root.Dump($"{TopologyId(root)}");
	}
}

IEnumerable<Node> GenerateTrees(int n)
{
	if (n == 1)
		return new[] { new Node() };
	
	// generate all trees of (n - 1) size
	IEnumerable<Node> prevLevelRoots = GenerateTrees(n - 1);
	
	var roots = new List<Node>();
	
	foreach (Node prevLevelRoot in prevLevelRoots)
	{
		// we can compose a new tree of a bigger size just by
		// adding a parent that has the previous tree as a left
		// or right subtree
		
		Node newTree1 = new Node(left: CloneTree(prevLevelRoot), right: null);
		Node newTree2 = new Node(left: null, right: CloneTree(prevLevelRoot));
		
		roots.Add(newTree1);
		roots.Add(newTree2);
		
		// additionally, there are other topologies that are formed by adding a child node
		// if the previous root was not complete (excluding the edge case where previous root is a single (leaf) node
		// where such algorithm will produce a duplicate with the technique above)
		
		// note that this technique also produces duplicates even for n=3
		// consider two trees from previopus level:
		//
		//      O             O
		//     /       and     \
		//    O                 O
		//
		// the below code would add a child to both cases with result of the same topology:
		//
		//          O
		//         / \
		//        O   O
		
		if (n > 2)
		{
			if (prevLevelRoot.Left == null)
			{
				Node newTree3 = CloneTree(prevLevelRoot);
				
				newTree3.Left = new Node();
				
				roots.Add(newTree3);
			}

			if (prevLevelRoot.Right == null)
			{
				Node newTree4 = CloneTree(prevLevelRoot);

				newTree4.Right = new Node();

				roots.Add(newTree4);
			}
		}
	}
	
	return roots;
}

IEnumerable<Node> GenerateTreesV2(int n)
{
	if (n == 1)
		return new[] { new Node() };
	
	// more generic and elegant generator...
	// basically for each tree of the len (n-1) we can construct other trees
	// by adding a node to each of n vacant places
	
	var roots = new List<Node>();
	
	IEnumerable<Node> prevRoots = GenerateTreesV2(n - 1);
	
	foreach (var prevRoot in prevRoots)
	{
		List<Node> newTreeRoots = new List<Node>();
		
		GenerateByAddingNode(prevRoot, prevRoot, newTreeRoots);
		
		roots.AddRange(newTreeRoots);
	}
	
	// leaves only ones with distinct topoly
	roots = roots.Distinct(ByTopologyComparer.Instance).ToList();
	
	return roots;
}

void GenerateByAddingNode(Node root, Node cur, List<Node> generated)
{
	if (cur.Left == null)
	{
		// add a new node to the left
		generated.Add(CloneTreeAddNode(root, cur, new Node(), addAsLeft: true));
	} else
	{
		GenerateByAddingNode(root, cur.Left, generated);
	}

	if (cur.Right == null)
	{
		// add a new node to the right
		generated.Add(CloneTreeAddNode(root, cur, new Node(), addAsLeft: false));
	} else
	{
		GenerateByAddingNode(root, cur.Right, generated);
	}
}

Node CloneTree(Node src)
{
	var cloned = new Node();
	
	if (src.Left != null)
		cloned.Left = CloneTree(src.Left);

	if (src.Right != null)
		cloned.Right = CloneTree(src.Right);
		
	return cloned;
}

Node CloneTreeAddNode(Node src, Node srcNodeToAddTo, Node nodeToBeAdded, bool addAsLeft)
{
	var cloned = new Node();

	if (src.Left != null)
		cloned.Left = CloneTreeAddNode(src.Left, srcNodeToAddTo, nodeToBeAdded, addAsLeft);

	if (src.Right != null)
		cloned.Right = CloneTreeAddNode(src.Right, srcNodeToAddTo, nodeToBeAdded, addAsLeft);

	// add a node as requested
	if (src == srcNodeToAddTo)
	{
		if (addAsLeft)
		{
			if (src.Left != null)
				throw new Exception("Left node already there!");
				
			cloned.Left = nodeToBeAdded;
		}
		else
		{
			if (src.Right != null)
				throw new Exception("Right node already there!");

			cloned.Right = nodeToBeAdded;
		}
	}

	return cloned;
}

class ByTopologyComparer : IEqualityComparer<Node>
{
	public static ByTopologyComparer Instance = new ByTopologyComparer();
	
	public bool Equals(Node x, Node y)
	{
		return TopologyId(x) == TopologyId(y);
	}

	public int GetHashCode(Node obj)
	{
		return TopologyId(obj).GetHashCode();
	}
}

static string TopologyId(Node cur)
{
	if (cur == null)
		return "nil";
	
	if (cur.Left == null && cur.Right == null)
		return "leaf";

	return $"[{TopologyId(cur.Left)}; {TopologyId(cur.Right)}]";
}

public class Node
{
	public Node Left { get; set; }
	public Node Right { get; set; }
	
	public Node(Node left = null, Node right = null)
	{
		Left = left;
		Right = right;
	}
}