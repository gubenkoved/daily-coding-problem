<Query Kind="Program" />

// This problem was asked by Yelp.
// 
// Given a mapping of digits to letters (as in a phone number),
// and a digit string, return all possible letters the number
// could represent. You can assume each valid number in the
// mapping is a single digit.
// 
// For example if {“2”: [“a”, “b”, “c”], 3: [“d”, “e”, “f”], …}
// then “23” should return [“ad”, “ae”, “af”, “bd”, “be”, “bf”,
// “cd”, “ce”, “cf"].

void Main()
{
	Dictionary<char, string[]> map = new Dictionary<char, string[]>()
	{
		{ '2', new [] { "a", "b", "c" } },
		{ '3', new [] { "d", "e", "f" } },
	};
	
	Generate(map, "23").Dump();
}

IEnumerable<string> Generate(Dictionary<char, string[]> mapping, string source)
{
	List<string> combinations = new List<string>();
	
	Generate(mapping, source, 0, new string[source.Length], combinations);
	
	return combinations;
}

void Generate(Dictionary<char, string[]> mapping, string source, int index, string[] chunks, List<string> combinations)
{
	if (index == source.Length)
	{
		combinations.Add(string.Join("", chunks));
		return;
	}
	
	foreach (string s in mapping[source[index]])
	{
		chunks[index] = s;
		
		Generate(mapping, source, index + 1, chunks, combinations);
	}
}