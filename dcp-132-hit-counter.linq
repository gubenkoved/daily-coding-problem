<Query Kind="Program" />

// This question was asked by Riot Games.
// 
// Design and implement a HitCounter class that keeps track of
// requests (or hits). It should support the following operations:
// 
// record(timestamp): records a hit that happened at timestamp
//
// total(): returns the total number of hits recorded
//
// range(lower, upper): returns the number of hits that occurred
//   between timestamps lower and upper (inclusive)
//
// Follow-up: What if our system has limited memory?

void Main()
{

	// it all depends on what we optimize on
	// if it's speed of adding hits that's one solution
	// if it's Range queries speed, then we must support ordering in inserts
	// but it sacrafies the insert performance...
	
	// as to follow up question: wondering how can we make it better in terms of memory
	// w/o loosing the precision, probably there is no way
	// but if we can loose precision
	// oh, they probably mean to use additional storage via the hard drives...

	var h = new HitCounter();
	
	h.Record(123);
	h.Record(124);
	h.Record(120);
	
	h.Total().Dump("3");
	h.Range(123, 124).Dump("2");
}


public class HitCounter
{
	private List<int> _hits = new List<int>();
	
	public void Record(int timestamp)
	{
		_hits.Add(timestamp);
	}
	
	public int Total()
	{
		return _hits.Count;
	}
	
	public int Range(int low, int high)
	{
		return _hits.Count(x => x >= low && x <= high);
	}
}