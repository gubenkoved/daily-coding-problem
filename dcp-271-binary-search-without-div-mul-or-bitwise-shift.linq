<Query Kind="Program" />

// This problem was asked by Netflix.
// 
// Given a sorted list of integers of length N, determine if an element x is
// in the list without performing any multiplication, division, or bit-shift operations.
// 
// Do this in O(log N) time.

void Main()
{
	// basically what we need to do is binary search, but with the given constraints
	// normally we would need to pick the index between two boundries and then branch
	// either left or right and normally it requires a division:
	// (l + r) / 2
	// so the question is basically: "how to split a range in two parts w/o division, multiplication
	// or bitwise shift in a constant time?" given this answer we just build binary search on top

	Contains(new[] { 1, 3, }, 1).Dump();
	Contains(new[] { 1, 3, }, 3).Dump();
	Contains(new[] { 1, 3, }, 2).Dump();
	Contains(new[] { 1, 2, 5, 6, 23, 25, 34, 55, 100, 234, 655  }, 34).Dump();
	Contains(new[] { 1, 2, 5, 6, 23, 25, 34, 55, 100, 234, 655  }, 30).Dump();
}

// we will build a table that consists of power of two and corresponding division results
// up to the target value
// then we will use this precalculation to divide source array

bool Contains(int[] sorted, int a)
{
	// log(n)
	Dictionary<int, int> divTable = DivTable(sorted.Length);
	
	// power of two of current increment we will use to split the array in peices
	// in order to pick next increment we will be just dividing it in two
	// as soon as the range 
	int step = divTable.Max(x => x.Key);
	
	int l = 0;
	int r = sorted.Length - 1;
	
	while (r - l > 1)
	{
		Util.Metatext($"range [{l}; {r}], step {step}").Dump();

		// see if we should swtich to a smaller step
		if (step >= r - l)
		{
			step = divTable[step];
			Util.Metatext($"  shrink step to {step}").Dump();
		}
		
		int m = l + step;
		
		Util.Metatext($"  picked a mid element with idx {m}").Dump();

		if (a < sorted[m])
		{
			Util.Metatext($"  branch left").Dump();
			r = m;
		}
		else if (a > sorted[m])
		{
			Util.Metatext($"  branch right").Dump();
			l = m;
		}
		else
		{
			Util.Metatext($"  found it!").Dump();
			return true; // found it!
		}
	}
	
	// range contains two elements if we got there, and we can not subdivide further
	// so simply check both
	
	bool found = sorted[l] == a || sorted[r] == a;
	
	return found;
}

// builds a -> a/2 table up to some number
// it only does it for the powers of two
// 2 -> 1
// 4 -> 2
// 8 -> 4
// 16 -> 8
// etc.
Dictionary<int, int> DivTable(int upTo)
{
	var divMap = new Dictionary<int, int>();

	// we would need to have at least 1 element in the division table

	divMap[2] = 1;

	int x = 1;
	int x2 = 2;
	
	while (x2 <= upTo) 
	{
		divMap[x2] = x;
		
		// multiply by two both
		x = x + x;
		x2 = x2 + x2;
	} 
	
	return divMap;
}
