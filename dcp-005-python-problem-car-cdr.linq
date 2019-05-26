<Query Kind="Program" />

// This problem was asked by Jane Street.
// 
// cons(a, b) constructs a pair, and car(pair) and cdr(pair) returns the first and last element of that pair.For example, car(cons(3, 4)) returns 3, and cdr(cons(3, 4)) returns 4.
// 
// Given this implementation of cons:
// 
// def cons(a, b):
//     def pair(f):
// 
// 		return f(a, b)
// 
// 	return pair
// Implement car and cdr.

void Main()
{
	car(cons(1, 2)).Dump();
	cdr(cons(1, 2)).Dump();
}

public Func<Func<int, int, Tuple<int, int>>, Tuple<int, int>> cons(int a, int b)
{
	Func<Func<int, int, Tuple<int, int>>, Tuple<int, int>> pair = f => f(a, b);
	
	return pair;
}

public int car(Func<Func<int, int, Tuple<int, int>>, Tuple<int, int>> c)
{
	return c(C).Item1;
}

public int cdr(Func<Func<int, int, Tuple<int, int>>, Tuple<int, int>> c)
{
	return c(C).Item2;
}

public Tuple<int, int> C(int a, int b)
{
	return Tuple.Create(a, b);
}