<Query Kind="Program" />

// This problem was asked by Bloomberg.
// 
// Determine whether there exists a one-to-one character mapping from
// one string s1 to another s2.
// 
// For example, given s1 = abc and s2 = bcd, return true since we can
// map a to b, b to c, and c to d.
// 
// Given s1 = foo and s2 = bar, return false since the o cannot map to
// two characters.

void Main()
{
	MappingExists("abc", "bcd").Dump();
	MappingExists("foo", "bar").Dump();
}

bool MappingExists(string a, string b)
{
	if (a.Length != b.Length)
		return false;
	
	var map = new Dictionary<char, char>();
	
	for (int i = 0; i < a.Length; i++)
	{
		if (!map.ContainsKey(a[i]))
		{
			map[a[i]] = b[i];
		}
		else
		{
			if (map[a[i]] != b[i])
				return false;
		}
	}
	
	return true;
}
