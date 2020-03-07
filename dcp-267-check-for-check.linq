<Query Kind="Program" />

// This problem was asked by Oracle.
// 
// You are presented with an 8 by 8 matrix representing the positions of pieces
// on a chess board. The only pieces on the board are the black king and various
// white pieces. Given this matrix, determine whether the king is in check.
// 
// For details on how each piece moves, see here.
// 
// For example, given the following matrix:
// 
// ...K....
// ........
// .B......
// ......P.
// .......R
// ..N.....
// ........
// .....Q..
//
// You should return True, since the bishop is attacking the king diagonally.

void Main()
{
	string board = @"
...K....
........
.B......
......P.
.......R
..N.....
........
.....Q..";

	IsCheck(Parse(board)).Dump();
}

char[,] Parse(string board)
{
	var s1 = board.Trim().Split(new[] { '\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
	
	char[][] bLines = s1.Select(x => x.ToArray()).ToArray();
	
	char[,] b = new char[8,8];
	
	for (int i = 0; i < 8; i++)
		for (int j = 0; j < 8; j++)
			b[i, j] = bLines[i][j];
			
	return b;
}

bool IsCheck(char[,] b)
{
	// find the king
	Tuple<int, int> kingPosition = default;
	
	for (int i = 0; i < 8; i++)
		for (int j = 0; j < 8; j++)
			if (b[i, j] == 'K')
				kingPosition = Tuple.Create(i, j);

	// check for check
	for (int i = 0; i < 8; i++)
	{
		for (int j = 0; j < 8; j++)
		{
			IEnumerable<Tuple<int, int>> attacked = null;
			
			switch (b[i, j])
			{
				case 'N': attacked = GetAttackingPositionsKnight(i, j); break;
				case 'P': attacked = GetAttackingPositionsPawn(i, j); break;
				case 'B': attacked = GetAttackingPositionsBishop(i, j, b); break;
				case 'R': attacked = GetAttackingPositionsRock(i, j, b); break;
				case 'Q': attacked = GetAttackingPositionsQueen(i, j, b); break;
				default: break;
			}
			
			if (attacked != null && attacked.Contains(kingPosition))
				return true;
		}
	}

	return false;
}

IEnumerable<Tuple<int, int>> GetAttackingPositionsKnight(int i, int j)
{
	return new Tuple<int, int> []
	{
		Tuple.Create(i - 2, j - 1),	
		Tuple.Create(i - 2, j + 1),
		Tuple.Create(i + 2, j - 1),
		Tuple.Create(i + 2, j + 1),
		Tuple.Create(i - 1, j - 2),
		Tuple.Create(i + 1, j - 2),
		Tuple.Create(i - 1, j + 2),
		Tuple.Create(i + 1, j + 2),
	};
}

IEnumerable<Tuple<int, int>> GetAttackingPositionsPawn(int i, int j)
{
	return new Tuple<int, int>[]
	{
		Tuple.Create(i - 1, j - 1),
		Tuple.Create(i - 1, j + 1),
	};
}

IEnumerable<Tuple<int, int>> GetAttackingPositionsRock(int i, int j, char[,] b)
{
	List<Tuple<int, int>> attacked = new List<System.Tuple<int, int>>();
	
	// up
	for (int i2 = i - 1; i2 >= 0; i2--)
	{
		attacked.Add(Tuple.Create(i2, j));
		
		if (b[i2, j] != '.')
			break;
	}

	// down
	for (int i2 = i + 1; i2 < 8; i2++)
	{
		attacked.Add(Tuple.Create(i2, j));

		if (b[i2, j] != '.')
			break;
	}

	// left
	for (int j2 = j - 1; j2 >= 0; j2--)
	{
		attacked.Add(Tuple.Create(i, j2));

		if (b[i, j2] != '.')
			break;
	}

	// right
	for (int j2 = j + 1; j2 < 8; j2++)
	{
		attacked.Add(Tuple.Create(i, j2));

		if (b[i, j2] != '.')
			break;
	}

	return attacked;
}

IEnumerable<Tuple<int, int>> GetAttackingPositionsBishop(int i, int j, char[,] b)
{
	List<Tuple<int, int>> attacked = new List<System.Tuple<int, int>>();

	// down right
	for (int d = 1; d <= 8; d++)
	{
		var p = Tuple.Create(i + d, j + d);
		
		if (!IsValid(p.Item1, p.Item2))
			break;
		
		attacked.Add(p);
		
		if (!IsEmpty(b[p.Item1, p.Item2]))
			break;
	}

	// up right
	for (int d = 1; d <= 8; d++)
	{
		var p = Tuple.Create(i - d, j + d);

		if (!IsValid(p.Item1, p.Item2))
			break;

		attacked.Add(p);

		if (!IsEmpty(b[p.Item1, p.Item2]))
			break;
	}

	// down left
	for (int d = 1; d <= 8; d++)
	{
		var p = Tuple.Create(i + d, j - d);

		if (!IsValid(p.Item1, p.Item2))
			break;

		attacked.Add(p);

		if (!IsEmpty(b[p.Item1, p.Item2]))
			break;
	}

	// up left
	for (int d = 1; d <= 8; d++)
	{
		var p = Tuple.Create(i - d, j - d);

		if (!IsValid(p.Item1, p.Item2))
			break;

		attacked.Add(p);

		if (!IsEmpty(b[p.Item1, p.Item2]))
			break;
	}

	return attacked.Where(x => IsValid(x.Item1, x.Item2)).ToArray();
}

IEnumerable<Tuple<int, int>> GetAttackingPositionsQueen(int i, int j, char[,] b)
{
	return GetAttackingPositionsBishop(i, j, b).Union(GetAttackingPositionsRock(i, j, b));
}

bool IsEmpty(char c)
{
	return c == '.';
}

bool IsValid(int i, int j)
{
	return i >= 0 && i < 8 && j >= 0 && j < 8;
}