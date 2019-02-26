<Query Kind="Program" />

// This problem was asked by Jane Street.
// 
// Suppose you are given a table of currency exchange rates,
// represented as a 2D array. Determine whether there is a possible arbitrage:
// that is, whether there is some sequence of trades you can make, starting
// with some amount A of any currency, so that you can end up with some amount
// greater than A of that currency.
// 
// There are no transaction costs and you can trade fractional quantities.

void Main()
{
	// rates[i, j] == a means that for 1 unit of i-th currency it's possible to get "a" units of j-th currency
	
	double[,] rates = new double[,]
	{
		// USD,  EUR,   RUR
		{ 1.000, 0.900, 90.000, }, // USD
		{ 1.050, 1.000, 70.000, }, // EUR
		{ 0.010, 0.011, 1.000, }, // RUR
	};
	
	FindArbitrage(rates);
}

// returns chain of indexes that leads to arbitrage
void FindArbitrage(double[,] rates)
{
	// math path len is n where n is amount of currencies
	// as it does not make sense to go through the same pair twice
	// it either yeilds profit, then why to continue, or it does not
	// then why to go twice
	
	// try every start idx
	for (int idx = 0; idx < rates.GetLength(0); idx++)
		F(rates, new List<int>() { idx }, 1.0);
}

void F(double[,] rates, List<int> visited, double runningRate)
{
	int n = rates.GetLength(0);
	
	int startIdx = visited[0];

	int lastIdx = visited[visited.Count - 1];

	// check if closure to startIdx leads to arbitrage
	if (visited.Count > 1)
	{
		double returnRate = runningRate * rates[lastIdx, startIdx];
		
		if (returnRate > 1)
			Util.Metatext(Annotate(rates, visited.Append(startIdx).ToArray())).Dump();
	}
	
	for (int idx = 0; idx < n; idx++)
	{
		if (visited.Contains(idx))
			continue;
			
		// if we got there index 'idx' is not yet visited, can visit
		
		double conversionRate = rates[lastIdx, idx];
		
		// branching
		visited.Add(idx);
		
		F(rates, visited, runningRate * conversionRate);
		
		visited.RemoveAt(visited.Count - 1);
	}
}

string Annotate(double[,] rates, int[] chain)
{
	double totalRate = 1;
	
	StringBuilder annotation = new StringBuilder();
	
	annotation.AppendLine("Arbitrage found!");
	
	for (int i = 1; i < chain.GetLength(0); i++)
	{
		int start = chain[i - 1];
		int end = chain[i];
		
		totalRate *= rates[start, end];

		annotation.AppendLine($"Step {i}: {start} -> {end} with rate {rates[start, end]:F3}, total rate is {totalRate:F3}");
	}
	
	annotation.AppendLine("");
	
	return annotation.ToString();
}