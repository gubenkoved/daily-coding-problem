<Query Kind="Program" />

// This problem was asked by Bloomberg.
// 
// There are N prisoners standing in a circle, waiting to be executed.
// The executions are carried out starting with the kth person, and
// removing every successive kth person going clockwise until there is no one left.
// 
// Given N and k, write an algorithm to determine where a prisoner should
// stand in order to be the last survivor.
// 
// For example, if N = 5 and k = 2, the order of executions would be
// [2, 4, 1, 5, 3], so you should return 3.
// 
// Bonus: Find an O(log N) solution if k = 2.

void Main()
{
	LastSurvivor(5, 1).Dump("5");
	LastSurvivor(5, 2).Dump("3");
	LastSurvivor(5, 5).Dump("2");
}

// O(n*n)
int LastSurvivor(int n, int k)
{
	List<int> alive = new List<int>(Enumerable.Range(1, n));
	
	for (int idx = k - 1; alive.Count > 1; )
	{
		idx = idx % alive.Count;
		
		//$"{alive[idx]}".Dump("killed");
		
		alive.RemoveAt(idx); // O(n)
		
		idx += k - 1; // -1 because we removed 1 element already and it shifts others
	}
	
	return alive.Single();
}
