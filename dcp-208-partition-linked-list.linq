<Query Kind="Program" />

// This problem was asked by LinkedIn.
// 
// Given a linked list of numbers and a pivot k, partition the linked list so that all
// nodes less than k come before nodes greater than or equal to k.
// 
// For example, given the linked list 5 -> 1 -> 8 -> 0 -> 3 and k = 3, the solution
// could be 1 -> 0 -> 5 -> 8 -> 3.

void Main()
{
	Partition(new Node(3, new Node(2, new Node(1))), 1).Dump();
	Partition(new Node(5, new Node(1, new Node(8, new Node(0, new Node(3))))), 3).Dump();
	Partition(new Node(5, new Node(1, new Node(0, new Node(3, new Node(6))))), 4).Dump();
	Partition(new Node(5, new Node(4, new Node(3, new Node(2, new Node(1))))), 1).Dump();
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

public Node Partition(Node head, int pivot)
{
	Node newHead = head;
	Node prev = null;
	Node cur = head;
	Node last = GetLast(head);
	
	Node firstMoved = null;
	
	while (cur != null)
	{
		// break the endless cycle!
		if (cur == firstMoved)
			break;
			
		Node next = cur.Next;
		
		// move "cur" to the right!
		if (cur.Value > pivot)
		{
			$"moving {cur.Value} to the right!".Dump();
			
			if (firstMoved == null)
				firstMoved = cur;
			
			if (cur != newHead)
				prev.Next = next; // rewire previous to the next skipping current
			else
				newHead = next; // moving the head!
			
			last.Next = cur; // previous last item now points to the current
			
			cur.Next = null; // current is now the last one!
			
			last = cur; // update the last pointer
		}
		
		// move on to the next one!
		prev = cur;
		cur = next;
	}
	
	return newHead;
}

public Node GetLast(Node head)
{
	Node cur = head;
	
	while (cur.Next != null)
		cur = cur.Next;
		
	return cur;
}