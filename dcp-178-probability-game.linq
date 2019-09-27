<Query Kind="Program" />

// This problem was asked by Two Sigma.
// 
// Alice wants to join her school's Probability Student Club. Membership dues are computed via one of two simple probabilistic games.
// 
// The first game: roll a die repeatedly. Stop rolling once you get a five followed by a six. Your number of rolls is the amount you pay, in dollars.
// 
// The second game: same, except that the stopping condition is a five followed by a five.
// 
// Which of the two games should Alice elect to play? Does it even matter? Write a program to simulate the two games and calculate their expected value.

void Main()
{
	// holy shit, I thought it will come out as equal...
	// why on earth it's different, does not it mean that conditional probability of 
	// 6 after 5 is different from probability of 5 after 5?! :'(
	
	Expectation(FirstGame).Dump("first (5, 6)");
	Expectation(SecondGame).Dump("second (5, 5)");

	// even in simplest cases math. expectation is one hell of a thing to compute!
	// https://www.wolframalpha.com/input/?i=sum+%28n%2B1%29%285%2F6%29%5En%2C+n%3D0+to+infinity
	Expectation(() => Game(new[] { 1, })).Dump("win [1]");
}

Random _rnd = new Random();

int Dice()
{
	return _rnd.Next() % 6 + 1;
}

double Expectation(Func<int> game, int n = 100000)
{
	Dictionary<int, int> freq = new Dictionary<int, int>();

	for (int i = 0; i < n; i++)
	{
		int r = game();
		
		if (!freq.ContainsKey(r))
			freq[r] = 0;
		
		freq[r] += 1;
	}
	
	freq.Chart(x => x.Key, x => x.Value, LINQPad.Util.SeriesType.Column).Dump();
	
	return freq.Sum(x => x.Key * x.Value) / (double) n;
}

int Game(int[] win)
{
	int roll = 0;

	List<int> history = new List<int>();

	while (true)
	{
		roll += 1;

		int cur = Dice();
		
		history.Add(cur);
		
		if (history.Count > win.Length)
			history.RemoveAt(0);

		if (history.Count == win.Count() && Enumerable.SequenceEqual(history, win))
			break;
	}

	return roll;
}

int FirstGame() => Game(new[] { 5, 6 });

int SecondGame() => Game(new[] { 5, 5 });