<Query Kind="Program" />

// This question was asked by Zillow.
// 
// You are given a 2-d matrix where each cell represents
// number of coins in that cell. Assuming we start at matrix[0][0],
// and can only move right or down, find the maximum number of coins
// you can collect by the bottom right corner.
// 
// For example, in this matrix
// 
// 0 3 1 1
// 2 0 0 4
// 1 5 3 1
//
// The most we can collect is 0 + 2 + 1 + 5 + 3 + 1 = 12 coins.

void Main()
{
	MaxCoins(new [,] 
	{
		{ 0, 3, 1, 1, },
		{ 2, 0, 0, 4, },
		{ 1, 5, 3, 1, },
	}).Dump("12");

	MaxCoins(new[,]
	{
		{ 2, 0, 0, 5, },
		{ 2, 0, 0, 5, },
		{ 2, 2, 2, 2, },
	}).Dump("14");
}

int MaxCoins(int[,] d)
{
	// that seems to be a classic dynamic programming problem
	// let's maintain rect array t of the same size,
	// such that t[i, j] is max possible when we get from (0,0) to (i,j)
	
	int k = d.GetLength(0);
	int n = d.GetLength(1);
	
	int[,] t = new int[k,n];
	
	// main loop
	for (int i = 0; i < k; i++)
	{
		for (int j = 0; j < n; j++)
		{
			if (i == 0 && j == 0)
				t[i, j] = d[i, j];
			else if (i == 0)
				t[i, j] = d[i, j] + t[i, j - 1];
			else if (j == 0)
				t[i, j] = d[i, j] + t[i - 1, j];
			else
				t[i, j] = d[i, j] + Math.Max(t[i - 1, j], t[i, j - 1]);
		}
	}
	
	//t.Dump();
	
	return t[k - 1, n - 1];
}
