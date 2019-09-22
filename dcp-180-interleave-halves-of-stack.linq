<Query Kind="Program">
  <Namespace>System</Namespace>
</Query>

void Main()
{
	var stack = BuildStack(new[] { 1, 2, 3, 4 });
	PrintStack(stack, "source");
	Interleave(stack);
	PrintStack(stack, "expected: 1, 4, 2, 3 <-- head");
	
	stack = BuildStack(new[] { 1, 2, 3, 4, 5 });
	Interleave(stack);
	PrintStack(stack, "expected: 1, 5, 2, 4, 3 <-- head");

	stack = BuildStack(new[] { 1, 2, 3, 4, 5, 6 });
	Interleave(stack);
	PrintStack(stack, "expected: 1, 6, 2, 5, 3, 4 <-- head");
}

Stack<int> BuildStack(int[] values)
{
	var stack = new Stack<int>();

	foreach (int v in values)
		stack.Push(v);
	
	return stack;
}

void PrintStack(Stack<int> stack, string label)
{
	var a = stack.Reverse().ToArray();
	
	$"{string.Join(", ", a)} <-- head".Dump(label);
}

void Interleave(Stack<int> stack)
{
	var queue = new Queue<int>();
	
	int n = stack.Count;
	
	// reverse first n/2 elements on stack first!
	//for (int i = 0; i < (int) Math.Floor(n / 2.0); i++)
	for (int i = 0; i < n / 2; i++)
		queue.Enqueue(stack.Pop());
	
  	for (int i = 0; i < n / 2; i++)
  		stack.Push(queue.Dequeue());
  		
  	// okay, now we can start intervealing, get the half into the queue again
  	// note that we will build an answer reversed first and then reverse a 
  	// whole thing again
  	for (int i = 0; i < n / 2; i++)
  		queue.Enqueue(stack.Pop());
	
	// if n was odd it messes up pair building meachnism bellow, so take it out!
	int? hanging = null;
	
	if (n % 2 != 0)
		hanging = stack.Pop();
	
	// what we got now: first half sitting on stack in direct order
	// second half sits on the queue in a direct order as well
	
	while (stack.Count > 0)
	{
		queue.Enqueue(queue.Dequeue());
		queue.Enqueue(stack.Pop());
	}

	if (hanging != null)
		stack.Push(hanging.Value);

	// okay now get everything to the stack
	while (queue.Count > 0)
		stack.Push(queue.Dequeue());
	
	// reverse a whole thing
	while (stack.Count > 0)
		queue.Enqueue(stack.Pop());
		
	while (queue.Count > 0)
		stack.Push(queue.Dequeue());
}