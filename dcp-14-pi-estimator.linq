<Query Kind="Program" />

// This problem was asked by Google.
// 
// The area of a circle is defined as πr^2. Estimate π to 3 decimal places using a Monte Carlo method.
// 
// Hint: The basic equation of a circle is x2 + y2 = r2.


void Main()
{
	// take square 2x2
	// throw N points and count which proportion is within circle
	Random r = new Random();
	
	int n = 100000000;
	int within = 0;
	
	for (int i = 0; i < n; i++)
	{
		double x = r.NextDouble() * 2 - 1;
		double y = r.NextDouble() * 2 - 1;
		
		if (IsWithin(x, y, 1))
			within += 1;
	}
	
	(4 * (within / (double) n)).Dump("PI");
}

public bool IsWithin(double x, double y, double r)
{
	return x * x + y * y <= r * r;
}
