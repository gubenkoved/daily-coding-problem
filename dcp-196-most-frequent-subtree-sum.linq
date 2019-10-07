<Query Kind="Program" />

// This problem was asked by Apple.
// 
// Given the root of a binary tree, find the most frequent subtree sum.
// The subtree sum of a node is the sum of all values under a node, including the node itself.
// 
// For example, given the following tree:
// 
//   5
//  / \
// 2  -5
//
// Return 2 as it occurs twice: once as the left leaf, and once as the sum of 2 + 5 - 5.



void Main()
{
	MostFrequentSubtreeSum(new Node(5, new Node(2), new Node(-5))).Dump("2");
}

public int MostFrequentSubtreeSum(Node root)
{
	var freq = new Dictionary<int, int>();
	
	Process(root, freq);
	
	freq.Dump();
	
	return freq.OrderByDescending(x => x.Value).First().Key;
}

public int Process(Node cur, Dictionary<int, int> freq)
{
	int subtreeSum = cur.Value;
	
	if (cur.Left != null)
		subtreeSum += Process(cur.Left, freq);

	if (cur.Right != null)
		subtreeSum += Process(cur.Right, freq);

	if (!freq.ContainsKey(subtreeSum))
		freq[subtreeSum] = 0;
	
	freq[subtreeSum] += 1;
	
	return subtreeSum;
}

public class Node
{
	public int Value { get; set; }
	public Node Left { get; set; }
	public Node Right { get; set; }
	
	public Node(int v, Node l = null, Node r = null)
	{
		Value = v;
		Left = l;
		Right = r;
	}
}