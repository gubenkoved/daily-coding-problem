<Query Kind="Program" />

// This problem was asked by Microsoft.
// 
// Recall that the minimum spanning tree is the subset of edges of a
// tree that connect all its vertices with the smallest possible total
// edge weight. Given an undirected graph with weighted edges, compute
// the maximum weight spanning tree.

void Main()
{
	// given Krascal alogithm works for minimizing it seems to me that
	// pretty much the same algorithm should work out for maxining as well
	// if only we were to pick the longest edge each time while we are not hitting
	// the same already connected vertices
	// as per: http://mathworld.wolfram.com/MaximumSpanningTree.html
	// alternatively we can just negate the weights of the edges and apply standard version
	// of the Kraskal algorithm, which is basically the same
	
	var a = new Node("a");
	var b = new Node("b");
	var c = new Node("c");
	var d = new Node("d");
	var e = new Node("e");
	
	var edges = new []
	{
		new Edge(a, b, 2),
		new Edge(a, c, 1),
		new Edge(b, c, 1),
		new Edge(a, d, 4),
		new Edge(c, d, 3),
		new Edge(b, e, 5),
		new Edge(c, e, 4),
		new Edge(d, e, 5),
	};
	
	MinimumSpanningTree(edges).Dump("min spanning tree");
	
	MaximumSpanningTree(edges).Dump("max spanning tree");
}

public class Node
{
	public object Value { get; set; }
	
	public Node(object val)
	{
		Value = val;
	}
}

public class Edge
{
	public Node A { get; set; }
	public Node B { get; set; }
	public decimal Weight { get; set; }
	
	public Edge(Node a, Node b, decimal weight)
	{
		A = a;
		B = b;
		Weight = weight;
	}
}

public IEnumerable<Edge> MinimumSpanningTree(IEnumerable<Edge> edges)
{
	int nextBucketNum = 0;
	Dictionary<Node, int> buckets = new Dictionary<UserQuery.Node, int>();
	
	List<Edge> mst = new List<UserQuery.Edge>();
	
	foreach (var edge in edges.OrderBy(x => x.Weight))
	{
		// if edge connects nodes of the same bucket -> discard
		// if edge connects two different "buckets" -> merge
		// if edge connects node from some bucket and non-bucketed node -> put the later into the bucket
		// if both are not in buckets -> put both into the same bucket
		
		if (buckets.ContainsKey(edge.A) && buckets.ContainsKey(edge.B))
		{
			int bucketNumA = buckets[edge.A];
			int bucketNumB = buckets[edge.B];
			
			if (bucketNumA == bucketNumB)
				continue; // already connected, discard!
				
			// okay, merge them up into the bucket with "A" node
			foreach (var node in buckets.Keys)
			{
				if (buckets[node] == bucketNumB)
					buckets[node] = bucketNumA;
			}
		} else if (!buckets.ContainsKey(edge.A) && !buckets.ContainsKey(edge.B))
		{
			// neither in buckets, create a new one
			int bucketNum = nextBucketNum++;
			
			buckets[edge.A] = bucketNum;
			buckets[edge.B] = bucketNum;
		} else
		{
			// one in bucket and another one is not
			
			if (buckets.ContainsKey(edge.A))
				buckets[edge.B] = buckets[edge.A];
			else
				buckets[edge.A] = buckets[edge.B];
		}
		
		mst.Add(edge);
	}	
	
	return mst;
}

public IEnumerable<Edge> MaximumSpanningTree(IEnumerable<Edge> edges)
{
	foreach (var edge in edges)
		edge.Weight *= -1;
		
	var result = MinimumSpanningTree(edges);

	// get the weights back!
	foreach (var edge in edges)
		edge.Weight *= -1;

	return result;
}

