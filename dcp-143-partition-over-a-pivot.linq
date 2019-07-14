<Query Kind="Program" />

// This problem was asked by Amazon.
// 
// Given a pivot x, and a list lst, partition the list into three parts.
// 
// The first part contains all elements in lst that are less than x
// The second part contains all elements in lst that are equal to x
// The third part contains all elements in lst that are larger than x
// Ordering within a part can be arbitrary.
// 
// For example, given x = 10 and lst = [9, 12, 3, 5, 14, 10, 10], one partition may be [9, 3, 5, 10, 10, 12, 14].

void Main()
{
	// it looks a part of the quick-sort...

	//var a = new[] { 9, 12, 3, 5, 14, 10, 10 };
	//var a = new[] { 6, 64, 5, 72, 50, 5, 68, 12, 60, 12 };
	
//	$"Src: {string.Join(", ", a)}".Dump();
//	
//	Partition(a, 10);
//	
//	$"Result: {string.Join(", ", a)}".Dump();
//	
//	return;
	
	"Validating on a random inputs...".Dump();
	
	for (int i = 0; i < 10000 * 1000; i++)
	{
		int[] d = Generate(10);
		int[] dCopy = (int[])d.Clone();
		int pivot = d[5];

		try
		{	        
			Partition(d, pivot);

			Validate(d, pivot);
		}
		catch (Exception)
		{
			$"WA! pivot = {pivot}; {string.Join(", ", dCopy)}".Dump();
		}
	}
}

int[] Generate(int n)
{
	var rnd = new Random();
	
	int[] a = new int[n];
	
	for (int i = 0; i < n; i++)
		a[i] = rnd.Next() % 100;
	
	return a;
}

void Validate(int[] a, int pivot)
{
	int state = 0; // less than or equal
	
	for (int i = 0; i < a.Length; i++)
	{
		switch (state)
		{
			case 0:
				if (a[i] > pivot)
					throw new Exception("wrong!");
				
				if (a[i] == pivot)
					state = 1;
					
				break;
			case 1:
				if (a[i] < pivot)
					throw new Exception("wrong!");
				
				if (a[i] > pivot)
					state = 2;
				
				break;
			case 2:
				if (a[i] <= pivot)
					throw new Exception("wrong!");
				break;
		}
	}
}

// O(n) time, O(1) memory
void Partition(int[] a, int pivot)
{
	int l = 0;
	int r = a.Length - 1;

	while (true)
	{
		while (l < a.Length - 1 && a[l] < pivot)
			l += 1;

		while (r > 0 && a[r] > pivot)
			r -= 1;

		if (l >= r || r < 0 || l >= a.Length)
			break;

		// a[l] >= pivot
		// a[r] <= pivot

		// swap
		if (a[l] > a[r])
		{
			Swap(a, l, r);
			
			if (a[l] < pivot)
				l += 1;
				
			if (a[r] > pivot)
				r -= 1;
		} else if (a[l] == a[r]) // it must be "pivot" if they are equals
		{
			// okay we now point to pivot with both pointers...
			// let's find first element to the right side of "l" which is not pivot 
			// and then figure what to do depending on it
			
			int l2 = l + 1;
			
			while (a[l2] == pivot && l2 < r)
				l2 += 1;

			if (l2 >= r)
				break;

			// okay now element at "l2" is not a pivot, let's swap with that
			if (a[l2] < pivot)
				Swap(a, l, l2);
			else //a[l2] > pivot
				Swap(a, l2, r);
		} else
		{
			throw new Exception("should not happen");
		}
		
		//Util.Metatext(("[" + string.Join(", ", a) + "]")).Dump();
	}
}

void Swap(int[] a, int l, int r)
{
	//Util.Metatext($"Swap {a[l]} (at {l}) and {a[r]} (at {r})").Dump();
	
	int tmp = a[l];
	a[l] = a[r];
	a[r] = tmp;
}