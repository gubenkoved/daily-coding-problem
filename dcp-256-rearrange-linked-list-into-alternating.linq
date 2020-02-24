<Query Kind="Program" />

// This problem was asked by Fitbit.
// 
// Given a linked list, rearrange the node values such that they appear
// in alternating low -> high -> low -> high ... form. For example,
// given 1 -> 2 -> 3 -> 4 -> 5, you should return 1 -> 3 -> 2 -> 5 -> 4.

void Main()
{
	// assumes no duplicates, otherwise might result in solutions
	// where two equal items are adjacent
	
	var h = new Node(1, new Node(2, new Node(3, new Node(4, new Node(5)))));
	
	RearangeToAlternating(h);
	
	h.Dump();

	// validate on random lists!
	Node rnd = Generate(1000);
	
	RearangeToAlternating(rnd);
	
	Validate(rnd);
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

void RearangeToAlternating(Node head)
{
	bool inc = true;

	Node cur = head;
	Node next = cur.Next;

	while (cur != null && next != null)
	{
		if (inc && cur.Value > next.Value || !inc && cur.Value < next.Value)
			(cur.Value, next.Value) = (next.Value, cur.Value);
		
		// advance pointers forward
		cur = next;
		next = next.Next;
		
		// alternate!
		inc = !inc;
	}
}

// auto-validation

Node Generate(int n)
{
	Random r = new Random();
	
	HashSet<int> numbers = new HashSet<int>();
	
	// ensures unique numbers
	Func<int> genFn = () =>
	{
		int num;
		
		do 
		{
			num = r.Next() % 1000000;
		} while (numbers.Contains(num));
		
		numbers.Add(num);
		
		return num;
	};
	
	Node head = new Node(genFn());
	
	Node cur = head;
	
	for (int i = 1; i < n; i++)
	{
		cur.Next = new Node(genFn());
		cur = cur.Next;
	}
	
	return head;
}

void Validate(Node head)
{
	bool inc = true;
	
	Node cur = head;
	Node next = cur.Next;
	
	while (cur != null && next != null)
	{
		if (inc && cur.Value > next.Value || !inc && cur.Value < next.Value)
			throw new Exception("WA!");
		
		cur = next;
		next = next.Next;
		inc = !inc;
	}
}