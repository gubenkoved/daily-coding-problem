<Query Kind="Program" />

// This problem was asked by Amazon.
// 
// Given an array and a number k that's smaller than the length
// of the array, rotate the array to the right k elements in-place.

// refs:
// http://www.azillionmonkeys.com/qed/case8.html

void Main()
{
	// there is more efficient O(n) time, const space algorithm...
	// but this one is simple

	var a = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };
	
	MoveRight(a, 3);
	
	a.Dump();
}

// O(k * n)
void MoveRight(int[] a, int k)
{
	// k * O(n)
	for (int q = 0; q < k; q++)
	{
		// move one to the right
		int tmp = a[a.Length - 1];

		// O(n)
		for (int i = a.Length - 1; i >= 1; i--)
			a[i] = a[i - 1];

		a[0] = tmp;
	}
}


