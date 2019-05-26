<Query Kind="Program" />

// This problem was asked by Quora.
// 
// Given a string, find the palindrome that can be made by inserting the fewest
// number of characters as possible anywhere in the word. If there is more than
// one palindrome of minimum length that can be made, return the lexicographically
// earliest one (the first one alphabetically).
// 
// For example, given the string "race", you should return "ecarace", since we
// can add three letters to it (which is the smallest amount to make a palindrome).
// There are seven other palindromes that can be made from "race" by adding three
// letters, but "ecarace" comes first alphabetically.
// 
// As another example, given the string "google", you should return "elgoogle".

void Main()
{
	Palindromize("ab").Dump();
	Palindromize("ba").Dump();
	Palindromize("race").Dump();
	Palindromize("google").Dump();
	Palindromize("alabcala").Dump();
	Palindromize("test").Dump();
	Palindromize("already").Dump();
	Palindromize("somebigbigword").Dump();
	Palindromize("somebigbigwordqweqwqwe").Dump();
	Palindromize("somebigbigwordfortesting").Dump();
	
	//Palindromize_V2("somebigbigwordqweqwqwe").Count().Dump();
}

// how to make sure to return "first one alphabetically"?
string Palindromize(string s)
{
	var state = new SearchState();
	
	Palindromize(s, state);
	
	state.Results.Count.Dump("found");
	
	int minLen = state.Results.OrderBy(x => x.Length).First().Length;
	
	return state.Results.Where(x => x.Length == minLen).OrderBy(x => x).First();
}

public class SearchState
{
	public HashSet<string> Results = new HashSet<string>();
	public int MinFoundPalindromLen = int.MaxValue;
}

// since at max we insert n chars and they can be one of n existing chars
// possible amount of combinations is less than n^n (which is pretty slow)
void Palindromize(string current, SearchState state)
{
	// optimization
	if (current.Length > state.MinFoundPalindromLen)
		return; // suboptimal already
	
	int l = 0;
	int r = current.Length - 1;

	while (r - l >= 1 && current[l] == current[r])
	{
		l += 1;
		r -= 1;
	}

	if (current[l] != current[r])
	{
		// we can either insert letter to the left or to the right
	 	// try both

		Palindromize(current.Insert(l, current[r].ToString()), state);
		Palindromize(current.Insert(r + 1, current[l].ToString()), state);
	}
	else // got a full palindrom
	{
		state.Results.Add(current);
		state.MinFoundPalindromLen = Math.Min(state.MinFoundPalindromLen, current.Length);
	}
}

IEnumerable<string> Palindromize_V2(string s)
{
	return Palindromize_V2(s, 0, s.Length - 1);
}

// recursively returns all palindroms for given string
// not faster than V1 at all (!)
IEnumerable<string> Palindromize_V2(string s, int l, int r)
{
	if (r - l <= 1)
		return new[] { s.Substring(l, r - l) };
		
	char left = s[l];
	char right = s[r];
	

	if (left == right)
	{
		return Palindromize_V2(s, l + 1, r - 1).Select(x => $"{left}{x}{right}").ToArray();
	}
	else
	{
		// chars different, try both ways
		List<string> results = new List<string>();

		// use char from right on the left side
		results.AddRange(Palindromize_V2(s, l, r - 1).Select(x => $"{right}{x}{right}"));

		// use char from left on the right side
		results.AddRange(Palindromize_V2(s, l + 1, r).Select(x => $"{left}{x}{left}"));
		
		return results;
	}
}