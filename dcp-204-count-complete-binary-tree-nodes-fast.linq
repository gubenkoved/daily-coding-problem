<Query Kind="Program" />

// This problem was asked by Amazon.
// 
// Given a complete binary tree, count the number of nodes in faster than O(n) time.
// Recall that a complete binary tree has every level filled except the last, and
// the nodes in the last level are filled starting from the left.



void Main()
{
	// does not seem [easy]...
	// first we find amount of levels by going all the way to left
	// let's say k is amount of levels (1-based)
	// amount of nodes for all levels but last is 2^(k - 1) - 1
	// then we need to count last level only
	// the last level can have from 1 to 2^k nodes
	// we need to use binary search there making use of the fact that path to
	// q-th node on the last level can be found converting q to binary form and using 
	// zeros and ones as an instruction to turn left or right (quite obvious on paper)
	
	var head =
		new Node(
			new Node(
				new Node(
					new Node(),
					new Node()),
				new Node(
					new Node(),
					null)),
			new Node(
				new Node(),
				new Node()));
				
	Count(head).Dump("10");

	var head2 =
		new Node(
			new Node());
				
	Count(head2).Dump("2");

	var head3 =
		new Node(
			new Node(),
			new Node());

	Count(head3).Dump("3");

	var head4 =
		new Node(
			new Node(
				new Node()),
			new Node());

	Count(head4).Dump("4");
}

// O(logn * logn)
// binary search is O(logn) for n items, but each item check is O(logn) as well...
public int Count(Node head)
{
	// step 1: depth + non-leaf level calculation
	
	int depth = Depth(head);
	
	int nonLeafNodes = (int)Math.Pow(2, depth - 1) - 1;
	
	// okay, the only thing now is count the leaf level!
	
	int leafNodes = 0;
	
	int min = 1;
	int max = (int) Math.Pow(2, depth - 1);
	
	// okay, now given LeafExists we can binary search the answer!
	while (true)
	{
		if (max - min == 1)
		{
			if (LeafExists(head, depth, max))
				leafNodes = max;
			else
				leafNodes = min;
				
			break;
		}
		
		// range is too large, split in half!
		int mid = (max + min) / 2;
		
		if (LeafExists(head, depth, mid))
			min = mid;
		else
			max = mid;
	}
	
	return nonLeafNodes + leafNodes;
}

// O(logn)
public bool LeafExists(Node head, int depth, int q)
{
	// okay, convert q to 0 based int, then to binary form
	string binary = Convert.ToString(q - 1, 2).PadLeft(depth - 1, '0');
	
	Node cur = head;
	
	for (int i = 0; i < depth - 1; i++)
	{
		if (binary[i] == '0')
			cur = cur.Left;
		else
			cur = cur.Right;
	}
	
	return cur != null;
}

public int Depth(Node head)
{
	Node cur = head;
	int depth = 1;
	
	while (cur.Left != null)
	{
		cur = cur.Left;
		depth += 1;
	}
	
	return depth;
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

