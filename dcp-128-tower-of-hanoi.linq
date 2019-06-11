<Query Kind="Program" />

// The Tower of Hanoi is a puzzle game with three rods
// and n disks, each a different size.
// 
// All the disks start off on the first rod in a stack.
// They are ordered by size, with the largest disk on the
// bottom and the smallest one at the top.
// 
// The goal of this puzzle is to move all the disks from the
// first rod to the last rod while following these rules:
// 
// You can only move one disk at a time.
//
// A move consists of taking the uppermost disk from one of
// the stacks and placing it on top of another stack.
//
// You cannot place a larger disk on top of a smaller disk.
//
// Write a function that prints out all the steps necessary to
// complete the Tower of Hanoi. You should assume that the rods
// are numbered, with the first rod being 1, the second (auxiliary)
// rod being 2, and the last (goal) rod being 3.
// 
// For example, with n = 3, we can do this in 7 moves:
// 
// Move 1 to 3
// Move 1 to 2
// Move 3 to 2
// Move 1 to 3
// Move 2 to 1
// Move 2 to 3
// Move 1 to 3

void Main()
{
	for (int i = 1; i < 10; i++)
	{
		$"{i} needs {HanoiCount(i)} moves:".Dump();
		
		Move(i);
		
		string.Empty.Dump();
	}
}

int HanoiCount(int n)
{
	if (n == 1)
		return 1;
	else
		return 2 * HanoiCount(n - 1) + 1;
}

void Move(int n, int src = 1, int trg = 3)
{
	// in order to move n disks I need to move n-1 disks into to second rod,
	// and then move n-th disk onto the 3rd one and move n-1 disks from 2nd rod
	// on 3rd

	if (n == 1)
	{
		$"  Move {src} to {trg}".Dump();
		return;
	}

	int tmp = new[] { 1, 2, 3 }.Except(new[] {src, trg}).Single();
	
	// move n-1 into the temp rod
	Move(n - 1, src, tmp);
	
	// move n-th
	Move(1, src, trg);
	
	// move n-1 again
	Move(n - 1, tmp, trg);
}