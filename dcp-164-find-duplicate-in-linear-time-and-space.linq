<Query Kind="Program" />

// This problem was asked by Google.
// 
// You are given an array of length n + 1 whose elements belong to the set {1, 2, ..., n}.
//
// By the pigeonhole principle, there must be a duplicate. Find it in linear time and space.

void Main()
{
	Dupe(new[] { 1, 2, 2, 3, 4  }).Dump("2");
	Dupe(new[] { 4, 1, 2, 3, 4  }).Dump("4");
	Dupe(new[] { 0, 1, 2, 3, 4  }).Dump("null");
	
	// googled another very intresting O(1) space solution:
	// find an excess of the sum over expected sum -- and that's it :)
}

// linear time, linear space
int? Dupe(int[] a)
{
	int n = a.Length - 1;
	
	bool[] exist = new bool[n];
	
	foreach (int x in a)
	{
		if (x <= 0 || x > n)
			continue; // throw new InvalidOperationException("invalid number: " + item.ToString());
		
		if (exist[x - 1])
			return x;
		
		exist[x - 1] = true;
	}
	
	return null;
}
