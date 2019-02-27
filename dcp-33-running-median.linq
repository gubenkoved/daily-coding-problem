<Query Kind="Program" />

// This problem was asked by Microsoft.
// 
// Compute the running median of a sequence of numbers. That is, given a stream of numbers,
// print out the median of the list so far on each new element.
// 
// Recall that the median of an even-numbered list is the average of the two middle numbers.
// 
// For example, given the sequence [2, 1, 5, 7, 2, 0, 5], your algorithm should print out:
// 
// 2
// 1.5
// 2
// 3.5
// 2
// 2
// 2

void Main()
{
	//	var l = new List<int>();
	//	
	//	BinarySearchInsert(l, 1);
	//	BinarySearchInsert(l, 5);
	//	BinarySearchInsert(l, 2);
	//	BinarySearchInsert(l, 4);
	//	BinarySearchInsert(l, 3);
	//	BinarySearchInsert(l, 0);
	//	BinarySearchInsert(l, 10);
	//	BinarySearchInsert(l, 5);
	//	
	//	l.Dump();

	RunningMedian(new[] { 2, 1, 5, 7, 2, 0, 5 });
	
	// time/space constraints are not specified in the question
	// but the better approach could be to use min/max heaps
}

// O(n) for each step...
void RunningMedian(IEnumerable<int> numbers)
{
	// simplest method -- just maintain sorted array
	// use binary search to find a place for new element
	
	// time for n elements is O(n * n)
	// space complexity is O(n)

	List<int> sorted = new List<int>();
	
	foreach (int num in numbers)
	{
		BinarySearchInsert(sorted, num);
		
		int med = sorted.Count / 2;
		
		if (sorted.Count % 2 == 1)
			sorted[med].Dump();
		else
			((sorted[med] + sorted[med - 1]) / 2.0).Dump();
	}
}

// O(n)
void BinarySearchInsert(List<int> sorted, int num)
{
	if (!sorted.Any())
	{
		sorted.Add(num);
		return;
	}
	
	int l = 0;
	int r = sorted.Count - 1;

	while (r - l > 1)
	{
		int m = (r + l) / 2;
		
		if (num >= sorted[m])
			l = m;
		else
			r = m;
	}
	
	// rats! insertion in list is O(n) :(
	
	if (num > sorted[r])
		sorted.Insert(r + 1, num);
	else if (num > sorted[l])
		sorted.Insert(l + 1, num);
	else
		sorted.Insert(l, num);
}