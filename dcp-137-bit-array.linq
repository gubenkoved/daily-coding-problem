<Query Kind="Program" />

// This problem was asked by Amazon.
// 
// Implement a bit array.
// 
// A bit array is a space efficient array that holds a value of 1 or 0 at each index.
// 
// init(size): initialize the array with size
// set(i, val): updates index at i with val where val is either 1 or 0.
// get(i): gets the value at index i.

void Main()
{
	var a = new BitArray(123);
	
	a.Set(12, true);
	a.Get(12).Dump("true");
	a.Set(13, true);
	a.Get(13).Dump("true");
	a.Get(12).Dump("true");
	a.Set(12, false);
	a.Get(12).Dump("false");
	a.Set(7, true);
	a.Get(7).Dump("true");
	a.Set(0, true);
	a.Get(0).Dump("true");
}

public class BitArray
{
	private byte[] _data;
	private int _size;
	
	public BitArray(int size)
	{
		_size = size;
		_data = new byte[(int)Math.Ceiling(size / 8.0)];
	}
	
	public bool Get(int idx)
	{
		if (idx >= _size || idx < 0)
			throw new ArgumentOutOfRangeException();	
		
		int byteIdx = idx / 8;
		int offset = idx - byteIdx * 8;
		
		byte d = _data[byteIdx];
		
		return ((d >> offset) & 1) == 1;
	}
	
	public void Set(int idx, bool val)
	{
		if (idx >= _size || idx < 0)
			throw new ArgumentOutOfRangeException();

		int byteIdx = idx / 8;
		int offset = idx - byteIdx * 8;

		byte d = _data[byteIdx];
		
		int valInt = val ? 1 : 0;
		
		// update the byte
		d = (byte)(d & ~(1 << offset) | valInt << offset);
		
		_data[byteIdx] = d;
	}
}
