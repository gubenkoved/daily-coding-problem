<Query Kind="Program" />

// This problem was asked by Google.
// 
// Given an array of integers and a number k, where 1 <= k <= length of the array,
// compute the maximum values of each subarray of length k.
// 
// For example, given array = [10, 5, 2, 7, 8, 7] and k = 3, we should get: [10, 7, 8, 8], since:
// 
// 10 = max(10, 5, 2)
// 7 = max(5, 2, 7)
// 8 = max(2, 7, 8)
// 8 = max(7, 8, 7)
// 
// Do this in O(n) time and O(k) space. You can modify the input array in-place
// and you do not need to store the results. You can simply print them out as you compute them.

// Status: did NOT solve it myself in O(n) time
// found the solution there https://www.geeksforgeeks.org/sliding-window-maximum-maximum-of-all-subarrays-of-size-k/
// https://stackoverflow.com/questions/8031939/finding-maximum-for-every-window-of-size-k-in-an-array/17249084#17249084

void Main()
{
	Action<int[], int> solver = slidingMax;
	//Action<int[], int> solver = naive;
	
	//solver(new[] { 10, 5, 2, 7, 8, 7 }, 3);
	//solver(new[] { 6, 5, 4, 3, 2, 1 }, 3);
	//solver(new[] { 1, 2, 3, 4, 5, 6 }, 3);
	solver(new[] {1, -2, 5, 6, 0, 9, 8, -1, 2, 0}, 3);
}

void slidingMax(int[] a, int k)
{
	var buffer = new LinkedList<int>();
	
	// initial buffer population
	for (int i = 0; i < k; i++)
	{
		while (buffer.Any() && a[i] >= a[buffer.Last.Value])
			buffer.RemoveLast();
			
		buffer.AddLast(i);

		Util.Metatext($" buffer (processed {i}th of value {a[i]}): {string.Join(", ", buffer)}, values: {string.Join(", ", buffer.Select(idx => a[idx]))}").Dump();
	}
	
	for (int i = k; i < a.Length; i++)
	{
		Console.Write($"{a[buffer.First.Value]} ");
		
		// remove out of windows elements
		while (buffer.Any() && buffer.First.Value <= i - k)
			buffer.RemoveFirst();
		
		// remove all smaller than current elements
		while (buffer.Any() && a[i] >= a[buffer.Last.Value])
			buffer.RemoveLast();
		
		buffer.AddLast(i);
		
		Util.Metatext($" buffer (processed {i}th of value {a[i]}): {string.Join(", ", buffer)}, values: {string.Join(", ", buffer.Select(idx => a[idx]))}").Dump();
	}
	
	Console.WriteLine($"{a[buffer.First.Value]}");
	Console.WriteLine("-------");
}



void f(int[] a, int k)
{
	// start at k and go backwards to start
	// maintain a buffer of len k where each element
	// is running maximum: b[i] = max(a[k - i], ..., a[i])
	// then each next j-th maximum can be calculated as max(b[1], a[j])
	// also we need to maintain the buffer efficiently -- we can use circular buffer
	// on each step b[i] = b[i-1] on a previous step
	
	// maintaining this kind of buffer is O(k) and it should be done O(n) times, so it's not O(n)
}

// total time complexity: O(k(n-k))
// the problem: when k = fraction of n, like 1/2 n
// then we have O of n squared complexity: O(1/4 n * n)
void naive(int[] a, int k)
{
	// O(n - k)
	for (int i = 0; i <= a.Length - k; i++)
	{
		// O(k)
		int max = Max(a, i, k);
		
		Console.Write($"{max} ");
	}
	
	Console.WriteLine("");
}

// returns max of given array between k elements starting at i
int Max(int[] a, int i, int k)
{
	int max = a[i];
	
	for (int pos = i; pos < i + k; pos++)
	{
		if (a[pos] > max)
			max = a[pos];
	}
	
	return max;
}
