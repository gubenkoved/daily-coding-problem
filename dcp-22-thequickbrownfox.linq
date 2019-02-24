<Query Kind="Program" />

// This problem was asked by Microsoft.
// 
// Given a dictionary of words and a string made up of those words (no spaces),
// return the original sentence in a list. If there is more than one possible
// reconstruction, return any of them. If there is no possible reconstruction, then return null.
// 
// For example, given the set of words 'quick', 'brown', 'the', 'fox',
// and the string "thequickbrownfox", you should return ['the', 'quick', 'brown', 'fox'].
// 
// Given the set of words 'bed', 'bath', 'bedbath', 'and', 'beyond',
// and the string "bedbathandbeyond", return either ['bed', 'bath', 'and', 'beyond]
// or ['bedbath', 'and', 'beyond'].

void Main()
{
	// we can go bruteforce there... it will be kind depth first search
	// on each step we figure out set of possible words and just try it out
	// adding the work to the stack of the current words untill we managed to 
	// consume all the text
	
	Split(new[] { "quick", "brown", "the", "fox" }, "thequickbrownfox").Dump();
	Split(new[] { "bad", "bath", "bedbath", "and", "beyond" }, "bedbathandbeyond").Dump();
	Split(new[] { "good", "goodness", "god" }, "goodnessgod").Dump();
	Split(new[] { "this", "modest", "dictionary", "some", "more", "words", "be", "cannot" }, "thiscannotbedecomposed").Dump();
	Split(new[] { "aa", "aaa", "bb", "bbb", "zzz", "zzzz" }, "aaazzzzzzzzbbbbb").Dump();
	Split(new[] { "aa", "aaaa", "bb", "bbb" }, "aaa").Dump();
	Split(new[] { "aa", "aaaa", "bb", "bbb" }, "aaaa").Dump();
}

string[] Split(string[] dictionary, string combined)
{
	string[] decomposition = f(dictionary, combined, new Stack<string>());
	
	return decomposition;
}

string[] f(string[] dic, string consumable, Stack<string> consumed)
{
	if (consumable.Length == 0)
		return consumed.Reverse().ToArray();
	
	foreach (string word in dic)
	{
		if (consumable.StartsWith(word))
		{
			consumed.Push(word);
			
			string[] inner = f(dic, consumable.Substring(word.Length), consumed);
			
			consumed.Pop();
			
			if (inner != null)
				return inner; // found the solution -- return instantly!
		}
	}
	
	return null;
}