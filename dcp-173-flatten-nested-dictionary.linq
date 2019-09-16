<Query Kind="Program" />

// This problem was asked by Stripe.
// 
// Write a function to flatten a nested dictionary. Namespace the keys
// with a period.
// 
// For example, given the following dictionary:
// 
// {
//     "key": 3,
//     "foo": {
//         "a": 5,
//         "bar": {
//             "baz": 8
//         }
//     }
// }
//
// it should become:
// 
// {
//     "key": 3,
//     "foo.a": 5,
//     "foo.bar.baz": 8
// }
//
// You can assume keys do not contain dots in them, i.e. no clobbering will occur.

void Main()
{
	var node = new ObjectNode();	
	
	node.Properties["key"] = new SimpleNode(3);

	node.Properties["foo"] = new ObjectNode(new Dictionary<string, Node>()
	{
		{ "a", new SimpleNode(5) },
		{ "bar", new ObjectNode(new Dictionary<string, Node> { { "baz", new SimpleNode(8) } }) },
	});
	
	node.Dump();
	
	Flatten(node).Dump();
}

public ObjectNode Flatten(ObjectNode src)
{
	var target = new ObjectNode();
	
	AddWithPrefix(target, string.Empty, src);
	
	return target;
}

public void AddWithPrefix(ObjectNode target, string prefix, ObjectNode source)
{
	foreach (var property in source.Properties)
	{
		string propertyName = prefix == "" ? property.Key : prefix + "." + property.Key;
		
		if (property.Value is SimpleNode)
		{
			target.Properties[propertyName] = property.Value;
		} else if (property.Value is ObjectNode)
		{
			AddWithPrefix(target, propertyName, property.Value as ObjectNode);
		}
	}
}

public abstract class Node
{
}

public class ObjectNode : Node
{
	public Dictionary<string, Node> Properties { get; set; } = new Dictionary<string, Node>();
	
	public ObjectNode(Dictionary<string, Node> properties = null)
	{
		Properties = properties ?? new Dictionary<string, Node>();
	}
}

public class SimpleNode : Node
{
	public object Value { get; set; }
	
	public SimpleNode(object value)
	{
		Value = value;
	}
}
