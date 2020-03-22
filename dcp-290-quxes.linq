<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// On a mysterious island there are creatures known as Quxes which come
// in three colors: red, green, and blue. One power of the Qux is that
// if two of them are standing next to each other, they can transform
// into a single creature of the third color.
// 
// Given N Quxes standing in a line, determine the smallest number of
// them remaining after any possible sequence of such transformations.
// 
// For example, given the input ['R', 'G', 'B', 'G', 'B'], it is possible
// to end up with a single Qux through the following steps:
// 
//         Arrangement       |   Change
// ----------------------------------------
// ['R', 'G', 'B', 'G', 'B'] | (R, G) -> B
// ['B', 'B', 'G', 'B']      | (B, G) -> R
// ['B', 'R', 'B']           | (R, B) -> G
// ['B', 'G']                | (B, G) -> R
// ['R']                     |

void Main()
{
	// i'm pretty confident that there is a simple analytical solution though...
	// and there it is: https://www.cnblogs.com/lz87/p/11518225.html
	// I knew there is a simple solution like that which does not imply running
	// all these transformations recoursively...
	
	N2(new[] { 'R', 'G', 'B', 'G', 'B' }).Dump("1");
	N2(new[] { 'R', 'B', 'B', 'B', 'B', 'G' }).Dump("1");
	N2(new[] { 'R', 'G', 'B', 'B', 'B', }).Dump("2");
	N2(new[] { 'R', 'G', 'R', 'G', 'R', 'G', }).Dump("1");
	N2(new[] { 'R', 'G', 'B', }).Dump("2");
	N2(new[] { 'R', 'G', 'R', 'G', 'R', 'G', 'R', 'G', 'R', 'G', 'R', 'G', 'R', 'G', 'R', 'G', 'R', 'G', 'R', 'G', 'R', 'G', 'R', 'G', }).Dump("2");
	N2(new[] { 'R', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G',  }).Dump("1");
	N2(new[] { 'R', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G', 'G' }).Dump("1");
	
	//_cache.Count.Dump();
	//_cache.Dump();
}

int N(char[] c)
{
	return N(new LinkedList<char>(c));
}

int N(LinkedList<char> ll)
{
	var cur = ll.First;
	var next = cur.Next;
	
	int bestLeftover = int.MaxValue;
	
	while (next != null)
	{
		if (cur.Value != next.Value)
		{
			// try to convert
			char third = Third(cur.Value, next.Value);
			
			var thirdNode = ll.AddAfter(cur, third);
			
			ll.Remove(cur);
			ll.Remove(next);
			
			int leftover = N(ll);
			
			if (leftover < bestLeftover)
				bestLeftover = leftover;
				
			ll.AddAfter(thirdNode, cur);
			ll.AddAfter(cur, next);
			ll.Remove(thirdNode);
		}
		
		// go the the next
		cur = next;
		next = cur.Next;
	}
	
	if (bestLeftover == int.MaxValue)
		return ll.Count;
	
	return bestLeftover;
}

Dictionary<string, int> _cache = new Dictionary<string, int>();

int N2(char[] c)
{
	return N2(new string(c));
}

int N2(string s)
{
	if (_cache.ContainsKey(s))
		return _cache[s];
	
	int bestLeftover = int.MaxValue;
	
	for (int idx = 0; idx < s.Length - 1; idx++)
	{
		char cur = s[idx];
		char next = s[idx + 1];
		
		if (cur != next)
		{
			// try to convert
			char third = Third(cur, next);

			string s2 = s.Remove(idx, 2);
			
			s2 = s2.Insert(idx, third.ToString());

			int leftover = N2(s2);

			if (leftover < bestLeftover)
				bestLeftover = leftover;
		}
	}

	if (bestLeftover == int.MaxValue)
		bestLeftover = s.Length;

	_cache[s] = bestLeftover;

	return bestLeftover;
}

char[] _colors = new[] { 'R', 'G', 'B'};

char Third(char a, char b)
{
	return _colors.First(x => x != a && x != b);
}