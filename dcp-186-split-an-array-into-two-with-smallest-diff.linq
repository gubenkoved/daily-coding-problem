<Query Kind="Program" />

// This problem was asked by Microsoft.
// 
// Given an array of positive integers, divide the array into
// two subsets such that the difference between the sum of the
// subsets is as small as possible.
// 
// For example, given [5, 10, 15, 20, 25], return the sets
// {10, 25} and {5, 15, 20}, which has a difference of 5, which
// is the smallest possible difference.

void Main()
{
	// I can only think on back-tracking serach trying to  go though all subsequences 
	// and get the subsequence sum clsoser to sum/2 than already found

	Split(new[] { 5, 10, 15, 20, 25, }).Dump();
	
	Split(new[] { 5, 10, 15, 20, 25, 5, }).Dump();
	
	// works..
	Split(new[] { 5, 2, 6, 2, 3, 6, 7, 11, 23, 1, 3, 55, 23, 12, 5, 45, 66, 234, 2, 5, 77, 34, 1, 34, 56, 21}).Dump();
	
	// does not work... already
	Split(new[] { 800, 5, 2, 6, 2, 3, 6, 7, 11, 23, 1, 3, 55, 23, 12, 5, 45, 66, 234, 2, 5, 77, 34, 1, 34, 56, 21}).Dump();
}

// O(over9000)
// I think it's O(2^n) since there are 2^n possible splits
// and in the worst case will will go through them all
int[][] Split(int[] a)
{
	var state = new State();
	Search(a, new HashSet<int>(), 0, a.Sum(), state);
	
	Util.Metatext(state.BestDiff.ToString()).Dump("best diff");

	return new[]
	{
		a.Where((v, idx) => state.Indicies.Contains(idx)).ToArray(),
		a.Where((v, idx) => !state.Indicies.Contains(idx)).ToArray(),
	};
}

class State
{
	public int BestDiff = int.MaxValue;
	public int[] Indicies = null;
	public bool Stop = false;
}

void Search(int[] a, HashSet<int> included, int currentSum, int totalSum, State state)
{
	if (state.Stop)
		return;
	
	int diff = (int)Math.Abs(totalSum - 2 * currentSum);
	
	// found an ideal split!
	if (currentSum == totalSum / 2) // almost diff == 0, but covers odd total as well
	{
		state.BestDiff = diff;
		state.Indicies = included.ToArray();
		state.Stop = true;
		return;
	}
	
	// there is no sense to continue adding, as the diff will only increase
	if (currentSum > totalSum / 2)
		return;
		
	if (diff < state.BestDiff)
	{
		state.BestDiff = diff;
		state.Indicies = included.ToArray();
	}
		
	for (int i = 0; i < a.Length; i++)
	{
		if (included.Contains(i))
			continue;
			
		// try to include it!
		included.Add(i);
		
		// dive in!
		Search(a, included, currentSum + a[i], totalSum, state);
		
		// revert
		included.Remove(i);
	}
}