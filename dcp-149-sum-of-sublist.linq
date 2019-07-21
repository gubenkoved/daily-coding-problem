<Query Kind="Program" />

// This problem was asked by Goldman Sachs.
// 
// Given a list of numbers L, implement a method sum(i, j) which
// returns the sum from the sublist L[i:j] (including i, excluding j).
// 
// For example, given L = [1, 2, 3, 4, 5], sum(1, 3) should return
// sum([2, 3]), which is 5.
// 
// You can assume that you can do some pre-processing. sum() should 
// be optimized over the pre-processing step.

void Main()
{
	var s = new Sumator(new[] { 1, 2, 3, 4, 5 });
	
	s.Sum(1, 3).Dump();
}

public class Sumator
{
	private long[] _partial;
	
	public Sumator(int[] l)
	{
		_partial = Preprocess(l);
	}

	public long Sum(int i, int j)
	{
		return _partial[j - 1] - _partial[i - 1];
	}

	private long[] Preprocess(int[] l)
	{
		long[] result = new long[l.Length];

		long partialSum = 0;

		for (int i = 0; i < l.Length; i++)
		{
			partialSum += l[i];
			result[i] = partialSum;
		}

		return result;
	}
}