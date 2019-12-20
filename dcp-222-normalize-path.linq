<Query Kind="Program" />

// This problem was asked by Quora.
// 
// Given an absolute pathname that may have . or .. as part of it, return
// the shortest standardized path.
// 
// For example, given "/usr/bin/../bin/./scripts/../", return "/usr/bin/".

void Main()
{
	StandardForm("/usr/bin/").Dump();
	StandardForm("/usr/bin/../bin/./scripts/../").Dump();
	StandardForm("/usr/bin/a/b/../../").Dump();
	StandardForm("/usr/bin/a/b/../../../bin/").Dump();
}

string StandardForm(string path)
{
	var segments = path.Split('/');
	
	Stack<string> simplified = new Stack<string>();
	
	foreach (string segment in segments)
	{
		if (segment == "..")
		{
			simplified.Pop();
		}
		else if (segment == ".")
		{
			// do nothing
		} else
		{
			simplified.Push(segment);
		}
	}
	
	return string.Join("/", simplified.Reverse());
}
