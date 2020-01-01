<Query Kind="Program" />

// This problem was asked by Airbnb.
// 
// You come across a dictionary of sorted words in a language
// you've never seen before. Write a program that returns the
// correct order of letters in this language.
// 
// For example, given ["xww", "wxyz", "wxyw", "ywx", "ywz"],
// you should return ["x", "z", "w", "y"].

void Main()
{
	Recover(new[] { "xww", "wxyz", "wxyw", "ywx", "ywz" }).Dump("x, z, w, y");
	Recover(new[] { "qwerty", "werty", "erty", "rty", "ty", "y" }).Dump("q, w, e, r, t, y");
	Recover(new[] { "qwerty", "werty", "erty", "rty", "tty", "ttt" }).Dump("q, w, e, r, y, t");
}

char[] Recover(IEnumerable<string> sorted)
{
	// that's the algorithm that I came with
	// 1. pre-process the source dictionary into the rules of type: "x" < "z"
	//    based on the word that have the same prefix and their order
	//    e.g. if "wxyz" is before "wxyw", then it means that "z" < "w"
	// 2. start with some order of all the found letters and for each pair check if there are
	// 	  any rules violated, and there a rule violated by some pair of letters
	//    then swap the letters and repeat until we check all the pairs and did not find any
	//    violations
	
	
	// set of pair which are when present means that Item1 is less than Item2 in the Tuple
	HashSet<Tuple<char, char>> lessThanSet = new HashSet<System.Tuple<char, char>>();
	
	ConstuctLessThanPairs(sorted, string.Empty, lessThanSet);
	
	lessThanSet.Dump();
	
	// construct the rules
	
	// reconstruct the alphabet
	
	char[] alphabet = sorted.SelectMany(x => x.ToArray()).Distinct().ToArray();
	
	bool working = false;
	
	do
	{
		working = false;
		
		for (int i = 0; i < alphabet.Length - 1; i++)
		{
			for (int j = i + 1; j < alphabet.Length; j++)
			{
				if (lessThanSet.Contains(Tuple.Create(alphabet[j], alphabet[i])))
				{
					char tmp = alphabet[i];
					alphabet[i] = alphabet[j];
					alphabet[j] = tmp;
					
					// swapped the pair, so that we have to repeat as during the swap
					// we could have violated more constraints
					working = true;
				}
			}
		}
	}
	while (working);
	
	return alphabet;
}

void ConstuctLessThanPairs(IEnumerable<string> sorted, string prefix, HashSet<Tuple<char, char>> lessThanSet)
{
	Util.Metatext($"constucting rules, prefix: '{prefix}'").Dump();
	
	IEnumerable<string> startWithPrefix = sorted.Where(x => x.StartsWith(prefix)).ToArray();
	
	if (startWithPrefix.Count() <= 1)
		return; // no useful information there
	
	// take all first letters of words with the same prefix
	char[] sortedChars = startWithPrefix
		.Where(x => x.Length > prefix.Length) //there is at least one more char, not only the prefix!
		.Select(x => x[prefix.Length]) // take the char next after the prefix ends
		.Distinct()
		.ToArray();
	
	for (int i = 0; i < sortedChars.Length - 1; i++)
	{
		for (int j = i + 1; j < sortedChars.Length; j++)
		{
			if (sortedChars[i] == sortedChars[j])
				continue;

			Util.Metatext($"  adding rule: '{sortedChars[i]}' < '{sortedChars[j]}'").Dump();
			
			lessThanSet.Add(Tuple.Create(sortedChars[i], sortedChars[j]));
		}
	}
	
	// dive in recoursively trying to increase the prefix!
	foreach (char extension in sortedChars)
	{
		ConstuctLessThanPairs(startWithPrefix, prefix + extension, lessThanSet);
	}
}