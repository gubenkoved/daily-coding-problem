<Query Kind="Program" />

// This problem was asked by Microsoft.
// 
// Implement a URL shortener with the following methods:
// 
// shorten(url), which shortens the url into a six-character
// alphanumeric string, such as zLg6wl.
// restore(short), which expands the shortened string into
// the original url. If no such shortened string exists, return null.
//
// Hint: What if we enter the same URL twice?

void Main()
{
	var s = new UrlShortener();

	string s1 = s.Shorten("https://google.com").Dump();
	string s2 = s.Shorten("https://yandex.ru").Dump();

	s.Restore(s1).Dump();
	s.Restore(s2).Dump();

	"\nLet's generate them all!\n".Dump();
	
	for (int i = 0; i < 10000; i++)
		s.Shorten("https://yandex.ru").Dump();
}

public class UrlShortener
{
	private Dictionary<string, string> _map = new Dictionary<string, string>();
	private Random _rand = new Random();
	private const int _len = 6;
	private char[] _alphabet;
	
	public UrlShortener()
	{
		_alphabet = GenerateAlphabet();
	}
	
	public string Shorten(string url)
	{
		// generate shortcut
		string shortcut;
		
		int round = 0;
		do
		{
			if (round > 3)
				throw new Exception($"Unable to allocate new shortcut after {round} rounds -- data is too dense, increase the len!");

			round += 1;

			if (round > 1)
				Util.Metatext($"Generating shortcut -- round {round}").Dump();
			
			shortcut = GenerateShortcut(_len);
			
		} while (_map.ContainsKey(shortcut));
		
		_map[shortcut] = url;
		
		return shortcut;
	}
	
	public string Restore(string shortcut)
	{
		if (!_map.ContainsKey(shortcut))	
			return null;
			
		return _map[shortcut];
	}
	
	private char[] GenerateAlphabet()
	{
		List<char> alphabet = new List<char>();
		
		for (char c = 'a'; c <= 'z'; c++)
		{
			alphabet.Add(c);
			alphabet.Add(char.ToUpperInvariant(c));
		}
		
		for (char c = '0'; c < '9'; c++)
			alphabet.Add(c);
		
		return alphabet.ToArray();
	}
	
	private string GenerateShortcut(int n)
	{
		string s = string.Empty;
		
		for (int i = 0; i < n; i++)
			s += _alphabet[_rand.Next() % _alphabet.Length];
		
		return s;
	}
}
