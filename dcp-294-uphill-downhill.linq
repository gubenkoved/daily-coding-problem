<Query Kind="Program" />

// This problem was asked by Square.
// 
// A competitive runner would like to create a route that starts and ends
// at his house, with the condition that the route goes entirely uphill at first,
// and then entirely downhill.
// 
// Given a dictionary of places of the form {location: elevation}, and a dictionary
// mapping paths between some of these locations to their corresponding distances,
// find the length of the shortest route satisfying the condition above. Assume the
// runner's home is location 0.
// 
// For example, suppose you are given the following input:
// 
// elevations = {0: 5, 1: 25, 2: 15, 3: 20, 4: 10}
//
// paths = {
//     (0, 1): 10,
//     (0, 2): 8,
//     (0, 3): 15,
//     (1, 3): 12,
//     (2, 4): 10,
//     (3, 4): 5,
//     (3, 0): 17,
//     (4, 0): 10
// }
//
// In this case, the shortest valid path would be 0 -> 2 -> 4 -> 0, with a distance of 28.

void Main()
{
	// looks likewe can basically use Deikstra, but we will need to extend it to store
	// two distances for each node -- uphill and downhill distance, uphill distance will be the distance
	// to the node if we were to travel to it going uphill
	// note that the same node can have both distances filled and they could be different
	// wehther node has uphill or downhill reachibility will difine will roads are possible as continuations
	// for instance, if we already going downhill, reject all edges that go uphill
	
	Path(
		new []
		{
			(0, 1, 10),
			(0, 2, 8),
			(0, 3, 15),
			(1, 3, 12),
			(2, 4, 10),
			(3, 4, 5),
			(3, 0, 17),
			(4, 0, 10),
		}, new Dictionary<int, int>()
		{
			{ 0, 5 },
			{ 1, 25 },
			{ 2, 15 },
			{ 3, 20 },
			{ 4, 10 },
		}).Dump();


	Path(
		new[]
		{
			(0, 1, 1),
			(0, 2, 10),
			(1, 2, 2),
			(1, 4, 3),
			(2, 3, 1),
			(2, 4, 5),
			(3, 0, 2),
			(4, 3, 2),
		}, new Dictionary<int, int>()
		{
			{ 0, 5 },
			{ 1, 20 },
			{ 2, 10 },
			{ 3, 15 },
			{ 4, 18 },
		}).Dump();
}

int[] Path((int from, int to, int len)[] edges, Dictionary<int, int> elevationMap)
{
	var edgesLookup = edges.ToLookup(x => x.from, x => new { x.to, x.len } );
	
	// ids to distance maps
	var uphillDistanceMap = new Dictionary<int, int>();
	var downhillDistanceMap = new Dictionary<int, int>();
	
	uphillDistanceMap[0] = 0;
	
	var uphillVisited = new HashSet<int>();
	var downhillVisited = new HashSet<int>();
	
	var uphillParentMap = new Dictionary<int, int>();
	var downhillParentMap = new Dictionary<int, int>();

	while (true)
	{
		int curIdx = -1;
		int dist = int.MaxValue;
		bool isUphill = false;
		
		// pick a node with the lowest distance (either one)
		foreach (var kvp in uphillDistanceMap)
		{
			if (uphillVisited.Contains(kvp.Key))
				continue;
				
			if (kvp.Value < dist)
			{
				isUphill = true;
				dist = kvp.Value;
				curIdx = kvp.Key;
			}
		}

		foreach (var kvp in downhillDistanceMap)
		{
			if (downhillVisited.Contains(kvp.Key))
				continue;

			if (kvp.Value < dist)
			{
				isUphill = false;
				dist = kvp.Value;
				curIdx = kvp.Key;
			}
		}
		
		// starting at found node (if there is one) update distances to the reachable ones
		// if they are shorter
		
		if (curIdx == -1)
			break; // unable to find a next one...

		Util.Metatext($"visiting {curIdx} node (d {dist}, uphill:{isUphill})").Dump();
		
		if (isUphill)
			uphillVisited.Add(curIdx);
		else
			downhillVisited.Add(curIdx);
		
		foreach (var reachable in edgesLookup[curIdx])
		{
			// if current node is uphill-reachable then we can consider continuing uphill OR go downhill
			// if however, we already gone downhill we can only go downhill now
			
			int reachableElevation = elevationMap[reachable.to];
			
			bool isReachableUphill = reachableElevation > elevationMap[curIdx];
			
			if (!isUphill && isReachableUphill)
				continue; // we can not go uphill from there...
			
			int reachableDist = dist + reachable.len;

			if (isReachableUphill)
			{
				if (!uphillDistanceMap.ContainsKey(reachable.to) || uphillDistanceMap[reachable.to] > reachableDist)
				{
					uphillParentMap[reachable.to] = curIdx;
					uphillDistanceMap[reachable.to] = reachableDist;
				}
			}
			else
			{
				if (!downhillDistanceMap.ContainsKey(reachable.to) || downhillDistanceMap[reachable.to] > reachableDist)
				{
					downhillParentMap[reachable.to] = curIdx;
					downhillDistanceMap[reachable.to] = reachableDist;
				}
			}
		}

		
	}

	if (downhillDistanceMap.ContainsKey(0))
	{
		//uphillDistanceMap.Dump();
		//downhillDistanceMap.Dump();

		Util.Metatext($"distance: {downhillDistanceMap[0]}").Dump();
	}
	
	// recover the path starting from the end
	List<int> path = new List<int>();
	
	path.Add(0);
	
	int tracebackCurIdx = 0;
	bool tracebackIsUphill = false;
	
	do
	{
		int nextTraceBackIdx;
		
		if (!tracebackIsUphill)
			nextTraceBackIdx = downhillParentMap[tracebackCurIdx];
		else
			nextTraceBackIdx = uphillParentMap[tracebackCurIdx];
			
		if (uphillDistanceMap.ContainsKey(nextTraceBackIdx) && downhillDistanceMap.ContainsKey(nextTraceBackIdx))
		{
			// pick whaever has a smaller distance if there are both
			tracebackIsUphill = uphillDistanceMap[nextTraceBackIdx] < downhillDistanceMap[nextTraceBackIdx];
		} else if (uphillDistanceMap.ContainsKey(nextTraceBackIdx))
		{
			tracebackIsUphill = true;
		} else
		{
			tracebackIsUphill = false;
		}
		
		tracebackCurIdx = nextTraceBackIdx;
		
		path.Add(tracebackCurIdx);
		
	} while (tracebackCurIdx != 0);
	
	// reverse the path
	path.Reverse();
	
	return path.ToArray();
}