<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// Given the mapping a = 1, b = 2, ... z = 26, and an encoded message, count the number of ways it can be decoded.
// 
// For example, the message '111' would give 3, since it could be decoded as 'aaa', 'ka', and 'ak'.
// 
// You can assume that the messages are decodable. For example, '001' is not allowed.

public static Dictionary<string, string> Map = new Dictionary<string, string>();

void Main()
{
	for (char c = 'a'; c <= 'z'; c++)
	{
		//map[c.ToString()] = ((c - 'a') + 1).ToString();
		Map[((c - 'a') + 1).ToString()] = c.ToString();
	}
		
	//Map.Dump();
	
	var results = Decode("11111111111111111111111111111");
	
	results.Dump(results.Count().ToString());
}

public class Results
{
	public List<string> Outputs { get;set; } = new List<string>();
	//public HashSet<string> Outputs { get;set; } = new HashSet<string>();
}

IEnumerable<string> Decode(string input)
{
	var results = new Results();
	
	Decode(input, 0, "", results);
	
	return results.Outputs;
}

void Decode(string input, int idx, string output, Results results)
{
	if (idx >= input.Length)
	{
		// done, nothing to decode
		results.Outputs.Add(output);
		return;
	}
	
	// find all candidates that consume chars at given index
	// when it concludes the full input store the decoded result
	foreach (var kvp in Map)
	{
		string encoded = kvp.Key;
		string decoded = kvp.Value;
		
		bool match = true;
		
		for (int i = idx; i < idx + encoded.Length; i++)
		{
			if (i >= input.Length || input[i] != encoded[i - idx])
			{
				match = false;
				break;
			}
		}
		
		if (match)
		{
			// consume encoded value, add decoded value to current values, recourse
			
			Decode(input, idx + encoded.Length, output + decoded, results);
		}
	}
}
