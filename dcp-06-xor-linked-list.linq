<Query Kind="Program">
  <Namespace>System.Runtime.InteropServices</Namespace>
</Query>

// This problem was asked by Google.
// 
// An XOR linked list is a more memory efficient doubly linked list.
// Instead of each node holding next and prev fields, it holds a
// field named both, which is an XOR of the next node and the previous node.
// Implement an XOR linked list; it has an add(element) which adds the
// element to the end, and a get(index) which returns the node at index.
// 
// If using a language that has no pointers (such as Python),
// you can assume you have access to get_pointer and dereference_pointer
// functions that converts between nodes and memory addresses.

void Main()
{
	// there is no way to implement this fully in C#
	// pinned pointers can not point to reference types as they are
	// GC managed and subject for move
	
	var list = new XorLinkedList<int>();
	
	list.Add(1);
	
	Memory.Dump();
	list.Dump();
	
	list.Add(2);
	
	Memory.Dump();
	list.Dump();
	
	list.Add(3);
	
	Memory.Dump();
	list.Dump();
	
	list.Add(4);
	
	Memory.Dump();
	list.Dump();
	
	list.Get(0).Dump();
	list.Get(1).Dump();
	list.Get(2).Dump();
	list.Get(3).Dump();
}

public class XorLinkedList<T>
{
	private class Node
	{
		public T Value;
		public int XorPointer;
	}
	
	private int _headPointer;
	private int _tailPointer;

	public void Dump()
	{
		Util.Metatext("*** Dumpling list ***").Dump();
		
		Node current = Memory.Dereference<Node>(_headPointer);
		int currentAddress = _headPointer;
		int prevAddress = 0;

		while (current != null)
		{
			Util.Metatext($"\taddress: {currentAddress}, val: {current.Value}, xor: {current.XorPointer}").Dump();
			
			if (current.XorPointer == 0)
				break;
			
			// iterate step forwrad
			int nextAddress = current.XorPointer ^ prevAddress;
			
			prevAddress = currentAddress;
			
			if (nextAddress == 0)
				break;

			current = Memory.Dereference<Node>(nextAddress);
			currentAddress = nextAddress;
		}
		
		"".Dump();
	}

	public T Get(int index)
	{
		Node current = Memory.Dereference<Node>(_headPointer);
		int currentAddress = _headPointer;
		int prevAddress = 0;
		
		while (index > 0)
		{
			// iterate step forwrad
			int nextAddress = current.XorPointer ^ prevAddress;
			
			if (nextAddress == 0)
				return default(T);
			
			prevAddress = currentAddress;
			
			current = Memory.Dereference<Node>(nextAddress);
			currentAddress = nextAddress;
			
			index -= 1;
		}
		
		return current.Value;
	}
	
	public void Add(T element)
	{
		Node newNode = new Node()
		{
			Value = element,
		};

		if (_tailPointer == 0)
		{
			// list is empty, add the fist node
			int address = Memory.GetAdderss(newNode);
			
			_headPointer = address;
			_tailPointer = address;
			
			return;
		}
		
		// okay list is not empty
		newNode.XorPointer = _tailPointer; // points to the preivous
		
		Node oldTail = Memory.Dereference<Node>(_tailPointer);
		
		_tailPointer = Memory.GetAdderss(newNode);
		
		oldTail.XorPointer ^= _tailPointer;
	}
}

public static class Memory
{
	private static Dictionary<int, object> _memory = new Dictionary<int, object>();
	
	private static Random _rnd = new Random();
	
	public static int GetAdderss(object o)
	{
		// see if object in the map
		
		if (_memory.ContainsValue(o))
			return _memory.First(x => x.Value == o).Key;
		
		int address = 0; 
		
		do
		{
			address = _rnd.Next();
		}
		while (_memory.ContainsKey(address));
		
		_memory[address] = o;
		
		return address;
	}
	
	public static T Dereference<T>(int address)
	{
		if (!_memory.ContainsKey(address))
			throw new Exception($"SegFault! nothing at {address}");
			
		return (T)_memory[address];
	}
	
	public static void Dump()
	{
		Util.Metatext("*** Dumpling memory ***").Dump();
		
		foreach (var item in _memory)
			Util.Metatext($"{item.Key,16}: {item.Value}").Dump();
			
		"".Dump();
	}
}
