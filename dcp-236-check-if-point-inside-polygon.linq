<Query Kind="Program" />

// This problem was asked by Nvidia.
// 
// You are given a list of N points (x1, y1), (x2, y2), ..., (xN, yN)
// representing a polygon. You can assume these points are given in
// order; that is, you can construct the polygon by connecting point
// 1 to point 2, point 2 to point 3, and so on, finally looping around
// to connect point N to point 1.
// 
// Determine if a new point p lies inside this polygon. (If p is on the
// boundary of the polygon, you should return False).

void Main()
{
	// I recall the smartass way to figure that out -- draw a line into the infinity
	// and see how much times it crosses the polygon
	// if it's even amount of times, then it's outside
	
	Intersection(
		new Point(1, 1), new Point(5, 2),
		new Point(2, 3), new Point(3, 1)).Dump();

	Intersection(
		new Point(1, 1), new Point(5, 2),
		new Point(2, 3), new Point(8, 1)).Dump("(5,2)");

	Intersection(
		new Point(1, 1), new Point(5, 2),
		new Point(2, 3), new Point(10, 1)).Dump("none");

	Intersection(
		new Point(1, 1), new Point(15, 14),
		new Point(4, 2), new Point(5, 1)).Dump("none");

	var poly = new[] { new Point(1, 1), new Point(2, 4), new Point(5, 3), new Point(4, 2), new Point(5, 1) };
	
	IsInsidePoly(poly, new Point(1, 1)).Dump("false -- edge");
	IsInsidePoly(poly, new Point(2, 2)).Dump("inside");
	IsInsidePoly(poly, new Point(3, 3)).Dump("inside");
	IsInsidePoly(poly, new Point(3, 2)).Dump("inside");
	IsInsidePoly(poly, new Point(2, 3)).Dump("inside");
	IsInsidePoly(poly, new Point(4, 3)).Dump("inside");
	IsInsidePoly(poly, new Point(4.5, 3)).Dump("inside");
	IsInsidePoly(poly, new Point(4.5, 2)).Dump("outside");
	IsInsidePoly(poly, new Point(1.5, 3)).Dump("outside");
	IsInsidePoly(poly, new Point(10, 10)).Dump("outside");
}


bool IsInsidePoly(IEnumerable<Point> poly, Point p)
{
	Point[] points = poly.ToArray();
	
	Point farAway = new Point(poly.Max(x => x.X) + 10, poly.Max(x => x.Y) + 10);
	
	int intersectionCount = 0;
	
	for (int i = 0; i < points.Length; i++)
	{
		Point a = points[i];
		Point b = points[(i + 1) % points.Length]; // mod to handle the last segment case
		
		Point? intersection = Intersection(p, farAway, a, b);

		if (intersection != null)
		{
			Util.Metatext($"({a}, {b}) segement crosses ({p}, {farAway}) at {intersection}").Dump();
			intersectionCount += 1;
		}
	}
	
	return intersectionCount % 2 == 1;
}

Point? Intersection(Point a1, Point a2, Point b1, Point b2)
{
	// convert each line into the form: y = q * x + c
	// then solve for (x,y) a system: y = q1 * x + c1, y = q2 * x + c2
	
	// first line
	double q1 = (a2.Y - a1.Y) / (a2.X - a1.X);
	double c1 = a1.Y - q1 * a1.X;

	// second line
	double q2 = (b2.Y - b1.Y) / (b2.X - b1.X);
	double c2 = b1.Y - q2 * b1.X;
	
	// now, solve for intersection point
	double x = (c2 - c1) / (q1 - q2);
	double y = q1 * x + c1;
	
	// check if the point is within the line boundries
	if (x >= Math.Min(a1.X, a2.X)
		&& x <= Math.Max(a1.X, a2.X)
		&& y >= Math.Min(a1.Y, a2.Y)
		&& y <= Math.Max(a1.Y, a2.Y)
		
		// line two case
		&& x >= Math.Min(b1.X, b2.X)
		&& x <= Math.Max(b1.X, b2.X)
		&& y >= Math.Min(b1.Y, b2.Y)
		&& y <= Math.Max(b1.Y, b2.Y))
	{
		return new Point(x, y);
	}

	return null;
}

public struct Point
{
	public double X { get; }
	public double Y { get; }
	
	public Point(double x, double y)
	{
		X = x;
		Y = y;
	}

	public override string ToString()
	{
		return $"({X:F2}, {Y:F2})";
	}
}
