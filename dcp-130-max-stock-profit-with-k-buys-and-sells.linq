<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// Given an array of numbers representing the stock prices of a company
// in chronological order and an integer k, return the maximum profit you
// can make from k buys and sells. You must buy the stock before you can
// sell it, and you must sell the stock before you can buy it again.
// 
// For example, given k = 2 and the array [5, 2, 4, 0, 1], you should return 3.

void Main()
{
	Func<int[], int, int> solver = MaxProfitDP;
	
	solver(new[] { 1, 2, }, 1).Dump("1");
	solver(new[] { 1, 2, 3, }, 1).Dump("2");
	solver(new[] { 2, 1, 3, }, 1).Dump("2");
	solver(new[] { 1, 2, 1, 2 }, 2).Dump("2");
	
	solver(new[] { 2, 1, 3, 1, 4, 2, 3, 3 }, 2).Dump("5");
	
	solver(new[] { 5, 2, 4, 0, 1 }, 2).Dump("3");
	
	solver(new[] { 5, 4, 3, 2, 1 }, 1).Dump("-1"); // assuming is it a MUST to buy and sell k times
	solver(new[] { 5, 4, 3, 2, 1 }, 2).Dump("-2"); // assuming is it a MUST to buy and sell k times
	solver(new[] { 1, 2, 3, 4, 5 }, 1).Dump("4");
	
	solver(new[] { 1, 10, 5, 4, 21 }, 1).Dump("20");
	solver(new[] { 1, 10, 5, 4, 21 }, 2).Dump("26");
	
	solver(new[] { 1, 11, 1, 11, 1, 11 }, 3).Dump("30");
	solver(new[] { 1, 11, 2, 13, 3, 15 }, 1).Dump("14");
	solver(new[] { 1, 11, 2, 13, 3, 15 }, 2).Dump("24");
	solver(new[] { 1, 11, 2, 13, 3, 15 }, 3).Dump("33");
}

int MaxProfitBruteForce(int[] p, int k)
{
	return MaxProfitBruteForce(p, k, 0);
}

// O(n*n ^ k) -- too freaking expensive...
int MaxProfitBruteForce(int[] p, int k, int start)
{
	if (k == 0)
		return 0;
		
	int n = p.Length;
	
	if (n - start < k * 2)
		return int.MinValue; // impossible to do k buys and sells there...
	
	int max = int.MinValue;
	
	// exlcusive O(n)
	for (int buy = start; buy < n - 1; buy++)
	{
		// excl. O(n)
		for (int sell = buy + 1; sell < n; sell++)
		{
			// let suppose we bought at 'buy' and sold at 'sell' for this round
			// then we recourse deeper to see what we can do in next round(s) about it
			// we will maximize the sum
			
			int profit = p[sell] - p[buy]; // current round profit
			
			// recourse k levels
			int inner = MaxProfitBruteForce(p, k - 1, sell + 1);
			
			if (inner == int.MinValue)
				continue; // preventing the overflow
			
			if (profit + inner > max)
				max = profit + inner;
		}
	}
	
	return max;
}

// O(k * n * n)
int MaxProfitDP(int[] p, int k)
{
	// it must be a dynamic programming problem...
	// probably two dimensional
	// if t[i, j] will be equal to max profit with i buys/sells at j-th day
	// then the answer will be t[k, len(p) - 1], but can we maintain such dynamic?
	
	// did not find a dynamic solution myself...
	
	int n = p.Length;
	
	long[,] t = new long[k + 1, n + 1];
	
	for (int trxCount = 1; trxCount <= k; trxCount++)
	{
		// boundry condition -- enforce exactly K trades
		t[trxCount, 0] = int.MinValue;
		
		for (int sellDay = 1; sellDay < n; sellDay++)
		{
			long sellTodayBest = int.MinValue;
			
			for (int buyDay = 0; buyDay < sellDay; buyDay++)
			{
				// sell at the 'sellDay', but we need to find the day where you could have bought it at 'buyDay'
				long sellTodayCurrent = p[sellDay] - p[buyDay] + t[trxCount - 1, buyDay]; 
				
				if (sellTodayBest < sellTodayCurrent)
					sellTodayBest = sellTodayCurrent;
			}
			
			t[trxCount, sellDay] = Math.Max(
				t[trxCount, sellDay - 1], // do nothing at this day best
				sellTodayBest);
		}
	}
	
	return (int)t[k, n - 1];
}