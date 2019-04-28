<Query Kind="Program" />

// This problem was asked by Palantir.
// 
// Given a number represented by a list of digits,
// find the next greater permutation of a number, in
// terms of lexicographic ordering. If there is not
// greater permutation possible, return the permutation
// with the lowest value/ordering.
// 
// For example, the list [1,2,3] should return [1,3,2]. The
// list [1,3,2] should return [2,1,3]. The list [3,2,1] should
// return [1,2,3].
// 
// Can you perform the operation without allocating extra memory
// (disregarding the input memory)?

void Main()
{
	// Aposteriori: ended up with algorithm like that:
	// https://www.nayuki.io/page/next-lexicographical-permutation-algorithm
	
	int[] current = new[] { 1, 2, 3, 4, 5, 6, };
	
	for (int i = 0; i <= Factorial(current.Length); i++)
	{
		string.Join(", ", current).Dump();
		
		current = NextPermutation(current);
	}
}

// seems to be O(n*n) by time, O(1) by space though...
public T[] NextPermutation<T>(T[] current)
	where T : IComparable<T>
{
	int n = current.Length;
	
	for (int i = n - 2; i >= 0; i--)
	{
		for (int j = n - 1; j > i; j--)
		{
			if (current[i].CompareTo(current[j]) < 0)
			{
				Swap(current, i, j);
				Reverse(current, i + 1);
				return current;
			}
		}
	}
	
	// did not find any pair to swap, so we have to reset it all...
	Reverse(current, 0);
	return current;
}

public void Reverse<T>(T[] current, int startIndex)
{
	int i = startIndex;
	int j = current.Length - 1;
	
	while (i < j)
	{
		Swap(current, i, j);
		
		i += 1;
		j -= 1;
	}
}

public void Swap<T>(T[] current, int i, int j)
{
	T tmp = current[i];
	current[i] = current[j];
	current[j] = tmp;
}

public int Factorial(int n)
{
	if (n == 1)
		return 1;
	
	return n * Factorial(n - 1);
}