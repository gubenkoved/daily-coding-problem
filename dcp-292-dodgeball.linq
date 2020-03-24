<Query Kind="Program" />

// This problem was asked by Twitter.
// 
// A teacher must divide a class of students into two teams to play dodgeball.
// Unfortunately, not all the kids get along, and several refuse to be put on
// the same team as that of their enemies.
// 
// Given an adjacency list of students and their enemies, write an algorithm
// that finds a satisfactory pair of teams, or returns False if none exists.
// 
// For example, given the following enemy graph you should return the teams
// {0, 1, 4, 5} and {2, 3}.
// 
// students = {
//     0: [3],
//     1: [2],
//     2: [1, 4],
//     3: [0, 4, 5],
//     4: [2, 3],
//     5: [3]
// }
//
// On the other hand, given the input below, you should return False.
// 
// students = {
//     0: [3],
//     1: [2],
//     2: [1, 3, 4],
//     3: [0, 2, 4, 5],
//     4: [2, 3],
//     5: [3]
// }

void Main()
{
	DivideIntoTeams(new Dictionary<int, int[]>()
	{
		{ 0, new [] { 3 } },
		{ 1, new [] { 2 } },
		{ 2, new [] { 1, 4 } },
		{ 3, new [] { 0, 4, 5 } },
		{ 4, new [] { 2, 3 } },
		{ 5, new [] { 3 } },
	}).Dump();

	DivideIntoTeams(new Dictionary<int, int[]>()
	{
		{ 0, new [] { 3 } },
		{ 1, new [] { 2 } },
		{ 2, new [] { 1, 3, 4 } },
		{ 3, new [] { 0, 2, 4, 5 } },
		{ 4, new [] { 2, 3 } },
		{ 5, new [] { 3 } },
	}).Dump();

	DivideIntoTeams(new Dictionary<int, int[]>()
	{
		{ 0, new int [] { } },
		{ 1, new int [] { } },
		{ 2, new int [] { } },
		{ 3, new int [] { } },
	}).Dump();

	DivideIntoTeams(new Dictionary<int, int[]>()
	{
		{ 0, new int [] { 1, 2 } },
		{ 1, new int [] { 0, 3 } },
		{ 2, new int [] { 0, 3 } },
		{ 3, new int [] { 1, 2 } },
	}).Dump();

	DivideIntoTeams(new Dictionary<int, int[]>()
	{
		{ 0, new int [] { 1, 2 } },
		{ 1, new int [] { 0, 2 } },
		{ 2, new int [] { 0, 1 } },
	}).Dump();

	DivideIntoTeams(new Dictionary<int, int[]>()
	{
		{ 0, new int [] { 2 } },
		{ 1, new int [] { 3 } },
		{ 2, new int [] { 0, 4 } },
		{ 3, new int [] { 1, 5 } },
		{ 4, new int [] { 2, 5 } },
		{ 5, new int [] { 4, 3 } },
	}).Dump();
}

object DivideIntoTeams(Dictionary<int, int[]> enemies)
{
	IEnumerable<int> teamA;
	IEnumerable<int> teamB;
	
	bool ok = DivideIntoTeams(enemies, out teamA, out teamB);
	
	return new 
	{
		ok,
		teamA,
		teamB,
	};
}

// returns true if division was successful
bool DivideIntoTeams(Dictionary<int, int[]> enemies, out IEnumerable<int> teamA, out IEnumerable<int> teamB)
{
	teamA = null;
	teamB = null;

	Dictionary<int, int> teamMap = new Dictionary<int, int>();
	
	Queue<int> q = new Queue<int>();
	
	int uncategorizedTeam = 0;
	
	foreach (int uncategorizedPlayer in enemies.Keys)
	{
		bool handled = teamMap.ContainsKey(uncategorizedPlayer);
	
		if (handled)
			continue;
			
		// start with not classified player -- put in first empty team
			
		q.Enqueue(uncategorizedPlayer);
		
		teamMap[uncategorizedPlayer] = uncategorizedTeam++ % 2;
		
		while (q.Any())
		{
			int cur = q.Dequeue();
			int curTeam = teamMap[cur];
			int enemyTeam = (curTeam + 1) % 2;
			
			foreach (int enemy in enemies[cur])
			{
				if (teamMap.ContainsKey(enemy))
				{
					// validate compatibility
					if (teamMap[enemy] != enemyTeam)
						return false; // incompatible!
				}
				else
				{
					teamMap[enemy] = enemyTeam;
					q.Enqueue(enemy);
				}
			}
		}
	}
	
	teamA = teamMap.Where(x => x.Value == 0).Select(x => x.Key).ToArray();
	teamB = teamMap.Where(x => x.Value == 1).Select(x => x.Key).ToArray();
	
	return true;
}

