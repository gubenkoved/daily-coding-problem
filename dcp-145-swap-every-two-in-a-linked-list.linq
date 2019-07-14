<Query Kind="Program" />

// This problem was asked by Google.
// 
// Given the head of a singly linked list, swap every two nodes and return its head.
// 
// For example, given 1 -> 2 -> 3 -> 4, return 2 -> 1 -> 4 -> 3.

void Main()
{
	//var head = new Node(1);
	//var head = new Node(1, new Node(2));
	//var head = new Node(1, new Node(2, new Node(3)));
	var head = new Node(1, new Node(2, new Node(3, new Node(4))));
	//var head = new Node(1, new Node(2, new Node(3, new Node(4, new Node(5)))));
	//var head = new Node(1, new Node(2, new Node(3, new Node(4, new Node(5, new Node(6, new Node(7, new Node(8))))))));
	
	Print(head);
	
	var newHead = SwapEveryTwo(head);
	
	Print(newHead);
}

public void Print(Node head)
{
	Node cur = head;
	
	while (cur != null)
	{
		Console.Write($"{cur.Value} -> ");
		
		cur = cur.Next;
	}
	
	Console.WriteLine("null");
}

Node SwapEveryTwo(Node head)
{
	// edge case
	if (head.Next == null)
		return head;

	Node newHead = head.Next;

	Node cur = head;
	Node prev = null;

	while (cur != null)
	{
		//$"Process {cur.Value}, prev value is {prev.Value}".Dump();

		Node next = cur.Next;
		Node nextNext = next?.Next;

		if (next == null)
		{
			//"next == null".Dump();
			prev.Next = cur;
			cur.Next = null;
			break;
		}

		// okay, if we are there, then we can perform a switch and advance further!
		if (prev != null)
			prev.Next = next;
		else // an edge case where there is no tail of a good list yet
			prev = next;
		
		next.Next = cur;

		// handle the end of the list cases
		cur.Next = null;

		// advance the pointers
		prev = cur; // maitain tail of already built result list
		cur = nextNext; // we step two nodes ahead (might become null)
	}

	return newHead;
}

public class Node
{
	public Node Next { get; set; }
	
	public int Value { get; set; }
	
	public Node(int value, Node next = null)
	{
		Value = value;
		Next = next;
	}
}
