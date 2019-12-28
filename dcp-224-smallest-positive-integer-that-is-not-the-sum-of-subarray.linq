<Query Kind="Program" />

// This problem was asked by Amazon.
// 
// Given a sorted array, find the smallest positive integer
// that is not the sum of a subset of the array.
// 
// For example, for the input [1, 2, 3, 10], you should return 7.
// 
// Do this in O(N) time.

void Main()
{
	SmallestPositiveIntegerThatIsNotSumOfSubArray(new[] { 1, 2, 3, 10 }).Dump("7");
	SmallestPositiveIntegerThatIsNotSumOfSubArray(new[] { 1, }).Dump("2");
	SmallestPositiveIntegerThatIsNotSumOfSubArray(new[] { 1, 1, 1, }).Dump("4");
	SmallestPositiveIntegerThatIsNotSumOfSubArray(new[] { 1, 2, 4, 8, }).Dump("16");
	SmallestPositiveIntegerThatIsNotSumOfSubArray(new[] { 1, 2, 4, 7, }).Dump("15");
	SmallestPositiveIntegerThatIsNotSumOfSubArray(new[] { 2, }).Dump("1");
	SmallestPositiveIntegerThatIsNotSumOfSubArray(new[] { 2, 2, 2, }).Dump("1");
	SmallestPositiveIntegerThatIsNotSumOfSubArray(new[] { 1, 2, 4, 8, 16, 32, 64, 128 }).Dump("256");
	SmallestPositiveIntegerThatIsNotSumOfSubArray(new[] { 1, 2, 4, 8, 16, 32, 64, 129 }).Dump("128");
}

int SmallestPositiveIntegerThatIsNotSumOfSubArray(int[] a)
{
	int sum = 0;
	
	for (int i = 0; i < a.Length; i++)
	{
		// if the current element bigger than ALL previous ones PLUS 1
		// then it effectively means we can never SUM UP to that SUM + 1
		if (a[i] > sum + 1)
			return sum + 1;
			
		sum += a[i];
	}
	
	// edge case, answer is SUM for ALL + 1
	return sum + 1;
}
