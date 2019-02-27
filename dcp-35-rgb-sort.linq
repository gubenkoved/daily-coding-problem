<Query Kind="Program" />

// This problem was asked by Google.
// 
// Given an array of strictly the characters 'R', 'G', and 'B',
// segregate the values of the array so that all the Rs come first,
// the Gs come second, and the Bs come last. You can only swap elements of the array.
// 
// Do this in linear time and in-place.
// 
// For example, given the array ['G', 'B', 'R', 'R', 'B', 'R', 'G'],
// it should become ['R', 'R', 'R', 'G', 'G', 'B', 'B'].

void Main()
{
	var data = new [] { 'R', 'G', 'B', 'R', 'R', 'B', 'R', 'G' };
	
	RgbSort(data);
	
	data.Dump();
}

// O(n) time, O(1) space
void RgbSort(char[] a)
{
	// at each pass with start with two pointers
	// one is at the begining and another is at the end
	// the right one is looking for some specific char
	// and as soon as found we do swap and moving left pointer
	// 1 cell to the right and right one to the left
	// process repeaed 3 times for each letter
	// by the way -- we only need single "left" pointer mooving for all chars

	char[] dic = new char[] { 'R', 'G', 'B' };
	
	int l = 0;
	
	foreach (char target in dic)
	{
		Util.Metatext($"Align {target} chars...").Dump();
		
		int r = a.Length - 1;
		
		while (r > l && l <= a.Length - 1)
		{
			// move l pointer along while it's target cahar
			while (l <= a.Length - 1 && a[l] == target)
				l += 1;
			
			if (a[r] == target && l <= a.Length - 1)
			{
				Util.Metatext($"\tSwap {a[l]} at {l} with {a[r]} at {r}").Dump();
				
				// swap
				var tmp = a[l];
				a[l] = a[r];
				a[r] = tmp;
				
				Util.Metatext($"\t[{string.Join(", ", a)}]").Dump();
				
				// move l pointer along while it's target cahar
				while (l <= a.Length - 1 && a[l] == target)
					l += 1;
			}
			
			r -= 1;
		}
	}
}