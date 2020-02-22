<Query Kind="Program" />

void Main()
{
	Decrypt("send", "more", "money").Dump();
	Decrypt("a", "b", "c").Dump();
	Decrypt("aa", "b", "cc").Dump();
	Decrypt("abcd", "bcde", "cefh").Dump();
}

Dictionary<char, int> Decrypt(string a, string b, string sum)
{
	var map = new Dictionary<char, int>();
	
	// first letter can not be zero unless it's 1 char long string
	var canNotBeZero = new HashSet<char>();
	
	if (a.Length > 1)
		canNotBeZero.Add(a[0]);

	if (b.Length > 1)
		canNotBeZero.Add(b[0]);

	if (sum.Length > 1)
		canNotBeZero.Add(sum[0]);

	bool foundIt = DecryptImpl(a, b, sum, map, canNotBeZero);
	
	if (!foundIt)
		return null;
	
	"solution:".Dump();

	$"{a,10}".Dump();
	$"+ {b,8}".Dump();
	$"{sum,10}".Dump();

	"~~~".Dump();

	$"{Map(a, map), 10}".Dump();
	$"+ {Map(b, map),8}".Dump();
	$"{Map(sum, map),10}".Dump();

	return map;
}

// recoursively resolves one unresolved symbol
bool DecryptImpl(string a, string b, string sum, Dictionary<char, int> map, HashSet<char> canNotBeZero)
{
	char[] unresolved = a.Union(b).Union(sum).Except(map.Keys).ToArray();

	if (!unresolved.Any())
	{
		if (Works(a, b, sum, map))
			return true; // STOP, found it!
			
		// keep looking
		return false;
	}
	
	// there are some unresolved
	char toPick = unresolved[0];
	
	foreach (int val in Enumerable.Range(0, 10))
	{
		if (canNotBeZero.Contains(toPick) && val == 0)
			continue;
			
		map[toPick] = val;

		//$"{new string(' ', map.Count)}try {toPick}={val}".Dump();
		
		if (!CouldWork(a, b, sum, map))
			continue; // backtracking -- discard that one, go to the next!
			
		// looks like it could work
		bool stop = DecryptImpl(a, b, sum, map, canNotBeZero);
		
		if (stop)
			return stop;
	}
	
	// if we got there, then we should keep looking AND we make this letter unresolved again
	map.Remove(toPick);
	
	return false;
}

bool CouldWork(string a, string b, string sum, Dictionary<char, int> map)
{
	int n = Math.Min(a.Length, b.Length);
	
	for (int i = 1; i <= n; i++)
	{
		// check if i-th from the end (!) char + i-th from the end of the second
		// matches up with i-th in sum
		
		char ac = a[a.Length - i];
		char bc = b[b.Length - i];
		char sumc = sum[sum.Length - i];
		
		if (map.ContainsKey(ac) && map.ContainsKey(bc) && map.ContainsKey(sumc))
		{
			if ((map[ac] + map[bc]) % 10 != map[sumc])
			{
				//$"can not work if {ac}={map[ac]} and {bc}={map[bc]} and {sumc}={map[sumc]}".Dump();
				
				return false;
			}
		}
	}
	
	// no contradictions
	return true;
}

bool Works(string a, string b, string sum, Dictionary<char, int> map)
{
	return Map(a, map) + Map(b, map) == Map(sum, map);
}

int Map(string a, Dictionary<char, int> map)
{
	return int.Parse(string.Join("", a.Select(x => map[x].ToString())));
}