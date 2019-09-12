<Query Kind="Program" />

// This problem was asked by Google.
// 
// Given a linked list, sort it in O(n log n) time and constant space.
// 
// For example, the linked list 4 -> 1 -> -3 -> 99 should become -3 -> 1 -> 4 -> 99.

void Main()
{
	//	var	h = new Node(1, new Node(2, new Node(4, new Node(9, new Node(1, new Node(4, new Node(8, new Node(9))))))));
	//	
	//	var h2 = MergeSort(h, Step(h, 4), 4);
	//	
	//	h2.Dump();

	var h = Create(new[] { 9, 1, 4, 2, 8, 9, 1, 4, 6, 5, 2, 2, 9 });
	//var h = Create(new[] { 4, 3, 2, 1 });
	//var h = Create(new[] { 4, 3, 2, 1, 0, });
	//var h = Create(new[] { 4, 1, -3, 99 });
	
	var h2 = Sort(h);
	
	h2.Dump();
}

public class Node
{
	public int Value { get; set; }
	public Node Next { get; set; }
	
	public Node(int value, Node next = null)
	{
		Value = value;
		Next = next;
	}
}

public Node Create(int[] a)
{
	Node head = null;
	Node cur = null;
	for (int i = 0; i < a.Length; i++)
	{
		if (cur == null)
		{
			cur = new Node(a[i]);
			head = cur;
		}
		else
		{
			cur.Next = new Node(a[i]);
			cur = cur.Next;
		}
	}
	
	return head;
}

public Node Sort(Node head)
{
	int n = Count(head); // O(n)
	
	for (int w = 1; w < n; w *= 2)
	{
		// on each pass we will sort sorted subsequences of length w for O(n)
		// there will be log(n) passes, giving the total time complexity of O(n * logn)

		$"pass with w={w}...".Dump();
		
		Node blockStart = head;
		Node prevBlockEnd = null;

		while (blockStart != null)
		{
			// we need to know that in advance, otherwise list may be messed up as we rearrange elements on the go
			Node nextBlockStart = Step(blockStart, w * 2); 
			
			Node l = blockStart;
			Node r = Step(blockStart, w);

			// now, merge sort two sorted subsequences of w elements (or less if reached the end) in total
			Node newBlockEnd;
			Node newBlockHead = MergeSortBlock(l, r, w, out newBlockEnd);
			
			if (prevBlockEnd != null)
				prevBlockEnd.Next = newBlockHead;

			prevBlockEnd = newBlockEnd;

			// maintain overall head!
			if (blockStart == head)
				head = newBlockHead;

			// advance to the next block!
			blockStart = nextBlockStart;
		}
		
		// break the cycle for last element!
		prevBlockEnd.Next = null;
		
		//head.Dump($"after pass for w={w}");
	}
	
	return head;
}

public Node MergeSortBlock(Node l, Node r, int w, out Node blockEnd)
{
	// w corresponds to maximum amount of items to be consumed starting from either l or r 
	int lq = w;
	int rq = w;
	
	Node prev = null;
	Node blockHead = null;
	
	while (true)
	{
		// we can exaust items even from the left pointer!
		if ((lq == 0 || l == null) && (rq == 0 || r == null))
			break;
		
		if (lq > 0 && (rq == 0 || r == null || l.Value <= r.Value))
		{
			// take from the l
			if (prev != null)
				prev.Next = l;
				
			prev = l;
			lq -= 1;
			l = l.Next;
		} else
		{
			// take from the right
			if (prev != null)
				prev.Next = r;
			
			prev = r;
			rq -= 1;
			r = r.Next;
		}
		
		// maintain new head!
		if (blockHead == null)
			blockHead = prev;
	}
	
	blockEnd = prev;
	
	return blockHead;
}

public int Count(Node head)
{
	int k = 0;
	
	Node cur = head;
	
	do
	{
		k += 1;	
		cur = cur.Next;
	} while (cur != null);
	
	return k;
}

public Node Step(Node cur, int k)
{
	Node result = cur;

	for (int i = 0; i < k; i++)
	{
		result = result.Next;
		
		if (result == null)
			break; // reached the end already...
	}
	
	return result;
}