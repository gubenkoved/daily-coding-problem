<Query Kind="Program" />

// This problem was asked by Amazon.
// 
// At a popular bar, each customer has a set of favorite drinks, and will happily
// accept any drink among this set. For example, in the following situation,
// customer 0 will be satisfied with drinks 0, 1, 3, or 6.
// 
// preferences = {
//     0: [0, 1, 3, 6],
//     1: [1, 4, 7],
//     2: [2, 4, 7, 5],
//     3: [3, 2, 5],
//     4: [5, 8]
// }
//
// A lazy bartender working at this bar is trying to reduce his effort by limiting
// the drink recipes he must memorize. Given a dictionary input such as the one above,
// return the fewest number of drinks he must learn in order to satisfy all customers.
// 
// For the input above, the answer would be 2, as drinks 1 and 5 will satisfy everyone.

void Main()
{
	Pick(new Dictionary<int, int[]>()
	{
		{ 0, new [] { 0, 1, 3, 6 } },
		{ 1, new [] { 1, 4, 7 } },
		{ 2, new [] { 2, 4, 7, 5 } },
		{ 3, new [] { 3, 2, 5 } },
		{ 4, new [] { 5, 8 } },
	}).Dump();
}

// avg complexity is hard to tell...
// O(n * n + n * k * logk) to consider edge cases...
// where n - number of customers, k - number of drinks
int[] Pick(Dictionary<int, int[]> preferencesMap)
{
	// it seems to me that greedy algorithm should work out there...

	HashSet<int> customers = new HashSet<int>(preferencesMap.Keys);
	
	HashSet<int> drinks = new HashSet<int>();

	while (customers.Any())
	{
		// find the drink that satisfies most amount of customers
		Dictionary<int, int> drinkFrequency = new Dictionary<int, int>();
		
		foreach (int customer in customers)
		{
			foreach (int drink in preferencesMap[customer])
			{
				if (!drinkFrequency.ContainsKey(drink))
					drinkFrequency[drink] = 0;
				
				drinkFrequency[drink] += 1;
			}
		}
		
		int pickedDrink = drinkFrequency.OrderByDescending(x => x.Value).First().Key;
		
		foreach (var customer in customers.ToArray())
		{
			if (preferencesMap[customer].Contains(pickedDrink))
				customers.Remove(customer);
		}
		
		drinks.Add(pickedDrink);
	}

	return drinks.ToArray();
}