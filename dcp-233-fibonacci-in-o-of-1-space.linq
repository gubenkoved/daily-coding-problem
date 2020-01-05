<Query Kind="Program" />

// This problem was asked by Apple.
// 
// Implement the function fib(n), which returns the nth number in
// the Fibonacci sequence, using only O(1) space.

void Main()
{
	for (int i = 1; i < 100; i++)
		Fib(i).Dump();
}

long Fib(int n)
{
	long p = 1;
	long pp = 1;

	for (int k = 3; k <= n; k++)
	{
		long next = p + pp;
		pp = p;
		p = next;
	}
	
	return p;
}
