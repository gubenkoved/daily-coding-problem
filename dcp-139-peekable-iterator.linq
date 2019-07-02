<Query Kind="Program" />

// This problem was asked by Google.
// 
// Given an iterator with methods next() and hasNext(), create
// a wrapper iterator, PeekableInterface, which also implements
// peek(). peek shows the next element that would be returned on
// next().
// 
// Here is the interface:
// 
// class PeekableInterface(object):
//     def __init__(self, iterator):
//         pass
// 
//     def peek(self):
//         pass
// 
//     def next(self):
//         pass
// 
//     def hasNext(self):
//         pass

void Main()
{
	var a = new [] { 1, 2, 3, 4, 5 };
	
	var iter = new Iterator<int>(a);
	var peekable = new PeekableIterator<int>(iter);
	
	peekable.Peek().Dump("1");
	peekable.Peek().Dump("1");
	peekable.Next().Dump("1");
	peekable.Peek().Dump("2");
	peekable.Next().Dump("2");
	peekable.Next().Dump("3");
	peekable.Next().Dump("4");
	peekable.HasNext().Dump("true");
	peekable.Next().Dump("5");
	peekable.HasNext().Dump("false");
}

public interface IIterator<T>
{
	T Next();
	bool HasNext();
}

public class Iterator<T> : IIterator<T>
{
	private int _nextIdx;
	private T[] _data;
	
	public Iterator(T[] data)
	{
		_data = data;
	}

	public bool HasNext()
	{
		return _nextIdx < _data.Length;
	}

	public T Next()
	{
		return _data[_nextIdx++];
	}
}

public class PeekableIterator<T> : IIterator<T>
{
	private IIterator<T> _iterator;
	private T _peeked;
	private bool _hasPeeked;
	
	public PeekableIterator(IIterator<T> iterator)
	{
		_iterator = iterator;
	}
	
	public T Peek()
	{
		if (_hasPeeked)
			return _peeked;
			
		if (!HasNext())
			throw new InvalidOperationException("No next!");
		
		_peeked = Next();
		_hasPeeked = true;
		
		return _peeked;
	}

	public bool HasNext()
	{
		if (_hasPeeked)
			return true;
			
		return _iterator.HasNext();
	}

	public T Next()
	{
		if (!_hasPeeked)
		{
			return _iterator.Next();
		} else // peeked!
		{
			_hasPeeked = false;
			
			return _peeked;
		}
	}
}