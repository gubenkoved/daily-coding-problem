<Query Kind="Program" />

// This problem was asked by Amazon.
// 
// Huffman coding is a method of encoding characters based on their frequency.
// Each letter is assigned a variable-length binary string, such as 0101 or 111110,
// where shorter lengths correspond to more common letters. To accomplish this,
// a binary tree is built such that the path from the root to any leaf uniquely maps
// to a character. When traversing the path, descending to a left child corresponds
// to a 0 in the prefix, while descending right corresponds to 1.
// 
// Here is an example tree (note that only the leaf nodes have letters):
// 
//         *
//       /   \
//     *       *
//    / \     / \
//   *   a   t   *
//  /             \
// c               s
//
// With this encoding, cats would be represented as 0000110111.
// 
// Given a dictionary of character frequencies, build a Huffman tree, and use it to
// determine a mapping between characters and their encoded binary strings.

void Main()
{
	GetCodes(new Dictionary<char, int>() { { 'a', 1 }, { 'b', 1 }, { 'c', 1 }, { 'd', 1 }, }).Dump();
	GetCodes(new Dictionary<char, int>() { { 'a', 5 }, { 'b', 9 }, { 'c', 12 }, { 'd', 13 }, { 'e', 16 }, { 'f', 45 }, }).Dump();
}

Dictionary<char, string> GetCodes(Dictionary<char, int> freq)
{
	var root = BuildTree(freq);

	var codesMap = GetCodes(root);
	
	return codesMap;
}

Dictionary<char, string> GetCodes(HuffmanNode root)
{
	var codes = new Dictionary<char, string>();
	
	FillCodesRecoursive(root, string.Empty, codes);
	
	return codes;
}

void FillCodesRecoursive(HuffmanNode cur, string code, Dictionary<char, string> codes)
{
	if (cur.IsLeaf())
		codes[cur.C] = code;
		
	if (cur.Left != null)
		FillCodesRecoursive(cur.Left, code + "0", codes);

	if (cur.Right != null)
		FillCodesRecoursive(cur.Right, code + "1", codes);
}

HuffmanNode BuildTree(Dictionary<char, int> freq)
{
	// i've read a blueprint of how to build a tree
	// in geeks-for-geek, I would not be able to come up with that algorithm
	// that's is ridicioulus, quote:
	//
	// "In 1951, David A. Huffman and his MIT information theory classmates were given the choice of a term
	// paper or a final exam. The professor, Robert M. Fano, assigned a term paper on the problem of finding
	// the most efficient binary code. Huffman, unable to prove any codes were the most efficient, was about
	// to give up and start studying for the final when he hit upon the idea of using a frequency-sorted
	// binary tree and quickly proved this method the most efficient."
	//
	// so, someone in MIT barely figured that out as part of final exam, and I am suppsed to figure that as
	// part of some little interview task? hm... 

	List<HuffmanNode> temp = freq.Select(x => new HuffmanNode(x.Key, x.Value)).ToList();
	
	// here is basic definition
	// a. start with all chars being disconnected nodes
	// b. pick two nodes having minimal sum of frequencies
	// c. construct a single node out of two by placing one to the left
	//    and another one to the right
	// d. repeat from b until we have only a single tree
	while (temp.Count > 1)
	{
		HuffmanNode[] arr = temp.OrderBy(x => x.Freq).Take(2).ToArray();
		
		HuffmanNode a = arr[0];
		HuffmanNode b = arr[1];
		
		// make a new node and make it the root over these two!
		HuffmanNode r = new HuffmanNode(default(char), a.Freq + b.Freq, a, b);
		
		temp.Remove(a);
		temp.Remove(b);
		
		temp.Add(r);
	}
	
	
	return temp.Single();
}

public class HuffmanNode
{
	public int Freq { get; set; }
	public char C { get; set; }
	public HuffmanNode Left { get; set; }
	public HuffmanNode Right { get; set; }
	
	public HuffmanNode(char c, int freq, HuffmanNode left = null, HuffmanNode right = null)
	{
		C = c;
		Freq = freq;
		Left = left;
		Right = right;
	}
	
	public bool IsLeaf()
	{
		return C != default(char);
	}
}