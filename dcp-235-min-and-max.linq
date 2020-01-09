<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// Given an array of numbers of length N, find both the minimum and maximum using less than 2 * (N - 2) comparisons.

void Main()
{
	// quite obvisouly, if we are talking about single min/max alone we can not do any better
	// then (N - 1) comparisons, as we need to take one number and compare against all other ones
	// so there is some kind of optimization that allows us to gather some useful infomoration for
	// min when we are searching for max (and the other way around)
	// for instance, if we got a number which is bigger than running maximum, there is no sense to check
	// and compare it with running minumum
	// however, such approach is not better in the worst case scenario (e.g. ordered descending series)
	
	// postmortem: there is a nice way to compare for ~1.5 * N comparisons
	// in a nutshell idea is to cmpare in pair where we determine first if fair is ascending or descending and
	// respectevely optimize for the checks (for instance if the pair is ascending) max can be updated only by the latter
	// number, and min only by the first one, it's all results in 3 comparisons per 2 items
	// https://www.geeksforgeeks.org/maximum-and-minimum-in-an-array/
	
	var a = G(10000);
	
	F(a, out int min, out int max);
	
	min.Dump("min");
	max.Dump("max");
}

public int[] G(int n)
{
	var a = new int[n];
	var rnd = new Random();
	
	for (int i = 0; i < n; i++)
		a[i] = rnd.Next() % 10000;
	
	return a;
}

public void F(int[] a, out int min, out int max)
{
	int comparisons = 0;
	
	min = a[0];
	max = a[0];
	
	for (int i = 1; i < a.Length; i++)
	{
		comparisons += 1;
		
		if (a[i] >= max)
		{
			max = a[i];
		} else
		{
			comparisons += 1;
			
			if (a[i] < min)
				min = a[i];
		}
	}
	
	Util.Metatext(comparisons.ToString()).Dump("comparisons");
}

