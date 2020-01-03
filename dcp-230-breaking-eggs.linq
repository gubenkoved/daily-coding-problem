<Query Kind="Program" />

// This problem was asked by Goldman Sachs.
// 
// You are given N identical eggs and access to a building with k floors.
// Your task is to find the lowest floor that will cause an egg to break,
// if dropped from that floor. Once an egg breaks, it cannot be dropped again.
// If an egg breaks when dropped from the xth floor, you can assume it will
// also break when dropped from any floor greater than x.
// 
// Write an algorithm that finds the minimum number of trial drops it will take,
// in the worst case, to identify this floor.
// 
// For example, if N = 1 and k = 5, we will need to try dropping the egg at every
// floor, beginning with the first, until we reach the fifth floor, so our solution will be 5.

void Main()
{
	// https://leetcode.com/problems/super-egg-drop/
	
	// finally figured that it seems to be DP problem!
	// unfortunately, my DP solution times out on big floors counts
	// as the runtime is growing as floors squared...
	
	//MinDrops(1, 5).Dump("5");
	//MinDrops(1, 2).Dump("2");
	//MinDrops(1, 3).Dump("3");
	//MinDrops(2, 2).Dump("2");
	//MinDrops(2, 3).Dump("2");
	//MinDrops(2, 4).Dump("3");
	//MinDrops(2, 6).Dump("3");
	//MinDrops(3, 14).Dump("4");
	//MinDrops(4, 15).Dump("4");
	//MinDrops(10, 100).Dump("10");
	//MinDrops(2, 10000).Dump("141");
	//MinDrops(3, 10000).Dump("40");
	//MinDrops(4, 10000).Dump("23"); // 1 sec
	//MinDrops(8, 10000).Dump("14");
	//MinDrops(16, 10000).Dump("14");
	//MinDrops(10, 5000).Dump("13");
	MinDrops(10, 10000).Dump("14");
	//MinDrops(100, 10000).Dump("14"); // 44 sec
	//MinDrops(10, 1000).Dump("10");
	//MinDrops(4, 1000).Dump("13");
}

// O(eggs * floors * floors)
int MinDrops(int eggs, int floors)
{
	// note that this solution times out on leet code for test cases with big
	// amount of floors
	
	int[,] d = new int[floors + 1, eggs + 1];

	for (int f = 0; f <= floors; f++)
		d[f, 1] = f; // 1 egg N floors base case
		
	for (int e = 1; e <= eggs; e++)
		d[1, e] = 1; // N eggs 1 floor base case

	for (int e = 2; e <= eggs; e++)
	{
		for (int f = 2; f <= floors; f++)
		{
			// that's where magic is happening, we optimize given the solutions for sub prodblems
			
			int best = int.MaxValue;

			for (int dropAt = 1; dropAt < f; dropAt++)
			{
				// if we drop at some floor there are two outcomes: eggs breaks and egg does not break
				// each outcome leads to a smaller (already solved) subproblem:
				// a. if egg breaks at Kth floor it means we should solve ((k-1) floor, (egg-1) eggs) subproblem
				// b. if egg does not break then we are solving ((total - k) floor , egg eggs) subproblem
				// and the metric of the current try would be the maximum of the metrics for the subproblems + 1 (as each try costs 1 drop)
				
				int cur = 1 + Math.Max(
					d[dropAt - 1, e - 1], // egg breaks up case
					d[f - dropAt, e]); // egg survives case

				// update the best
				if (cur < best)
					best = cur;
			}
			
			d[f, e] = best;
		}
	}
	
	//d.Dump();
	
	return d[floors, eggs];
}