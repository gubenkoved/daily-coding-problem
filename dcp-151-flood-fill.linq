<Query Kind="Program" />

// Given a 2 - D matrix representing an image, a location of a pixel
// in the screen and a color C, replace the color of the given pixel
// and all adjacent same colored pixels with C.
// 
//   For example, given the following matrix, and location pixel of
// (2, 2), and 'G' for green:
// 
// 
//   B B W
//   W W W
//   W W W
//   B B B
//   Becomes
//   
// 
//   B B G
//   G G G
//   G G G
//   B B B
//   

void Main()
{
	var pic = new [,]
	{
		{ 'B', 'B', 'W', },
		{ 'W', 'W', 'W', },
		{ 'W', 'W', 'W', },
		{ 'B', 'B', 'B', },
	};
	
	pic.Dump();
	
	Recolor(pic, 'G', 2, 2);
	
	pic.Dump();
}

public void Recolor(char[,] picture, char target, int x, int y)
{
	RecolorWorker(picture, picture[x, y], target, x, y);
}

public void RecolorWorker(char[,] picture, char source, char target, int cx, int cy)
{
	int w = picture.GetLength(0);
	int h = picture.GetLength(1);

	bool[,] traversed = new bool[w,h];
	
	Queue<Tuple<int, int>> queue = new Queue<System.Tuple<int, int>>();
	
	queue.Enqueue(Tuple.Create(cx, cy));

	while (queue.Count > 0)
	{
		var curPoint = queue.Dequeue();
		
		int x = curPoint.Item1;
		int y = curPoint.Item2;
		
		Util.Metatext($"Handling ({x}, {y})").Dump();
		
		picture[x, y] = target;
		traversed[x, y] = true;

		var adjacent = new[] { (x - 1, y), (x + 1, y), (x, y - 1), (x, y + 1) };

		foreach (var point in adjacent)
		{
			int px = point.Item1;
			int py = point.Item2;

			if (px < 0 || px >= w || py < 0 || py >= h)
				continue;

			if (traversed[px, py])
				continue;

			if (picture[px, py] != source)
				continue;
				
			queue.Enqueue(Tuple.Create(px, py));
		}
	}
}
