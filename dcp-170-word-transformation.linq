<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// Given a start word, an end word, and a dictionary of valid words,
// find the shortest transformation sequence from start to end such
// that only one letter is changed at each step of the sequence, and
// each transformed word exists in the dictionary. If there is no possible
// transformation, return null. Each word in the dictionary have the
// same length as start and end and is lowercase.
// 
// For example, given start = "dog", end = "cat",
// and dictionary = {"dot", "dop", "dat", "cat"},
// return ["dog", "dot", "dat", "cat"].
// 
// Given start = "dog", end = "cat",
// and dictionary = {"dot", "tod", "dat", "dar"}, return null as
// there is no possible transformation from dog to cat.

void Main()
{
	// the task is basically perform a breadth first search from one node in the graph to another
	// when words in the dictionary are the nodes and and an edge between two given nodes exists
	// when only one letter is different

	FindBFS("dog", "cat", new[] { "dot", "dop", "dat", "cat" }).Dump("dog -> dot -> dat -> cat");
	
	FindBFS("dog", "cat", new[] { "dot", "tod", "dat", "dar" }).Dump("null");
}

// BFS should produce optimal results
string[] FindBFS(string start, string end, IEnumerable<string> dictionary)
{
	var adjacencyMap = GetAdjacencyMap(dictionary, start);
	
	// stored preceding word according to BFS
	Dictionary<string, string> parentMap = new Dictionary<string, string>();
	
	parentMap[start] = null;
	
	Queue<string> queue = new Queue<string>();
	
	queue.Enqueue(start);
	
	HashSet<string> visited = new HashSet<string>();
	
	while (queue.Count > 0)
	{
		// there is something to work woth
		
		string current = queue.Dequeue();
		
		visited.Add(current);
		
		foreach (var adjacent in adjacencyMap[current])
		{
			if (visited.Contains(adjacent))
				continue;
			
			parentMap[adjacent] = current;
			queue.Enqueue(adjacent);
			
			if (adjacent == end)
			{
				// found it! recover the path! start from the last one and trace all the way to the start
				List<string> path = new List<string>();
				
				string tracebackPointer = end;
				
				do
				{
					path.Add(tracebackPointer);
					tracebackPointer = parentMap[tracebackPointer];
				} while (tracebackPointer != null);
				
				path.Reverse();
				
				return path.ToArray();
			}
		}
	}
	
	return null;
}

string[] FindDFS(string start, string end, IEnumerable<string> dictionary)
{
	var adjacencyMap = GetAdjacencyMap(dictionary, start);
	var currentPath = new List<string>();

	currentPath.Add(start);

	var result = FindDFS(adjacencyMap, currentPath, end);

	return result?.ToArray();
}

// depth first might produce suboptimal results
List<string> FindDFS(Dictionary<string, List<string>> adjacencyMap, List<string> currentPath, string target)
{
	string current = currentPath.Last();

	if (adjacencyMap[current].Contains(target))
	{
		// found it!
		var result = currentPath.ToList();
		result.Add(target);

		return result;
	}

	// okay, it's not near yet and we will try to recoursively dive into
	foreach (var adjacent in adjacencyMap[current])
	{
		if (currentPath.Contains(adjacent))
			continue;

		currentPath.Add(adjacent);

		var subResult = FindDFS(adjacencyMap, currentPath, target);

		if (subResult != null)
			return subResult; // found it!

		currentPath.Remove(adjacent);
	}

	// did not find...
	return null;
}

Dictionary<string, List<string>> GetAdjacencyMap(IEnumerable<string> dictionary, string start)
{
	dictionary = dictionary.Union(new[] { start }).ToArray();
	
	Dictionary<string, List<string>> adjacencyMap = new Dictionary<string, System.Collections.Generic.List<string>>();
	
	foreach (var word in dictionary)
	{
		adjacencyMap[word] = new List<string>();
		
		foreach (string word2 in dictionary)
		{
			if (word == word2)
				continue;

			if (Diff(word, word2) == 1)
				adjacencyMap[word].Add(word2);
		}
	}
	
	return adjacencyMap;
}

int Diff(string a, string b)
{
	int diff = 0;
	
	for (int i = 0; i < a.Length; i++)
	{
		if (a[i] != b[i])
			diff += 1;
	}
	
	return diff;
}


