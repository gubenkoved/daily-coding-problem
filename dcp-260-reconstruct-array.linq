<Query Kind="Program" />

// This problem was asked by Pinterest.
// 
// The sequence [0, 1, ..., N] has been jumbled, and the only clue you have
// for its order is an array representing whether each number is larger or smaller than the last.
// Given this information, reconstruct an array that is consistent with it. For example,
// given [None, +, +, -, +], you could return [1, 2, 3, 0, 4].

void Main()
{
	// we can try brute force it with backtracking search for instance...

	Reconstruct(new char[] { (char)0, '+', '+', '-', '+' }).Dump();
	Reconstruct(new char[] { (char)0, '+', '+', '+', '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+' }).Dump();
	Reconstruct(new char[] { (char)0, '+', '+', '+', '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+', '+', '+', '+', '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+' }).Dump();
}

int[] Reconstruct(char[] a)
{
	var available = new HashSet<int>(Enumerable.Range(0, a.Length));

	var result = new int[a.Length];

	bool found = Reconstruct(a, 0, available, result);
	
	if (!found)
		return null;
		
	return result;
}

bool Reconstruct(char[] a, int idx, HashSet<int> available, int[] result)
{
	if (idx == result.Length)
		return true;
	
	// search with constraint
	foreach (int x in available.ToArray())
	{
		if (idx > 0)
		{
			if (a[idx] == '+' && x < result[idx - 1])
				continue;

			if (a[idx] == '-' && x > result[idx - 1])
				continue;
		}

		available.Remove(x);

		result[idx] = x;

		bool found = Reconstruct(a, idx + 1, available, result);

		if (found)
			return true;

		available.Add(x);
	}

	// not found!
	return false;
}