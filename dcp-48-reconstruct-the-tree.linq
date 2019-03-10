<Query Kind="Program" />

// This problem was asked by Google.
// 
// Given pre-order and in-order traversals of a binary
// tree, write a function to reconstruct the tree.
// 
// For example, given the following preorder traversal:
// 
// [a, b, d, e, c, f, g]
// 
// And the following inorder traversal:
// 
// [d, b, e, a, f, c, g]
// 
// You should return the following tree:
// 
//     a
//    / \
//   b   c
//  / \ / \
// d  e f  g

public class Node<T>
{
	public T Value { get; set; }
	public Node<T> Left { get; set; }
	public Node<T> Right { get; set; }
	
	public Node(T val)
	{
		Value = val;
	}
}

void Main()
{
	Reconsturct(new[] { "a", "b", "d", "e", "c", "f", "g" },
		new[] { "d", "b", "e", "a", "f", "c", "g" }).Dump();

	Reconsturct(new[] { "a", "b", "d", "e", "c", "f", "g" },
		new[] { "b", "a", "c", "e", "f", "d", "g" }).Dump();

	Reconsturct(new[] { "a", "b", "d", "c", "f", "e", "g" },
		new[] { "d", "b", "a", "e", "f", "c", "g" }).Dump();
}

// O(n logn) time, O(n) space
// time complexity is not obvious ... let's consider fully balanced case
// for root element we traverse n/2 elements, for left of root it's n/4 and for right part n/4
// it's not hard to see that on each level we traverse n/2 elements which add up to
// (n/2 * h) where h is height of the tree, in balanced case it's proportional to logn
// which gives us average time complexity of O(n logn)
// in worst case where elements are forming a line it will be (n-1) + (n-2) ... + (1) which is O(n*n)
public Node<T> Reconsturct<T>(T[] preorder, T[] inorder)
{
	return Reconsturct(new ArraySegment<T>(preorder), new ArraySegment<T>(inorder));
}

public Node<T> Reconsturct<T>(ArraySegment<T> preorder, ArraySegment<T> inorder)
{
	if (preorder.Count != inorder.Count)
		throw new InvalidOperationException();
		
	int n = preorder.Count;
	
	if (n == 0)
		return null;
	
	// first node in preorder is our root
	Node<T> root = new Node<T>(((IList<T>)preorder)[0]);
	
	// find index of root inside inorder traversal result
	int rootIdx = ((IList<T>)inorder).IndexOf(root.Value);
	
	if (rootIdx == -1)
		throw new Exception("Bad data!");
	
	int leftCount = rootIdx;
	int rightCount = n - leftCount - 1;
	
	ArraySegment<T> leftPreorder = new ArraySegment<T>(preorder.Array, preorder.Offset + 1, leftCount);
	ArraySegment<T> rightPreorder = new ArraySegment<T>(preorder.Array, preorder.Offset + leftCount + 1, rightCount);

	ArraySegment<T> leftInorder = new ArraySegment<T>(inorder.Array, inorder.Offset, leftCount);
	ArraySegment<T> rightInorder = new ArraySegment<T>(inorder.Array, inorder.Offset + leftCount + 1, rightCount);

	root.Left = Reconsturct(leftPreorder, leftInorder);
	root.Right = Reconsturct(rightPreorder, rightInorder);
	
	return root;
}

