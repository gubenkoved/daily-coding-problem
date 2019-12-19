<Query Kind="Program" />

// This problem was asked by Salesforce.
// 
// Connect 4 is a game where opponents take turns dropping red or black discs
// into a 7 x 6 vertically suspended grid. The game ends either when one player
// creates a line of four consecutive discs of their color (horizontally, vertically,
// or diagonally), or when there are no more spots left in the grid.
// 
// Design and implement Connect 4.

void Main()
{
	var game = new ConnectN();
	
	game.Turn(0, 0);
	game.Turn(1, 0);
	
	game.Turn(0, 1);
	game.Turn(1, 1);
	
	game.Turn(0, 2);
	game.Turn(1, 2);
	
	game.Turn(0, 3);
	
	// game is over!
	game.IsGameOver.Dump("game over?");

	// new game!
	game = new ConnectN();

	game.Turn(0, 0);
	game.Turn(1, 0);

	game.Turn(1, 1);
	game.Turn(0, 1);

	game.Turn(2, 2);
	game.Turn(1, 2);

	game.Turn(3, 3);

	// game is over!
	game.IsGameOver.Dump("game over?");

	// new game!
	game = new ConnectN();

	game.Turn(0, 3);
	game.Turn(1, 0);

	game.Turn(1, 2);
	game.Turn(0, 1);

	game.Turn(2, 1);
	game.Turn(1, 3);

	game.Turn(3, 0);

	// game is over!
	game.IsGameOver.Dump("game over?");
}

public class ConnectN
{
	private int _n;
	private int?[,] _field;
	private int _player;
	
	private bool _isGameOver;
	private int _winner;
	
	public bool IsGameOver
	{
		get { return _isGameOver; }
	}
	
	public ConnectN()
		: this(rows: 7, columns: 6, n: 4)
	{
	}
	
	public ConnectN(int rows, int columns, int n)
	{
		_field = new int?[rows, columns];
		_n = n;
		_player = 1;
	}
	
	public void Turn(int row, int col)
	{
		// place the mark if valid, check for game over
		
		if (_isGameOver)
			throw new Exception("The game is already over!");
			
		if (_field[row, col] != null)
			throw new Exception("This cell is already taken!");
			
		// place the mark!
		_field[row, col] = _player;

		// check for game over!
		_isGameOver = IsGameOverImpl(_player);
		
		if (_isGameOver)
		{
			Util.Metatext($"Player {_player} wins!").Dump();
			_winner = _player;
		}
		
		// change the player
		NextPlayer();
	}
	
	private void NextPlayer()
	{
		_player = _player == 1 ? 2 : 1;
	}
	
	private bool IsGameOverImpl(int player)
	{
		int rows = _field.GetLength(0);
		int cols = _field.GetLength(1);
		
		for (int startRow = 0; startRow < rows - _n + 1; startRow++)
		{
			for (int startCol = 0; startCol < cols - _n + 1; startCol++)
			{
				// check horizontal starting there

				bool win = CheckHorizontal(startRow, startCol, player)
					|| CheckVertical(startRow, startCol, player)
					|| CheckDiagonalType1(startRow, startCol, player)
					|| CheckDiagonalType2(startRow, startCol, player);
					
				if (win)
					return win;
			}
		}
				
		return false;
	}
	
	// -
	private bool CheckHorizontal(int startRow, int startCol, int player)
	{
		for (int diff = 0; diff < _n; diff++)
			if (_field[startRow, startCol + diff] != _player)
				return false;
		
		return true;
	}

	// |
	private bool CheckVertical(int startRow, int startCol, int player)
	{
		for (int diff = 0; diff < _n; diff++)
			if (_field[startRow + diff, startCol] != _player)
				return false;

		return true;
	}

	// \
	private bool CheckDiagonalType1(int startRow, int startCol, int player)
	{
		for (int diff = 0; diff < _n; diff++)
			if (_field[startRow + diff, startCol + diff] != _player)
				return false;

		return true;
	}

	// /
	private bool CheckDiagonalType2(int startRow, int startCol, int player)
	{
		for (int diff = 0; diff < _n; diff++)
			if (_field[startRow + diff, startCol + _n - diff - 1] != _player)
				return false;

		return true;
	}
}
