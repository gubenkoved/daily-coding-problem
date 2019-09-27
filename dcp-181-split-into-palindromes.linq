<Query Kind="Program" />

// This problem was asked by Google.
// 
// Given a string, split it into as few strings as possible such that each string is a palindrome.
// 
// For example, given the input string racecarannakayak, return ["racecar", "anna", "kayak"].
// 
// Given the input string abc, return ["a", "b", "c"].

void Main()
{
	// the only solution I can currently think of is
	// optimization of the brute force
	
	SplitIntoPalindromes("racecarannakayak").Dump();
	SplitIntoPalindromes("abc").Dump();
	SplitIntoPalindromes("aabcddcba").Dump();
	SplitIntoPalindromes("kwjeffejwjksdffdsbqweracecarannakayaktbefruiwebbewfisbdfuyqweoevyvodfbsiubiwvdpdf").Dump();
}

IEnumerable<string> SplitIntoPalindromes(string s)
{
	var state = new State();
	
	Search(s, new Queue<string>(), 0, state);
	
	return state.Best;
}

public class State
{
	public string[] Best;
}

void Search(string s, Queue<string> consumed, int start, State state)
{
	// find all palindromes starting at startAt (inclusinve)
	// and repeat the search
	
	if (start >= s.Length)
	{
		// all consumed, see if we got any better
		if (state.Best == null || state.Best.Count() > consumed.Count())
			state.Best = consumed.ToArray();
	}
	
	// we did not consume everything, but there is a better split already, so
	// it does not make sense to continue there
	if (state.Best != null && consumed.Count() > state.Best.Count())
		return;
		
	for (int end = s.Length - 1; end >= start; end--)
	{
		if (IsPalindrome(s, start, end))
		{
			// okay, try to take this palindrome!
			consumed.Enqueue(s.Substring(start, end - start + 1));
			
			// dive in!
			Search(s, consumed, end + 1, state);
			
			consumed.Dequeue();
		}
	}
}

bool IsPalindrome(string s, int start, int end)
{
	int n = end - start;
	
	for (int i = start; i <= start + n / 2; i++)
	{
		int j = end - (i - start);
		
		if (s[i] != s[j])
			return false;
	}
	
	return true;
}