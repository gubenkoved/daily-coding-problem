<Query Kind="Program" />

// This question was asked by Google.
// 
// Given an N by M matrix consisting only of 1's and 0's, find the
// largest rectangle containing only 1's and return its area.
// 
// For example, given the following matrix:
// 
// [[1, 0, 0, 0],
//  [1, 0, 1, 1],
//  [1, 0, 1, 1],
//  [0, 1, 0, 0]]
//
// Return 4.

void Main()
{
	MaxRect(new[,]
	{
		{ 1, },
	}).Dump("1");

	MaxRect(new[,]
	{
		{ 1, 1, },
		{ 1, 1, },
	}).Dump("4");

	MaxRect(new[,]
	{
		{ 1, 1, 1, },
		{ 1, 1, 1, },
		{ 1, 1, 1, },
	}).Dump("9");

	MaxRect(new[,]
	{
		{ 1, 0, 0, 0 },
		{ 1, 0, 1, 1 },
		{ 1, 0, 1, 1 },
		{ 0, 1, 0, 0 },
	}).Dump("4");

	MaxRect(new[,]
	{
		{ 1, 1, 0, 0 },
		{ 1, 1, 0, 1 },
		{ 1, 0, 0, 1 },
		{ 1, 0, 0, 0 },
		{ 1, 0, 0, 0 },
	}).Dump("5");
}

// O(n*m) if I'm not mistaken given the optimization
// in the worst case we go via each cell only twice
int MaxRect(int[,] d)
{
	int max = 0;
	
	// start at every (x, y) with 1
	for (int i = 0; i < d.GetLength(0); i++)
	{
		for (int j = 0; j < d.GetLength(1); j++)
		{
			// optimization -- there is no need to check cells
			// which have '1' from left or from the top side as 
			// these are already covered
			if (i > 0 && d[i - 1, j] == 1 || j > 0 && d[i, j - 1] == 1)
				continue;
			
			for (int w = 1; w <= d.GetLength(0) - i; w++)
			{
				for (int h = 1; h <= d.GetLength(1) - j; h++)
				{
					if (IsFilledWithOnes(d, i, j, w, h))
					{
						if (w * h > max)
						{
							Util.Metatext($"new best rect found at ({i}, {j}) with size {w}x{h}").Dump();
							max = w * h;
						}
					} else
					{
						break; // no point continuing this cycle as it is already is not filled with all '1'
					}
				}
			}
		}
	}
	
	return max;
}

bool IsFilledWithOnes(int[,] d, int i, int j, int w, int h)
{
	bool result = true;
	
	for (int ii = i; ii < i + w; ii++)
	{
		for (int jj = j; jj < j + h; jj++)
		{
			if (d[ii, jj] != 1)
				return false;
		}
	}
	
	return true;
}