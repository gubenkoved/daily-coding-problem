<Query Kind="Program" />

// This problem was asked by Flipkart.
// 
// Snakes and Ladders is a game played on a 10 x 10 board, the goal of which
// is get from square 1 to square 100. On each turn players will roll a six-sided
// die and move forward a number of spaces equal to the result. If they land on a
// square that represents a snake or ladder, they will be transported ahead or
// behind, respectively, to a new square.
// 
// Find the smallest number of turns it takes to play snakes and ladders.
// 
// For convenience, here are the squares representing snakes and ladders, and their outcomes:
// 
// snakes = {16: 6, 48: 26, 49: 11, 56: 53, 62: 19, 64: 60, 87: 24, 93: 73, 95: 75, 98: 78}
// ladders = {1: 38, 4: 14, 9: 31, 21: 42, 28: 84, 36: 44, 51: 67, 71: 91, 80: 100}

void Main()
{
	SmallestAmountOfTurns(
		new[] { (16, 6), (48, 26), (49, 11), (56, 53), (62, 19), (64, 60), (87, 24), (93, 73), (95, 75), (98, 78) },
		new[] { (1, 38), (4, 14), (9, 31), (21, 42), (28, 84), (36, 44), (51, 67), (71, 91), (80, 100) }).Dump();
}

int SmallestAmountOfTurns((int s, int t)[] snakes, (int s, int t)[] ladders)
{
	Dictionary<int, int> teleports = new Dictionary<int, int>();
	
	foreach (var snake in snakes)
		teleports[snake.s] = snake.t;

	foreach (var ladder in ladders)
		teleports[ladder.s] = ladder.t;
		
	return SmallestAmountOfTurns(teleports);
}

int SmallestAmountOfTurns(Dictionary<int, int> teleports)
{
	const int targetIdx = 100;
	
	int?[] d = new int?[targetIdx + 1];
	
	d[1] = 0; // we start at 1st cell
	
	int currentMove = 0;
	
	while (d[targetIdx] == null)
	{
		for (int startIdx = 1; startIdx < targetIdx; startIdx++)
		{
			if (d[startIdx] != currentMove)
				continue;
				
			// okay starting at startIdx we roll the dice and mark all reachable cells
			for (int roll = 1; roll <= 6; roll++)
			{
				int endIdx = startIdx + roll;
				
				if (endIdx > targetIdx)
					continue;
				
				// process snakes and ladders
				while (teleports.ContainsKey(endIdx))
					endIdx = teleports[endIdx];
					
				// mark the target idx as reachable
				d[endIdx] = Math.Min(d[endIdx] ?? int.MaxValue, d[startIdx].Value + 1);
			}
		}
		
		currentMove += 1;
	}
	
	//d.Dump();
	
	return d[targetIdx].Value;
}
