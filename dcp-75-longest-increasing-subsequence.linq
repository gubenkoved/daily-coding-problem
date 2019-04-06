<Query Kind="Program" />

// This problem was asked by Microsoft.
// 
// Given an array of numbers, find the length of the longest
// increasing subsequence in the array. The subsequence does
// not necessarily have to be contiguous.
// 
// For example, given the array [0, 8, 4, 12, 2, 10, 6, 14,
// 1, 9, 5, 13, 3, 11, 7, 15], the longest increasing
// subsequence has length 6: it is 0, 2, 6, 9, 11, 15.

void Main()
{
	//var a = new[] { 1, 2, 3 }; // 3 
	//var a = new[] { 3, 2, 1 }; // 1
	var a = new[] { 0, 8, 4, 12, 2, 10, 6, 14, 1, 9, 5, 13, 3, 11, 7, 15 }; // 6
	//var a = RandomArray(50); // 0.001 sec
	//var a = RandomArray(100); // 0.010 sec
	//var a = RandomArray(150); // 0.1 sec
	//var a = RandomArray(200); // 0.5 sec
	//var a = RandomArray(300); // 0.5 sec
	
	// edge cases
	//var a = Enumerable.Range(0, 1000).ToArray(); // 1000
	//var a = Enumerable.Range(0, 1000).Select(x => 1000 - x).ToArray(); // 1
	
	//LongestIncreasingSubsequenceBacktracking(a).Dump();
	LongestIncreasingSubsequenceDynamicPrograming(a).Dump();
}

int[] RandomArray(int n)
{
	Random rnd = new Random();
	
	var a = new int[n];
	
	for (int i = 0; i < n; i++)
		a[i] = rnd.Next() % 1000;
	
	return a;
}

int LongestIncreasingSubsequenceDynamicPrograming(int[] a)
{
	// I came up with backtracking not very efficient solution myself
	// and after reading some articles found out that there is also a DP
	// solution that works for O(n*n) -- as simple as that...
	
	int[] l = new int[a.Length];
	
	for (int i = a.Length - 1; i >= 0; i--)
	{
		if (i == a.Length - 1)
		{
			l[i] = 1; // edge case
		} else
		{
			int best = 1;
			for (int j = i + 1; j < a.Length; j++)
			{
				if (a[j] >= a[i] && l[j] >= best)
					best = 1 + l[j];
			}
			
			l[i] = best;
		}
	}
	
	Util.Metatext("DP: " + string.Join(", ", l)).Dump();
	
	return l.Max();
}

// NP...
int LongestIncreasingSubsequenceBacktracking(int[] a)
{
	Dictionary<int, int[]> candidatesMap = new Dictionary<int, int[]>();

	for (int i = -1; i < a.Length - 1; i++)
	{
		List<int> idx = new List<int>();
		
		for (int j = i + 1; j < a.Length; j++)
		{
			if (i == -1 || a[j] >= a[i])
				idx.Add(j);
		}
		
		candidatesMap[i] = idx.ToArray();
	}
	
	return FindMaxSubsequenceLenBacktracking(a, candidatesMap, 0, -1);
}

// return amount of items in increasing sequence
// candidatesMap[i] contains indexes of items which are more than (or equal to) element i
// and they are located to the right from i
int FindMaxSubsequenceLenBacktracking(int[] a, Dictionary<int, int[]> candidatesMap, int startIndex, int thresholdIdx)
{
	if (startIndex >= a.Length)
		return 0;
		
	// two items long sequence
	if (startIndex == a.Length - 1)
		return a[a.Length - 1] > a[thresholdIdx] ? 1 : 0;
	
	int max = 0;
	
	int currentMinimumTried = int.MaxValue;
	
	foreach (int candidateIdx in candidatesMap[thresholdIdx])
	{
		// if we already tried smaller candiadate and with smaller index
		// there is no point in trying this one out as we would have included
		// it to a longer chain starting with such element
		if (currentMinimumTried < a[candidateIdx])
			continue; // there is no point looking at these! 
		
		// okay, try to add current item to subsequence
		int sub = 1 + FindMaxSubsequenceLenBacktracking(a, candidatesMap, candidateIdx + 1, candidateIdx);

		currentMinimumTried = a[candidateIdx];

		if (sub > max)
			max = sub;
	}

	return max;
}