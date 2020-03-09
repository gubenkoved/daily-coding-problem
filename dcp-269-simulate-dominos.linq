<Query Kind="Program" />

// This problem was asked by Microsoft.
// 
// You are given an string representing the initial conditions of some dominoes.
// Each element can take one of three values:
// 
// L, meaning the domino has just been pushed to the left,
// R, meaning the domino has just been pushed to the right, or
// ., meaning the domino is standing still.
//
// Determine the orientation of each tile when the dominoes stop falling. Note
// that if a domino receives a force from the left and right side simultaneously,
// it will remain upright.
// 
// For example, given the string .L.R....L, you should return LL.RRRLLL.
// 
// Given the string ..R...L.L, you should return ..RR.LLLL.

void Main()
{
	Simulate(".L.R....L").Dump();
	Simulate("..R...L.L").Dump();
	Simulate("R........").Dump();
	Simulate("R.......L").Dump();
	Simulate("R......LL").Dump();
	Simulate("R.L.R.L.R").Dump();
	Simulate("L.R.L.R.L").Dump();
	Simulate("..LR..LR.").Dump();
}

// seems to be O(n logn) in a worst case
string Simulate(string init)
{
	char[] a = init.ToArray();
	
	while(true)
	{
		// find falling to the left ones
		HashSet<int> l = new HashSet<int>();
		HashSet<int> r = new HashSet<int>();
		
		for (int i = 0; i < a.Length; i++)
		{
			if (a[i] == 'L')
			{
				if (i > 0 && a[i - 1] == '.')
					l.Add(i - 1);
			} else if (a[i] == 'R')
			{
				if (i < a.Length - 1 && a[i + 1] == '.')
					r.Add(i + 1);
			}
		}

		bool noChange = true;

		// something has happened
		foreach (var lIdx in l)
		{
			if (!r.Contains(lIdx))
			{
				a[lIdx] = 'L';
				noChange = false;
			}
		}

		foreach (var rIdx in r)
		{
			if (!l.Contains(rIdx))
			{
				a[rIdx] = 'R';
				noChange = false;
			}
		}
		
		if (noChange)
			break;
	}

	return new string(a);
}
