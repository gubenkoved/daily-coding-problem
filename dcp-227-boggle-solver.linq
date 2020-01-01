<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>System.Net.Http</Namespace>
</Query>

// This problem was asked by Facebook.
// 
// Boggle is a game played on a 4 x 4 grid of letters. The goal is to
// find as many words as possible that can be formed by a sequence of
// adjacent letters in the grid, using each cell at most once. Given a
// game board and a dictionary of valid words, implement a Boggle solver.

void Main()
{
	var dic = GetDictionary();

	BoggleSolver(new[,]
	{
		{ 'a', 'c', 'r', 'o' },
		{ 'r', 'c', 'o', 'b' },
		{ 'c', 'c', 'o', 'a' },
		{ 'a', 'b', 'o', 't' },
	}, dic).Dump();

	BoggleSolver(new[,]
	{
		{ 'a', 'c', 'r', 'o' },
		{ 'q', 'q', 'q', 'b' },
		{ 'q', 'q', 'q', 'a' },
		{ 'q', 'q', 'q', 't' },
	}, dic).Dump();
}

IEnumerable<string> GetDictionary()
{
	string url = "https://raw.githubusercontent.com/dwyl/english-words/master/words_alpha.txt";
	
	HttpClient client = new HttpClient();
	
	Util.Metatext("downloading the dictionary...").Dump();
	
	string resultContent = client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;

	IEnumerable<string> dic = resultContent
		.Split(new[] { '\r', '\n'})
		.Select(x => x.ToLowerInvariant())
		.Where(x => x.Length >= 3) // take onle ones which are more than 3 chars
		.ToArray();
	
	Util.Metatext($"downloaded {dic.Count()} words").Dump();
	
	return dic;
}

IEnumerable<string> BoggleSolver(char[,] grid, IEnumerable<string> dictionary)
{
	HashSet<string> found = new HashSet<string>();

	for (int i = 0; i < grid.GetLength(0); i++)
	{
		for (int j = 0; j < grid.GetLength(1); j++)
		{
			Util.Metatext($"starting at ({i}, {j})").Dump();
			
			Find(grid, string.Empty, new HashSet<Tuple<int, int>>(), i, j, dictionary, found);

			Util.Metatext($"  currently found {found.Count} words").Dump();
		}
	}
	
	return found;
}

// recoursive search function, tries to see if it's possible to find any words from dictionary
// given it is must to take the letter on (i, j) and current prefix
void Find(char[,] grid, string prev, HashSet<Tuple<int, int>> used, int i, int j, IEnumerable<string> candidates, HashSet<string> found)
{
	string current = prev + grid[i, j];

	IEnumerable<string> newCandidates = candidates.Where(x => x.StartsWith(current)).ToArray();

	// no need to branch further!
	if (!newCandidates.Any())
		return;
	
	// add the word if it is present in the dictionary
	if (newCandidates.Contains(current))
		found.Add(current);
	
	// okay, looks like we should continue!
	// try to consume the letter
	var taken = Tuple.Create(i, j);
	used.Add(taken);

	List<Tuple<int, int>> next = new List<Tuple<int, int>>()
	{
		Tuple.Create(i, j - 1),
		Tuple.Create(i, j + 1),
		Tuple.Create(i - 1, j),
		Tuple.Create(i + 1, j),
	};
	
	foreach (var nextCoordinate in next)
	{
		if (!(nextCoordinate.Item1 >= 0 && nextCoordinate.Item2 >= 0 && nextCoordinate.Item1 < grid.GetLength(0) && nextCoordinate.Item2 < grid.GetLength(1)))
			continue; // invalid!
			
		if (used.Contains(nextCoordinate))
			continue; // already taken!
		
		// can try!
		Find(grid, current, used, nextCoordinate.Item1, nextCoordinate.Item2, newCandidates, found);
	}

	//Find(grid, newCurrent,

	// restore the state after we are back
	used.Remove(taken);
}


