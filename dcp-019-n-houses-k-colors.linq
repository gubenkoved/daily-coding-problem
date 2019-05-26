<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// A builder is looking to build a row of N houses that can be of K different colors.
// He has a goal of minimizing cost while ensuring that no two neighboring houses are of the same color.
// 
// Given an N by K matrix where the nth row and kth column represents the cost to
// build the nth house with kth color, return the minimum cost which achieves this goal.

void Main()
{
	int[,] price = new int[3,5]
	{
		{ 8, 2, 0, 1, 9 }, // color 0
		{ 1, 6, 9, 6, 8 }, // color 1
		{ 4, 5, 9, 2, 3 }, // color 2
	};
	
	price.Dump("price");
	
	int n = price.GetLength(1);
	int k = price.GetLength(0);
	
	int?[,] m = new int?[k, n];
	
	// use deikstra algorith to find a shortest path in the graph
	// graph is trivial each node on each column is connected to each node but the one in the same row
	
	// due to nature of the graph, we can simply go one by one in columns then rows
	
	for (int house = 0; house < n; house++)
	{
		for (int color = 0; color < k; color++)
		{
			// calculate min distance from start to given node in the graph
			if (house == 0) // first houses are edge cases, the shortest path to them is just their price
			{
				m[color, house] = price[color, house];
				continue;
			}
			
			// when house is not the first the shortest distance to it is minimal
			// accoss houses on previous state + price of the current house
			
			int minRunningPrice = int.MaxValue;
			
			for (int color2 = 0; color2 < k; color2++)
			{
				if (color2 == color)
					continue; // two houses of the same color are forbiden
					
				int testPrice = m[color2, house - 1].Value + price[color, house];
				
				if (testPrice < minRunningPrice)
					minRunningPrice = testPrice;
			}
			
			m[color, house] = minRunningPrice;
		}
	}
	
	m.Dump("distances");
	
	int minPrice = int.MaxValue;
	for (int color = 0; color < k; color++)
	{
		if (m[color, n - 1].Value < minPrice)
			minPrice = m[color, n - 1].Value;
	}
	
	minPrice.Dump("min price");
}

