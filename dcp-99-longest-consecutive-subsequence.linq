<Query Kind="Program" />

// This problem was asked by Microsoft.
// 
// Given an unsorted array of integers, find the length
// of the longest consecutive elements sequence.
// 
// For example, given [100, 4, 200, 1, 3, 2], the longest
// consecutive element sequence is [1, 2, 3, 4]. Return
// its length: 4.
// 
// Your algorithm should run in O(n) complexity.

void Main()
{
	// note that numbers subsequence can start at any number
	// not only from 1 as the examples go
	
	LongestSequence(new[] { 0 }).Dump("1");
	LongestSequence(new[] { 100, 4, 200, 1, 3, 2 }).Dump("4");
	LongestSequence(new[] { 1, 2, 3, 4, }).Dump("4");
	LongestSequence(new[] { 4, 3, 2, 1 }).Dump("4");
	LongestSequence(new[] { 1000, 1, 2, 100, 101, 1002, 1001 }).Dump("3");
	LongestSequence(new[] { 5, 1, 4, 2, 3 }).Dump("5");
	LongestSequence(new[] { 1, 2, 4, 5, 3 }).Dump("5");
	LongestSequence(new[] { 2, 1, 4, 5, 3 }).Dump("5");
}

int LongestSequenceStarts1(int[] a)
{
	bool[] t = new bool[a.Length];

	foreach (var x in a)
		if (x <= a.Length && x >= 1)
			t[x - 1] = true; // 1 based
	
	int index = 0;
	
	while (index < a.Length && t[index])
		index += 1;
	
	return index;
}

int LongestSequenceAlmost(int[] a)
{
	Dictionary<int, int> map = new Dictionary<int, int>();
	
	int max = 0;
	
	foreach (var x in a)
	{
		if (map.ContainsKey(x))
			continue;
			
		map[x] = 1;
		
		if (map.ContainsKey(x - 1))
			map[x] += map[x - 1];
		
		if (map.ContainsKey(x + 1))
			map[x] += map[x + 1];
			
		if (map[x] > max)
			max = map[x];
	}

	map.Dump();
	
	return max;
}

// O(n) since we will not process the same number twice
// (algouht it's is possible to encointer it twice, which is not a problem)
int LongestSequence(int[] a)
{
	// number -> visited flag
	Dictionary<int, bool> map = new Dictionary<int, bool>();
	
	// provide a fast way (O(1)) to check if any specific number exists
	foreach (var x in a) // O(n)
		map[x] = false; // not visited
		
	int max = 0;
	
	// calculation pass
	foreach (var x in a) // O(n)
	{
		if (map[x])
			continue;
		
		map[x] = true; // mark current as visited
		
		int counter = 1; // current
		
		int cur = x;
		
		// grab all the numbers in increasing order
		while (map.ContainsKey(cur + 1))
		{
			map[cur + 1] = true;
			counter += 1;
			cur += 1;
		}

		cur = x;

		// grab all the numbers in decreasing order
		while (map.ContainsKey(cur - 1))
		{
			map[cur - 1] = true;
			counter += 1;
			cur -= 1;
		}
		
		// now see if we got a bigger group than the current
		if (counter > max)
			max = counter;
	}

	return max;
}
