<Query Kind="Program" />

// This problem was asked by Twitter.
// 
// A network consists of nodes labeled 0 to N. You are given
// a list of edges (a, b, t), describing the time t it takes
// for a message to be sent from node a to node b. Whenever
// a node receives a message, it immediately passes the message
// on to a neighboring node, if possible.
// 
// Assuming all nodes are connected, determine how long it will take for
// every node to receive a message that begins at node 0.
// 
// For example, given N = 5, and the following edges:
// 
// edges = [
//     (0, 1, 5),
//     (0, 2, 3),
//     (0, 5, 4),
//     (1, 3, 8),
//     (2, 3, 1),
//     (3, 5, 10),
//     (3, 4, 5)
// ]
//
// You should return 9, because propagating the message
// from 0 -> 2 -> 3 -> 4 will take that much time.

void Main()
{
	TotalTime(new []
	{
		new Edge(0, 1, 5),
		new Edge(0, 2, 3),
		new Edge(0, 5, 4),
		new Edge(1, 3, 8),
		new Edge(2, 3, 1),
		new Edge(3, 5, 10),
		new Edge(3, 4, 5)
	}).Dump("9");

	TotalTime(new[]
	{
		new Edge(0, 1, 1),
		new Edge(1, 2, 2),
		new Edge(2, 3, 3),
		new Edge(3, 4, 4),
	}).Dump("10");

	TotalTime(new[]
	{
		new Edge(0, 1, 1),
		new Edge(0, 2, 2),
		new Edge(0, 3, 3),
		new Edge(0, 4, 4),
	}).Dump("4");

	TotalTime(new[]
	{
		new Edge(0, 1, 1),
		new Edge(1, 0, 2),
		new Edge(0, 2, 3),
		new Edge(2, 0, 4),
		new Edge(2, 3, 5),
	}).Dump("8");

	TotalTime(new[]
	{
		new Edge(0, 1, 1),
		new Edge(1, 2, 11),
		new Edge(2, 3, 11),
		new Edge(3, 4, 11),
		new Edge(4, 5, 11),
		
		// fast routes!
		new Edge(1, 2, 1),
		new Edge(1, 3, 1),
		new Edge(1, 4, 1),
		new Edge(1, 5, 1),
	}).Dump("2");
}

public class Edge
{
	public int Source { get; set; }
	public int Target { get; set; }
	public int Time { get; set; }
	
	public Edge(int source, int target, int t)
	{
		Source = source;
		Target = target;
		Time = t;
	}
}

int TotalTime(IEnumerable<Edge> edges)
{
	ILookup<int, Edge> outgoingEdgesLookup = edges.ToLookup(x => x.Source);
	
	Queue<int> queue = new Queue<int>();
	
	queue.Enqueue(0);
	
	HashSet<int> visited = new HashSet<int>();
	
	visited.Add(0);
	
	Dictionary<int, int> timeMap = new Dictionary<int, int>();
	
	timeMap[0] = 0;
	
	while (queue.Count > 0)
	{
		int cur = queue.Dequeue();
		
		visited.Add(cur);
		
		foreach (Edge reachable in outgoingEdgesLookup[cur])
		{
			if (!timeMap.ContainsKey(reachable.Target) || timeMap[reachable.Target] > timeMap[cur] + reachable.Time)
				timeMap[reachable.Target] = timeMap[cur] + reachable.Time;
				
			if (!visited.Contains(reachable.Target))
				queue.Enqueue(reachable.Target);
		}
	}
	
	//timeMap.Dump();
	
	return timeMap.Values.Max();
}
