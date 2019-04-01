<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// Given a list of integers, return the largest product that can be made by multiplying any three integers.
// 
// For example, if the list is [-10, -10, 5, 2], we should return 500, since that's -10 * -10 * 5.
// 
// You can assume the list has at least three integers.

void Main()
{
	// as an optimization we can probably
	// sort and array, take 3 biggest positive numbers and 3 biggest by absulute value negative values
	// and then calculate the max product as other numbers are not going to matter
	
	LargestProductOfThreeItems(new[] { -10, -10, 5, 2 }).Dump();
}

public long LargestProductOfThreeItems(int[] a)
{
	long largestProduct = long.MinValue;
	
	for (int i = 0; i < a.Length; i++)
	{
		for (int j = i + 1; j < a.Length; j++)
		{
			for (int k = j + 1; k < a.Length; k++)
			{
				if (a[i] * a[j] * a[k] > largestProduct)
					largestProduct = a[i] * a[j] * a[k];
			}
		}
	}
	
	return largestProduct;
}
