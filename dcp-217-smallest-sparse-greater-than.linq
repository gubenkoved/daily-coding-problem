<Query Kind="Program" />

// This problem was asked by Oracle.
// 
// We say a number is sparse if there are no adjacent ones in its binary
// representation. For example, 21 (10101) is sparse, but 22 (10110) is
// not. For a given input N, find the smallest sparse number greater than
// or equal to N.
// 
// Do this in faster than O(N log N) time.

void Main()
{
	//int n = 11;
	
	for (int n = 0; n <= 2048; n++)
	{
		bool isSparse = IsSparse(n);

		int ideal = SmallestSparseGreaterThan_Slow(n);
		int result = SmallestSparseGreaterThan_Fast(n);

		bool isCorrect = result == ideal;

		object info = $"{n,-4} = {Binary(n),16} - sparse? {isSparse,5}, smallest sparse gt: {ideal,4}, v2: {result,4} ({Binary(ideal)})";
		
		if (!isCorrect)
			info = Util.Highlight(info);
			
		info.Dump();
	}
}

// can go as far as n steps, so overall is O(n*logn)
// as converting to binary is logn
public int SmallestSparseGreaterThan_Slow(int n)
{
	int k = n;
	
	while (true)
	{
		if (IsSparse(k))
			return k;
			
		k += 1;
	}
}

// we traverse all binary digits which is O(logn) and each time we traverse up to all digits as well
// so it's O((logn)^2) overall
public int SmallestSparseGreaterThan_Fast(int n)
{
	string bin = Binary(n);
	
	List<char> workingBinReversed = bin.Reverse().ToList();
	
	//Util.Metatext($"{n} -> {bin} -> {string.Join("", workingBinReversed)} (reversed)").Dump();
	
	for (int idx = 1; idx < workingBinReversed.Count; idx++)
	{
		if (workingBinReversed[idx - 1] == '1' && workingBinReversed[idx] == '1')
		{
			// we have to do something with these pair of ones
			// and we can do that zeroing out everything until we hit the first zero, replacing it by 1
			// this way the number is going to increase

			//Util.Metatext($"  found adjacent 1 at reversed indx {idx}").Dump();
			
			bool fixedIt = false;
			
			for (int idx2 = 0; idx2 < workingBinReversed.Count; idx2++)
			{
				if (idx2 <= idx)
				{
					workingBinReversed[idx2] = '0';
				} else
				{
					// okay we passed original cursor, stop when we hit first '0' and replace it with '1'
					if (workingBinReversed[idx2] == '0')
					{
						workingBinReversed[idx2] = '1';
						
						fixedIt = true;
						
						//Util.Metatext($"  fix: {string.Join("", workingBinReversed)}").Dump();
						
						break;
					} else // still '1'? continue replacing with '0'
					{
						workingBinReversed[idx2] = '0';
					}
				}
			}
			
			if (!fixedIt)
			{
				// we have to add leading '1', and it means that there is no meaning in continuing as well, as it's 100...00 kind of number, which is sparse
				workingBinReversed.Add('1');
				
				//Util.Metatext($"  fix (fallback): {string.Join("", workingBinReversed)}").Dump();
				break;
			}
		}
	}
	
	string processedBin = new string(workingBinReversed.AsEnumerable().Reverse().ToArray());
	
	return Decimal(processedBin);
}

string Binary(int n)
{
	return Convert.ToString(n, 2);
}

int Decimal(string bin)
{
	return Convert.ToInt32(bin, 2);
}

bool IsSparse(int n)
{
	return Binary(n).Split('0').Select(x => x.Length).Max() <= 1;
}