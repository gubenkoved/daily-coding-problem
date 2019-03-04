<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// Given an unordered list of flights taken by someone, each represented
// as (origin, destination) pairs, and a starting airport, compute the
// person's itinerary. If no such itinerary exists, return null. If there
// are multiple possible itineraries, return the lexicographically smallest
// one. All flights must be used in the itinerary.
// 
// For example, given the list of flights [('SFO', 'HKO'), ('YYZ', 'SFO'),
// ('YUL', 'YYZ'), ('HKO', 'ORD')] and starting airport 'YUL', you should
// return the list ['YUL', 'YYZ', 'SFO', 'HKO', 'ORD'].
// 
// Given the list of flights [('SFO', 'COM'), ('COM', 'YYZ')] and starting
// airport 'COM', you should return null.
// 
// Given the list of flights [('A', 'B'), ('A', 'C'), ('B', 'C'), ('C', 'A')]
// and starting airport 'A', you should return the list ['A', 'B', 'C', 'A', 'C']
// even though ['A', 'C', 'A', 'B', 'C'] is also a valid itinerary.
// However, the first one is lexicographically smaller.

void Main()
{
	// we can use regualar backtracking depth first search to solve this one...

	Decipher("YUL", new []
	{
		Tuple.Create("SFO", "COM"),
		Tuple.Create("COM", "YYZ"),
		Tuple.Create("YUL", "YYZ"),
		Tuple.Create("HKO", "ORD"),
	}).Dump();

	Decipher("A", new[]
	{
		Tuple.Create("A", "B"),
		Tuple.Create("A", "C"),
		Tuple.Create("B", "C"),
		Tuple.Create("C", "A"),
	}).Dump();

	Decipher("SRT", new[]
	{
		Tuple.Create("SRT", "SFO"),
		Tuple.Create("SFO", "JFK"),
		Tuple.Create("JFK", "SFO"),
		Tuple.Create("SFO", "SRT"),
		// adding a loop!
		Tuple.Create("JFK", "LAX"),
		Tuple.Create("LAX", "JFK"),
	}).Dump();
}

string[] Decipher(string start, IEnumerable<Tuple<string, string>> flights)
{
	var itineraries = new List<string[]>();

	Worker(flights.ToList(), new HashSet<Tuple<string, string>>(), new List<string>() { start }, itineraries);
	
	Util.Metatext($"Found {itineraries.Count} possibility(-ies):").Dump();
	
	itineraries.ForEach(itinerary =>
		Util.Metatext("\t" + string.Join(" -> ", itinerary)).Dump());
	
	if (!itineraries.Any())
		return null;
	
	return itineraries.OrderBy(x => string.Join("", x)).First();
}

void Worker(List<Tuple<string, string>> flights, HashSet<Tuple<string, string>> used, List<string> currentItinerary, List<string[]> itineraries)
{
	if (flights.Count == used.Count)
	{
		itineraries.Add(currentItinerary.ToArray());
		return;
	}
	
	string current = currentItinerary.Last();
		
	foreach (Tuple<string, string> flight in flights)
	{
		if (used.Contains(flight))
			continue;
		
		bool canTry = flight.Item1 == current;
		
		if (canTry)
		{
			used.Add(flight);
			
			// all the destination to the itinerary
			currentItinerary.Add(flight.Item2);
			
			// we need to go deeper!
			Worker(flights, used, currentItinerary, itineraries);
			
			// restore state
			currentItinerary.RemoveAt(currentItinerary.Count - 1);
			
			used.Remove(flight);
		}
	}
}