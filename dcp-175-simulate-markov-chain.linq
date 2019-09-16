<Query Kind="Program" />

// This problem was asked by Google.
// 
// You are given a starting state start, a list of transition probabilities
// for a Markov chain, and a number of steps num_steps. Run the Markov chain
// starting from start for num_steps and compute the number of times we visited each state.
// 
// For example, given the starting state a, number of steps 5000, and the
// following transition probabilities:
// 
// [
//   ('a', 'a', 0.9),
//   ('a', 'b', 0.075),
//   ('a', 'c', 0.025),
//   ('b', 'a', 0.15),
//   ('b', 'b', 0.8),
//   ('b', 'c', 0.05),
//   ('c', 'a', 0.25),
//   ('c', 'b', 0.25),
//   ('c', 'c', 0.5)
// ]
//
// One instance of running this Markov chain might produce { 'a': 3012, 'b': 1656, 'c': 332 }.

void Main()
{
	Simulate(new []
	{
		Tuple.Create("a", "a", 0.9),
		Tuple.Create("a", "b", 0.075),
		Tuple.Create("a", "c", 0.025),
		Tuple.Create("b", "a", 0.15),
		Tuple.Create("b", "b", 0.8),
		Tuple.Create("b", "c", 0.05),
		Tuple.Create("c", "a", 0.25),
		Tuple.Create("c", "b", 0.25),
		Tuple.Create("c", "c", 0.5),
	}, 5000).Dump();
}

Dictionary<string, int> Simulate(IEnumerable<Tuple<string, string, double>> probabilities, int n)
{
	var map = probabilities
		.GroupBy(x => x.Item1)
		.ToDictionary(g => g.Key, g => g.Select(x =>
			new
			{
				Target = x.Item2,
				P = x.Item3,
			}).ToArray());
	
	Dictionary<string, int> counter = new Dictionary<string, int>();
	
	string state = "a";
	
	var rnd = new Random();
	
	for (int i = 0; i < n; i++)
	{
		double r = rnd.NextDouble();
		double c = 0;

		var targets = map[state];

		for (int j = 0; j < targets.Length; j++)
		{
			c += targets[j].P;
			
			if (r <= c)
			{
				state = targets[j].Target;
				break;
			}
		}
		
		if (!counter.ContainsKey(state))
			counter[state] = 0;
		
		counter[state] += 1;
	}
	
	return counter;
}
