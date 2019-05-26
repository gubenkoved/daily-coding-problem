<Query Kind="Program" />

// This problem was asked by Microsoft.
// 
// You have an N by N board. Write a function that, given N,
// returns the number of possible arrangements of the board
// where N queens can be placed on the board without threatening
// each other, i.e. no two queens share the same row, column, or diagonal.

void Main()
{
	for (int n = 1; n <= 12; n++)
	{
		Count(n).Dump(n.ToString());
	}
}

public struct Point
{
	public int X { get; set; }
	public int Y { get; set; }
	
	public Point(int x, int y)
	{
		X = x;
		Y = y;
	}
}

int Count(int n)
{
	var solutions = new List<Point[]>();
	
	Place(n, new List<Point>(), solutions);
	
	return solutions.Count();
}

void Place(int n, List<Point> placed, List<Point[]> solutions)
{
	// the job before recursion step is to find all possible placements
	// for next queen and then place to each place and then recusion step happens

	if (placed.Count == n)
	{
		solutions.Add(placed.ToArray());
		return;
	}
	
	for (int x = 0; x < n; x++)
	{
		// note that queens are the same, so we always can place 1st queen at first row, 2nd at second
		// and be sure that we do not miss anything
		int y = placed.Count;
		
		//for (int y = 0; y < n; y++)
		{
			bool canPlace = !placed.Any(p => p.X == x || p.Y == y || Math.Abs(x - p.X) == Math.Abs(y - p.Y));
			
			if (canPlace)
			{
				placed.Add(new Point(x, y));
				
				Place(n, placed, solutions);
				
				placed.RemoveAt(placed.Count - 1);
			}
		}
	}
}