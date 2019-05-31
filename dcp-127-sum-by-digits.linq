<Query Kind="Program" />

// This problem was asked by Microsoft.
// 
// Let's represent an integer in a linked list format
// by having each node represent a digit in the number.
// The nodes make up the number in reversed order.
// 
// For example, the following linked list:
// 
// 1 -> 2 -> 3 -> 4 -> 5
//
// is the number 54321.
// 
// Given two linked lists in this format, return their
// sum in the same linked list format.
// 
// For example, given
// 
// 9 -> 9
// 5 -> 2
//
// return 124 (99 + 25) as:
// 
// 4 -> 2-> 1

void Main()
{
	Sum(
		a: new Node(1),
		b: new Node(1)).Dump();

	Sum(
		a: new Node(9, new Node(9)),
		b: new Node(5, new Node(2))).Dump();

	Sum(
		a: new Node(9, new Node(9)),
		b: new Node(1)).Dump();
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

public Node Sum(Node a, Node b)
{
	Node result = new Node(0);
	
	Node current = result;
	int overflow = 0;
	
	while (a != null || b != null || overflow != 0)
	{
		// handle the "next" ditits
		int aVal = a?.Value ?? 0;
		int bVal = b?.Value ?? 0;
		
		int curVal = (aVal + bVal + overflow) % 10;
		
		overflow = ((aVal + bVal + overflow) - curVal) / 10;
		
		current.Value = curVal;
		
		// go to the next one!
		a = a?.Next;
		b = b?.Next;

		if (!(a == null && b == null && overflow == 0))
		{
			current.Next = new Node(0);
			current = current.Next;
		}
	}
	
	return result;
}