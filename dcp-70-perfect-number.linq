<Query Kind="Program" />

// This problem was asked by Microsoft.
// 
// A number is considered perfect if its digits sum up to exactly 10.
// 
// Given a positive integer n, return the n-th perfect number.
// 
// For example, given 1, you should return 19. Given 2, you should return 28.

void Main()
{
	for (int i = 1; i <= 1000; i++)
		PerfectNumber(i).Dump(i.ToString());
}

// not efficient, but problem does not contain any constraints...
public int PerfectNumber(int n)
{
	int currentN = 0;
	int currentNumberToCheck = 0;
	
	do
	{
		currentNumberToCheck += 1;
		
		if (DigitsSum(currentNumberToCheck) == 10)
			currentN += 1;
	} while (currentN < n);
	
	return currentNumberToCheck;
}

public int DigitsSum(int number)
{
	return number.ToString().Select(x => int.Parse(x.ToString())).Sum();
}

