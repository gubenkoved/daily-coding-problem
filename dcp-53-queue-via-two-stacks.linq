<Query Kind="Program" />

// This problem was asked by Apple.
// 
// Implement a queue using two stacks. Recall that a queue
// is a FIFO (first-in, first-out) data structure with the
// following methods: enqueue, which inserts an element
// into the queue, and dequeue, which removes it.

void Main()
{
	var queue = new MyQueue<int>();
	
	queue.Enqueue(1);
	queue.Enqueue(2);
	queue.Enqueue(3);
	queue.Enqueue(4);
	
	queue.Dequeue().Dump("1");
	queue.Dequeue().Dump("2");
	queue.Dequeue().Dump("3");
	
	queue.Enqueue(5);
	queue.Enqueue(6);
	
	queue.Dequeue().Dump("4");
	queue.Dequeue().Dump("5");
	queue.Dequeue().Dump("6");
}

public class MyQueue<T>
{
	private Stack<T> _straight;
	private Stack<T> _inverted;

	public MyQueue()
	{
		_straight = new Stack<T>();
		_inverted = new Stack<T>();
	}
	
	public void Enqueue(T element)
	{
		_straight.Push(element);
	}
	
	public T Dequeue()
	{
		// when inverted is empty move from another stack reversing the order
		if (_inverted.Count == 0)
		{
			if (_straight.Count == 0)
				throw new Exception("Queue is empty");
			
			while (_straight.Count > 0)
				_inverted.Push(_straight.Pop());
		}

		// return from inverted stack
		return _inverted.Pop();
	}
}
