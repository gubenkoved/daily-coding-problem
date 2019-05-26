<Query Kind="Program" />

// This problem was asked by Dropbox.
// 
// Sudoku is a puzzle where you're given a partially-filled
// 9 by 9 grid with digits. The objective is to fill the
// grid with the constraint that every row, column, and box
// (3 by 3 subgrid) must contain all of the digits from 1 to 9.
// 
// Implement an efficient sudoku solver.

void Main()
{
	int[,] field = new int[,]
	{
		{ 0, 0, 0, 5, 0, 7, 0, 0, 0 },
		{ 0, 0, 2, 4, 0, 6, 3, 0, 0 },
		{ 0, 9, 0, 0, 1, 0, 0, 2, 0 },
		{ 2, 7, 0, 0, 0, 0, 0, 6, 8 },
		{ 0, 0, 3, 0, 0, 0, 1, 0, 0 },
		{ 1, 4, 0, 0, 0, 0, 0, 9, 3 },
		{ 0, 6, 0, 0, 4, 0, 0, 5, 0 },
		{ 0, 0, 9, 2, 0, 5, 6, 0, 0 },
		{ 0, 0, 0, 9, 0, 3, 0, 0, 0 },
	};
	
	// solved
	int[,] field2 = new int[,]
	{
		{ 1, 2, 3, 6, 7, 8, 9, 4, 5 },
		{ 5, 8, 4, 2, 3, 9, 7, 6, 1 },
		{ 9, 6, 7, 1, 4, 5, 3, 2, 8 },
		{ 3, 7, 2, 4, 6, 1, 5, 8, 9 },
		{ 6, 9, 1, 5, 8, 3, 2, 7, 4 },
		{ 4, 5, 8, 7, 9, 2, 6, 1, 3 },
		{ 8, 3, 6, 9, 2, 4, 1, 5, 7 },
		{ 2, 1, 9, 8, 5, 7, 4, 3, 6 },
		{ 7, 4, 5, 3, 1, 6, 8, 9, 2 },
	};

	int[,] field3 = new int[,]
	{
		{ 0, 2, 3, 0, 0, 0, 0, 0, 5 },
		{ 5, 0, 0, 0, 0, 9, 7, 0, 1 },
		{ 0, 6, 0, 1, 0, 0, 0, 2, 8 },
		{ 0, 0, 0, 0, 0, 0, 0, 0, 9 },
		{ 0, 9, 1, 0, 0, 0, 2, 0, 4 },
		{ 4, 0, 8, 0, 0, 0, 0, 0, 3 },
		{ 8, 0, 0, 9, 0, 0, 0, 0, 7 },
		{ 2, 0, 0, 0, 0, 7, 0, 3, 6 },
		{ 0, 4, 0, 0, 1, 0, 0, 0, 0 },
	};

	// The World's Hardest Sudoku
	int[,] field4 = new int[,]
	{
		{ 8, 0, 0, 0, 0, 0, 0, 0, 0 },
		{ 0, 0, 3, 6, 0, 0, 0, 0, 0 },
		{ 0, 7, 0, 0, 9, 0, 2, 0, 0 },
		{ 0, 5, 0, 0, 0, 7, 0, 0, 0 },
		{ 0, 0, 0, 0, 4, 5, 7, 0, 0 },
		{ 0, 0, 0, 1, 0, 0, 0, 3, 0 },
		{ 0, 0, 1, 0, 0, 0, 0, 6, 8 },
		{ 0, 0, 8, 5, 0, 0, 0, 1, 0 },
		{ 0, 9, 0, 0, 0, 0, 4, 0, 0 },
	};

	int[,] field5 = new int[,]
	{
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
		{ 0, 0, 0, 0, 0, 0, 0, 0, 8 },
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
	};

	var toSolve = field5;

	Solve(toSolve).Dump("solved?");
	
	toSolve.Dump();
}

bool Solve(int[,] field)
{
	// classic backtracking depth-first search..
	// find each candiate digit and then recoursively
	// solve for other cells until we stuck
	
	for (int col = 0; col < 9; col++)
	{
		for (int row = 0; row < 9; row++)
		{
			if (field[row, col] > 0)
				continue; // already in place
			
			// okay if we got there we found not yet filled place
			
			bool[] used = new bool[10];
			
			// check column
			for (int k = 0; k < 9; k++)
				used[field[k, col]] = true;

			// check row
			for (int k = 0; k < 9; k++)
				used[field[row, k]] = true;
			
			int blockOffsetRow = row % 3;
			int blockOffsetCol = col % 3;
			
			int blockRow = row - blockOffsetRow;
			int blockCol = col - blockOffsetCol;
			
			// check block
			for (int k = 0; k < 9; k++)
				used[field[blockRow + k / 3, blockCol + (k % 3)]] = true;
				
			// okay now we have the digits which are not yet used, check them all!
			
			for (int digit = 1; digit <= 9; digit++)
			{
				if (!used[digit])
				{
					int cur = field[row, col];
					field[row, col] = digit;

					//Util.Metatext($"Trying {digit} at ({row}, {col})").Dump();
					
					if (Solve(field))
						return true;
					
					// not solved -- backtrack...
					field[row, col] = cur;
				}
			}
			
			// if we got there we were unable to solve sub-sudoku
			return false;
		}
	}
	
	return true;
}