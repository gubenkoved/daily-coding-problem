<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// Given a function f, and N return a debounced f
// of N milliseconds.
// 
// That is, as long as the debounced f continues to be invoked, f
// itself will not be called for N milliseconds.

void Main()
{
	var g = Debounce(f, 1000);

	for (int i = 0; i < 3; i++)
	{
		g();
		Thread.Sleep(500);
	}
	
	Thread.Sleep(5000);

	for (int i = 0; i < 3; i++)
	{
		g();
		Thread.Sleep(500);
	}

	Thread.Sleep(5000);
}

Action Debounce(Action src, int ms)
{
	Guid lastInvokationId; // this will be part of the closure auto-object, effectively static state for the lambda below
	
	return () =>
	{
		lastInvokationId = Guid.NewGuid();
		
		Guid invokationId = lastInvokationId;
		
		System.Threading.Tasks.Task.Delay(ms)
			.ContinueWith(t =>
			{
				if (lastInvokationId != invokationId)
				{
					// if we got there it means there was another call made
					Console.WriteLine($"skip at {DateTime.Now.ToString("HH:mm:ss.ffff")}");
					return; // there was another call made
				}
				
				src.Invoke();
			});
	};
}

void f()
{
	Console.WriteLine($"I was called at {DateTime.Now.ToString("HH:mm:ss.ffff")}");
}