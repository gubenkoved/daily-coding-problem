<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>System.Net.Http</Namespace>
</Query>

// This problem was asked by Pivotal.
// 
// A step word is formed by taking a given word, adding a letter,
// and anagramming the result. For example, starting with the word
// "APPLE", you can add an "A" and anagram to get "APPEAL".
// 
// Given a dictionary of words and an input word, create a function that returns all valid step words.



void Main()
{
	IEnumerable<string> dic = GetDic();
	
	$"downloaded dictionary of {dic.Count()} words".Dump();
	
	StepWords(dic, "apple").Dump();
}

IEnumerable<string> GetDic()
{
	var httpClient = new HttpClient();
	
	return httpClient.GetAsync("https://github.com/dwyl/english-words/raw/master/words.txt").Result.Content
		.ReadAsStringAsync().Result
		.Split('\r', '\n')
		.Select(x => x.ToLowerInvariant());
}

IEnumerable<string> StepWords(IEnumerable<string> dic, string word)
{
	// O(n)
	var lookup = dic.ToLookup(x => Jucify(x));
	
	for (char c = 'a'; c <= 'z'; c++)
	{
		string target = Jucify(word + c);
		
		IEnumerable<string> partial = lookup[target];
		
		foreach (var w in partial)
			yield return w;
	}
}

// extracts the string out of the source word so that
// the end result is the same of the letter counts are the same for two words
string Jucify(string a)
{
	IDictionary<char, int> map = a.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());

	return string.Join("", map.Keys.OrderBy(x => x).Select(x => $"{x}{map[x]}"));
}