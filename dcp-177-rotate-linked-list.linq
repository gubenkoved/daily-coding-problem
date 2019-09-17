<Query Kind="Program" />

// This problem was asked by Airbnb.
// 
// Given a linked list and a positive integer k, rotate the list
// to the right by k places.
// 
// For example, given the linked list 7 -> 7 -> 3 -> 5 and k = 2,
// it should become 3 -> 5 -> 7 -> 7.
// 
// Given the linked list 1 -> 2 -> 3 -> 4 -> 5 and k = 3, it should
// become 3 -> 4 -> 5 -> 1 -> 2.

void Main()
{
	var h = new Node(1, new Node(2, new Node(3, new Node(4, new Node(5)))));
	
	Rotate(h, 3).Dump();
}

// O(n * k) (can be optimized to O(n))
public Node Rotate(Node head, int k)
{
	for (int i = 0; i < k; i++)
	{
		Node last2 = Last(head, 2); // O(n)
		
		Node newHead = last2.Next;

		last2.Next.Next = head;
		
		last2.Next = null;
		
		head = newHead;
	}
	
	return head;
}

public Node Last(Node head, int k)
{
	Node l = head;
	Node r = head;
	
	for (int i = 0; i < k; i++)
		r = r.Next;

	while (r != null)
	{
		l = l.Next;
		r = r.Next;
	}
		
	return l;
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
