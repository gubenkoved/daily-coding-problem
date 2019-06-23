<Query Kind="Program" />

// This question was asked by Snapchat.
// 
// Given the head to a singly linked list, where each node also
// has a “random” pointer that points to anywhere in the linked
// list, deep clone the list.

void Main()
{
	var head = new Node(1, new Node(2, new Node(3, new Node(4))));
	
	head.Random = head.Next.Next;
	head.Next.Next.Random = head.Next;
	
	string serialized = Serialize(head);
	
	serialized.Dump();
	
	var head2 = Deserialize(serialized);
	
	head2.Dump();
	
	Serialize(head2).Dump("control");
}

public class Node
{
	public int Value { get; set; }
	public Node Next { get; set; }
	public Node Random { get; set; }
	
	public Node(int value, Node next = null, Node rand = null)
	{
		Value = value;
		Next = next;
		Random = rand;
	}
}

public string Serialize(Node head)
{
	// format: (id, value, rand-id) -> ... -> null
	
	// pass one -- assign IDs to each node
	Dictionary<Node, string> idMap = new Dictionary<UserQuery.Node, string>();
	
	Node cur = head;
	int id = 0;
	while (cur != null)
	{
		idMap[cur] = (id++).ToString();
		cur = cur.Next;
	}
	
	// pass two serialize!
	StringBuilder builder = new StringBuilder();
	
	cur = head;

	while (true)
	{
		if (cur != null)
		{
			string randPtr = cur.Random != null ? idMap[cur.Random] : "*";

			builder.Append($"({idMap[cur]}, {cur.Value}, {randPtr}) -> ");
			
			cur = cur.Next;
		}
		else
		{
			builder.Append("*");
			break;
		}
	}

	return builder.ToString();
}

public Node Deserialize(string serialized)
{
	Dictionary<string, Node> idReverseMap = new Dictionary<string, UserQuery.Node>();
	Dictionary<Node, string> rndMap = new Dictionary<UserQuery.Node, string>();
	
	string buffer = string.Empty;
	
	int idx = 0;
	
	Node head = null;
	Node cur = null;
	
	while (true)
	{
		// read first char
		
		if (serialized[idx] == '*')
			break; // end of list reached
		
		while (serialized[idx] != ')')
		{
			buffer += serialized[idx];
			idx += 1;
		}
		
		buffer = buffer.TrimStart('(');
		
		// after reading a node " -> " is expected, do not check there
		idx += " -> ".Length + 1;
		
		// process the node
		buffer.Dump("node data");
		
		var nodeData = buffer.Split(',').Select(x => x.Trim()).ToArray();
		
		string id = nodeData[0];
		
		var newNode = new Node(int.Parse(nodeData[1]));
		
		idReverseMap[id] = newNode;
		
		if (nodeData[2] != "*")
			rndMap[newNode] = nodeData[2];
		
		if (cur == null)
		{
			// first node
			head = newNode;
			cur = head;
		} else
		{
			cur.Next = newNode;
			cur = newNode;
		}
		
		buffer = string.Empty;
	}
	
	// okay, now traverse the list and rewire "Random"
	foreach (var node in rndMap.Keys)
		node.Random = idReverseMap[rndMap[node]];
	
	return head;
}