<Query Kind="Program" />

// This problem was asked by Google.
// 
// Given a list of integers S and a target number k, write a function
// that returns a subset of S that adds up to k. If such a subset
// cannot be made, then return null.
// 
// Integers can appear more than once in the list. You may assume all
// numbers in the list are positive.
// 
// For example, given S = [12, 1, 61, 5, 9, 2] and k = 24, return
// [12, 9, 2, 1] since it sums up to 24.

void Main()
{
	// we can use depth first backtracking again... 
	FindSubset(new[] { 12, 1, 61, 5, 9, 2 }, 24).Dump();
	FindSubset(new[] { 1, 1, 1, 1 }, 2).Dump();
	FindSubset(new[] { 1, 1, 1, 1 }, 4).Dump();
	FindSubset(new[] { 4, 2, 5, 3 }, 15).Dump();
}

int[] FindSubset(int[] numbers, int k)
{
	var solutions = new List<int[]>();
	
	Worker(numbers, new HashSet<int>(), k, solutions);

	Util.Metatext($"Found {solutions.Count} solution(s)").Dump();
	
	solutions.ForEach(s =>
		Util.Metatext(string.Join(", ", s)).Dump());
	
	return solutions.FirstOrDefault();
}

void Worker(int[] available, HashSet<int> usedIndexes, int sum, List<int[]> solutions)
{
	if (sum == 0)
	{
		solutions.Add(usedIndexes.Select(x => available[x]).ToArray());
		return;
	}
	
	// note that since order does not matter we can always go to increasing index
	// to automatically get rid of duplicates
	
	int startIdx = 0;
	
	if (usedIndexes.Any())
		startIdx = usedIndexes.Max() + 1;
	
	for (int i = startIdx; i < available.Length; i++)
	{
		if (usedIndexes.Contains(i))
			continue;
		
		int number = available[i];

		usedIndexes.Add(i);
		
		// we need to go deeper!
		Worker(available, usedIndexes, sum - number, solutions);
		
		usedIndexes.Remove(i);
	}
}