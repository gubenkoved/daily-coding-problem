<Query Kind="Program" />

// This problem was asked by Google.
// 
// Implement a PrefixMapSum class with the following methods:
// 
// insert(key: str, value: int): Set a given key's value in
// the map. If the key already exists, overwrite the value.
//
// sum(prefix: str): Return the sum of all values of keys that begin with a given prefix.
//
// For example, you should be able to run the following code:
// 
// mapsum.insert("columnar", 3)
// assert mapsum.sum("col") == 3
// 
// mapsum.insert("column", 2)
// assert mapsum.sum("col") == 5

void Main()
{
	// since there is no other constraints I stick to the simplest method
	// depending on specific constraints we can optimize for sum/search (e.g. with sorted list)
	
	var mapSum = new MapSum();
	
	mapSum.Sum("col").Dump("0");
	
	mapSum.Insert("columnar", 3);
	
	mapSum.Sum("col").Dump("3");
	
	mapSum.Insert("column", 2);
	
	mapSum.Sum("col").Dump("5");
}

public class MapSum
{
	Dictionary<string, int> _map = new Dictionary<string, int>();
	
	public void Insert(string s, int count)
	{
		_map[s] = count;
	}
	
	public int Sum(string prefix)
	{
		return _map.Where(x => x.Key.StartsWith(prefix)).Sum(x => x.Value);
	}
}