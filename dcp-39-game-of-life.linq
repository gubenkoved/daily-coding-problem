<Query Kind="Program" />

// This problem was asked by Dropbox.
// 
// Conway's Game of Life takes place on an infinite two-dimensional board
// of square cells. Each cell is either dead or alive, and at each tick,
// the following rules apply:
// 
// Any live cell with less than two live neighbours dies.
// Any live cell with two or three live neighbours remains living.
// Any live cell with more than three live neighbours dies.
// Any dead cell with exactly three live neighbours becomes a live cell.
// A cell neighbours another cell if it is horizontally, vertically,
// or diagonally adjacent.
// 
// Implement Conway's Game of Life. It should be able to be initialized
// with a starting list of live cell coordinates and the number of steps
// it should run for. Once initialized, it should print out the board state
// at each step. Since it's an infinite board, print out only the relevant
// coordinates, i.e. from the top-leftmost live cell to bottom-rightmost live cell.
// 
// You can represent a live cell with an asterisk (*) and a dead cell with a dot (.).

void Main()
{
	var game = new GameOfLife(new []
	{
		new Point(0, 0),
		new Point(1, 0),
		new Point(0, 1),
	});
	
	// glider
	game = new GameOfLife(new []
	{
		new Point(1, 0),
		new Point(2, 1),
		new Point(2, 2),
		new Point(1, 2),
		new Point(0, 2),
	});

//	game = new GameOfLife(new[]
//	{
//		new Point(0, 0),
//		new Point(0, 1),
//	});

//	game = new GameOfLife(new[]
//	{
//		new Point(0, 0),
//		new Point(0, 1),
//		new Point(0, 2),
//	});

	for (int steps = 0; steps < 10; steps++)
	{
		game.Print();
		game.Step();
	}
}

public struct Point
{
	public int X { get; set; }
	public int Y { get; set; }
	
	public Point(int x, int y)
	{
		X = x;
		Y = y;
	}
}

public class GameOfLife
{
	public HashSet<Point> Alive { get; private set; } = new HashSet<UserQuery.Point>();
	
	public int Round { get; private set; }
	
	public GameOfLife(IEnumerable<Point> init)
	{
		foreach (var element in init)
			Alive.Add(element);
			
		Round = 1;
	}
	
	public void Step()
	{
		Round += 1;
		
		// we only interested in calculating cells with at least 1 
		// then given this limited set of coordinates we can calculate number
		// of adjacent cells and finally decide whether cell should be alive or not
		
		Dictionary<Point, int> degreeMap = new Dictionary<UserQuery.Point, int>();
		
		// stage one -- calculate degrees
		foreach (Point alive in Alive)
		{
			Point[] adjacent = new []
			{
				new Point(alive.X - 1, alive.Y - 1),
				new Point(alive.X, alive.Y - 1),
				new Point(alive.X + 1, alive.Y - 1),
				new Point(alive.X - 1, alive.Y),
				new Point(alive.X + 1, alive.Y),
				new Point(alive.X - 1, alive.Y + 1),
				new Point(alive.X, alive.Y + 1),
				new Point(alive.X + 1, alive.Y + 1),
			};
			
			foreach (var p in adjacent)
			{
				if (!degreeMap.ContainsKey(p))
					degreeMap[p] = 0;
					
				degreeMap[p] += 1;
			}
		}
		
		// stage two -- given degrees calculated apply the rules
		
		HashSet<Point> nextStepAlive = new HashSet<UserQuery.Point>();
		
		foreach (var kvp in degreeMap)
		{
			int degree = kvp.Value;

			if (degree == 2)
			{
				// keep alive if already alive
				if (Alive.Contains(kvp.Key))
					nextStepAlive.Add(kvp.Key);
			}
				
			 if (degree == 3)
			 	nextStepAlive.Add(kvp.Key);
		}
		
		Alive = nextStepAlive;
	}
	
	public void Print(int border = 2)
	{
		Util.Metatext($"Round #{Round}").Dump();
		
		if (!Alive.Any())
		{
			Util.Metatext("(empty)").Dump();
			string.Empty.Dump();
			
			return;
		}
		
		int left = Alive.Min(x => x.X);
		int right = Alive.Max(x => x.X);
		int bottom = Alive.Min(p => p.Y);
		int top = Alive.Max(x => x.Y);
		
		left -= border;
		right += border;
		bottom -= border;
		top += border;
		
		for (int i = bottom; i <= top; i++)
		{
			for (int j = left; j <= right; j++)
			{
				bool isAlive = Alive.Contains(new Point(j, i));
				
				if (isAlive)
					Console.Write("#");
				else
					Console.Write(".");
			}
			
			Console.WriteLine();
		}
		
		Console.WriteLine();
	}
}
