<Query Kind="Program" />

// This problem was asked by Samsung.
// 
// A group of houses is connected to the main water plant by means of a set of
// pipes. A house can either be connected by a set of pipes extending directly
// to the plant, or indirectly by a pipe to a nearby house which is otherwise connected.
// 
// For example, here is a possible configuration, where A, B, and C are houses, and arrows
// represent pipes:
// 
// A <--> B <--> C <--> plant
//
// Each pipe has an associated cost, which the utility company would like to minimize.
// Given an undirected graph of pipe connections, return the lowest cost configuration of
// pipes such that each house has access to water.
// 
// In the following setup, for example, we can remove all but the pipes from plant to A,
// plant to B, and B to C, for a total cost of 16.
// 
// pipes = {
//     'plant': {'A': 1, 'B': 5, 'C': 20},
//     'A': {'C': 15},
//     'B': {'C': 10},
//     'C': {}
// }

void Main()
{
	// sounds exactly like finding a minimum spanning graph taht remains connected 
	// that is solvable by easy gready Krsakal algorithm...
	
	MinCost(new []
	{
		("plant", "a", 1),
		("plant", "b", 5),
		("plant", "c", 20),
		("a", "c", 15),
		("b", "c", 10),
	}).Dump();
}

int MinCost((string from, string to, int cost)[] pipes)
{
	// we will use joint-set structure to see and drop all pipes that do the cycle
	
	int cost = 0;

	Dictionary<string, string> parents = pipes.SelectMany(x => new[] { x.from, x.to }).Distinct().ToDictionary(x => x, x => x);
	
	Func<string, string> ultimateParentOf = s =>
	{
		 string parent = parents[s];
		 
		 while (parent != parents[parent])
		 	parent = parents[parent];
		
		// auto-normilize as we go! (not required)
		parents[s] = parent;
		
		return parent;
	};
	
	foreach (var pipe in pipes.OrderBy(x => x.cost))
	{
		string fromParent = ultimateParentOf(pipe.from);
		string toParent = ultimateParentOf(pipe.to);

		// see if we connect already connected regions, and skip if we do so
		if (fromParent != toParent)
		{
			cost += pipe.cost;
			
			// join!
			parents[toParent] = fromParent;
		}
	}
	
	return cost;
}
