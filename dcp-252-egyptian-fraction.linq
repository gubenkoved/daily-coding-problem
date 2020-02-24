<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Numerics.dll</Reference>
  <Namespace>System.Numerics</Namespace>
</Query>

// This problem was asked by Palantir.
// 
// The ancient Egyptians used to express fractions as a sum of several terms
// where each numerator is one. For example, 4 / 13 can be represented as
// 1 / 4 + 1 / 18 + 1 / 468.
// 
// Create an algorithm to turn an ordinary fraction a / b, where a < b, into
// an Egyptian fraction.

void Main()
{
	P(ToEgyptianFraction(4, 13));
	P(ToEgyptianFraction(3, 655));
	P(ToEgyptianFraction(2, 45));
	
	// primes fraction look like especialy hard to convert!
	P(ToEgyptianFraction(17, 191));
	P(ToEgyptianFraction(191, 193));
	P(ToEgyptianFraction(269, 271)); 
	P(ToEgyptianFraction(7919, 7927)); 
	P(ToEgyptianFraction(104729, 104743)); 
}

void P<T>(T[] e)
{
	string.Join(" + ", e.Select(x => $"1/{x}")).Dump();
}

BigInteger[] ToEgyptianFraction(BigInteger a, BigInteger b)
{
	List<BigInteger> dividors = new List<BigInteger>();

	while (true)
	{
		BigInteger d = b / a;

		// last step then!
		if (b % a == 0)
		{
			dividors.Add(d);
			break;
		}
		
		// nope, have to go further
		d = d + 1;
		
		dividors.Add(d);
		
		// recalc reminder
		(a, b) = (a * d - b, b * d);
	}
	
	return dividors.ToArray();
}
