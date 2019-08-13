<Query Kind="Program" />

void Main()
{
	var m = new [,]
	{
		{ 1, 2, 3, 4, },
		{ 5, 6, 7, 8, },
		{ 9, 10, 11, 12, },
		{ 13, 14, 15, 16, },
	};
	
	Rotate(m).Dump();
	
	RotateInplace(m);
	
	m.Dump();

	var m2 = new[,]
	{
		{ 01, 02, 03, 04, 05 },
		{ 06, 07, 08, 09, 10 },
		{ 11, 12, 13, 14, 15 },
		{ 16, 17, 18, 19, 20 },
		{ 21, 22, 23, 24, 25 },
	};

	RotateInplace(m2);
	
	m2.Dump();
}

// uses additional space
int[,] Rotate(int[,] m)
{
	int n = m.GetLength(0);
	
	int[,] r = new int[n,n];
	
	for (int i = 0; i < n; i++)
	{
		for (int j = 0; j < n; j++)
		{
			int i_new = j;
			int j_new = n - 1 - i;
			
			r[i_new, j_new] = m[i, j];
		}
	}
	
	return r;
}

void RotateInplace(int[,] m)
{
	int n = m.GetLength(0);
	
	for (int i = 0; i <= n / 2; i++)
	{
		for (int j = i; j < n - i - 1; j++)
		{
			//$"R({i}, {j})".Dump();
			
			R(m, i, j);
			
			//m.Dump();
		}
	}
}

// rotates either 4 elements around the center starting at driver (x, y)
// or does nothing when element is the center
void R(int[,] m, int x, int y)
{
	int n = m.GetLength(0);
	
	int x2, y2;
	int x3, y3;
	int x4, y4;
	
	T(n, x, y, out x2, out y2);
	T(n, x2, y2, out x3, out y3);
	T(n, x3, y3, out x4, out y4);
	
	// okay, so we got 4 coordinates and we need to rotate these 4 elements clock-wise
	int tmp = m[x, y];
	
	m[x, y] = m[x4, y4];
	m[x4, y4] = m[x3, y3];
	m[x3, y3] = m[x2, y2];
	m[x2, y2] = tmp;
}

// returns the final destination for any given (x, y)
// after the rotation
void T(int n, int x, int y, out int x2, out int y2)
{
	x2 = y;
	y2 = n - 1 - x;
}