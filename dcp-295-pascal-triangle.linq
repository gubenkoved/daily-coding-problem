<Query Kind="Program" />

// This problem was asked by Stitch Fix.
// 
// Pascal's triangle is a triangular array of integers constructed with the
// following formula:
// 
// The first row consists of the number 1.
// For each subsequent row, each element is the sum of the numbers directly
// above it, on either side.
// For example, here are the first few rows:
// 
//     1
//    1 1
//   1 2 1
//  1 3 3 1
// 1 4 6 4 1
//
// Given an input k, return the kth row of Pascal's triangle.
// 
// Bonus: Can you do this using only O(k) space?

void Main()
{
	for (int i = 1; i < 10; i++)
		string.Join(" ", Pascal(i)).Dump();
}

int[] Pascal(int k)
{
	int[] cur = new int[k];
	
	cur[0] = 1;
	
	int l = 1;
	
	while (l < k)
	{
		int[] next = new int[k];
		
		for (int i = 0; i <= l; i++)
		{
			next[i] = cur[i];
			
			if (i > 0)
				next[i] += cur[i - 1];
		}
		
		cur = next;
		l += 1;
	}
	
	return cur;
}
