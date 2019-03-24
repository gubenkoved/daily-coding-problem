<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// There is an N by M matrix of zeroes. Given N and M,
// write a function to count the number of ways of starting
// at the top-left corner and getting to the bottom-right
// corner. You can only move right or down.
// 
// For example, given a 2 by 2 matrix, you should return 2,
// since there are two ways to get to the bottom-right:
// 
// Right, then down
// Down, then right
// Given a 5 by 5 matrix, there are 70 ways to get to the
// bottom-right.

void Main()
{
	// that's classic dynamic programming puzzle
	// we can start with matrix with 1 in the top left corner
	// and then calculate all the cells as "left cell" + "up cell"
	
	Count(2, 2).Dump();
	Count(3, 3).Dump();
	Count(4, 4).Dump();
	Count(5, 5).Dump();
	Count(4, 7).Dump();
}

int Count(int n, int m)
{
	int[,] a = new int[n, m];
	
	a[0, 0] = 1;
	
	for (int i = 0; i < n; i++)
	{
		for (int j = 0; j < m; j++)
		{
			if (j > 0)
				a[i, j] += a[i, j - 1]; // add from left
				
			if (i > 0)
				a[i, j] += a[i - 1, j]; // add from up
		}
	}
	
	return a[n - 1, m - 1];
}

