<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// You have a large array with most of the elements as zero.
// 
// Use a more space-efficient data structure, SparseArray,
// that implements the same interface:
// 
// init(arr, size): initialize with the original large array and size.
// set(i, val): updates index at i with val.
// get(i): gets the value at index i.


void Main()
{
	var orig = new[] { 0, 0, 0, 0, 1, 0, 0, 123, 0, 0, 42  };

	var a = new SparseArray<int>(orig);
	
	for (int i = 0; i < orig.Length; i++)
		Console.Write($"{a.Get(i)} ");
}

public class SparseArray<T>
{
	private int _size;
	private Dictionary<int, T> _data = new Dictionary<int, T>();
	
	public SparseArray(T[] orig)
	{
		_size = orig.Length;
		
		for (int i = 0; i < orig.Length; i++)
		{
			var val = orig[i];
			
			if (!val.Equals(default(T)))
				_data[i] = orig[i];
		}

		Util.Metatext($"initialized with storage capacity: {_data.Count}").Dump();
	}
	
	public void Set(int idx, T val)
	{
		_data[idx] = val;
	}
	
	public T Get(int idx)
	{
		if (idx < 0 || idx >= _size)
			throw new ArgumentOutOfRangeException(nameof(idx));
			
		if (!_data.ContainsKey(idx))
			return default(T);
		
		return _data[idx];
	}
}
