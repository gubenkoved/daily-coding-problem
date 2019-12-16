<Query Kind="Program" />

// This problem was asked by Apple.
// 
// A Collatz sequence in mathematics can be defined as follows. Starting with any positive integer:
// 
// if n is even, the next number in the sequence is n / 2
// if n is odd, the next number in the sequence is 3n + 1
// It is conjectured that every such sequence eventually reaches the number 1. Test this conjecture.
// 
// Bonus: What input n <= 1000000 gives the longest sequence?

void Main()
{
	for (int n = 1; n < 10; n++)
	{
		string.Join(" -> ", CollatzSequence(n)).Dump(n);
	}
	
	// caching (< 1 sec)
	
	long maxLen = 1;
	
	for (int n = 1; n < 1000000; n++)
	{
		long curLen = CollatzLen(n);

		if (curLen > maxLen)
		{
			$"found longer chain for number {n}, len is {curLen}".Dump();
			maxLen = curLen;
		}
	}
	
	maxLen.Dump("max found len for n <= 1000000").Dump();
	
	// brute force way (< 3 sec)
	long runningLongest = 1;
	
	for (int n = 1; n < 1000000; n++)
	{
		IEnumerable<long> currentSequence = CollatzSequence(n);
		
		long currentCount = currentSequence.LongCount();
		
		if (currentCount > runningLongest)
		{
			$"Found sequence of len {currentCount} for number {n}: {string.Join(" -> ", currentSequence)}".Dump();
			runningLongest = currentCount;
		}
	}
}

IEnumerable<long> CollatzSequence(long n)
{
	// first one!
	yield return n;

	while (n != 1)
	{
		if (n % 2 == 0)
			n = n / 2;
		else
			n = 3 * n + 1;
			
		yield return n;
	}
}

Dictionary<long, long> _lenCache = new Dictionary<long, long>() { { 1, 1 } };

long CollatzLen(int n)
{
	// we need to memorize these in order to
	//List<int> traversed = new List<int>();
	LinkedList<long> traversed = new LinkedList<long>();
	
	foreach (int element in CollatzSequence(n))
	{
		if (_lenCache.ContainsKey(element))
		{
			long cutLen = _lenCache[element];
			
			// we cut short, we need to cache all traversed items before returning!
			long lenFromCut = 1;
			
			foreach (int traversedElement in traversed.Reverse())
			{
				//$"len from {traversedElement} is {lenFromCut + cutLen}".Dump();
				_lenCache[traversedElement] = lenFromCut + cutLen;
				
				lenFromCut += 1;
			}
			
			return traversed.Count + _lenCache[element];
		}
		
		// nope? move next
		//traversed.Add(element);
		traversed.AddLast(element);
	}
	
	throw new Exception("Should not be there!");
}

