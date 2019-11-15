<Query Kind="Program" />

//This problem was asked by IBM.
//
// Given an integer, find the next permutation of it in absolute order.
// For example, given 48975, the next permutation would be 49578.

void Main()
{
	string cur = "48975";
	
	cur.Dump();
	
	for (int i = 0; i < 200; i++)
	{
		cur = StringPerm(cur);
		cur.Dump();
	}
}

string StringPerm(string s)
{
	return new string(NextPermutation(s).ToArray());
}

IEnumerable<T> NextPermutation<T>(IEnumerable<T> current)
	where T : IComparable<T>
{
	// algorithim:
	// we need to find the first element from the right (say it's i-th)
	// and swap it with element which is "bigger" from the left (say it's j-th)
	// then we need to sort lexicograthically items to the right of j index
	// consider the following series:
	// 1234 -> 1243 -> 1324 -> 1342 -> 1423 -> 1432 -> 2134 -> etc...
	
	int n = current.Count();
	var next = current.ToArray();
	
	for (int j = n - 2; j >= 0; j--)
	{
		for (int i = n - 1; i > j; i--)
		{		
			if (next[j].CompareTo(next[i]) < 0)
			{
				// found it! j-th element being to the left is less than i-th, swap!
				T tmp = next[i];
				next[i] = next[j];
				next[j] = tmp;
				
				// then sort all the items which are to the right of j
				
				if (j < n - 2)
					Array.Sort(next, j + 1, n - (j + 1));
				
				return next;
			}
		}
	}
	
	// if we got there, it means we got the last permuntation 
	// and were unable to find next one, so start it over
	
	Util.Metatext("start over!").Dump();
	
	Array.Sort(next);
	
	return next;
}
