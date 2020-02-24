<Query Kind="Program" />

// This problem was asked by PayPal.
// 
// Given a string and a number of lines k, print the string in zigzag form.
// In zigzag, characters are printed out diagonally from top left to bottom right
// until reaching the kth line, then back up to top right, and so on.
// 
// For example, given the sentence "thisisazigzag" and k = 4, you should print:
// 
// t     a     g
//  h   s z   a
//   i i   i z
//    s     g
// 

void Main()
{
	ZigZagPrint("thisisazigzag", 4);
	ZigZagPrint("thisisazigzagthisisazigzagthisisazigzagthisisazigzagthisisazigzagthisisaz", 9);
	ZigZagPrint("thisisazigzagthisisazigzagthisisazigzagthisisazigzagthisisazigzagthisisaz", 5);
}

void ZigZagPrint(string s, int k)
{
	char[,] plane = new char[k,s.Length];
	
	bool down = true;
	int lineNumber = 0;
	
	
	for (int i = 0; i < s.Length; i++)
	{
		plane[lineNumber, i] = s[i];
		
		// bounce!
		if (down && lineNumber == k - 1 || !down && lineNumber == 0)
			down = !down;
		
		// update line number
		lineNumber = down ? lineNumber + 1 : lineNumber - 1;
	}

	for (int i = 0; i < k; i++)
	{
		for (int j = 0; j < s.Length; j++)
		{
			if (plane[i, j] == default(char))
				Console.Write(' ');
			else
				Console.Write(plane[i, j]);
		}
		Console.WriteLine();
	}
}
