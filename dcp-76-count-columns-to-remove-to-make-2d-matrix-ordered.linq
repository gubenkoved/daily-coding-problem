<Query Kind="Program" />

// This problem was asked by Google.
// 
// You are given an N by M 2D matrix of lowercase letters.
// Determine the minimum number of columns that can be
// removed to ensure that each row is ordered from top to
// bottom lexicographically. That is, the letter at each
// column is lexicographically later as you go down each
// row. It does not matter whether each row itself is
// ordered lexicographically.
// 
// For example, given the following table:
// 
// cba
// daf
// ghi
//
// This is not ordered because of the a in the center. We
// can remove the second column to make it ordered:
// 
// ca
// df
// gi
//
// So your function should return 1, since we only needed
// to remove 1 column.
// 
// As another example, given the following table:
// 
// abcdef
//
// Your function should return 0, since the rows are
// already ordered (there's only one row).
// 
// As another example, given the following table:
// 
// zyx
// wvu
// tsr

void Main()
{
	// not sure why this problem is marked [medium] is that
	// can be solved with simplest greedy algorithm...
	
	char[,] a = new char[,]
	{
		{ 'c', 'b', 'a', },
		{ 'd', 'a', 'f', },
		{ 'g', 'h', 'i', },
	};
	
	a = new char[,]
	{
		{ 'a', 'b', 'c', 'd', 'e', 'f' },
	};
	
	CountColumnsToRemoveToGetLexicographicOrderingInRows(a).Dump();
}

int CountColumnsToRemoveToGetLexicographicOrderingInRows(char[,] a)
{
	int count = 0;
	
	for (int col = 0; col < a.GetLength(1); col++)
	{	
		bool unordered = false;
		
		for (int row = 1; row < a.GetLength(0); row++)
			if (a[row, col] < a[row - 1, col])
				unordered = true;
				
		if (unordered)
			count += 1;
	}
	
	return count;
}