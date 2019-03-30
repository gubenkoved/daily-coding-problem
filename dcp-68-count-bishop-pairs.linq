<Query Kind="Program" />

// This problem was asked by Google.
// 
// On our special chessboard, two bishops attack each other
// if they share the same diagonal. This includes bishops
// that have another bishop located between them, i.e.
// bishops can attack through pieces.
// 
// You are given N bishops, represented as (row, column) tuples
// on a M by M chessboard. Write a function to count the number
// of pairs of bishops that attack each other. The ordering of
// the pair doesn't matter: (1, 2) is considered the same as (2, 1).
// 
// For example, given M = 5 and the list of bishops:
// 
// (0, 0)
// (1, 2)
// (2, 2)
// (4, 0)
//
// The board would look like this:
// 
// [b 0 0 0 0]
// [0 0 b 0 0]
// [0 0 b 0 0]
// [0 0 0 0 0]
// [b 0 0 0 0]
//
// You should return 2, since bishops 1 and 3 attack each other,
// as well as bishops 3 and 4.

void Main()
{
	char[,] board = new [,]
	{
		{ 'b', '0', '0', '0', '0', },
		{ '0', '0', 'b', '0', '0', },
		{ '0', '0', 'b', '0', '0', },
		{ '0', '0', '0', '0', '0', },
		{ 'b', '0', '0', '0', '0', },
	};
	
	CountBishopAttackPairs(board).Dump();
}

int CountBishopAttackPairs(char[,] board)
{
	Tuple<int, int>[] bishops = Find(board, 'b');
	
	int count = 0;
	
	for (int i = 0; i < bishops.Length - 1; i++)
	{
		for (int j = i + 1; j < bishops.Length; j++)
		{
			if (IsSameDiagonal(bishops[i].Item1, bishops[i].Item2, bishops[j].Item1, bishops[j].Item2))
				count += 1;
		}
	}
	
	return count;
}

Tuple<int, int>[] Find(char[,] board, char c)
{
	List<Tuple<int, int>> result = new List<System.Tuple<int, int>>();
	
	for (int x = 0; x < board.GetLength(0); x++)
	{
		for (int y = 0; y < board.GetLength(1); y++)
		{
			if (board[x, y] == c)
				result.Add(Tuple.Create(x, y));
		}
	}
	
	return result.ToArray();
}

bool IsSameDiagonal(int x1, int y1, int x2, int y2)
{
	return Math.Abs(x1 - x2) == Math.Abs(y1 - y2);
}