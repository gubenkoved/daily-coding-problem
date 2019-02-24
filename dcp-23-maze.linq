<Query Kind="Program" />

// This problem was asked by Google.
// 
// You are given an M by N matrix consisting of booleans that represents a board.
// Each True boolean represents a wall. Each False boolean represents a tile you can walk on.
// 
// Given this matrix, a start coordinate, and an end coordinate, return the
// minimum number of steps required to reach the end coordinate from the start.
// If there is no possible path, then return null. You can move up, left, down, and right.
// You cannot move through walls. You cannot wrap around the edges of the board.
// 
// For example, given the following board:
// 
// [[f, f, f, f],
// [t, t, f, t],
// [f, f, f, f],
// [f, f, f, f]]
// and start = (3, 0) (bottom left) and end = (0, 0) (top left), the minimum number
// of steps required to reach the end is 7, since we would need to go through (1, 2)
// because there is a wall everywhere else on the second row.

void Main()
{
	// we can use deikstra algorthim to find the shortest distances there
	
	int[,] walls = new [,]
	{
		{ 0, 0, 0, 0 },
		{ 1, 1, 0, 1 },
		{ 0, 0, 0, 0 },
		{ 0, 0, 0, 0 },
	};	

	int?[,] distances = Deikstra(walls, 3, 0);
	
	distances.Dump();
	
	// sample 2

	walls = new[,]
	{
		{ 0, 0, 0, 0, 0 },
		{ 1, 1, 1, 1, 0 },
		{ 0, 0, 0, 0, 0 },
		{ 1, 1, 0, 1, 1 },
		{ 0, 1, 0, 0, 0 },
	};
	
	Deikstra(walls, 0, 0).Dump();
}

int?[,] Deikstra(int[,] walls, int startX, int startY)
{
	int?[,] distances = new int?[walls.GetLength(0), walls.GetLength(1)];
	
	for (int i = 0; i < walls.GetLength(0); i++)
		for (int j = 0; j < walls.GetLength(1); j++)
			distances[i, j] = null;
			
	distances[startX, startY] = 0;
	
	bool[,] visited = new bool[walls.GetLength(0), walls.GetLength(1)];
	
	var current = new Queue<(int x, int y)>();
	
	current.Enqueue((startX, startY));
	
	while (current.Any())
	{
		var point = current.Dequeue();
		
		(int x, int y)[] neighbors = new (int x, int y)[]
		{
			(point.x - 1, point.y),
			(point.x + 1, point.y),
			(point.x, point.y - 1),
			(point.x, point.y + 1),
		};
		
		foreach (var neighbor in neighbors)
		{
			if (neighbor.x < 0 || neighbor.x >= walls.GetLength(0) || neighbor.y < 0 || neighbor.y >= walls.GetLength(1))
				continue;

			if (walls[neighbor.x, neighbor.y] != 0)
				continue;
			
			if (visited[neighbor.x, neighbor.y])
				continue;
			
			int distance = distances[point.x, point.y].Value + 1;
			
			distances[neighbor.x, neighbor.y] = Math.Min(distances[neighbor.x, neighbor.y] ?? int.MaxValue, distance);
			
			current.Enqueue(neighbor);
		}
		
		visited[point.x, point.y] = true;
	}
	
	return distances;
}
