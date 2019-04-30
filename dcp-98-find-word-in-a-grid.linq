<Query Kind="Program" />

// This problem was asked by Coursera.
// 
// Given a 2D board of characters and a word, find if the word
// exists in the grid.
// 
// The word can be constructed from letters of sequentially
// adjacent cell, where "adjacent" cells are those horizontally
// or vertically neighboring. The same letter cell may not be
// used more than once.
// 
// For example, given the following board:
// 
// [
//   ['A','B','C','E'],
//   ['S','F','C','S'],
//   ['A','D','E','E']
// ]
// exists(board, "ABCCED") returns true, exists(board, "SEE")
// returns true, exists(board, "ABCB") returns false.

void Main()
{
	char[,] m = new char[,]
	{
		{ 'A', 'B', 'C', 'E' },
		{ 'S', 'F', 'C', 'S' },
		{ 'A', 'D', 'E', 'E' },
	};
	
	Exists(m, "ABCCED").Dump("true");
	Exists(m, "SEE").Dump("true");
	Exists(m, "ABCB").Dump("false");
}

bool Exists(char[,] m, string word)
{
	// first letter pass
	int rows = m.GetLength(0);
	int cols = m.GetLength(1);
	
	for (int i = 0; i < rows; i++)
		for (int j = 0; j < cols; j++)
			if (m[i, j] == word[0] && Exists(m, new bool[rows, cols], word, 0, i, j))
				return true;
	
	return false;
}

bool Exists(char[,] m, bool[,] visited, string word, int index, int i, int j)
{
	// classic depth first backtracking search
	
	if (m[i, j] != word[index])
		throw new Exception(); // should not happen

	// if it is the last one, stop
	if (index == word.Length - 1)
		return true; // found the last char!

	// mark current as visited
	visited[i, j] = true;

	char next = word[index + 1];
	
	int[][] adjacentCells = new int[][]
	{
		new [] { i - 1, j },
		new [] { i + 1, j },
		new [] { i, j - 1 },
		new [] { i, j + 1 },
	};
	
	foreach (int[] adjacent in adjacentCells)
	{
		if (adjacent[0] < 0 || adjacent[0] >= m.GetLength(0)
			|| adjacent[1] < 0 || adjacent[1] >= m.GetLength(1))
			continue;
			
		if (m[adjacent[0], adjacent[1]] != next)
			continue;
			
		if (visited[adjacent[0], adjacent[1]])
			continue; // visited already... skip
			
		// okay try to go deeper!
		if (Exists(m, visited, word, index + 1, adjacent[0], adjacent[1]))
			return true;
	}
	
	// undo visited, release for other paths
	visited[i, j] = false;
	
	// not found
	return false;
}