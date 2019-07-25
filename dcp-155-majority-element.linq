<Query Kind="Program" />

// This problem was asked by MongoDB.
// 
// Given a list of elements, find the majority element, which appears
// more than half the time (> floor(len(lst) / 2.0)).
// 
// You can assume that such element exists.
// 
// For example, given [1, 2, 1, 1, 3, 4, 0], return 1.

void Main()
{
	// worth noting: majority element, if it exists (which by definition does)
	// is the same as the median... so we can find a median O(n) time, const space...

	MajorityElementSimple(new int[] { 3, 3, 3, 3, 3, 3, 1, 1, 1, 2, 2 }).Dump("3");

	MajorityElementSimple(new[] { 1, 2, 1, 1, 3, 4, 0 }).Dump("1");
}

// O(n) time, O(n) space
int MajorityElementSimple(int[] a)
{
	Dictionary<int, int> freq = new Dictionary<int, int>();
	
	int highestFreq = 0;
	int highestFreqElement = a[0];
	
	for (int i = 0; i < a.Length; i++)
	{
		if (!freq.ContainsKey(a[i]))
			freq[a[i]] = 0;
			
		freq[a[i]] += 1;
		
		if (freq[a[i]] > highestFreq)
		{
			highestFreq = freq[a[i]];
			highestFreqElement = a[i];
		}
	}
	
	return highestFreqElement;
}