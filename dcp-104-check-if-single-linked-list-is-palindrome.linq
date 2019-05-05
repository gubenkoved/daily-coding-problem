<Query Kind="Program" />

// This problem was asked by Google.
// 
// Determine whether a doubly linked list is a
// palindrome. What if it’s singly linked?
// 
// For example, 1 -> 4 -> 3 -> 4 -> 1 returns
// True while 1 -> 4 returns False.

void Main()
{
	// solving for double linked list is straightforward
	// for single linked list case we can:
	// a. find the len of the list for O(n)
	// b. for first [n / 2] elements inverse the list for O(n)
	// c. given two (halfs) single linked lists do the comparisons for O(n)
	// d. we can rewire the lists back
	
	Node root = new Node(1, new Node(2, new Node(3, new Node(4, new Node(5)))));
	
//	Node root1;
//	Node root2;
//	
//	PartialReverse(root, 2, out root1, out root2);
//	
//	root1.Dump();
//	root2.Dump();

	IsPalindrome(root).Dump("false");
	//root.Dump();
	
	root = new Node(1, new Node(4, new Node(3, new Node(4, new Node(1))))); 
	IsPalindrome(root).Dump("true");

	root = new Node(1, new Node(4, new Node(4, new Node(1))));
	IsPalindrome(root).Dump("true");
}

bool IsPalindrome(Node root)
{
	int n = Count(root);
	
	Node root1;
	Node root2;
	
	PartialReverse(root, n / 2, out root1, out root2);
	
	Node cur1 = root1;
	Node cur2 = root2;
	
	if (n % 2 == 1) // compensate for odd len
		cur2 = cur2.Next;
	
	bool isPalindrome = true;
	
	while (cur1 != null)
	{
		if (cur1.Value != cur2.Value)
		{
			isPalindrome = false;
			break;
		}
		
		cur1 = cur1.Next;
		cur2 = cur2.Next;
	}
	
	// okay, we know the answer already, but let's restore the original list!
	Node tmp1, tmp2;
	PartialReverse(root1, n / 2, out tmp1, out tmp2); // reverse the first half back

	// merge the lists back!
	root1.Next = root2;
	
	return isPalindrome;
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

int Count(Node root)
{
	Node cur = root;
	
	int counter = 0;
	
	while (cur != null)
	{
		counter += 1;
		cur = cur.Next;
	}
	
	return counter;
}

// reverses first k elements and returns two new heads
void PartialReverse(Node root, int k, out Node root1, out Node root2)
{
	Node prev = null;
	Node cur = root;

	int counter = 0;
	
	while (counter < k)
	{
		Node next = cur.Next;
		cur.Next = prev;
		
		// move pointers
		prev = cur;
		cur = next;
		
		counter += 1;
	}
	
	root1 = prev;
	root2 = cur;
}