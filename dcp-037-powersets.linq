<Query Kind="Program" />

// This problem was asked by Google.
// 
// The power set of a set is the set of all its subsets.
// Write a function that, given a set, generates its power set.
// 
// For example, given the set {1, 2, 3}, it should return
// {{}, {1}, {2}, {3}, {1, 2}, {1, 3}, {2, 3}, {1, 2, 3}}.
// 
// You may also use a list or array to represent a set.

void Main()
{
	Describe(PowerSet(new int[] {  })).Dump();
	Describe(PowerSet(new[] { 1 })).Dump();
	Describe(PowerSet(new[] { 1, 2, 3 })).Dump();
	Describe(PowerSet(new[] { 'a', 'b', 'c' })).Dump();
}

string Describe<T>(IEnumerable<IEnumerable<T>> powerset)
{
	return "{" + string.Join(", ", powerset.Select(set => "{" + string.Join(", ", set) + "}")) + "}" + $" -- {powerset.Count()} element(s)";
}

List<T[]> PowerSet<T>(T[] elemenets)
{
	var result = new List<T[]>();
	
	PowerSet(elemenets.ToList(), new List<T>(), 0, result);
	
	return result;
}

void PowerSet<T>(List<T> data, List<T> choosen, int idx, List<T[]> result)
{
	result.Add(choosen.ToArray());
	
	for (int i = idx; i < data.Count; i++)
	{
		choosen.Add(data[i]);
		
		PowerSet(data, choosen, i + 1, result);
		
		choosen.RemoveAt(choosen.Count - 1);
	}
}