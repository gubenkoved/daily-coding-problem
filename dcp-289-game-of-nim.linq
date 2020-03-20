<Query Kind="Program" />

// This problem was asked by Google.
// 
// The game of Nim is played as follows. Starting with three heaps, each containing
// a variable number of items, two players take turns removing one or more items
// from a single pile. The player who eventually is forced to take the last stone
// loses. For example, if the initial heap sizes are 3, 4, and 5, a game could be
// played as shown below:
// 
//   A  |  B  |  C
// -----------------
//   3  |  4  |  5
//   3  |  1  |  3
//   3  |  1  |  3
//   0  |  1  |  3
//   0  |  1  |  0
//   0  |  0  |  0 
//
// In other words, to start, the first player takes three items from pile B. The
// second player responds by removing two stones from pile C. The game continues
// in this way until player one takes last stone and loses.
// 
// Given a list of non-zero starting values [a, b, c], and assuming optimal play,
// determine whether the first player has a forced win.

void Main()
{
	// this is zero-sum game, which then can be solved via
	// negamax/minimax algorithms pretty easily
	Winner(new [] { 3, 4, 5 }, 1).Dump("1");
	Winner(new [] { 0, 2, 0 }, 1).Dump("1");
	Winner(new [] { 0, 1, 0 }, 1).Dump("2");
	Winner(new [] { 1, 1, 0 }, 1).Dump("1");
	Winner(new [] { 1, 1, 1 }, 1).Dump("2");
	Winner(new [] { 1, 2, 1 }, 1).Dump("1");
	Winner(new [] { 10, 10, 10 }, 1).Dump("1");
	Winner(new [] { 100, 100, 100 }, 1).Dump("1");
}

// for given position shows if the current player wins
Dictionary<Tuple<int, int, int>, bool> _cache = new Dictionary<System.Tuple<int, int, int>, bool>();

// returns player how wins
int Winner(int[] piles, int currentPlayer)
{
	if (piles.Count(x => x == 0) == 2)
	{
		int nonEmptyPile = piles.Single(x => x > 0);
		
		// current player loses, as this is the only pile with the only stone
		if (nonEmptyPile == 1)
			return NextPlayer(currentPlayer);
		
		// otherwise we can just take all but 1 stone and the next player looses!
		return currentPlayer;
	}
	
	// start assuming current player loses,
	// if there is a move that will make current player win
	// then we will assume that current player will optimize for the win
	// and will do that move
	
	var cacheKey = Tuple.Create(piles[0], piles[1], piles[2]);
	
	if (_cache.ContainsKey(cacheKey))
	{
		bool currentPlayerWins = _cache[cacheKey];
		
		if (currentPlayerWins)
			return currentPlayer;
		else
			return NextPlayer(currentPlayer);
	}
	
	for (int idx = 0; idx < piles.Length; idx++)
	{
		for (int take = 1; take <= piles[idx]; take++)
		{
			piles[idx] -= take;
			
			int subWinner = Winner(piles, NextPlayer(currentPlayer));
			
			piles[idx] += take;

			// looks like current player has a winning move
			if (subWinner == currentPlayer)
			{
				_cache.Add(cacheKey, true);
				
				return currentPlayer;
			}
		}
	}
	
	_cache.Add(cacheKey, false);
	
	return NextPlayer(currentPlayer);
}

int NextPlayer(int player)
{
	return player == 1 ? 2 : 1;
}