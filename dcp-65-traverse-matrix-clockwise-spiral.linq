<Query Kind="Program" />

// This problem was asked by Amazon.
// 
// Given a N by M matrix of numbers, print out the matrix in a clockwise spiral.
// 
// For example, given the following matrix:
// 
// [[1,  2,  3,  4,  5],
//  [6,  7,  8,  9,  10],
//  [11, 12, 13, 14, 15],
//  [16, 17, 18, 19, 20]]
//
// You should print out the following:
// 
// 1
// 2
// 3
// 4
// 5
// 10
// 15
// 20
// 19
// 18
// 17
// 16
// 11
// 6
// 7
// 8
// 9
// 14
// 13
// 12


void Main()
{
	TraverseClockwiseSpiral(new[,]
	{
		{ 01, 02, 03, 04, 05 },
		{ 06, 07, 08, 09, 10 },
		{ 11, 12, 13, 14, 15 },
		{ 16, 17, 18, 19, 20 },
	}).Dump();
}

public IEnumerable<int> TraverseClockwiseSpiral(int[,] matrix)
{
	// it's hard to come up with coordinates analyticly
	// so let's just traverse and will turn when we hit
	
	int rows = matrix.GetLength(0);
	int columns = matrix.GetLength(1);
	
	int traversed = 0;
	
	// directions: 0 = right, 1 = down, 2 = left, 3 = up
	int direction = 0;
	
	int x = 0;
	int y = 0;
	
	int wallY0 = 0;
	int wallY1 = rows - 1;
	int wallX0 = 0;
	int wallX1 = columns - 1;
	
	do 
	{
		yield return matrix[y, x];
		
		bool hitWall;

		if (direction == 0) // right
		{
			x += 1;
			hitWall = x == wallX1;
			
			if (hitWall)
				wallY0 += 1;
		}
		else if (direction == 1) // down
		{
			y += 1;
			hitWall = y == wallY1;
			
			if (hitWall)
				wallX1 -= 1;
		}
		else if (direction == 2) // left
		{
			x -= 1;
			hitWall = x == wallX0;
			
			if (hitWall)
				wallY1 -= 1;
		}
		else //if (direction == 3) // up
		{
			y -= 1;
			
			hitWall = y == wallY0;
			
			if (hitWall)
				wallX0 += 1;
		}

		if (hitWall)
			direction = (direction + 1) % 4;
		
		traversed += 1;
	} while (traversed < rows * columns);
}
