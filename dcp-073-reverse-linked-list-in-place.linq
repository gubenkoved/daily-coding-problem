<Query Kind="Program" />

// This problem was asked by Google.
// 
// Given the head of a singly linked list, reverse it in-place.

void Main()
{
	var head = new Node(1, new Node(2, new Node(3, new Node(4))));
	
	head.Dump();
	
	head = Reverse(head);
	
	head.Dump();
}

public class Node
{
	public object Value { get; set; }
	public Node Next { get; set; }
	
	public Node(object value, Node next = null)
	{
		Value = value;
		Next = next;
	}
}

public Node Reverse(Node head)
{
	Node prev = null;
	Node cur = head;
	
	do
	{
		Node next = cur.Next;
		
		cur.Next = prev;
		
		prev = cur;
		cur = next;
	} while (cur != null);
	
	return prev;
}