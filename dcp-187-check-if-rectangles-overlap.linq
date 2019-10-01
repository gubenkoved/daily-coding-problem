<Query Kind="Program" />

// This problem was asked by Google.
// 
// You are given given a list of rectangles represented by min and
// max x- and y-coordinates. Compute whether or not a pair of rectangles
// overlap each other. If one rectangle completely covers another, it is
// considered overlapping.
// 
// For example, given the following rectangles:
// 
// {
//     "top_left": (1, 4),
//     "dimensions": (3, 3) # width, height
// },
// {
//     "top_left": (-1, 3),
//     "dimensions": (2, 1)
// },
// {
//     "top_left": (0, 5),
//     "dimensions": (4, 3)
// }
//
// return true as the first and third rectangle overlap each other.

void Main()
{
	IsOverlapping(new[]
	{
		new Rect(1, 4, 3, 3),
		new Rect(-1, 3, 2, 1),
		new Rect(0, 5, 4, 3),
	}).Dump("true");

	IsOverlapping(new[]
	{
		new Rect(0, 0, 1, 1),
		new Rect(1, 1, 1, 1),
		new Rect(2, 2, 1, 1),
	}).Dump("false");

	IsOverlapping(new[]
	{
		new Rect(0, 0, 1, 1),
		new Rect(1, 1, 1, 1),
		new Rect(2, 2, 1, 1),
		new Rect(-10, -10, 20, 20),
	}).Dump("true");
}

public class Rect
{
	public double X { get; set; }
	public double Y { get; set; }
	public double W { get; set; }
	public double H { get; set; }
	
	public Rect(double x, double y, double w, double h)
	{
		X = x;
		Y = y;
		W = w;
		H = h;
	}
}

bool IsOverlapping(IEnumerable<Rect> rectangles)
{
	var a = rectangles.ToArray();
	
	for (int i = 0; i < a.Length; i++)
	{
		for (int j = 0; j < a.Length; j++)
		{
			if (i == j)
				continue;


			if (IsOverlapping(a[i], a[j]))
			{
				Util.Metatext($"{i+1}th overlaps {j+1}th").Dump();
				return true;
			}
			
		}
	}
	
	return false;
}

bool IsOverlapping(Rect a, Rect b)
{
	return OverlapLen(a.X, a.X + a.W, b.X, b.X + b.W) > 0
		&& OverlapLen(a.Y, a.Y + a.H, b.Y, b.Y + b.H) > 0;
}

double OverlapLen(double x1, double x2, double y1, double y2)
{
	double start = Math.Max(x1, y1);
	double end = Math.Min(x2, y2);
	
	if (end - start < 0)
		return 0;
		
	return end - start;
}