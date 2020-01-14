<Query Kind="Program" />

// This problem was asked by Uber.
// 
// One way to unlock an Android phone is through a pattern of swipes across a 1-9 keypad.
// 
// For a pattern to be valid, it must satisfy the following:
// 
// All of its keys must be distinct.
// It must not connect two keys by jumping over a third key, unless that key has already been used.
// For example, 4 - 2 - 1 - 7 is a valid pattern, whereas 2 - 1 - 7 is not.
// 
// Find the total number of valid unlock patterns of length N, where 1 <= N <= 9.

Dictionary<char, Tuple<int, int>> _coordinatesMap = new Dictionary<char, System.Tuple<int, int>>()
{
	{ '1', Tuple.Create(0, 0) },
	{ '2', Tuple.Create(0, 1) },
	{ '3', Tuple.Create(0, 2) },
	{ '4', Tuple.Create(1, 0) },
	{ '5', Tuple.Create(1, 1) },
	{ '6', Tuple.Create(1, 2) },
	{ '7', Tuple.Create(2, 0) },
	{ '8', Tuple.Create(2, 1) },
	{ '9', Tuple.Create(2, 2) },
};

Dictionary<Tuple<int, int>, char> _coordinatesReverseMap = new Dictionary<System.Tuple<int, int>, char>();

void Main()
{
	// since N <= 9 it should be possible just to brute force every combination
	
	// fill in reverse map
	foreach (var kvp in _coordinatesMap)
		_coordinatesReverseMap[kvp.Value] = kvp.Key;
	
	for (int n = 1; n <= 9; n++)
		Amount(n).Dump(n.ToString());
}

int Amount(int n)
{
	int start = (int) Math.Pow(10, n - 1);
	int end = (int)Math.Pow(10, n);
	
	int count = 0;
	
	for (int x = start; x < end; x++)
	{
		string combination = x.ToString();
		
		if (combination.IndexOf('0') != -1)
			continue;
		
		if (combination.Distinct().Count() != n)
			continue;
			
		bool isOk = true;

		for (int i = 1; i < n; i++)
		{
			if (!IsAdjacent(combination[i], combination[i - 1]))
			{
				// allow it olny if we jumping over already used ones
				var aCoordinate = _coordinatesMap[combination[i]];
				var bCoordinate = _coordinatesMap[combination[i - 1]];
				
				int jumpedOverX = (aCoordinate.Item1 + bCoordinate.Item1) / 2;
				int jumpedOverY = (aCoordinate.Item2 + bCoordinate.Item2) / 2;
				
				char jumpedOverChar = _coordinatesReverseMap[Tuple.Create(jumpedOverX, jumpedOverY)];
				
				int jumpedOverCharIdx = combination.IndexOf(jumpedOverChar);

				if (jumpedOverCharIdx == -1 || jumpedOverCharIdx > i)
				{
					isOk = false;
					break; // disallow if we did not see this char yet
				}
			}
		}

		if (isOk)
		{
			//combination.Dump();
			count += 1;
		}
	}
	
	return count;
}

bool IsAdjacent(char a, char b)
{
	var aCoordinate = _coordinatesMap[a];
	var bCoordinate = _coordinatesMap[b];
	
	return Math.Abs(aCoordinate.Item1 - bCoordinate.Item1) <= 1 && Math.Abs(aCoordinate.Item2 - bCoordinate.Item2) <= 1;
}
