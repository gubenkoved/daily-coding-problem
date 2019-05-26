<Query Kind="Program" />

// This problem was asked by Google.
// 
// We can determine how "out of order" an array A is by counting
// the number of inversions it has. Two elements A[i] and A[j]
// form an inversion if A[i] > A[j] but i < j. That is, a smaller
// element appears after a larger element.
// 
// Given an array, count the number of inversions it has. Do this
// faster than O(N^2) time.
// 
// You may assume each element in the array is distinct.
// 
// For example, a sorted list has zero inversions. The array [2, 4,
// 1, 3, 5] has three inversions: (2, 1), (4, 1), and (4, 3). The
// array [5, 4, 3, 2, 1] has ten inversions: every distinct pair
// forms an inversion.

void Main()
{
	// status -- did NOT solve myself
	// found O(nlogn) solution there: https://www.geeksforgeeks.org/counting-inversions/
	
	// var source = new[] { 1, 2, 5, 10, 0, 4, 6, 20, 30, 50 };
	// var target = new int[source.Length];
	// 
	// MergeTwoSubsequences(source, target, 0, 9, 3);
	// 
	// target.Dump();
	
	//var result = MergeSort(new[] { 5, 3, 2, 64, 23, 12, 9, 20 });
	//var a = new[] { 7, 5, 4, 6, 8, 3, 2, 1, 9 };
	
	//return;
	
	Func<int[], int> counter = CountInversions_MergeSort;
	//Func<int[], int> counter = CountInversions;
	
	counter(new[] { 2, 4, 1, 3, 5, }).Dump();
	counter(new[] { 4, 1, 3, 5, }).Dump();
	counter(new[] { 1, 3, 5, }).Dump();
	counter(new[] { 3, 5, }).Dump();
	counter(new[] { 5, }).Dump();
	
	"***".Dump();
	
	counter(new[] { 5, 4, 3, 2, 1, }).Dump();
	counter(new[] { 4, 3, 2, 1, }).Dump();
	counter(new[] { 3, 2, 1, }).Dump();
	counter(new[] { 2, 1, }).Dump();
	counter(new[] { 1, }).Dump();

	"***".Dump();
	
	counter(new[] { 9, 1, 6, 2, 7, 3 }).Dump();
	counter(new[] { 1, 6, 2, 7, 3 }).Dump();
	counter(new[] { 6, 2, 7, 3 }).Dump();
	counter(new[] { 2, 7, 3 }).Dump();
	counter(new[] { 7, 3 }).Dump();
	counter(new[] { 3 }).Dump();

	"***".Dump();

	counter(new[] { 9, 1, 6, 2, 7, 3 }).Dump();
	counter(new[] { 9, 1, 6, 2, 7, }).Dump();
	counter(new[] { 9, 1, 6, 2, }).Dump();
	counter(new[] { 9, 1, 6, }).Dump();
	counter(new[] { 9, 1, }).Dump();
	counter(new[] { 9, }).Dump();
}

// O(n^2)
int CountInversions(int[] a)
{
	int count = 0;
	
	for (int i = 0; i < a.Length - 1; i++)
	{
		for (int j = i + 1; j < a.Length; j++)
		{
			if (a[i] > a[j])
				count += 1;
		}
	}
	
	return count;
}

int CountInversions_MergeSort(int[] a)
{
	int inversions = MergeSort(a);
	
	return inversions;
}

// returns number of invertions
int MergeSort(int[] data)
{
	int[] working = new int [data.Length];
	
	Array.Copy(data, working, data.Length);
	
	return MergeSortImpl(working, data, 0, data.Length - 1);
	
	// working.Dump(); // previous step
}

// returns number of inversions
int MergeSortImpl(int[] source, int[] target, int l, int r)
{
	// select mid point
	// recoursively sort left and right from a into the b
	// merge b into a
	
	if (r - l == 0)
		return 0; // 1 element long array is already sorted...
	
	int mid = (l + r) / 2;
	
	int inversions = 0;
	
	// split stage; data is moving from target to source
	inversions +=  MergeSortImpl(target, source, l, mid);
	inversions += MergeSortImpl(target, source, mid + 1, r);
	
	// merge stage; data is moving from source to target
	inversions += MergeTwoSubsequences(source, target, l, mid, r);
	
	//                              vvvvvvvvvvvvvvvv - merge phase
	// overall data path: target -> source -> target
	//                    ^^^^^^^^^^^^^^^^ - split phase

	return inversions;
}

// returns number of inversions
int MergeTwoSubsequences(int[] src, int[] trg, int l, int mid, int r)
{
	// merges two already sorted sequences in src to trg
	// indexes: [l, l + 1, ..., mid] and [mid + 1, ..., r - 1, r]
	// target indexes will also be [l, ..., r]
	// NOTE that mid is included into the LEFT subsequence
	
	int i = l;
	int j = mid + 1;
	
	int trgIndx = l;
	
	int inversions = 0;
	
	while (trgIndx <= r)
	{
		// pick smallest element from both subsequences
		// insert into target
		
		if (j > r || i <= mid && src[i] < src[j]) // take from left subsequence
		{
			trg[trgIndx++] = src[i];
			i += 1;
		} else // take from right subsequence
		{
			trg[trgIndx++] = src[j];
			j += 1;
			
			// when we take from right it means we found (mid - i + 1) additional inversions
			// as all elements from left subsequence after i (included) are bigger than a[j]
			inversions = inversions + (mid - i + 1); 
		}
	}
	
	return inversions;
}