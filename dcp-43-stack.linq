<Query Kind="Program" />

// This problem was asked by Amazon.
// 
// Implement a stack that has the following methods:
// 
// push(val), which pushes an element onto the stack
//
// pop(), which pops off and returns the topmost element of the stack.
// If there are no elements in the stack, then it should throw an error or return null.
//
// max(), which returns the maximum value in the stack currently.
// If there are no elements in the stack, then it should throw an error or return null.
// Each method should run in constant time.


void Main()
{
	var stack = new MyStack<int>();
	
	stack.Push(5);
	stack.Push(3);
	stack.Push(17);
	stack.Push(-1);
	stack.Push(11);
	stack.Push(345);
	stack.Push(700);
	stack.Push(12);
	
	stack.Max().Dump();
	stack.Pop();

	stack.Max().Dump();
	stack.Pop();

	stack.Max().Dump();
	stack.Pop();

	stack.Max().Dump();
	stack.Pop();

	stack.Max().Dump();
	stack.Pop();

	stack.Max().Dump();
	stack.Pop();

	stack.Max().Dump();
	stack.Pop();
	
	stack.Max().Dump();
	stack.Pop();

	stack.Count.Dump("count");
}

public class MyStack<T>
	where T : IComparable<T>
{
	private T[] _data;
	private T[] _max;
	private int _indx;
	
	public int Count
	{
		get
		{
			return _indx;
		}
	}
	
	public MyStack(int size = 4)
	{
		_data = new T[size];
		_max = new T[size];
	}
	
	public void Push(T item)
	{
		EnsureSize(_indx + 1);
		
		_data[_indx] = item;
		
		if (_indx == 0)
			_max[_indx] = item;
		else
			_max[_indx] = Max(item, _max[_indx - 1]);
		
		_indx += 1;
	}
	
	public T Pop()
	{
		if (_indx == 0)
			throw new InvalidOperationException("Stack is empty");
	
		T item = _data[_indx - 1];
		
		_indx -= 1;
		
		return item;
	}
	
	public T Max()
	{
		if (_indx == 0)
			throw new InvalidOperationException("Stack is empty");
		
		return _max[_indx - 1];
	}
	
	private void EnsureSize(int size)
	{
		if (_data.Length < size)
		{
			// resize!
			int newSize = _data.Length * 2;
			
			_data = Resize(_data, newSize);
			_max = Resize(_max, newSize);
		}
	}
	
	private T[] Resize(T[] a, int size)
	{
		T[] newArray = new T[size];
		
		Array.Copy(a, newArray, a.Length);
		
		return newArray;
	}
	
	private T Max(T a, T b)
	{
		return a.CompareTo(b) > 0 ? a : b;
	}
}
