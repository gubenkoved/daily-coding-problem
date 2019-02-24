<Query Kind="Program" />

// This problem was asked by Google.
// 
// Given the root to a binary tree, implement serialize(root), which serializes the tree into a string,
// and deserialize(s), which deserializes the string back into the tree.
// 
// For example, given the following Node class
// 
// class Node:
//     def __init__(self, val, left=None, right=None):
//         self.val = val
//         self.left = left
//         self.right = right
// The following test should pass:
// 
// node = Node('root', Node('left', Node('left.left')), Node('right'))
// assert deserialize(serialize(node)).left.left.val == 'left.left'

public class Node
{
	public string Value { get; set; }
	public Node Left { get; set; }
	public Node Right { get; set; }
	
	public Node(string val, Node left = null, Node right = null)
	{
		Value = val;
		Left = left;
		Right = right;
	}
}

// format: ("root" ("l" ("l.l")) ("r"))
// json: { "value": "root", "left": { "value": "left", "left": { "value" = "left.left" } }, "right": { "value": "right" } }
public class Serializer
{
	public static string Serialize(Node root)
	{
		StringBuilder builder = new StringBuilder();
		
		WriteNode(builder, root);
		
		return builder.ToString();
	}
	
	public static Node Deserialize(string s)
	{
		int idx = 0;
		
		return ReadNode(s, ref idx);
	}
	
	private static void WriteNode(StringBuilder builder, Node node)
	{
		if (node == null)
		{
			builder.Append("0");
			return;
		}
		
		builder.Append("(\"")
			.Append(node.Value)
			.Append("\"");
			
		builder.Append(" ");
		WriteNode(builder, node.Left);

		builder.Append(" ");
		WriteNode(builder, node.Right);
		
		builder.Append(")");
	}
	
	private static Node ReadNode(string s, ref int idx)
	{
		// it must be ("value" [<Node>] [<Node>])
		
		string next = Read(1, s, ref idx);
		
		if (next == "0")
			return null;
		
		next += Read(1, s, ref idx);
		
		if (next != "(\"")
			throw new Exception();
			
		// read until " -- value
		string value = ReadWhile(c => c != '"', s, ref idx);
		
		next = Read(1, s, ref idx); // consume "
		
		Node node = new Node(value);
		
		next = Read(1, s, ref idx);

		// leaf
		if (next == ")")
		{
			idx += 1;
			return node;
		}
		else if (next == " ")
		{
			// okay there is left
			node.Left = ReadNode(s, ref idx);
			next = Read(1, s, ref idx);
			
			if (next == " ")
			{
				// there is right as well
				node.Right = ReadNode(s, ref idx);
				next = Read(1, s, ref idx);
			}
			
			if (next == ")")
			{
				return node;
			}
		}
		
		return node;
	}
	
	private static string Escape(string s)
	{
		return s.Replace("\"", "\\\"");
	}
	
	private static string Unescape(string s)
	{
		return s.Replace("\\\"", "\"");
	}
	
	private static string Read(int count, string s, ref int idx)
	{
		if (idx >= s.Length)
			return null;
		
		string result = "";
		
		for (int i = 0; i < count; i++)
			result += s[idx++];
		
		return result;
	}

	private static string ReadWhile(Predicate<char> f, string s, ref int idx)
	{
		string result = "";

		while (f(s[idx]))
		{
			result += s[idx];
			idx += 1;
		}

		return result;
	}
}

void Main()
{
	var root = new Node("root",
		new Node("left",
			new Node("left.left", null, new Node("left.left.right"))),
		new Node("right"));
		
	//root.Dump();
	string s = Serializer.Serialize(root);

	s.Dump();
	
	//Serializer.Deserialize("(\"root\" (\"l\" (\"l.l\")) (\"r\"))").Dump();
	
	var d = Serializer.Deserialize(s);
	
	d.Dump();
	
	
}
