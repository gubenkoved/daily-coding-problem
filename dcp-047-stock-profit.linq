<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// Given a array of numbers representing the stock prices
// of a company in chronological order, write a function
// that calculates the maximum profit you could have made
// from buying and selling that stock once. You must buy
// before you can sell it.
// 
// For example, given [9, 11, 8, 5, 7, 10], you should
// return 5, since you could buy the stock at 5 dollars
// and sell it at 10 dollars.

void Main()
{
	MaxProfit(new[] { 9, 11, 8, 5, 7, 10}).Dump("5");
	MaxProfit(new[] { 1, 2, 3, 4, 5, }).Dump("4");
	MaxProfit(new[] { 5, 4, 3, 2, 1 }).Dump("-1");
}

int MaxProfit(int[] prices)
{
	int runningMin = prices[0];
	int bestDiff = prices[1] - prices[0];
	
	for (int i = 1; i < prices.Length; i++)
	{
		int diff = prices[i] - runningMin;
		
		runningMin = Math.Min(runningMin, prices[i]);

		if (diff > bestDiff)
		{
			Util.Metatext($"UPD: Buy at {runningMin:C2}, sell at {prices[i]:C2}, profit: {diff:C2}").Dump();
			bestDiff = diff;
		}
	}
	
	return bestDiff;
}
