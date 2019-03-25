<Query Kind="Program" />

// This problem was asked by Microsoft.
// 
// Given a 2D matrix of characters and a target word, write a
// function that returns whether the word can be found in the
// matrix by going left-to-right, or up-to-down.
// 
// For example, given the following matrix:
// 
// [['F', 'A', 'C', 'I'],
//  ['O', 'B', 'Q', 'P'],
//  ['A', 'N', 'O', 'B'],
//  ['M', 'A', 'S', 'S']]
// 
// and the target word 'FOAM', you should return true,
// since it's the leftmost column. Similarly, given the
// target word 'MASS', you should return true, since it's the last row.

void Main()
{
	char[][] matrix = new char[][]
	{
		new [] { 'F', 'A', 'C', 'I' },
		new [] { 'O', 'B', 'Q', 'P' },
		new [] { 'A', 'N', 'O', 'B' },
		new [] { 'M', 'A', 'S', 'S' },
	};
	
	FindWord(matrix, "FOAM").Dump();
	FindWord(matrix, "MASS").Dump();
	FindWord(matrix, "HELL").Dump();
}

bool FindWord(char[][] matrix, string word)
{
	// go left to right
	for (int row = 0; row < matrix.Length; row++)
	{
		if (new string(matrix[row]) == word)
			return true;
	}
	
	// go up to down
	for (int col = 0; col < matrix[0].Length; col++)
	{
		char[] colChars = Enumerable.Range(0, matrix.Length).Select(row => matrix[row][col]).ToArray();
		
		if (new string(colChars) == word)
			return true;
	}
	
	return false;
}
