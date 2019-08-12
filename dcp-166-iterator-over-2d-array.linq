<Query Kind="Program" />

// This problem was asked by Uber.
// 
// Implement a 2D iterator class. It will be initialized with an array of arrays, and should implement the following methods:
// 
// next(): returns the next element in the array of arrays. If there are no more elements, raise an exception.
// has_next(): returns whether or not the iterator still has elements left.
// For example, given the input [[1, 2], [3], [], [4, 5, 6]], calling next() repeatedly should output 1, 2, 3, 4, 5, 6.
// 
// Do not use flatten or otherwise clone the arrays. Some of the arrays can be empty.

void Main()
{
	var a = new MyArray<int>(new[] { new[] { 1, 2, }, new int[] { 3, }, new int[] { }, new int[] { 4, 5, 6 } });
	
	foreach (var x in a)
		x.Dump();

	"***".Dump();

	var a_empty = new MyArray<int>(new int[][] { });

	foreach (var x in a_empty)
		x.Dump();

	"***".Dump();

	var a_empty2 = new MyArray<int>(new int[][] { new int[] {} });

	foreach (var x in a_empty2)
		x.Dump();

	"***".Dump();

	var b = new MyArray<int>(new int[][] { new int[] { 1, 2, 3, }, new int[] { }, new int[] { }, new int[] { 4, 5 } });
 
	foreach (var x in b)
		x.Dump();
}

public class MyArray<T> : IEnumerable<T>
{
	private class Enumerator : IEnumerator<T>
	{
		private MyArray<T> _array;
		private int _outer;
		private int _inner;
		
		public Enumerator(MyArray<T> array)
		{
			_array = array;
			
			Reset();
		}
		
		public T Current => GetCurrent();
		object IEnumerator.Current => GetCurrent();

		public void Dispose()
		{
		}

		public bool MoveNext()
		{
			if (_outer == -1)
			{
				// first one!
				if (_array._data.Length > 0 && _array._data[0].Length > 0)
				{
					_outer = 0;
					return true;
				} else
				{
					return false; // no elements!
				}
			}
			
			if (_inner < _array._data[_outer].Length - 1)
			{
				_inner += 1;
				return true;
			}
			else // current bucket is over
			{
				_inner = 0;
				
				do {
					_outer += 1; // next bucket while empty
				} while (_outer < _array._data.Length && _array._data[_outer].Length == 0);
				
				return _outer < _array._data.Length && _array._data[_outer].Length > 0;
			}
		}

		public void Reset()
		{
			_outer = -1;
			_inner = 0;
		}
		
		private T GetCurrent()
		{
			return _array._data[_outer][_inner];
		}
	}

	private T[][] _data;
	
	public MyArray(T[][] data)
	{
		_data = data;
	}

	public IEnumerator<T> GetEnumerator()
	{
		return new Enumerator(this);
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return new Enumerator(this);
	}
}
