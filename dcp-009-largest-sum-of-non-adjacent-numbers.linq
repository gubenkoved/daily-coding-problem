<Query Kind="Program" />

// This problem was asked by Airbnb.
// 
// Given a list of integers, write a function that returns the largest sum of non-adjacent numbers. Numbers can be 0 or negative.
// 
// For example, [2, 4, 6, 2, 5] should return 13, since we pick 2, 6, and 5. [5, 1, 1, 5] should return 10, since we pick 5 and 5.
// 
// Follow-up: Can you do this in O(N) time and constant space?

void Main()
{
	Func<int[], int> solver = greedy2;
	
	solver(new[] { 2, 5, 6, }).Dump("8");
	solver(new[] { 2, 5, 4, }).Dump("6"); // fuck!
	solver(new[] { 2, 4, 6, 2, 5}).Dump("13");
	solver(new[] { 1, 0, 1, 0, 0, 1, 0, 1, 0 }).Dump("4");
	solver(new[] { 5, 1, 1, 5 }).Dump("10");
	solver(new[] { 1, 5, 5, 1 }).Dump("6");
	solver(new[] { 1, 5, 1, 5 }).Dump("10");
	solver(new[] { 1, 5, 1, 1, 5, 1, 1, 5 }).Dump("15");
	solver(new[] { -1, -1, 7, -1, -1, }).Dump("7");
	solver(new[] { 2, 1, 1, 1, 1, 1 }).Dump("4");
	solver(new[] { 2, 1, 1, 1, 1, 3 }).Dump("6");
	solver(new[] { 2, 1, 1, 1, 1, 1, 1, 1}).Dump("5");
	solver(new[] { 2, 1, 1, 1, 1, 1, 1, 3 }).Dump("7");
	solver(new[] { 2, 1, 1, 1, 1, 1, 1, 1, 3 }).Dump("8");
	solver(new[] { 3, 1, 1, 3, 1, 1, 3, 1 }).Dump("9");
	solver(new[] { -1, -1, -1, -1, -1, 1, 2, -1 }).Dump("2");
	solver(new[] { -1, -1, -1, -1, -1, -1, 1, 2, -1 }).Dump("2");
	solver(new[] { -1, -1, -1, -1, -1, 1, 1, 2, -1 }).Dump("3");
	solver(new[] { -1, -1, -1, -1, -1, 2, 1, 2, -1 }).Dump("4");
	solver(new[] { -1, -1, -1, -1, 2, 1, 2, -1 }).Dump("4");
}

public int naive(int[] a)
{
	List<int> taken = new List<int>();
	int idx = 0;

	while (idx < a.Length)
	{
		if (idx == a.Length - 1) // the last
		{
			// take if positive
			
			if (a[idx] > 0)
				taken.Add(a[idx]);
			
			break;
		}
		
		if (a[idx] > a[idx + 1])
		{
			taken.Add(a[idx]);
			idx += 2;
		} else
		{
			taken.Add(a[idx + 1]);
			idx += 3;
		}
	}
	
	//taken.Dump();
	
	return taken.Sum();
}

public int f2(int[] a)
{
	// brute force
	// at given index try two branches take item at index or take the next one
	
	return 0;
}

public int greedy(int[] a)
{
	// greedy algorithm
	// not sure if this is going to work out
	// calculate euristic on each cell as (value - sum or values for adj cells)
	// then pick one with better metric
	// if there is a case where later elements affect a decision made a lot earlier
	// then it's not going to work out...
	
	List<int> taken = new List<int>();
	
	for (int i = 0; i < a.Length;)
	{
		if (i == a.Length - 1) // last?
		{
			// take if positive
			if (a[i] > 0)
				taken.Add(a[i]);
				
			break;
		}

		if (a[i] <= 0)
		{
			i += 1;
			continue;
		}

		int curMetric = a[i];
		
		if (i < a.Length - 1)
			curMetric -= a[i + 1];
			
		int nextMetric = a[i + 1];

		if (i >= 0)
			nextMetric -= a[i];

		if (i < a.Length - 2)
			nextMetric -= a[i + 2];
			
		if (curMetric >= nextMetric)
		{
			// take current
			taken.Add(a[i]);
			i += 2;
		} else
		{
			// take next
			taken.Add(a[i + 1]);
			i += 3;
		}
	}
	
	Util.Metatext("taken: " + string.Join(", ", taken.Select(x => x.ToString()))).Dump();
	
	return taken.Sum();
	
}

public int greedy2(int[] a)
{
	// credits to Pavel :)
	
	List<int> taken = new List<int>();

	for (int i = 0; i < a.Length;)
	{
		if (i == a.Length - 1) // last?
		{
			// take if positive
			if (a[i] > 0)
				taken.Add(a[i]);

			break;
		}

		if (a[i] <= 0)
		{
			i += 1;
			continue;
		}

		int metric = a[i + 1] - a[i];
		
		if (i < a.Length - 2)
			metric -= a[i + 2];
			
		if (metric > 0)
		{
			taken.Add(a[i + 1]);
			i += 3;
		} else
		{
			taken.Add(a[i]);
			i += 2;
		}
	}

	Util.Metatext("taken: " + string.Join(", ", taken.Select(x => x.ToString()))).Dump();

	return taken.Sum();

}