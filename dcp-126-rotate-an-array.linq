<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// Write a function that rotates a list by k elements.
// For example, [1, 2, 3, 4, 5, 6] rotated by two becomes
// [3, 4, 5, 6, 1, 2]. Try solving this without creating
// a copy of the list. How many swap or move operations
// do you need?

void Main()
{
	for (int n = 1; n < 100; n++)
	{
		for (int shift = 0; shift <= n; shift++)
		{
			int[] array = Enumerable.Range(1, n).ToArray();
			
			int[] expected = RotateSimple(array, shift);
			
			RotateInPlace(array, shift);

			if (!Enumerable.SequenceEqual(expected, array))
			{
				array.Dump();
				throw new Exception($"WA, shift = {shift}");
			}
		}
	}
	
	"in place works!".Dump();
}

int[] RotateSimple(int[] d, int k)
{
	int[] r = new int[d.Length];
	
	for (int i = 0; i < d.Length; i++)
		r[i] = d[(i + k) % d.Length];
		
	return r;
}

void RotateInPlace(int[] d, int k)
{
	int n = d.Length;
	
	for (int i = 0; i < n; i++)
	{
		// okay now let's find the index to swap with
		int j = i + k;
		
		while (j >= n || j < i)
			j = (j + k) % n;
		
		//Util.Metatext($"swap d[{i}] = {d[i]} and d[{j}] = {d[j]}").Dump();
		
		// swap i and j
		int tmp = d[i];
		d[i] = d[j];
		d[j] = tmp;
		
		//d.Dump();
	}
}