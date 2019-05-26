<Query Kind="Program" />

// This problem was asked by Google.
// 
// The edit distance between two strings refers to the minimum number
// of character insertions, deletions, and substitutions required to
// change one string to the other. For example, the edit distance
// between “kitten” and “sitting” is three: substitute the “k” for “s”,
// substitute the “e” for “i”, and append a “g”.
// 
// Given two strings, compute the edit distance between them.

void Main()
{
	Distance("", "aaa").Dump("3");
	Distance("aaa", "").Dump("3");
	Distance("a", "aa").Dump("1");
	Distance("aa", "a").Dump("1");
	Distance("a", "b").Dump("1");
	Distance("aaa", "aaaaaa").Dump("3");
	Distance("aaaa", "aabaa").Dump("1");
	Distance("aabaa", "aaaa").Dump("1");
	Distance("kitten", "sitting").Dump("3");
	Distance("bug", "bag").Dump("1");
	Distance("instagram", "instaram").Dump("1");
	Distance("galaxy", "paralax").Dump("4");
	Distance("earth", "moon").Dump("5");
}

int Distance(string a, string b)
{
	var d = new int[a.Length + 1, b.Length + 1];

	// we will try to solve using dynamic programming technique
	// I've picked up via reading the definition and breif familirization with topic
	// let d[i, j] be the distance from a.substring(0, i) to b.substring(0, j)
	// then the answer will be d[a.len - 1, b.len - 1]

	for (int i = 0; i <= a.Length; i++)
	{
		for (int j = 0; j <= b.Length; j++)
		{
			int dist;

			if (i == 0 || j == 0)
			{
				dist = Math.Max(i, j);
			}
			else
			{
				int substitutionCost = a[i - 1] == b[j - 1] ? 0 : 1; // chars match up? zero cost, otherwise we have to substitute
				
				dist = new[]
				{
					d[i - 1, j] + 1, // removing 1 char
					d[i, j - 1] + 1, // inserting 1 char
					d[i - 1, j - 1] + substitutionCost,
				}.Min();
			}

			d[i, j] = dist;
		}
	}

	int answer = d[a.Length, b.Length];

	Util.Metatext($"Distance from {a} to {b} is {answer}").Dump();

	d.Dump();

	return answer;
}

int Distance2(string a, string b)
{
	var d = new int[a.Length, b.Length];
	
	// we will try to solve using dynamic programming technique
	// I've picked up via reading the definition and breif familirization with topic
	// let d[i, j] be the distance from a.substring(0, i) to b.substring(0, j)
	// then the answer will be d[a.len - 1, b.len - 1]
	
	for (int i = 0; i < a.Length; i++)
	{
		for (int j = 0; j < b.Length; j++)
		{
			int cost = a[i] == b[j] ? 0 : 1; // chars match up? zero cost, otherwise we have to substitute

			int dist;

			if (i == 0 && j == 0)
				dist = cost;
			else if (i == 0)
				dist = d[i, j - 1] + 1;
			else if (j == 0)
				dist = d[i - 1, j] + 1;
			else
				dist = new[]
				{
					d[i - 1, j] + 1, // removing 1 char
					d[i, j - 1] + 1, // inserting 1 char
					d[i - 1, j - 1] + cost,
				}.Min();
			
			d[i, j] = dist;
		}
	}
	
	int answer = d[a.Length - 1, b.Length - 1];

	Util.Metatext($"Distance from {a} to {b} is {answer}").Dump();
	
	d.Dump();
	
	return answer;
}