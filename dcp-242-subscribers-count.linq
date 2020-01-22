<Query Kind="Program" />

// This problem was asked by Twitter.
// 
// You are given an array of length 24, where each element represents
// the number of new subscribers during the corresponding hour. Implement
// a data structure that efficiently supports the following:
// 
// update(hour: int, value: int): Increment the element at index hour by value.
//
// query(start: int, end: int): Retrieve the number of subscribers that have
// signed up between start and end (inclusive).
//
// You can assume that all values get cleared at the end of the day, and that
// you will not be asked for start and end values that wrap around midnight.

void Main()
{
	// hm, given the constant len, basic Dictionary-based solution is O(const) = O(1)
	// however, the problem can be generalized to n segments
	//
	// it makes simple solution be O(1) for update, but O(n) for query
	// simple optimization for query can give us O(1) query, but it will make update be O(n):
	//    so, we can store it as partial sums in array p (let's say)
	//    so that p[i] is a sum for counts from 0 to i (inclusive)
	//    then finding sum of a range (i, j) is simply O(1) operation: p[j] - p[i - 1] (or 0 if i - 1 < 0)
	//
	// given the complexity label there is probably a fancies way to do that w/o being O(n) for either query or update
	
	var c = new CounterQueryOptimzied(24);
	
	c.Update(3, 10);
	c.Update(5, 20);
	c.Update(6, 30);
	c.Update(7, 5);
	
	c.Query(3, 6).Dump("60");
}

public class CounterSimple
{
	private Dictionary<int, int> _m = new Dictionary<int, int>();
	
	// O(1)
	public void Update(int h, int val)
	{
		_m[h] = val;
	}
	
	// O(n) where n is amount of segments
	public int Query(int start, int end)
	{
		int result = 0;
		
		for (int h = start; h <= end; h++)
		{
			if (_m.ContainsKey(h))
				result += _m[h];
		}
		
		return result;
	}
}

public class CounterQueryOptimzied
{
	private int[] _cummulative;

	public CounterQueryOptimzied(int n)
	{
		_cummulative = new int[n];
	}

	// O(n)
	public void Update(int h, int val)
	{
		int diff = val - Get(h);
		
		// we then need to add diff to all partial sums more than or equal to h
		for (int i = h; i < _cummulative.Length; i++)
			_cummulative[i] += diff;
	}

	// O(1)
	public int Query(int start, int end)
	{
		int result = _cummulative[end];
		
		if (start > 0)
			result -= _cummulative[start - 1];
			
		return result;
	}

	private int Get(int h)
	{
		int val = _cummulative[h];

		if (h > 0)
			val -= _cummulative[h - 1];

		return val;
	}
}


