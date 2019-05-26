<Query Kind="Program" />

// This problem was asked by Google.
// 
// Given k sorted singly linked lists, write a function
// to merge all the lists into one sorted singly linked list.

void Main()
{
//	var head = Merge(
//		new Node<int>(1, new Node<int>(2, new Node<int>(3))),
//		new Node<int>(10, new Node<int>(12, new Node<int>(13))),
//		new Node<int>(21, new Node<int>(22, new Node<int>(23))));

	//	head = Merge(
	//		new Node<int>(1, new Node<int>(2, new Node<int>(3))),
	//		new Node<int>(5, new Node<int>(6, new Node<int>(7))));

	var head = Merge(
		new Node<int>(11, new Node<int>(100)),
		new Node<int>(19, new Node<int>(33)),
		new Node<int>(8, new Node<int>(32)),
		new Node<int>(7, new Node<int>(11)));

	head.Dump();
}

public class Node<T>
	where T : IComparable<T>
{
	public T Value { get; set; }
	public Node<T> Next { get; set; }
	
	public Node(T value, Node<T> next = null)
	{
		Value = value;
		Next = next;
	}
}

Node<T> Merge<T>(params Node<T>[] heads)
	where T : IComparable<T>
{
	Node<T> head = null;
	Node<T> resultCurrentPointer = null;
	
	Node<T>[] pointers = heads.ToArray();
	
	do
	{
		int minValueNodeIndx = -1;
		bool foundAtLeastOne = false;
		
		for (int pointerIdx = 0; pointerIdx < pointers.Length; pointerIdx++)
		{
			if (pointers[pointerIdx] == null)
				continue;
			
			T curValue = pointers[pointerIdx].Value;
			
			if (minValueNodeIndx == -1 || curValue.CompareTo(pointers[minValueNodeIndx].Value) <= 0)
				minValueNodeIndx = pointerIdx;
				
			foundAtLeastOne = true;			
		}

		if (!foundAtLeastOne)
			break;

		if (resultCurrentPointer != null)
		{
			// move pointer that points to the node that 
			resultCurrentPointer.Next = pointers[minValueNodeIndx];
			// move forward
			resultCurrentPointer = resultCurrentPointer.Next; 
		} else
		{
			head = pointers[minValueNodeIndx];
			resultCurrentPointer = pointers[minValueNodeIndx];
		}
		
		// move to the next
		pointers[minValueNodeIndx] = pointers[minValueNodeIndx].Next;

		Util.Metatext($"Moving pointer {minValueNodeIndx} to the next value").Dump();
		
	} while (true);
	
	return head;
}
