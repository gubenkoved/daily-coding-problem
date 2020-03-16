<Query Kind="Program" />

// This problem was asked by Netflix.
// 
// Given an array of integers, determine whether it contains a Pythagorean
// triplet. Recall that a Pythogorean triplet (a, b, c) is defined by the
// equation a^2+ b^2= c^2

void Main()
{
	HasPythogorianTriplet(new[] { 3, 4, 5 }).Dump();
	HasPythogorianTriplet(new[] { 3, 4, 6 }).Dump();
	HasPythogorianTriplet(new[] { 30, 40, 50 }).Dump();
	HasPythogorianTriplet(new[] { 1, 2, 3, 4, 5, 6 }).Dump();
	HasPythogorianTriplet(new[] { 1, 1, 1, 1, 1, 1 }).Dump();
}

// O(n^2) time, O(n) space
bool HasPythogorianTriplet(int[] a)
{
	HashSet<int> squares = new HashSet<int>();
	
	foreach (int x in a)
		squares.Add(x * x);
		
	for (int i = 0; i < a.Length; i++)
	{
		for (int j = 0; j < a.Length; j++)
		{
			if (i == j)
				continue;
				
			int target = a[i] * a[i] + a[j] * a[j];

			if (squares.Contains(target))
				return true;
		}
	}
	
	return false;
}