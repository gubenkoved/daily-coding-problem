<Query Kind="Program" />

// This problem was asked by Twitter.
// 
// A permutation can be specified by an array P, where P[i] represents the
// location of the element at i in the permutation. For example, [2, 1, 0]
// represents the permutation where elements at the index 0 and 2 are swapped.
// 
// Given an array and a permutation, apply the permutation to the array. For
// example, given the array ["a", "b", "c"] and the permutation [2, 1, 0], return ["c", "b", "a"].

void Main()
{
	// since there is no requirement about using additional memory

	ApplyPermutation2(new[] { "a", "b", "c" }, new[] { 2, 1, 0 }).Dump();
}

T[] ApplyPermutation2<T>(T[] items, int[] order)
{
	ApplyPermutation(items, order);
	
	return items;
}

void ApplyPermutation<T>(T[] items, int[] order)
{
	int[] curOrder = order.ToArray(); // so that we do not modify order array
	
	for (int i = 0; i < items.Length; i++)
	{
		// find i at orders
		int j = Array.IndexOf(curOrder, i);
		
		Swap(items, i, j);
		Swap(curOrder, i, j);
	}
}

void Swap<T>(T[] a, int i, int j)
{
	T tmp = a[i];
	a[i] = a[j];
	a[j] = tmp;
}

