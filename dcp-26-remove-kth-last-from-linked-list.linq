<Query Kind="Program" />

// This problem was asked by Google.
// 
// Given a singly linked list and an integer k, remove the kth last element
// from the list. k is guaranteed to be smaller than the length of the list.
// 
// The list is very long, so making more than one pass is prohibitively expensive.
// 
// Do this in constant space and in one pass.

void Main()
{
	//var head = new Node("1", new Node("2", new Node("3", new Node("4", new Node("5", new Node("6", new Node("7")))))));
	
	//head = RemoveKLast(head, 2); // zero based -- remove "5"
	
	var head = new Node("1", new Node("2", new Node("3")));
	
	head = RemoveKLast(head, 3); // head is being removed
	
	head.Dump();
}

public Node RemoveKLast(Node head, int k)
{
	Node kLastPrev = head;
	
	Node current = head;

	// fast forward current K elements
	for (int i = 0; i <= k + 1; i++)
	{
		// edge case -- removing the first element
		if (i == k + 1 && current == null)
			return head.Next;

		if (current == null)
			throw new Exception($"Unable to find {k}-th (0 based) last element because end of list is reached (found amount: {i})");
		
		current = current.Next;
	}

	// now diff between klast and current is k element
	// go till the end and remove element that is pointed by klast pointer
	
	while (current != null)
	{
		current = current.Next;
		kLastPrev = kLastPrev.Next;
	}
	
	kLastPrev.Next.Dump("to be removed");
	
	kLastPrev.Next = kLastPrev.Next.Next;
	
	return head;
}

public class Node
{
	public string Value { get; set; }
	public Node Next { get; set; }
	
	public Node(string value, Node next = null)
	{
		Value = value;
		Next = next;
	}
}
