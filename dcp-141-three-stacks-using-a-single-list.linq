<Query Kind="Program" />

// This problem was asked by Microsoft.
// 
// Implement 3 stacks using a single list:
// 
// class Stack:
//     def __init__(self):
//         self.list = []
// 
//     def pop(self, stack_number):
//         pass
// 
//     def push(self, item, stack_number):
//         pass

void Main()
{
	var ms = new MultiStack<string>(3);
	
	ms.Push("a", 0);
	ms.Push("b", 1);
	ms.Push("c", 2);

	ms.Push("aa", 0);
	ms.Push("bb", 1);
	ms.Push("cc", 2);

	ms.Pop(0).Dump("aa");
	ms.Pop(0).Dump("a");
	ms.Pop(2).Dump("cc");
	ms.Pop(2).Dump("c");
	ms.Pop(1).Dump("bb");
	ms.Pop(1).Dump("b");
	
	ms.Push("a", 1);
	ms.Push("b", 1);
	ms.Push("c", 1);
	
	ms.Pop(1).Dump("c");
	ms.Pop(1).Dump("b");
	ms.Pop(1).Dump("a");

	ms.Push("a", 0);
	ms.Push("b", 2);
	
	try
	{	        
		ms.Pop(1);
	}
	catch (Exception ex)
	{
		"when stack is empty it fails -- ok!".Dump();
	}
	
}

class MultiStack<T>
{
	private List<T> _data;
	private int[] _offsets;
	
	public MultiStack(int n)
	{
		_offsets = new int[n]; // constant memory
		_data = new List<T>();
	}
	
	public void Push(T item, int stack)
	{
		if (stack >= _offsets.Length)
			throw new InvalidOperationException();
			
		int offset = _offsets[stack];
		
		_data.Insert(offset, item);
		
		// fix offsets for stacks to the "right"
		for (int stackIndex = stack + 1; stackIndex < _offsets.Length; stackIndex++)
			_offsets[stackIndex] += 1;
	}
	
	public T Pop(int stack)
	{
		if (stack >= _offsets.Length)
			throw new InvalidOperationException();

		int offset = _offsets[stack];

		if (offset >= _data.Count)
			throw new InvalidOperationException($"Stack {stack} is empty");
			
		if (stack < _offsets.Length - 1) // no the last stack
		{
			int nextOffset = _offsets[stack + 1];
			
			if (offset == nextOffset)
				throw new InvalidOperationException($"Stack {stack} is empty");
		}

		T item = _data[offset];
		
		_data.RemoveAt(offset);

		for (int stackIndex = stack + 1; stackIndex < _offsets.Length; stackIndex++)
			_offsets[stackIndex] -= 1;
			
		return item;
	}
}