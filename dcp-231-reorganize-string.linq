<Query Kind="Program" />

// This problem was asked by IBM.
// 
// Given a string with repeated characters, rearrange the string so that
// no two adjacent characters are the same. If this is not possible, return None.
// 
// For example, given "aaabbc", you could return "ababac". Given "aaab", return None.

void Main()
{
	// https://leetcode.com/problems/reorganize-string
	
	R("aaabbc").Dump("+");
	R("cbbaaa").Dump("+");
	R("aaab").Dump("-");
	R("aaaabbbbccc").Dump("possible -- abcabcabcab");
	R("tndsewnllhrtwsvxenkscbivijfqnysamckzoyfnapuotmdexzkkrpmppttficzerdndssuveompqkemtbwbodrhwsfpbmkafpwyedpcowruntvymxtyyejqtajkcjakghtdwmuygecjncxzcxezgecrxonnszmqmecgvqqkdagvaaucewelchsmebikscciegzoiamovdojrmmwgbxeygibxxltemfgpogjkhobmhwquizuwvhfaiavsxhiknysdghcawcrphaykyashchyomklvghkyabxatmrkmrfsppfhgrwywtlxebgzmevefcqquvhvgounldxkdzndwybxhtycmlybhaaqvodntsvfhwcuhvuccwcsxelafyzushjhfyklvghpfvknprfouevsxmcuhiiiewcluehpmzrjzffnrptwbuhnyahrbzqvirvmffbxvrmynfcnupnukayjghpusewdwrbkhvjnveuiionefmnfxao").Dump("+");
}

// gready algoritm -- trying to spend the most frequent letter at each step
// not sure how to prove that it's correct, but it passed all leetcode cases
public string R(string s)
{
	var counts = s.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
	string result = string.Empty;
	
	while (true)
	{
		if (result.Length == s.Length)
			break;

		char? next = FindMaxFreq(counts, result != string.Empty ? result[result.Length - 1] : (char?) null);

		if (next == null)
			return null;

		counts[next.Value] -= 1;
		
		result += next.Value;
	}
	
	return result;
}

public char? FindMaxFreq(Dictionary<char, int> counts, char? excluding)
{
	var kvps = counts.OrderByDescending(g => g.Value).ToArray();
	
	for (int i = 0; i < kvps.Length; i++)
	{
		if (kvps[i].Value == 0 || kvps[i].Key == excluding)
			continue;
			
		return kvps[i].Key;
	}
	
	return null;
}