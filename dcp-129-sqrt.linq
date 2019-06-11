<Query Kind="Program" />

// Given a real number n, find the square root of n. For example, given n = 9, return 3.

void Main()
{
	for (int i = 1; i < 100; i++)
		$"Square root of {i} is {Sqrt(i)}".Dump();
}

double Sqrt(double x, int digits = 10)
{
	//return Math.Sqrt(x); // no? okay...
	
	double precision = 1;
	
	for (int i = 0; i <= digits; i++)
		precision /= 10.0;
	
	double t = SqrtBinaryFind(x, 0, x, precision);
	
	return Math.Round(t, digits);
}

double SqrtBinaryFind(double x, double l, double r, double precision)
{
	double c = (l + r) / 2;

	if (r - l <= precision)
		return c;
	
	if (c * c > x)
		return SqrtBinaryFind(x, l, c, precision);
	else
		return SqrtBinaryFind(x, c, r, precision);
}

