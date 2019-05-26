<Query Kind="Program" />

// This problem was asked by Google.
// 
// Implement an LFU (Least Frequently Used) cache. It should be
// able to be initialized with a cache size n, and contain the
// following methods:
// 
// set(key, value): sets key to value. If there are already n
// items in the cache and we are adding a new item, then it should
// also remove the least frequently used item. If there is a tie,
// then the least recently used key should be removed.
//
// get(key): gets the value at key. If no such key exists, return null.
// Each operation should run in O(1) time.

void Main()
{
	var lfu = new LfuCache(4);
	
	lfu.Set("a", 1);
	lfu.Set("b", 2);
	lfu.Set("c", 3);
	lfu.Set("d", 4);
	
	// use a, then b
	lfu.Get("a").Dump("a:1");
	lfu.Get("b").Dump("b:2");
	
	// add new element cause eviction of c as last recently used amont freq = 0
	lfu.Set("e", 5);
	
	// use e two times
	lfu.Get("e");
	lfu.Get("e");
	
	// verify c is not there anymore
	lfu.Get("c").Dump("c:null");
	
	// add one more, evict d
	lfu.Set("f", 6);

	// use f two times
	lfu.Get("f");
	lfu.Get("f");

	// verify
	lfu.Get("d").Dump("d:null");
	
	// one more, verify that a to be evicted
	lfu.Set("g", 7);
	
	lfu.Get("a").Dump("a:null");
	
	// stats!
	lfu.PrintStats();
}

public class LfuCache
{
	private class CacheItem
	{
		public string Key { get; }
		public object Value { get; set; }
		
		public CacheItem(string key, object value)
		{
			Key = key;
			Value = value;
		}
	}

	private class Bucket
	{
		public int Frequency { get; }
		public LinkedList<CacheItem> Items { get; }
		
		public Bucket(int freq)
		{
			Frequency = freq;
			Items = new LinkedList<CacheItem>();
		}
	}
	
	private class Locator
	{
		public LinkedListNode<Bucket> BucketNode { get; set; }
		public LinkedListNode<CacheItem> ItemNode { get; set; }
	}
	
	private int _capacity;
	private Dictionary<string, Locator> _lookup;
	private LinkedList<Bucket> _buckets;
	
	public LfuCache(int capacity)
	{
		_capacity = capacity;
		_lookup = new Dictionary<string, Locator>();
		_buckets = new LinkedList<Bucket>();
	}
	
	public void Set(string key, object value)
	{
		// algorithm
		// add item to the first 0 freq bucket to the head of it

		if (!_lookup.ContainsKey(key))
		{
			// evict if capacity exceeded
			if (_lookup.Count >= _capacity)
				EvictOne();

			LinkedListNode<Bucket> targetBucketNode;
			
			if (_buckets.First == null || _buckets.First.Value.Frequency != 0)
			{
				targetBucketNode = _buckets.AddFirst(new Bucket(0));
			} else
			{
				// just use already existing bucket
				targetBucketNode = _buckets.First; // O(1)
			}
				
			// add new node as the last item
			LinkedListNode<CacheItem> itemNode = targetBucketNode.Value.Items.AddLast(new CacheItem(key, value));
			
			_lookup[key] = new Locator()
			{
				ItemNode = itemNode,
				BucketNode = targetBucketNode,
			};
			
			
		} else
		{
			// such key already in the cache, update value
			Locator locator = _lookup[key]; // O(1)
			
			// update the value
			locator.ItemNode.Value.Value = value;
		}
	}
	
	public object Get(string key)
	{
		// algorithm
		// find the item by lookup for O(1)
		// move item from the bucket into the next bucket and create it if does not exist O(1)
		
		if (!_lookup.ContainsKey(key)) // O(1)
			return null;
			
		Locator locator = _lookup[key]; // O(1)
		
		IncreaseFrequency(locator); // O(1)
		
		return locator.ItemNode.Value.Value;
	}
	
	public void PrintStats()
	{
		$"*** LFU stats ***".Dump();
		$"	Capacity: {_capacity}".Dump();
		$"	Buckets:".Dump();
		
		
		foreach (var bucket in _buckets)
		{
			$"		Freq: {bucket.Frequency}:".Dump();
			
			foreach (var item in bucket.Items)
				$"			{item.Key}: {item.Value}".Dump();
		}
	}
	
	// O(1)
	private void IncreaseFrequency(Locator locator)
	{
		CacheItem cacheItem = locator.ItemNode.Value;
		LinkedListNode<Bucket> bucketNode = locator.BucketNode;
		LinkedListNode<Bucket> nextBucketNode = locator.BucketNode.Next;

		LinkedListNode<Bucket> targetBucketNode;

		if (nextBucketNode == null || nextBucketNode.Value.Frequency != bucketNode.Value.Frequency + 1)
		{
			// insert new bucket after the current one
			targetBucketNode = _buckets.AddAfter(bucketNode, new Bucket(bucketNode.Value.Frequency + 1)); // O(1)
		}
		else
		{
			// looks like next bucket is fine to use!
			targetBucketNode = nextBucketNode;
		}

		// add to target bucket new node to the tail
		LinkedListNode<CacheItem> updateItemNode = targetBucketNode.Value.Items.AddLast(cacheItem); // O(1)

		// remove item from the current bucket
		bucketNode.Value.Items.Remove(locator.ItemNode); // O(1)
		
		// if empty will become empty, remove it altogether
		if (bucketNode.Value.Items.First == null)
			_buckets.Remove(bucketNode);

		locator.BucketNode = targetBucketNode;
		locator.ItemNode = updateItemNode;
	}

	private void EvictOne()
	{
		// evicts first item from the first bucket
		LinkedListNode<Bucket> firstBucketNode = _buckets.First; // O(1)
		
		LinkedListNode<CacheItem> itemNode = firstBucketNode.Value.Items.First;

		// we need to maintain the invariant of buckets being NOT empty!
		if (itemNode == null)
			throw new Exception("there are not items in the first bucket!");

		Util.Metatext($"Evicting element with key {itemNode.Value.Key} (freq: {firstBucketNode.Value.Frequency})").Dump();
		
		firstBucketNode.Value.Items.RemoveFirst(); // O(1)
		
		// if empty will become empty, remove it altogether
		if (firstBucketNode.Value.Items.First == null)
			_buckets.Remove(firstBucketNode);
		
		_lookup.Remove(itemNode.Value.Key); // O(1)
	}
}
