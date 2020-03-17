<Query Kind="Program" />

// This problem was asked by Mailchimp.
// 
// You are given an array representing the heights of neighboring buildings on a
// city street, from east to west. The city assessor would like you to write an
// algorithm that returns how many of these buildings have a view of the setting sun,
// in order to properly value the street.
// 
// For example, given the array [3, 7, 8, 3, 6, 1], you should return 3, since the top
// floors of the buildings with heights 8, 6, and 1 all have an unobstructed view to the west.
// 
// Can you do this using just one forward pass through the array?

void Main()
{
	// hm, i can do it with one pass, but it's not the FORWARD pass...
	CountBuildings(new[] { 3, 7, 8, 3, 6, 1 }).Dump();
	
	// that was added after checking out another solution...
	CountBuildingsV2(new[] { 3, 7, 8, 3, 6, 1 }).Dump();
}

int CountBuildings(int[] h)
{
	int max = int.MinValue;
	
	int count = 0;
	
	for (int i = h.Length - 1; i >= 0; i--)
	{
		if (h[i] > max)
		{
			count += 1;
			max = h[i];
		}
	}
	
	return count;
}

int CountBuildingsV2(int[] h)
{
	LinkedList<int> l = new LinkedList<int>();
	
	int count = 0;
	
	foreach (int x in h)
	{
		while (l.Last != null && l.Last.Value < x)
		{
			l.RemoveLast();
			count -= 1;
		}
			
		l.AddLast(x);
		count += 1;
	}
	
	return count;
}