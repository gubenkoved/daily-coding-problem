<Query Kind="Program" />

// This problem was asked by Facebook.
// 
// Given a number in Roman numeral format, convert it to decimal.
// 
// The values of Roman numerals are as follows:
// 
// {
//     'M': 1000,
//     'D': 500,
//     'C': 100,
//     'L': 50,
//     'X': 10,
//     'V': 5,
//     'I': 1
// }
//
// In addition, note that the Roman numeral system uses subtractive notation for numbers such as IV and XL.
// 
// For the input XIV, for instance, you should return 14.

void Main()
{
	RomanToDecimal("XIV").Dump("14");
	RomanToDecimal("XV").Dump("15");
	RomanToDecimal("XVI").Dump("16");
	RomanToDecimal("XXXV").Dump("35");
	RomanToDecimal("LXV").Dump("65");
	RomanToDecimal("LIX").Dump("59");
	RomanToDecimal("XCIX").Dump("99");
}

int RomanToDecimal(string roman)
{
	// we can start from converting all romao symbols into the numberic values
	// then we need to group into individually recognizable and evaluatable tokens
	// and then calculate sum for all the tokens
	// token groups lexems while value of lexem is increasing
	
	int[] lexems = roman.Select(SymbolToDecimal).ToArray();
	
	
	
	int n = 0;
	
	List<int[]> tokens = new List<int[]>();
	
	List<int> curToken = new List<int>();
	
	for (int i = 0; i < lexems.Length; i++)
	{
		if (curToken.Any())
		{
			int lastLexem = curToken[curToken.Count - 1];

			if (lexems[i] <= lastLexem)
			{
				tokens.Add(curToken.ToArray());
				curToken.Clear();
			}
		}
		
		// else - simply continue token
		
		curToken.Add(lexems[i]);
	}
	
	// flush the last token
	tokens.Add(curToken.ToArray());

	Util.Metatext($"tokenized: {roman} -> ({string.Join(", ", tokens.Select(token => "[" + string.Join(",", token) + "]"))}) -> ({string.Join(", ", tokens.Select(TokenValue))}) -> {tokens.Sum(TokenValue)}").Dump();
	
	return tokens.Sum(TokenValue);
}

int TokenValue(int[] token)
{
	// start with last lexem and subtract all to the left
	
	int n = token[token.Length - 1];
	
	for (int i = 0; i < token.Length - 1; i++)
		n -= token[i];
		
	return n;
}

int SymbolToDecimal(char romanSymbol)
{
	Dictionary<char, int> map = new Dictionary<char, int>()
	{
		{ 'M', 1000 },
		{ 'D', 500 },
		{ 'C', 100 },
		{ 'L', 50 },
		{ 'X', 10 },
		{ 'V', 5 },
		{ 'I', 1 },
	};
	
	if (!map.ContainsKey(romanSymbol))
		throw new Exception($"Unknown symbol: {romanSymbol}");
	
	return map[romanSymbol];
}