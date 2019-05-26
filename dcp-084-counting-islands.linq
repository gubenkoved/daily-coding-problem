<Query Kind="Program" />

// This problem was asked by Amazon.
// 
// Given a matrix of 1s and 0s, return the number of "islands"
// in the matrix. A 1 represents land and 0 represents water,
// so an island is a group of 1s that are neighboring whose
// perimeter is surrounded by water.
// 
// For example, this matrix has 4 islands.
// 
// 1 0 0 0 0
// 0 0 1 1 0
// 0 1 1 0 0
// 0 0 0 0 0
// 1 1 0 0 1
// 1 1 0 0 1

void Main()
{
	int[,] matrix = new int[,]
	{
		{ 1, 0, 0, 0, 0, },
		{ 0, 0, 1, 1, 0, },
		{ 0, 1, 1, 0, 0, },
		{ 0, 0, 0, 0, 0, },
		{ 1, 1, 0, 0, 1, },
		{ 1, 1, 0, 0, 1, },
	};
	
	Count(matrix).Dump("4");
}

int Count(int[,] matrix)
{
	int label = 0;
	
	int[,] labels = new int[matrix.GetLength(0),matrix.GetLength(1)];
	
	for (int i = 0; i < matrix.GetLength(0); i++)
	{
		for (int j = 0; j < matrix.GetLength(1); j++)
		{
			if (matrix[i, j] == 1 && labels[i, j] == 0)
			{
				Traverse(matrix, labels, j, i, ++label);
			}
		}
	}
	
	labels.Dump();
	
	return label;
}

void Traverse(int[,] matrix, int[,] temp, int x, int y, int label)
{
	var adjacacents = new int[][]
	{
		new [] { x - 1, y },
		new [] { x + 1, y },
		new [] { x, y - 1 },
		new [] { x, y + 1 },
	};
	
	// mark cell with the label
	temp[y, x] = label;
	
	foreach (int[] adjacent in adjacacents)
	{
		int ax = adjacent[0];
		int ay = adjacent[1];
		
		if (!(ax >= 0 && ax < matrix.GetLength(1) // x (col)
			&& ay >= 0 && ay < matrix.GetLength(0))) // y (row)
		{
			continue; // invalid!
		}
		
		if (matrix[ay, ax] == 1 && temp[ay, ax] == 0)
			Traverse(matrix, temp, ax, ay, label);
	}
}
