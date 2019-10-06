<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// Suppose you are given two lists of n points, one list p1, p2, ..., pn
// on the line y = 0 and the other list q1, q2, ..., qn on the line y = 1.
// Imagine a set of n line segments connecting each point pi to qi. Write
// an algorithm to determine how many pairs of the line segments intersect.

void Main()
{
	Intersections(
		new[] { 1, 2, 3, 4, 5 },
		new[] { 1, 2, 3, 4, 5 }).Dump("0");

	Intersections(
		new[] { 1, 2, 3, },
		new[] { 3, 2, 1 }).Dump("3");

	Intersections(
		new[] { 3, 2, 1 },
		new[] { 1, 2, 3 }).Dump("3");

	Intersections(
		new[] { 1, 2, 3, },
		new[] { 1, 3, 2 }).Dump("1");
}

public int Intersections(int[] p, int[] q)
{
	int n = p.Length;
	
	int intersections = 0;
	
	for (int i = 1; i < n; i++)
	{
		for (int j = 0; j < i; j++)
		{
			// see if segment (p[i], q[i]) insersects with (p[j], q[j])
			
			if (!(p[i] >= p[j] && q[i] >= q[j] ||
				p[i] < p[j] && q[i] < q[j]))
			{
				intersections += 1;
			}
		}
	}
	
	return intersections;
}
