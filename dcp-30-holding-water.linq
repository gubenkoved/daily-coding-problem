<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// You are given an array of non-negative integers that represents a two-dimensional
// elevation map where each element is unit-width wall and the integer is the height.
// Suppose it will rain and all spots between two walls get filled up.
// 
// Compute how many units of water remain trapped on the map in O(N) time and O(1) space.
// 
// For example, given the input [2, 1, 2], we can hold 1 unit of water in the middle.
// 
// Given the input [3, 0, 1, 3, 0, 5], we can hold 3 units in the first index,
// 2 in the second, and 3 in the fourth index (we cannot hold 5 since it would run off
// to the left), so we can trap 8 units of water.

static bool annotations = false;

void Main()
{
	// there is an easy O(n)/O(n) time/space solution
	// where we compute an array of running max from the right
	// and then running through the list computing the max from the left
	// and find min of the maxes from the left and right to figure out
	// the max amount of water given cell can hold
	// but how to get to O(1) space as requested???

	Func<int[], int> solver = Calculate;

	solver(new[] { 3, 0, 1, 3, 0, 5 }).Dump("8");
	solver(new[] { 3, 0, 3, 0, 5 }).Dump("6");
	solver(new[] { 2, 1, 2 }).Dump("1");
	solver(new[] { 0, 1, 2, 0, 4, 2 }).Dump("2");
	solver(new[] { 4, 0, 3, 0, 4 }).Dump("9");
	solver(new[] { 0, 1, 2, 3, 2, 1, 0 }).Dump("0");
	solver(new[] { 1, 0, 2, 0, 3, 0, 4, 0, 3 }).Dump("9");
	
	Verify(solver);
}

void Verify(Func<int[], int> solver)
{
	for (int i = 0; i < 1000; i++)
	{
		var heights = Generate(100);
		
		int solution = solver(heights);
		int rightSolution = CalculateNaive(heights);
		
		if (solution != rightSolution)
			throw new Exception($"Found mismatch for the following input: [{string.Join(", ", heights)}]");
	}
	
	$"Solution has been verified on random data! Works!".Dump();
}

int[] Generate(int n)
{
	Random rnd = new Random();
	
	int[] heights = new int[n];
	
	for (int i = 0; i < n; i++)
		heights[i] = rnd.Next() % 100;
		
	return heights;
}

// O(n)/O(n)
int CalculateNaive(int[] heights)
{
	int n = heights.Length;
	int[] rightToLeftMax = new int[n];

	rightToLeftMax[n - 1] = heights[n - 1];
	
	for (int i = n - 2; i >= 0; i--)
		rightToLeftMax[i] = Math.Max(rightToLeftMax[i + 1], heights[i]);
		
	int runningLeftToRight = 0;
	
	int water = 0;
	
	for (int i = 0; i < n; i++)
	{
		runningLeftToRight = Math.Max(runningLeftToRight, heights[i]);
		
		int canHold = Math.Min(runningLeftToRight, rightToLeftMax[i]);
		
		water += Math.Max(0, canHold - heights[i]);
	}
	
	return water;
}

// O(n)/O(1)
int Calculate(int[] heights)
{
	// start with full rectangle formed by left right and max hieght element
	// then make a pass from left subtracting all water that could not be hold
	// do the same for the right-to-left direction
	
	int max = heights.Max();
	int n = heights.Length;
	int water = max * n - heights.Sum();
	
	// left to right pass
	int runningMax = 0;
	
	if (annotations) Util.Metatext($"Left to right").Dump();
	
	for (int i = 0; i < n; i++)
	{
		runningMax = Math.Max(runningMax, heights[i]);
		
		int leaked = max - runningMax;
		
		water -= leaked;

		if (annotations) Util.Metatext($"\tidx {i}, leak: {leaked}").Dump();
	}

	// right to lest pass
	runningMax = 0;
	
	if (annotations) Util.Metatext($"Right to left").Dump();
	
	for (int i = n - 1; i >= 0; i--)
	{
		runningMax = Math.Max(runningMax, heights[i]);

		int leaked = max - runningMax;

		water -= leaked;
		
		if (annotations) Util.Metatext($"\tidx {i}, leak: {leaked}").Dump();
	}
	
	return water;
}

int CalcRinat(int[] walls)
{
	if (walls.Length < 3)
		return 0;

	int totalV = 0;
	int curV = 0;
	int localMaxH = walls[0];
	int localMaxIndex = 0;

	for (int i = 1; i < walls.Length; i++)
	{
		int curH = walls[i];

		if (curH >= localMaxH)
		{
			totalV += curV;

			curV = 0;
			localMaxH = curH;
			localMaxIndex = i;
		}
		else
		{
			if (i == walls.Length - 1)
			{
				curV = 0;
				localMaxH = curH;

				break;
			}

			curV += localMaxH - curH;
		}
	}

	for (int i = walls.Length - 2; i >= localMaxIndex; i--)
	{
		int curH = walls[i];

		if (curH >= localMaxH)
		{
			totalV += curV;

			curV = 0;
			localMaxH = curH;
		}
		else
		{
			curV += localMaxH - curH;
		}
	}

	return totalV;
}