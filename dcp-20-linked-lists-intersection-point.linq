<Query Kind="Program" />

// This problem was asked by Google.
// 
// Given two singly linked lists that intersect at some point, find the intersecting node. The lists are non-cyclical.
// 
// For example, given A = 3 -> 7 -> 8 -> 10 and B = 99 -> 1 -> 8 -> 10, return the node with value 8.
// 
// In this example, assume nodes with the same value are the exact same node objects.
// 
// Do this in O(M + N) time (where M and N are the lengths of the lists) and constant space.

// Clarification of task from another source:
// There are two singly linked lists in a system. By some programming error,
// the end node of one of the linked list got linked to the second list,
// forming an inverted Y shaped list. Write a program to get the point where two linked list merge.
// WARNING, contains an answer as well:
// https://www.geeksforgeeks.org/write-a-function-to-get-the-intersection-point-of-two-linked-lists/

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

void Main()
{
	var a = new Node(3, new Node(7, new Node(8, new Node(10, new Node(12, new Node(13))))));
	//var a = new Node(3, new Node(7, new Node(8, new Node(10))));
	//var a = new Node(3, new Node(7)); // no intersection
	
	var intersection = a.Next.Next;
	
	var b = new Node(99, new Node(88, new Node(1, intersection)));
	
	Print(a);
	Print(b);
	
	FindIntersection(a, b).Dump("intersection node");

	//a = Reverse(a);
	
	//Print(a);
	
	// Alg 1
	// Naive O(M * N) time and O(1) space algorithm
	// start iterating one list and run by second fully till the end
	// check if elements are equals
	
	// Alg 2
	// O(M + N) time and O(M) space alg
	// create hashset of all nodes of the first chain
	// navigate second and find the first element which is foind in first chain
	
	// How to get good space complexity while still having O(M + N) time?...
	// Alg 3
	// Algebraic solution based on observed linked lists length and using
	// reversing a list techniqueue to resolve uncertanty as we will have
	// 2 equations with 3 unknown value otherwise
}

void Print(Node node)
{
	Node cur = node;
	
	do
	{
		Console.Write($"{cur.Value} > ");
		
		cur = cur.Next;
	} while (cur != null);
	
	Console.WriteLine("(end)");
}

Node FindIntersection(Node head1, Node head2)
{
	// lets say M is len of first list, N is len of seconds
	// and C is amount of common elements in the two lists (amount of elements after intersection point)
	// then lets also say that m is amount of elements from the begining of the first list
	// till the very intersection (excluding it), and n is defined accordingly for the second list
	// 
	// Then we can write the following equations:
	//
	// 1) m + C = M (trivial)
	// 2) n + C = N (trivial)
	//
	// At this point we got 2 equations with 3 unknowns so we have to come up with more information
	// to get it we can inverse the first list and count elements
	// note that it does not really matter which list we inverse, we will get the same len if we will
	// then calculate len of the second list; Lets reverse the first list and count nodes via second;
	// Let's say that resulting second list len is A, then:
	//
	// 3) A = m + n + 1 (see this graphically; +1 is needed to count intersection point)
	//
	// let's solve it, sum (1) + (2):
	// 		m + n + 2*C = M + N
	// then subtract (3) from it:
	//		2*C - 1 = M + N - A
	//
	// that it! now we can inverse the list back and we have all: C, m, n,
	// so we can just start iterating first list and get (m+1)-th element or (n+1)-th from second

	int M = Len(head1);
	int N = Len(head2);

	Node reversed1 = Reverse(head1);

	int A = Len(head2);
	
	Console.Write("Inverted: ");
	Print(reversed1);

	Console.Write("Updated second list: ");
	Print(head2);

	// restore the list!
	Reverse(reversed1);

	int C = (M + N - A + 1) / 2;
	
	int m = M - C;
	int n = N - C;

	new { M, N, A, C, m, n }.Dump();
	
	// take the m+1 from frist (index is m) or (n+1) from second
	// we need one more special check to solve for no intersectino case
	// check two results against each other -- if both pointing
	// to same element we are good, otherwise no intersection
	
	Node r1 = Take(head1, m);
	Node r2 = Take(head2, n);
	
	if (r1 == r2)
		return r1; // there is intersection
	else
		return null; // no intersection
}

Node Reverse(Node head)
{
	Node prev = null;
	Node cur = head;
	
	do
	{
		Node tmp = cur.Next;
		cur.Next = prev;
		prev = cur;
		cur = tmp;
	} while (cur != null);
	
	return prev;
}

int Len(Node head)
{
	int len = 0;
	
	while (head != null)
	{
		len += 1;
		head = head.Next;
	}
	
	return len;
}

Node Take(Node head, int index)
{
	Node cur = head;
	
	while (index > 0 && cur != null)
	{
		cur = cur.Next;
		index -= 1;
	}
	
	return cur;
}