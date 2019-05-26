<Query Kind="Program">
  <Namespace>System</Namespace>
</Query>

// This problem was asked by Microsoft.
// 
// You have n fair coins and you flip them all at the same
// time. Any that come up tails you set aside. The ones that
// come up heads you flip again. How many rounds do you expect
// to play before only one coin remains?
// 
// Write a function that, given n, returns the number of rounds
// you'd expect to play until one coin remains.

void Main()
{
	F(16).Dump("4");
	F(20).Dump("4..5"); // not sure if we should take floor or ceil, so... let it be fractional :)
	F(32).Dump("5");
}

double F(int n)
{
	// basically series goes like
	// n, n/2, n/4, n/8, ..., 
	// we need to figure out how much rounds it will take until one coin remains
	
	return Math.Log(n, 2);
}
