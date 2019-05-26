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
	LargestProductOfThreeItems(new[] { -10, 10, 5, 2 }).Dump();
	
	for (int i = 0; i < 5; i++)
	{
		int[] a = RandomArray(1000);
		
		long result = LargestProductOfThreeItemsOptmized(a);
		long resultToCheck = LargestProductOfThreeItems(a);

		if (result != resultToCheck)
		{
			a.Dump();
			
			result.Dump("result");
			resultToCheck.Dump("result to check");
			
			throw new Exception("WA!");
		}
	}
	
	"Verified!".Dump();
}

public int[] RandomArray(int n)
{
	Random rnd = new Random(Guid.NewGuid().GetHashCode());
	
	int[] a = new int[n];
	
	for (int i = 0; i < n; i++)
		a[i] = rnd.Next() % 1000000 - 500000;
	
	return a;
}

// O(n logn)
public long LargestProductOfThreeItemsOptmized(int[] a)
{
	// three maximal positive values and two maximal by absolute values
	// negative values are going to matter ONLY
	
	if (a.Length <= 5)
		return LargestProductOfThreeItems(a);
	
	int[] sorted = a.OrderBy(x => x).ToArray();
	
	int n = a.Length;

	return LargestProductOfThreeItems(new[] { sorted[0], sorted[1], sorted[n - 1], sorted[n - 2], sorted[n - 3], });
}

// O(n^3)
public long LargestProductOfThreeItems(int[] a)
{
	long largestProduct = int.MinValue;
	
	for (int i = 0; i < a.Length; i++)
	{
		for (int j = i + 1; j < a.Length; j++)
		{
			for (int k = j + 1; k < a.Length; k++)
			{
				if ((long)a[i] * (long)a[j] * (long)a[k] > largestProduct)
					largestProduct = (long)a[i] * (long)a[j] * (long)a[k];
			}
		}
	}
	
	return largestProduct;
}
