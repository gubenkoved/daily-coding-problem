<Query Kind="Program" />

// This problem was asked by LinkedIn.
// 
// Given a list of points, a central point, and an integer k, find
// the nearest k points from the central point.
// 
// For example, given the list of points[(0, 0), (5, 4), (3, 1)],
// the central point (1, 2), and k = 2, return [(0, 0), (3, 1)].

void Main()
{
	NearestK(new[] { (0, 0), (5, 4), (3, 1) }, (1, 2), 2).Select(p => $"({p.x}, {p.y})").Dump();
}

(int x, int y)[] NearestK((int x, int y)[] points, (int x, int y) center, int k)
{
	return points.OrderBy(p => Dist(p, center)).Take(k).ToArray();
}

public double Dist((int x, int y) p1, (int x, int y) p2)
{
	return Math.Sqrt((p1.x - p2.x) * (p1.x - p2.x) + (p1.y - p2.y) * (p1.y - p2.y));
}