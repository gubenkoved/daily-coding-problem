<Query Kind="Program" />

// This problem was asked by Google.
// 
// You are in an infinite 2D grid where you can move in any of
// the 8 directions:
// 
//  (x,y) to
//     (x+1, y),
//     (x-1, y),
//     (x, y+1),
//     (x, y-1),
//     (x-1, y-1),
//     (x+1,y+1),
//     (x-1,y+1),
//     (x+1,y-1)
// 
// You are given a sequence of points and the order in which you
// need to cover the points. Give the minimum number of steps in
// which you can achieve it. You start from the first point.
// 
// Example:
// 
// Input: [(0, 0), (1, 1), (1, 2)]
// Output: 2
// It takes 1 step to move from (0, 0) to (1, 1). It takes one
// more step to move from (1, 1) to (1, 2).

void Main()
{
	PathLen(new[] { (0, 0), (1, 1), (1, 2) }).Dump("2");
	PathLen(new[] { (0, 0), (0, 1), (0, 3) }).Dump("3");
	PathLen(new[] { (0, 0), (2, 1), (0, 0) }).Dump("4");
}

int PathLen((int x, int y)[] p)
{
	int len = 0;
	
	for (int i = 1; i < p.Length; i++)
	{
		var prev = p[i - 1];
		var cur = p[i];
		
		// make it easier, take by absolute values
		int dx = Math.Abs(cur.x - prev.x); 
		int dy = Math.Abs(cur.y - prev.y);
		
		// diagonal component
		int d = Math.Min(dx, dy);
		
		len +=
			+ d
			+ (dx - d)
			+ (dy - d);
	}
	
	return len;
}