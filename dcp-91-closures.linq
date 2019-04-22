<Query Kind="Program" />

// What does the below code snippet print out?
// How can we fix the anonymous functions to
//behave as we'd expect?
// 
// functions = []
// for i in range(10):
//     functions.append(lambda : i)
// 
// for f in functions:
//     print(f())

void Main()
{
	// if lambdas working the same way as they do
	// in C# it will output 10 ten times
	// translation to C# + fix below
	
	"original".Dump();
	
	f1();
	
	"fixed:".Dump();
	
	f2();
}

public void f1()
{
	List<Func<int>> functions = new List<Func<int>>();
	
	for (int i = 0; i < 10; i++)
		functions.Add(() => i);
		
	foreach (var f in functions)
		Console.WriteLine(f());
}

public void f2()
{
	List<Func<int>> functions = new List<Func<int>>();

	for (int i = 0; i < 10; i++)
	{
		int iCopy = i;
		functions.Add(() => iCopy);
	}

	foreach (var f in functions)
		Console.WriteLine(f());
}
