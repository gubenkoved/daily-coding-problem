<Query Kind="Program" />

// This problem was asked by Google.
// 
// Let A be an N by M matrix in which every row and every column is sorted.
// 
// Given i1, j1, i2, and j2, compute the number of elements of M smaller than
// M[i1, j1] and larger than M[i2, j2].
// 
// For example, given the following matrix:
// 
// [[01, 03, 07, 10, 15, 20],
//  [02, 06, 09, 14, 22, 25],
//  [03, 08, 10, 15, 25, 30],
//  [10, 11, 12, 23, 30, 35],
//  [20, 25, 30, 35, 40, 45]]
//
// And i1 = 1, j1 = 1, i2 = 3, j2 = 3, return 15 as there are 15 numbers in
// the matrix smaller than 6 or greater than 23.

void Main()
{
	// can we assume that M[i1, j1] < M[i2, j2]?!
	
	Count(
		new[,]
		{
			{ 01, 03, 07, 10, 15, 20 },
			{ 02, 06, 09, 14, 22, 25 },
			{ 03, 08, 10, 15, 25, 30 },
			{ 10, 11, 12, 23, 30, 35 },
			{ 20, 25, 30, 35, 40, 45 }
		}, 1, 1, 3, 3).Dump("15");

	Count(
		new[,]
		{
			{01, 03, 07, 10, 15, 20},
			{02, 06, 09, 14, 22, 25},
			{03, 08, 10, 15, 25, 30},
			{10, 11, 12, 23, 30, 35},
			{20, 25, 30, 35, 40, 45}
		}, 0, 0, 4, 5).Dump("0");

	Count(
		new[,]
		{
			{01, 03, 07, 10, 15, 20},
			{02, 06, 09, 14, 22, 25},
			{03, 08, 10, 15, 25, 30},
			{10, 11, 12, 23, 30, 35},
			{20, 25, 30, 35, 40, 45}
		}, 4, 5, 0, 0).Dump("30");
}

// O(n * k) where n, k are dimensions
int CountNaive(int[,] m, int i1, int j1, int i2, int j2)
{
	int c = 0;
	
	for (int i = 0; i < m.GetLength(0); i++)
		for (int j = 0; j < m.GetLength(1); j++)
			if (m[i, j] < m[i1, j1] || m[i, j] > m[i2, j2])
				c += 1;
				
	return c;
}


// O(n + k) where n, k are dimensions
int Count(int[,] m, int i1, int j1, int i2, int j2)
{
	// count items less than [i1, j1]
	
	int less = 0;
	
	int j = 0;
	
	for (int i = m.GetLength(0) - 1; i >= 0; i--)
	{
		while (j < m.GetLength(1) && m[i, j] < m[i1, j1])
			j += 1;
			
		less += j;
	}

	int bigger = 0;

	bool rangesIntersect = m[i1, j1] > m[i2, j2];

	int comparand = Math.Max(m[i2, j2], m[i1, j1]); // to avoid counting the same items twice!
	
	j = m.GetLength(1) - 1;

	for (int i = 0; i < m.GetLength(0); i++)
	{
		while (j > 0 && (m[i, j] > comparand || rangesIntersect && m[i, j] >= comparand))
			j -= 1;

		bigger += m.GetLength(1) - j - 1;
	}

	return less + bigger;
}