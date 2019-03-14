<Query Kind="Program" />

// This problem was asked by Google.
// 
// Implement an LRU (Least Recently Used) cache. It should be
// able to be initialized with a cache size n, and contain
// the following methods:
// 
// set(key, value): sets key to value. If there are already n
// items in the cache and we are adding a new item, then it
// should also remove the least recently used item.
//
// get(key): gets the value at key. If no such key exists,
// return null.
//
// Each operation should run in O(1) time.

void Main()
{
	// rats! max suggested a solution in 5 minutes w/o me trying it enough :'(
	
	var cache = new LruCache(4);
	
	cache.Get("does not exist").Dump("null");
	
	cache.Set("a", 1);
	cache.Set("b", 2);
	cache.Set("c", 3);
	cache.Set("d", 4);
	
	// causes eviction of a
	cache.Set("e", 5);
	cache.Get("a").Dump("null");
	
	// update lru on b
	cache.Get("b").Dump("2");

	// cause eviction of c
	cache.Set("f", 6);
	cache.Get("c").Dump("null");
	
	// ensure b is still there
	cache.Get("b").Dump("2");
}

public class LruCache
{
	private class Item
	{
		public string Key;
		public object Value;
	}
	
	private Dictionary<string, LinkedListNode<Item>> _hashset;
	private LinkedList<Item> _list;
	private int _size;

	public LruCache(int size)
	{
		_hashset = new Dictionary<string, LinkedListNode<Item>>();
		_list = new LinkedList<UserQuery.LruCache.Item>();
		_size = size;
	}

	public void Set(string key, object value)
	{
		// add item to the head of the linked list
		// add hashset reference to the linked list node
		// evict from the tail if needed
		
		var node = _list.AddFirst(new Item()
		{
			Key = key,
			Value = value,
		});
		
		// O(1)
		_hashset[key] = node;
		
		if (_hashset.Count > _size)
		{
			// evict last recently used -- the last element in the linked list
			var nodeToBeEvicted = _list.Last; // O(1)
			
			// O(1)
			_hashset.Remove(nodeToBeEvicted.Value.Key);
			
			// O(1)
			_list.RemoveLast();
		}
	}
	
	public object Get(string key)
	{
		// find element in hashset
		// move element to the head of the linked list
		
		if (!_hashset.ContainsKey(key))
			return null;
			
		var node = _hashset[key];

		// move node to the head of the list
		_list.Remove(node); // O(1)
		
		var updatedNode = _list.AddFirst(node.Value); // O(1)

		// update the node reference in map
		_hashset[key] = updatedNode;
		
		return node.Value.Value;
	}
}
