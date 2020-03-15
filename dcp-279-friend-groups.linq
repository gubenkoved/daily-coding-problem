<Query Kind="Program" />

// This problem was asked by Twitter.
// 
// A classroom consists of N students, whose friendships can be represented in
// an adjacency list. For example, the following descibes a situation where 0
// is friends with 1 and 2, 3 is friends with 6, and so on.
// 
// {0: [1, 2],
//  1: [0, 5],
//  2: [0],
//  3: [6],
//  4: [],
//  5: [1],
//  6: [3]} 
//
// Each student can be placed in a friend group, which can be defined as the
// transitive closure of that student's friendship relations. In other words,
// this is the smallest set such that no student in the group has any friends
// outside this group. For the example above, the friend groups would be
//
// {0, 1, 2, 5}, {3, 6}, {4}.
// 
// Given a friendship list such as the one above, determine the number of friend
// groups in the class.

void Main()
{
	// basically the task is to find connected components
	FindConnectedComponents(new Dictionary<int, int[]>()
	{
		{ 0, new int[] { 1, 2 } },
		{ 1, new int[] { 0, 5} },
		{ 2, new int[] { 0 } },
		{ 3, new int[] { 6 } },
		{ 4, new int[] { } },
		{ 5, new int[] { 1 } },
		{ 6, new int[] { 3 } },
	}).Dump();
}

IEnumerable<int[]> FindConnectedComponents(IDictionary<int, int[]> adjacencyList)
{
	HashSet<int> visited = new HashSet<int>();
	
	List<int[]> components = new List<int[]>();
	
	foreach (var start in adjacencyList.Keys)
	{
		if (visited.Contains(start))
			continue;
		
		int[] component = GetConnected(start, adjacencyList, visited);
		
		components.Add(component);
	}
	
	return components;
}

int[] GetConnected(int start, IDictionary<int, int[]> adjacencyList, HashSet<int> visited)
{
	Queue<int> queue = new Queue<int>();
	
	queue.Enqueue(start);
	
	var component = new List<int>();
	
	while (queue.Any())
	{
		int cur = queue.Dequeue();
		
		visited.Add(cur);
		component.Add(cur);
		
		foreach (int reachable in adjacencyList[cur])
		{
			if (!visited.Contains(reachable))
				queue.Enqueue(reachable);
		}
	}
	
	return component.ToArray();
}
