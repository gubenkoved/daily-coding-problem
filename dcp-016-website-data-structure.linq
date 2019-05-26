<Query Kind="Program" />

// This problem was asked by Twitter.
// 
// You run an e-commerce website and want to record the last N order ids in a log.
// Implement a data structure to accomplish this, with the following API:
// 
// record(order_id): adds the order_id to the log
// get_last(i): gets the ith last element from the log. i is guaranteed to be smaller than or equal to N.

void Main()
{
	var buffer = new Buffer<int>(3);
	
	buffer.Add(1);
	buffer.Add(2);
	buffer.Add(3);
	buffer.Add(4);
	
	buffer.GetLast(1).Dump(); 
	buffer.GetLast(2).Dump(); 
	buffer.GetLast(3).Dump(); 
	
	buffer.Add(5);
	
	"***".Dump();

	buffer.GetLast(1).Dump();
	buffer.GetLast(2).Dump();
	buffer.GetLast(3).Dump();
}

public class CircularBuffer<T>
{
	private T[] _data;
	private int _idx;
	private int _n;
	
	public CircularBuffer(int capacity)
	{
		_n = capacity;
		_data = new T[_n];
	}
	
	public void Add(T item)
	{
		_data[_idx] = item;
		
		_idx += 1;
		
		if (_idx >= _n)
			_idx = 0;
	}
	
	public T GetLast(int i)
	{
		int resultIdx = _idx - i;
		
		while (resultIdx < 0)
			resultIdx += _n;
		
		return _data[resultIdx];
	}
}