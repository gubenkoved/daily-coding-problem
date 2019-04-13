<Query Kind="Program" />

// This problem was asked Microsoft.
// 
// Using a read7() method that returns 7 characters from a file, implement readN(n) which reads n characters.
// 
// For example, given a file with the content “Hello world”, three read7() returns “Hello w”, “orld” and then “”.

void Main()
{
	foreach (var chunk in ReadN("Hello_world!", 2))
		chunk.Dump();

	foreach (var chunk in ReadN("Hello_world!", 10))
		chunk.Dump();

	foreach (var chunk in ReadN("Hello_world!Hello_world!Hello_world!Hello_world!Hello_world!", 10))
		chunk.Dump();
}

IEnumerable<string> ReadN(string text, int n)
{
	string buffer = string.Empty;
	
	var chunk7Enumerator = Read7(text).GetEnumerator();
	
	while (buffer.Length < n && chunk7Enumerator.MoveNext())
	{
		// add to buffer
		buffer += chunk7Enumerator.Current;
		
		// remove from buffer while more than n chars
		while (buffer.Length >= n)
		{
			yield return buffer.Substring(0, n);
			
			buffer = buffer.Substring(n);
		}
	}
	
	if (buffer.Length > 0)
		yield return buffer;
}

IEnumerable<string> Read7(string text)
{
	string buffer = null;
	
	for (int i = 0; i < text.Length; i++)
	{
		buffer += text[i];

		if (buffer.Length == 7)
		{
			yield return buffer;
			
			buffer = null;
		}
	}
	
	if (!string.IsNullOrEmpty(buffer))
		yield return buffer;
}
