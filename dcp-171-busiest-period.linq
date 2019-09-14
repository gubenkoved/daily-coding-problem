<Query Kind="Program" />

// This problem was asked by Amazon.
// 
// You are given a list of data entries that represent entries and exits of
// groups of people into a building. An entry looks like this:
// 
// {"timestamp": 1526579928, count: 3, "type": "enter"}
// 
// This means 3 people entered the building. An exit looks like this:
// 
// {"timestamp": 1526580382, count: 2, "type": "exit"}
// 
// This means that 2 people exited the building. timestamp is in Unix time.
// 
// Find the busiest period in the building, that is, the time with the most
// people in the building. Return it as a pair of (start, end) timestamps.
// You can assume the building always starts off and ends up empty, i.e.
// with 0 people inside.

void Main()
{
	GetBusiestPeriod(new []
	{
		new Event(1, 2, false),
		new Event(2, 3, false),
		new Event(3, 1, true),
		new Event(5, 4, false),
		new Event(10, 9, true),
	}.ToList()).Dump();
}

public class Event
{
	public long Timestamp { get; set; }
	public int Count { get; set; }
	public bool IsExit { get; set; }
	
	public Event(long timestamp, int count, bool isExit)
	{
		Timestamp = timestamp;
		Count = count;
		IsExit = isExit;
	}
}

public (long start, long end) GetBusiestPeriod(IEnumerable<Event> inputEvents)
{
	var events = inputEvents.OrderBy(x => x.Timestamp).ToArray();
	
	int current = 0;
	int startIndex = 0;
	int max = 0;
	
	for (int i = 0; i < events.Count(); i++)
	{
		var @event = events[i];
		
		current += @event.IsExit ? -@event.Count : +@event.Count;
		
		if (current > max)
			startIndex = i;
	}
	
	return (events[startIndex].Timestamp, events[startIndex+1].Timestamp);
}