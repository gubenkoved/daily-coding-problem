<Query Kind="Program" />

// This problem was asked by Dropbox.
//
// Spreadsheets often use this alphabetical encoding for its columns:
// "A", "B", "C", ..., "AA", "AB", ..., "ZZ", "AAA", "AAB", ....
//
// Given a column number, return its alphabetical column id. For example,
// given 1, return "A". Given 27, return "AA".

void Main()
{
	for (int i = 1; i <= 17576; i++)
		$"{i} -> {ColId(i)}".Dump();
}

string ColId(int n)
{
	string id = string.Empty;
	
	bool stop;
	
	do
	{		
		char c = (char)((int)'A' + ((n - 1) % 26));
		
		stop = n <= 26;
		
		n = 1 + ((n - 1) / 26);
		
		id = c + id;
	} while (!stop);
	
	return id;
}
