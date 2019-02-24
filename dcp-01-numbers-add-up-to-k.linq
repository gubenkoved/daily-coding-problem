<Query Kind="Program" />

// This problem was recently asked by Google.
// 
// Given a list of numbers and a number k, return whether any two numbers from the list add up to k.
// 
// For example, given [10, 15, 3, 7] and k of 17, return true since 10 + 7 is 17.
// 
// Bonus: Can you do this in one pass?

void Main()
{
	AnyTwoNumbersAddUpTo(new[] { 10, 15, 3, 7 }, 17);
	AnyTwoNumbersAddUpTo(new[] { 10, 15, 3, 7 }, 20);
	AnyTwoNumbersAddUpTo(new[] { 5, 5, 3, 7 }, 10);
}

// O(n)
bool AnyTwoNumbersAddUpTo(int[] a, int k)
{
	$"Solving for [{string.Join(", ", a)}] with k = {k}".Dump();
	
	HashSet<int> seen = new HashSet<int>();

	// O(n) * O(1) -> O(n)
	for (int i = 0; i < a.Length; i++)
	{
		// O(1)
		if (seen.Contains(k - a[i]))
		{
			Util.Metatext($"{k - a[i]} + {a[i]} = {k}").Dump();
			return true;
		}
		
		// O(1)
		seen.Add(a[i]);
	}
	
	Util.Metatext("nope...").Dump();
	
	return false;
}