<Query Kind="Program" />

// This problem was asked by Slack.
// 
// You are given an N by M matrix of 0s and 1s. Starting from the top left corner, how many ways are there to reach the bottom right corner?
// 
// You can only move right and down. 0 represents an empty space while 1 represents a wall you cannot walk through.
// 
// For example, given the following matrix:
// 
// [[0, 0, 1],
//  [0, 0, 1],
//  [1, 0, 0]]
// 
// Return two, as there are only two ways to get to the bottom right:
// 
// Right, down, down, right
// Down, right, down, right
// The top left corner and bottom right corner will always be 0.

void Main()
{
	CountWays(new [,]
	{
		{ 0, 0, 1 },
		{ 0, 0, 1 },
		{ 1, 0, 0 },
	}).Dump("2");

	CountWays(new[,]
	{
		{ 0, 0, 1 },
		{ 1, 0, 1 },
		{ 1, 0, 0 },
	}).Dump("1");

	CountWays(new[,]
	{
		{ 0, 0, 0 },
		{ 0, 0, 0 },
		{ 0, 0, 0 },
	}).Dump("6");
}

public int CountWays(int[,] m)
{
	int[,] counts = new int[m.GetLength(0),m.GetLength(1)];
	
	for (int i = 0; i < m.GetLength(0); i++)
	{
		for (int j = 0; j < m.GetLength(1); j++)
		{
			if (m[i, j] == 1)
				continue; // wall!
			
			if (i == 0 && j == 0)
			{
				counts[i, j] = 1;
				continue;
			}
			
			int current = 0;
			
			if (i >= 1)
				current += counts[i - 1, j];
				
			if (j >= 1)
				current += counts[i, j - 1];
				
			counts[i, j] = current;
		}
	}
	
	//counts.Dump();
	
	return counts[m.GetLength(0) - 1, m.GetLength(1) - 1];
}
