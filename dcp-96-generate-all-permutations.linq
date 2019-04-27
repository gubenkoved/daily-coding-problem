<Query Kind="Program" />

void Main()
{
	Permutations(new object[] { 'a', 'b', 'c' }).Dump();
}

IEnumerable<object[]> Permutations(object[] objects)
{
	List<object[]> permutations = new List<object[]>();
	
	HashSet<object> available = new HashSet<object>();
	
	foreach (var obj in objects)
		available.Add(obj);
	
	Generator(new object[objects.Length], available, 0, permutations);
	
	return permutations;
}

void Generator(object[] current, HashSet<object> availableObjects, int idx, List<object[]> permutations)
{
	if (idx == current.Length)
	{
		permutations.Add(current.ToArray());
		return;
	}
		
	// how to avoid such an expensive copying all over again?
	foreach (var obj in availableObjects.ToArray())
	{
		current[idx] = obj;
		
		availableObjects.Remove(obj);
		
		Generator(current, availableObjects, idx + 1, permutations);
		
		availableObjects.Add(obj);
	}
}
