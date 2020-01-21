<Query Kind="Program" />

// This problem was asked by Spotify.
// 
// There are N couples sitting in a row of length 2 * N. They are
// currently ordered randomly, but would like to rearrange themselves
// so that each couple's partners can sit side by side.
// 
// What is the minimum number of swaps necessary for this to happen?

void Main()
{
	// https://leetcode.com/problems/couples-holding-hands/description/
	// https://gubenkoved.tinytake.com/tt/NDAwOTkxNV8xMjMzNTQ0OA
	
	// wow. this solution worked by the first submission and it beats 100% of other solutions
	// by both time and memory. did not expect that at all
	// sometimes, the complexity labels can be missleading

	MinSwaps(new[] { 0, 2, 1, 3 }).Dump(1);
	MinSwaps(new[] { 3, 2, 0, 1 }).Dump(0);
	MinSwaps(new[] { 0, 2, 4, 1, 3, 5 }).Dump(2);
}

int MinSwaps(int[] a)
{
	// map from 
	Dictionary<int, int> idxMap = new Dictionary<int, int>(); // number to index map

	// preparation pass
	for (int i = 0; i < a.Length; i += 1)
		idxMap[a[i]] = i;
	
	int swaps = 0;
	
	// working pass
	for (int i = 0; i < a.Length; i += 2)
	{
		int x = a[i];
		int y = a[i + 1];

		if (IsPair(x, y))
			continue;
			
		// if we got there -- not a pair on this cushion
		
		// let's say that we are leaving the first one and swapping the second one to make up a pair
		int pairForX = PairFor(x);
		int pairForXIdx = idxMap[pairForX];
		
		// swap (i+1) and (pairForXIdx)
		Swap(a, i + 1, pairForXIdx);
		
		// update aux structure
		idxMap[pairForX] = i + 1;
		idxMap[y] = pairForXIdx;
		
		swaps += 1;
	}
	
	//Util.Metatext("[" + string.Join(", ", a) + "]").Dump("result");
	
	return swaps;
}

void Swap(int[] a, int idx1, int idx2)
{
	int tmp = a[idx1];
	a[idx1] = a[idx2];
	a[idx2] = tmp;
}

int PairFor(int x)
{
	if (x % 2 == 0)
		return x + 1;
	else
		return x - 1;
}

bool IsPair(int x, int y)
{
	return PairFor(x) == y;
}