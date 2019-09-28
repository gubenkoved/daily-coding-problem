<Query Kind="Program" />

// This problem was asked by Google.
// 
// Given two rectangles on a 2D graph, return the area of their intersection.
// If the rectangles don't intersect, return 0.
// 
// For example, given the following rectangles:
// 
// {
//     "top_left": (1, 4),
//     "dimensions": (3, 3) # width, height
// }
// and
// 
// {
//     "top_left": (0, 5),
//     "dimensions": (4, 3) # width, height
// }
// return 6.



void Main()
{
	IntersectionArea(
		new Rect() { X = 1, Y = 4, W = 3, H = 3 },
		new Rect() { X = 0, Y = 5, W = 4, H = 3 }).Dump("6");
}

class Rect
{
	public double X { get; set; }
	public double Y { get; set; }
	public double W { get; set; }
	public double H { get; set; }
}

double IntersectionArea(Rect a, Rect b)
{
	return IntersectionLen(a.X, a.X + a.W, b.X, b.X + b.W)
		* IntersectionLen(a.Y, a.Y + a.H, b.Y, b.Y + b.H);
}

double IntersectionLen(double x1, double x2, double y1, double y2)
{
	double r1 = Math.Max(x1, y1);
	double r2 = Math.Min(x2, y2);
	
	if (r1 >= r2)
		return 0;
		
	return r2 - r1;
}
