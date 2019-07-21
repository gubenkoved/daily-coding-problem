<Query Kind="Program" />

// Given a list, sort it using this method: reverse(lst, i, j),
// which reverses lst from i to j.

void Main()
{
	var a = Generate(100);
	
	a.Dump();
	
	Sort(a);
	
	a.Dump();
	
	for (int i = 1; i < a.Length; i++)
	{
		if (a[i] < a[i - 1])
			throw new Exception("WA!");
	}
}

int[] Generate(int n)
{
	int[] data = new int[n];
	
	var rnd = new Random();
	
	for (int i = 0; i < n; i++)
		data[i] = rnd.Next() % 1000;
	
	return data;
}

public void Sort(int[] a)
{
	// Bubble sort?)
	
	for (int i = a.Length; i > 0; i--)
	{
		for (int j = 1; j < i; j++)
		{
			if (a[j - 1] > a[j])
				Reverse(a, j - 1, j);
		}
		
		//a.Dump();
	}
}

public void Reverse(int[] a, int i, int j)
{
	int l = i;
	int r = j;
	
	while (l < r)
	{
		Util.Metatext($"Swap {a[l]} at ({l}) and {a[r]} at ({r})").Dump();
		
		int tmp = a[l];
		a[l] = a[r];
		a[r] = tmp;
		
		l += 1;
		r -= 1;
	}
}
