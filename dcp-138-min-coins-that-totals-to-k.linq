<Query Kind="Program" />

// This problem was asked by Google.
// 
// Find the minimum number of coins required to make n cents.
// 
// You can use standard American denominations, that is, 1¢, 5¢, 10¢, and 25¢.
// 
// For example, given n = 16, return 3 since we can make it with a 10¢, a 5¢, and a 1¢.

void Main()
{
	// it looks like this SPECIFIC atoms setup can be solved with
	// a gready algorithm... however in a general case
	// greedy algorithm will sometimes lead sub-optimal solution
	// I think this is NP-complete problem without efficient solution...
	// so the solution would be just brute force with some optimizations may be...
	
	//Func<int[], int, int> solver = MinCoins;
	//Func<int[], int, int> solver = MinCoinsBruteForce;
	Func<int[], int, int> solver = MinCoinsBruteForceV2;
	
	int[] atoms = new int[] { 1, 5, 10, 25 };
	
	solver(atoms, 16).Dump("3"); // 1x10 + 1x5 + 1x1
	solver(atoms, 26).Dump("2"); // 1x25 + 1x1
	solver(atoms, 39).Dump("6"); // 1x25 + 1x10 + 4x4
	
	// counter examples! greedy won't pass!
	
	int[] atoms2 = new int[] { 1, 12, 40, 50 };
	
	solver(atoms2, 99).Dump("6 (greedy will give 11)"); // optimal: 1x50 + 4x12 + 1x1; greedy: 1x50 + 1x40 + 9x1 -> 11

	int[] atoms3 = new int[] { 1, 1000, 9001 };

	solver(atoms3, 10000).Dump("10 (greedy will give 1'000!)"); // optimal: 10x1000; greedy: 1x9001 + 999x1 -> 1000

	int[] atoms4 = new int[] { 1, 100000, 9000001 };

	solver(atoms4, 1000000).Dump("10 (greedy will give 100'000!)");

	solver(new int[] { 2 }, 3).Dump("-1 (meaning impossible)");
	solver(new int[] { 2, 4, }, 100001).Dump("-1 (meaning impossible)");
}

int MinCoins(int[] atoms, int k)
{
	int[] sortedAtoms = atoms.OrderByDescending(x => x).ToArray();
	
	int reminder = k;
	
	int coins = 0;
	
	while (reminder != 0)
	{
		for (int atomIdx = 0; atomIdx < atoms.Length; atomIdx++)
		{
			int atom = sortedAtoms[atomIdx];
			
			while (reminder >= atom)
			{
				reminder -= atom;
				coins += 1;
			}
		}
	}
	
	return coins;
}

int MinCoinsBruteForce(int[] atoms, int k)
{
	return MinCoinsBruteForce(atoms, k, 0);
}

// this version will cause stack overflows as it uses recursion
// I should rewrite this one to use Stack explicitly to mitigate that...
int MinCoinsBruteForce(int[] atoms, int k, int currentCoins)
{
	if (k == 0)
		return currentCoins;

	if (k < 0)
		return -1;

	int min = -1;
	
	foreach (int atom in atoms)
	{
		if (atom > k)
			continue;
		
		int localMin = MinCoinsBruteForce(atoms, k - atom, currentCoins + 1);
		
		if (localMin == -1)
			continue; // no solution
		
		if (min == -1 || localMin < min)
			min = localMin;
	}
	
	return min;
}

class State
{
	public int Reminder;
	public int CurrentCoins;
}

// very slow... but it does not use Call Stack heavily
// O(very bad)
int MinCoinsBruteForceV2(int[] atoms, int k)
{
	// sort from big to small
	atoms = atoms.OrderByDescending(x => x).ToArray();
	
	int globalMin = -1;
	
	var stack = new Stack<State>();

	stack.Push(new State() { Reminder = k, CurrentCoins = 0 });
	
	while (stack.Count > 0)
	{
		// there is something to check!
		
		State state = stack.Pop();

		List<int> workedAtoms = new List<int>();

		foreach (int atom in atoms)
		{
			if (atom > state.Reminder)
				continue; // unusable
			
			// atom is usable there!
			
			// OPTIMIZATION: If we used some atom we can skip usage of other atoms that LCM (least common multiple) = atom
			// (for instance if we have 10 and 1 as atoms and we can use 10, there is no need in tring 10x1 as in the solution it can be
			// optimized by the replacement 10x1 -> 1x10

			bool prune = false;

			if (workedAtoms.Any())
			{
				// see if we can prune!
				foreach (var worked in workedAtoms)
				{
					// there is alredy an atom that worked that can be divided
					// w/o a reminder to the current atom
					// so, there is no way in trying it in this round at least
					if (worked % atom == 0)
					{
						// skip!
						//Util.Metatext("Prune!").Dump();
						prune = true;
						break;
					}
				}
			}
			
			if (prune)
				continue;
			
			workedAtoms.Add(atom);
			
			if (atom == state.Reminder)
			{
				// found a solution!
				int currentMin = state.CurrentCoins + 1;
				
				if (globalMin == -1 || currentMin < globalMin)
					globalMin = currentMin;
			}
			
			// try something out!
			stack.Push(new State(){ Reminder = state.Reminder - atom, CurrentCoins = state.CurrentCoins + 1 });
		}
	}
	
	return globalMin;
}