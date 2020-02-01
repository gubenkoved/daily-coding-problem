<Query Kind="Program" />

// This problem was asked by Dropbox.
// 
// Given a list of words, determine whether the words can be
// chained to form a circle. A word X can be placed in front
// of another word Y in a circle if the last character of X
// is same as the first character of Y.
// 
// For example, the words ['chair', 'height', 'racket', touch',
// 'tunic'] can form the following circle:
// chair --> racket --> touch --> height --> tunic --> chair

void Main()
{
	// if we will represent start and end char of each word as node
	// and the word as an edge, then the problems becomes equivalent to
	// the Euler cycle problem

	// checking to see if there is a cycle is as easy as checking that
	// for each node input degree is equal to the output degree
	// (i've just looked that up in wiki for this one)
	// building up a cycle is harder, and given there are no efficency 
	// constraints can be done with backtracking

	CanFormACycle(new[] { "ab", "ba" }).Dump();
	CanFormACycle(new[] { "ab", "bab", "boob", "bob", "bab",  "ba" }).Dump();
	CanFormACycle(new[] { "chair", "height", "racket", "touch", "tunic" }).Dump();
	CanFormACycle(new[] { "abc", "cde", "efg", "gfh", }).Dump();
	CanFormACycle(new[] { "abc", "cde", "efg", "gfh", "hja" }).Dump();
	CanFormACycle(new[] { "abc", "cde", "efg", "gfh", "hja" }.Reverse()).Dump();
}

bool CanFormACycle(IEnumerable<string> words)
{
	IEnumerable<string> circle = FormACycle(
		chain: words.Take(1).ToList(), // we can start with any word, since it will be part of the cycle
		available: words.Skip(1).GroupBy(x => x).ToDictionary(g => g.Key, g => new Counter() { Value = g.Count() }));
	
	Util.Metatext((circle != null ? string.Join(" > ", circle) : "nope")).Dump();
	
	return circle != null;
}

IEnumerable<string> FormACycle(List<string> chain, Dictionary<string, Counter> available)
{
	char first = chain[0][0];
	string lastWord = chain[chain.Count - 1];
	char last = lastWord[lastWord.Length - 1];

	if (available.Values.All(x => x.Value == 0))
	{
		if (first == last)
			return chain.ToArray();
	}

	foreach (var kvp in available)
	{
		if (kvp.Value.Value == 0)
			continue; // no words left
		
		string word = kvp.Key;
		
		if (word[0] == last)
		{
			// dive!
			available[word].Value -= 1;
			chain.Add(word);
			
			var inner = FormACycle(chain, available);
			
			if (inner != null)
				return inner;

			available[word].Value += 1;
			chain.Remove(word);
		}
	}
	
	return null;
}

// otherwise we can not change the map values inplace as collection modifed error will pop
public class Counter
{
	public int Value { get; set; }
}