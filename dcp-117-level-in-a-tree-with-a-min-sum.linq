<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// Given a binary tree, return the level of the tree with minimum sum.

void Main()
{
	var root = new Node(1,
		new Node(2,
			new Node(3),
			new Node(-100,
				new Node(4))),
		new Node(5,
			new Node(6,
				new Node(7))));
				
	LevelWithMinSum(root).Dump("2");
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

int LevelWithMinSum(Node root)
{
	var map = new Dictionary<int, long>();
	
	Traverse(root, 0, map);
	
	//map.Dump();
	
	return map.OrderBy(x => x.Value).First().Key;
}

void Traverse(Node cur, int level, Dictionary<int, long> map)
{
	if (!map.ContainsKey(level))
		map[level] = 0;
		
	map[level] += cur.Value;
	
	if (cur.Left != null)
		Traverse(cur.Left, level + 1, map);
		
	if (cur.Right != null)
		Traverse(cur.Right, level + 1, map);
}