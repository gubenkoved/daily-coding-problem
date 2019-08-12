<Query Kind="Program" />

// This problem was asked by Google.
// 
// Given an array of integers, return a new array where each element
// in the new array is the number of smaller elements to the right of
// that element in the original input array.
// 
// For example, given the array [3, 4, 9, 6, 1], return [1, 1, 2, 1, 0],
// since:
// 
// There is 1 smaller element to the right of 3
// There is 1 smaller element to the right of 4
// There are 2 smaller elements to the right of 9
// There is 1 smaller element to the right of 6
// There are no smaller elements to the right of 1

void Main()
{
	// obviously doing this task in O(n^2) in time is trivial

	//F_Trivial(new[] { 3, 4, 9, 6, 1 }).Dump();
	
	var testSet = Generate(1000);
	
	var correct = F_Trivial(testSet);
	var actual = F(testSet);
	
	correct.SequenceEqual(actual).Dump("correct?");
	
	Bench(F_Trivial);
	Bench(F); // no better...
}

void Bench(Func<int[], int[]> f)
{
	var data = new List<Tuple<int, TimeSpan>>();

	for (double nd = 8; nd < 64 * 1000; nd *= 1.5)
	{
		int n = (int)nd;

		var a = Generate(n);

		var elapsed = TimeIt(() => f(a));

		$"n={n}, time={elapsed.TotalSeconds:F3}s".Dump();

		data.Add(Tuple.Create(n, elapsed));
	}

	data.Chart(x => x.Item1, x => x.Item2.TotalSeconds, LINQPad.Util.SeriesType.Line).Dump(f.Method.Name);
}

int[] F_Trivial(int[] a)
{
	int[] r = new int[a.Length];
	
	for (int i = 0; i < a.Length; i++)
	{
		int count = 0;
		
		for (int j = i + 1; j < a.Length; j++)
		{
			if (a[j] < a[i])
				count += 1;
		}
		
		r[i] = count;
	}
	
	return r;
}

int[] F(int[] a)
{
	int[] result = new int[a.Length];
	
	for (int i = a.Length - 2; i >= 0; i--)
	{
		// digest to the right and maintain possible min/max
		// when they collaps into a single value, stop!
		int min = 0;
		int max = a.Length - i - 1;
		int smaller = 0;
		
		for (int j = i + 1; j < a.Length; j++)
		{
			if (a[j] < a[i])
			{
				smaller += 1;
				min = Math.Max(min, smaller + result[j]);
			} else // a[j] >= a[i]
			{
				max = Math.Min(max, smaller + result[j]);
				//max = Math.Min(max, smaller + a.Length - j - 1); // cut-off by elements to go -- does not help, really...
			}
			
			if (min == max)
			{
				//$"optimized! i={i}, j={j}".Dump();
				result[i] = min; // or max...
				break;
			}

			// reached the very end...
			if (j == a.Length - 1)
				result[i] = smaller;
		}
	}
	
	return result;
}

int[] Generate(int n)
{
	Random r = new Random();
	
	int[] result = new int[n];
	
	for (int i = 0; i < n; i++)
	{
		result[i] = r.Next() % (n * 2);
	}
	
	return result;
}

TimeSpan TimeIt(Action f)
{
	Stopwatch sw = new Stopwatch();
	
	sw.Start();
	
	f();
	
	sw.Stop();
	
	return sw.Elapsed;
}