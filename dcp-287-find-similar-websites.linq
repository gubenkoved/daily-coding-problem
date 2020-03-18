<Query Kind="Program" />

// This problem was asked by Quora.
// 
// You are given a list of (website, user) pairs that represent users visiting
// websites. Come up with a program that identifies the top k pairs of websites
// with the greatest similarity.
// 
// For example, suppose k = 1, and the list of tuples is:
// 
// [("a", 1), ("a", 3), ("a", 5),
//  ("b", 2), ("b", 6),
//  ("c", 1), ("c", 2), ("c", 3), ("c", 4), ("c", 5)
//  ("d", 4), ("d", 5), ("d", 6), ("d", 7),
//  ("e", 1), ("e", 3), ("e": 5), ("e", 6)]
//
// Then a reasonable similarity metric would most likely conclude that a and e
// are the most similar, so your program should return [("a", "e")].

void Main()
{
	FindSimilar(new []
	{
		("a", 1), ("a", 3), ("a", 5), ("b", 2), ("b", 6),
		("c", 1), ("c", 2), ("c", 3), ("c", 4), ("c", 5),
		("d", 4), ("d", 5), ("d", 6), ("d", 7), ("e", 1),
		("e", 3), ("e", 5), ("e", 6)
	}, 5).Dump();
}

// O(n*n) where n is amount of source pairs
public (string website1, string website2, double similarity)[] FindSimilar(IEnumerable<(string website, int userId)> raw, int k)
{
	// similarity is not well defined, so let"s define
	// similarity as the percentage of user base for two given sites
	// which is intersected:
	// similarity(a, b) = count(users(a) intersect users(b)) / count(users(a) + users(b))
	
	// O(n)
	var map = raw.GroupBy(x => x.Item1).ToDictionary(g => g.Key, g => g.Select(x => x.userId).ToArray());
	
	var similarities = new List<(string website1, string website2, double similarity)>();
	
	// both: O(n*n)
	foreach (string a in map.Keys)
	{
		foreach (string b in map.Keys)
		{
			if (a == b)
				continue;
				
			double similarity = map[a].Intersect(map[b]).Count() / ((double) map[a].Count() + map[b].Count());
			
			similarities.Add((a, b, similarity));
		}
	}
	
	return similarities
		.OrderByDescending(x => x.Item3)
		.Take(k)
		.ToArray();
}
