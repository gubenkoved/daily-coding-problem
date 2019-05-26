<Query Kind="Program" />

// This problem was asked by Google.
// 
// Implement locking in a binary tree. A binary tree node can be locked or unlocked only
// if all of its descendants or ancestors are not locked.
// 
// Design a binary tree node class with the following methods:
// 
// is_locked, which returns whether the node is locked
// lock, which attempts to lock the node. If it cannot be locked, then it should return false.
// Otherwise, it should lock it and return true.
// unlock, which unlocks the node. If it cannot be unlocked, then it should return false.
// Otherwise, it should unlock it and return true.
// You may augment the node to add parent pointers or any other property you would like.
// You may assume the class is used in a single-threaded program, so there is no need for
// actual locks or mutexes. Each method should run in O(h), where h is the height of the tree.

void Main()
{
	// wondering how we can check "all of its descendants or ancestors" in O(h) time...
	
	var head = new Node("root",
		new Node("left",
			new Node("left.left"),
			new Node("left.right")),
		new Node("right",
			new Node("right.left")));
			
	head.Lock().Dump("lock head");
	
	head.Dump();
	
	head.Unlock().Dump("unlock head");
	
	head.Dump();
	
	head.Left.Lock().Dump("lock left");
	
	head.Lock().Dump("lock head");
	
	head.Dump();
	
	head.Right.Lock().Dump("lock right");
	
	head.Dump();
}

public class Node
{
	public string Value { get; set; }
	public bool IsLocked { get; set; }
	public Node Left { get; set; }
	public Node Right { get; set; }
	
	private Node _parent = null;
	private int _lockedChilds = 0;

	public int LockedChilds
	{
		get { return _lockedChilds; }
	}
	
	public Node(string value, Node left = null, Node right = null)
	{
		Value = value;
		Left = left;
		Right = right;
		
		if (Left != null)
			Left._parent = this;
			
		if (Right != null)
			Right._parent = this;
	}
	
	// O(h)
	public bool Lock()
	{
		// O(1)
		if (_lockedChilds > 0)
			return false;
		
		// O(h)
		if (AnyAncestorLocked())
			return false;
		
		// O(h)
		// update the counter on all parents
		IterateAncestors(x => x._lockedChilds += 1);
			
		IsLocked = true;
		return true;
	}
	
	// O(h)
	public bool Unlock()
	{
		// O(1)
		if (_lockedChilds > 0)
			return false;

		// O(h)
		if (AnyAncestorLocked())
			return false;

		// O(h)
		IterateAncestors(x => x._lockedChilds -= 1);

		IsLocked = false;
		return true;
	}

	// O(h)
	private bool AnyAncestorLocked()
	{
		bool lockedAncestor = false;
		
		IterateAncestors(x =>
		{
			if (x.IsLocked)
				lockedAncestor = true;
		});
		
		return lockedAncestor;
	}
	
	// O(h)
	private void IterateAncestors(Action<Node> fn)
	{
		Node current = _parent;

		while (current != null)
		{
			fn(current);
				
			current = current._parent;
		}
	}
}