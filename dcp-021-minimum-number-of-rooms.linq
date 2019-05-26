<Query Kind="Program" />

// This problem was asked by Snapchat.
// 
// Given an array of time intervals (start, end) for classroom lectures
// (possibly overlapping), find the minimum number of rooms required.
//
// For example, given [(30, 75), (0, 50), (60, 150)], you should return 2.

void Main()
{
	// since we do not have to determind exact rollup
	// between rooms we can just need to calculate the max amount of lectures
	// at any given moment in time
	// moreover we can check just a starts of every lecture and count amount of going
	// lecutures, because if one lecture starts at the middle of another we will still
	// go ahead to the start of "another" lecture and calculate it all

	//(int start, int end)[] schedule = new[] { (30, 75), (0, 50), (60, 150) };
	//(int start, int end)[] schedule = new[] { (0, 1), (1, 2), (2, 3) };
	//(int start, int end)[] schedule = new[] { (0, 1), (1, 2), (2, 3), (0, 1), (1, 2), (2, 3), (0, 1), (1, 2), (2, 3), };
	(int start, int end)[] schedule = new[] { (0, 1), (1, 2), (2, 3), (0, 3), (2, 4), };

	int max = 0;

	for (int i = 0; i < schedule.Length; i++)
	{
		int counter = 0; // amount of lectures going at the start of the current
		
		int at = schedule[i].start;
		
		for (int j = 0; j < schedule.Length; j++)
		{
			if (schedule[j].start <= at && at < schedule[j].end)
				counter += 1;
		}
		
		if (counter > max)
			max = counter;
	}
	
	max.Dump();
}

