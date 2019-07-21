<Query Kind="Program" />

// This problem was asked by Amazon.
// 
// Implement a stack API using only a heap. A stack implements
// the following methods:
// 
// push(item), which adds an element to the stack
// pop(), which removes and returns the most recently added element
// (or throws an error if there is nothing on the stack)
//
// Recall that a heap has the following operations:
// 
// push(item), which adds a new key to the heap
// pop(), which removes and returns the max value of the heap

void Main()
{
	// obviously if we use always increasing number as a key it all just works out
	
	var stack = new MyStack<int>();
	
	stack.Push(1);
	stack.Push(2);
	stack.Push(3);
	
	stack.Pop().Dump("3");
	
	stack.Push(4);
	stack.Push(5);
	
	stack.Pop().Dump("5");
	stack.Pop().Dump("4");
	stack.Pop().Dump("2");
	stack.Pop().Dump("1");
}

public class MyStack<T>
{
	private class Wrapper
	{
		public T Item { get; set; }
		public int Value { get; set; }
	}
	
	private FakeHeap<Wrapper> _heap;
	
	private int _counter = 0;
	
	public MyStack()
	{
		_heap = new UserQuery.FakeHeap<UserQuery.MyStack<T>.Wrapper>(x => x.Value);
	}
	
	public void Push(T item)
	{
		_heap.Push(new Wrapper() { Item = item, Value = _counter++ });
	}
	
	public T Pop()
	{
		return _heap.Pop().Item;
	}
}

public class FakeHeap<T>
{
	private List<T> _items = new List<T>();
	private Func<T, int> _getValueFn;
	
	public FakeHeap(Func<T, int> getValueFn)
	{
		_getValueFn = getValueFn;
	}
	
	public void Push(T item)
	{
		_items.Add(item);
	}
	
	public T Pop()
	{
		if (!_items.Any())
			throw new Exception("Empty!");
		
		var toReturn = _items.OrderByDescending(x => _getValueFn(x)).First();
		
		_items.Remove(toReturn);
		
		return toReturn;
	}
}