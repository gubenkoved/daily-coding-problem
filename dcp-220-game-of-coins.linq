<Query Kind="Program" />

// This problem was asked by Square.
// 
// In front of you is a row of N coins, with values v1, v1, ..., vn.
// 
// You are asked to play the following game. You and an opponent take
// turns choosing either the first or last coin from the row, removing
// it from the row, and receiving the value of the coin.
// 
// Write a program that returns the maximum amount of money you can win
// with certainty, if you move first, assuming your opponent plays optimally.

void Main()
{
	// classic example of zero-sum game
	// can be solved with plain brute force via minimax (or negamax)
	
	MaxWin(new[] { 1 }).Dump("1");
	MaxWin(new[] { 1, 2 }).Dump("2");
	MaxWin(new[] { 1, 2, 10, 2 }).Dump("11");
	MaxWin(new[] { 1, 2, 10, 2, 0 }).Dump("5");
	MaxWin(new[] { 0, 1, 2, 10, 2 }).Dump("4");
	MaxWin(new[] { 0, 1, 2, 10, 2, 0 }).Dump("11");
	MaxWin(new[] { 1, 4, 2, 1, 5, 2, 2, 6, }).Dump("14");
}


int MaxWin(int[] coins)
{
	return Estimate(coins);
}

// negamax!
int Estimate(int[] coins)
{
	if (coins.Length == 1)
		return coins[0];
	
	// you either take from the left of from the right
	int currentTotal = coins.Sum();
	
	// suppose we take from the left
	int[] coins1 = TakeLeft(coins);
	
	int estimate1 = currentTotal - Estimate(coins1);

	// now try from the right
	int[] coins2 = TakeRight(coins);

	int estimate2 = currentTotal - Estimate(coins2);

	return Math.Max(estimate1, estimate2);
}

int[] TakeLeft(int[] coins)
{
	int[] newCoins = new int[coins.Length - 1];
	
	Array.Copy(coins, 1, newCoins, 0, coins.Length - 1);
	
	return newCoins;
}

int[] TakeRight(int[] coins)
{
	int[] newCoins = new int[coins.Length - 1];

	Array.Copy(coins, 0, newCoins, 0, coins.Length - 1);

	return newCoins;
}