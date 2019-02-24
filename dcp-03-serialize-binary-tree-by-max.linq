<Query Kind="Program" />

void Main()
{
	var node = new Node("\\root",
		new Node("left",
			new Node("left.left")),
		new Node("right",
			null,
			new Node("right.right")));			
	
	var sb = new StringBuilder();
	Serialize(node, sb);
	var serialized = sb.ToString();
	serialized.Dump();
	
	var deserialized = DeserializeTree(serialized);

	deserialized.Equals(node).Dump();
}

Node DeserializeTree(string serialized)
{
	Node lastPoppedNode = null;
	var stack = new Stack<StackItem>();
	var encoded = false;
	
	for (int i = 0; i < serialized.Length; i++)
	{
		var c = serialized[i];
		
		if (encoded)
		{
			stack.Peek().node.Value += c;
			encoded = false;
			continue;
		}
		
		if (c == '(')
		{
			var newNode = new Node("");
			if (stack.Any())
			{
				var top = stack.Peek();
				
				if (top.leftRead)
				{
					top.node.Right = newNode;
				}
				else
				{
					top.node.Left = newNode;
					top.leftRead = true;
				}
			}
			stack.Push(new StackItem(newNode));
		}
		else if (c == ')')
		{
			var popped = stack.Pop();
			lastPoppedNode = popped.node;
		}
		else if (c == 'e')
		{
			if (stack.Any())
			{
				var top = stack.Peek();
				top.leftRead = true;
			}
		}
		else if (c == '\\')
		{
			encoded = true;
		}
		else
		{
			stack.Peek().node.Value += c;
		}
	}
	
	return lastPoppedNode;
}
void Serialize(Node node, StringBuilder sb)
{
	if (node == null)
	{
		sb.Append('e');
		return;
	}
	
	sb.Append('(');
	
	AppendEncoded(sb, node.Value);
	Serialize(node.Left, sb);
	Serialize(node.Right, sb);
	
	sb.Append(')');
}

void AppendEncoded(StringBuilder sb, string value)
{
	foreach (var ch in value)
	{
		if (IsSpecialChar(ch))
		{
			sb.Append('\\');
		}
		sb.Append(ch);
	}
}

bool IsSpecialChar(char ch) => (ch == '(' || ch == ')' || ch == 'e' || ch == '\\');

class StackItem
{
	public Node node;
	public bool leftRead;
	
	public StackItem(Node node)
	{
		this.node = node;
		leftRead = false;
	}
}

class Node : IEquatable<Node>
{
	public Node Left { get; set; }
	public Node Right { get; set; }
	public string Value { get; set; }
	
	public Node(string value, Node left = null, Node right = null)
	{
		Value = value;
		Left = left;
		Right = right;
	}

	public override string ToString()
	{
		return Value;
	}

	public bool Equals(Node other)
	{
		if (Value != other.Value) return false;
		if (Left == null && other.Left != null || Left != null && other.Left == null) return false;
		if (Right == null && other.Right != null || Right != null && other.Right == null) return false;
		
		if (Left != null && !Left.Equals(other.Left)) return false;
		if (Right != null && !Right.Equals(other.Right)) return false;
		
		return true;
	}
}