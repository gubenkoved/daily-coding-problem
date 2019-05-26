<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// Given a multiset of integers, return whether it
// can be partitioned into two subsets whose sums are the same.
// 
// For example, given the multiset { 15, 5, 20, 10, 35, 15, 10 },
// it would return true, since we can split it up into { 15, 5, 10, 15, 10 }
// and { 20, 35 }, which both add up to 55.

void Main()
{
	// basically the goal is to sind the subset with the sum = sum of array / 2

	CanSplitInTwo(new[] { 10, 10 }).Dump();
	CanSplitInTwo(new[] { 15, 5, 20, 10, 35, 15, 10 }).Dump();
	CanSplitInTwo(new[] { 15, 5, 20, 10, 35, 15, 12 }).Dump();
}

bool CanSplitInTwo(int[] numbers)
{
	int sum = numbers.Sum();
	
	if (sum % 2 == 1)
		return false;
	
	Dictionary<int, int> counts = new Dictionary<int, int>();

	foreach (var num in numbers)
	{
		if (!counts.ContainsKey(num))
			counts[num] = 0;
			
		counts[num] += 1;
	}
	
	return SubsetWithSum(counts, sum / 2);
}

// numbers is map: number -> amount
bool SubsetWithSum(Dictionary<int, int> numbers, int k)
{
	if (k < 0)
		return false;
	
	// backtracking depth first search
	foreach (int number in numbers.Keys.ToArray())
	{
		if (numbers[number] == 0)
			continue; // no numbers of this value left
			
		// okay we got a number
		
		if (number == k)
			return true; // found it!
		
		numbers[number] -= 1;
		
		// take a number to try
		if (SubsetWithSum(numbers, k - number))
			return true;
			
		// return number back
		numbers[number] += 1;
	}
	
	// should have successed at this point if there is a solution
	return false;
}