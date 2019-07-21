<Query Kind="Program" />

// This problem was asked by Apple.
// 
// Gray code is a binary code where each successive value differ
// in only one bit, as well as when wrapping around. Gray code is
// common in hardware so that we don't see temporary spurious values
// during transitions.
// 
// Given a number of bits n, generate a possible gray code for it.
// 
// For example, for n = 2, one gray code would be [00, 01, 11, 10].

void Main()
{
	GenerateGrayCode(2).Select((x, idx) => new { idx, code = CodeToString(x) }).Dump();
	GenerateGrayCode(3).Select((x, idx) => new { idx, code = CodeToString(x) }).Dump();
	GenerateGrayCode(4).Select((x, idx) => new { idx, code = CodeToString(x) }).Dump();
}

BitArray[] GenerateGrayCode(int n)
{
	int total = (int) Math.Pow(2, n);
	
	var codes = Enumerable.Range(0, total).Select(x => new BitArray(n)).ToArray();
	
	FillGrayCode(codes,  n - 1);
	
	return codes;
}

void FillGrayCode(BitArray[] codes, int n)
{
	Util.Metatext($"Filling codes for {n}-th bit").Dump();
	
	// we only fill bit at n-th position there using the reflection of 
	// the values so that on one side new bit is 1 and on another side it's 0
	
	if (n == 0)
	{
		// seed
		codes[0][0] = false;
		codes[1][0] = true;
		
		return;
	}
	
	FillGrayCode(codes, n - 1);
	
	int reflectAt = (int) Math.Pow(2, n);
	
	for (int idx = reflectAt; idx < 2 * reflectAt; idx++)
	{
		int offset = idx - reflectAt;
		
		BitArray reflected = (BitArray)codes[reflectAt - offset - 1].Clone();
		BitArray cur = codes[idx];
		
		CopyBits(src: reflected, trg: cur);
		
		//reflected[n] = false; // redundant
		cur[n] = true;
	}
}

string CodeToString(BitArray a)
{
	string r = "";

	for (int i = 0; i < a.Length; i++)
		r += a[i] ? "1" : "0";

	return string.Join("", r.Reverse());
}

void CopyBits(BitArray src, BitArray trg)
{
	for (int k = 0; k < src.Length; k++)
		trg[k] = src[k];
}

