<Query Kind="Program" />

// This problem was asked by Stripe.
// 
// Write a map implementation with a get function that
// lets you retrieve the value of a key at a particular time.
// 
// It should contain the following methods:
// 
// set(key, value, time): sets key to value for t = time.
// get(key, time): gets the key at t = time.
// The map should work like this. If we set a key at a particular
// time, it will maintain that value forever or until it gets set
// at a later time. In other words, when we get a key at a time,
// it should return the value that was set for that key set at the
// most recent time.
// 
// Consider the following examples:
// 
// d.set(1, 1, 0) # set key 1 to value 1 at time 0
// d.set(1, 2, 2) # set key 1 to value 2 at time 2
// d.get(1, 1) # get key 1 at time 1 should be 1
// d.get(1, 3) # get key 1 at time 3 should be 2
// d.set(1, 1, 5) # set key 1 to value 1 at time 5
// d.get(1, 0) # get key 1 at time 0 should be null
// d.get(1, 10) # get key 1 at time 10 should be 1
// d.set(1, 1, 0) # set key 1 to value 1 at time 0
// d.set(1, 2, 0) # set key 1 to value 2 at time 0
// d.get(1, 0) # get key 1 at time 0 should be 2

void Main()
{
	var map = new HistoricalMap();
	
	map.Set("1", 1, 0);
	map.Set("1", 2, 2);
	map.Get("1", 1).Dump("1");
	map.Get("1", 3).Dump("2");
	map.Set("1", 1, 5);
	map.Get("1", -1).Dump("null");
	map.Get("1", 10).Dump("1");
	map.Set("1", 1, 0);
	map.Set("1", 2, 0);
	map.Get("1", 0).Dump("2");
}

public class HistoricalMap
{
	private class Holder
	{
		public int Time { get; private set; }
		public object Value { get; private set; }
		
		public Holder(int time, object value)
		{
			Time = time;
			Value = value;
		}
	}
	
	private Dictionary<string, List<Holder>> _data = new Dictionary<string, List<Holder>>();
	
	// O(k) where k list count per key
	public void Set(string key, object value, int time)
	{
		List<Holder> list;
		
		if (_data.ContainsKey(key)) // O(1)
		{
			list = _data[key];
		}
		else
		{
			list = new List<Holder>();
			_data[key] = list;
		}
		
		// delete from list the values timed with the same timestamp
		list.RemoveAll(x => x.Time == time); // O(n)
		
		// add new value
		list.Add(new Holder(time, value)); // O(1)
	}
	
	// O(k) where k list count per key
	public object Get(string key, int time)
	{
		if (!_data.ContainsKey(key)) // O(1)
			return null;
			
		List<Holder> list = _data[key];
		
		Holder closest = null;
		
		foreach (Holder timedValue in list) // O(n) where n is list count
		{
			if (timedValue.Time > time)
				continue;

			if (closest == null)
			{
				closest = timedValue;
				continue;
			}
				
			if ((time - timedValue.Time) < (time - closest.Time))
				closest = timedValue;
		}
		
		return closest?.Value;
	}
}