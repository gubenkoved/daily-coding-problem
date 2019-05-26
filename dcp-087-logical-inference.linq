<Query Kind="Program" />

// This problem was asked by Uber.
// 
// A rule looks like this:
// 
// A NE B
// 
// This means this means point A is located northeast of point B.
// 
// A SW C
// 
// means that point A is southwest of C.
// 
// Given a list of rules, check if the sum of the rules validate. For example:
// 
// A N B
// B NE C
// C N A
// does not validate, since A cannot be both north and south of C.
// 
// A NW B
// A N B
// is considered valid.

void Main()
{
	Statement[] statements;
	
	// nope
	statements = new []
	{
		new Statement("A", P.N, "B"),
		new Statement("B", P.NE, "C"),
		new Statement("C", P.N, "A"),
	};
	
	// nope
	statements = new []
	{
		new Statement("A", P.N, "B"),
		new Statement("B", P.N, "A"),
	};
	
	// ok
	statements = new []
	{
		new Statement("A", P.NW, "B"),
		new Statement("A", P.N, "B"),
	};

	// nope
	statements = new[]
	{
		new Statement("A", P.N, "B"),
		new Statement("D", P.N, "E"),
		new Statement("E", P.N, "A"),
		new Statement("B", P.N, "C"),
		new Statement("C", P.N, "D"), // close the gap
	};

	Validator.AreCompatible(statements).Dump();
}

public enum P // predicates
{
	N = 1,
	NE = 2,
	E = 3,
	SE = 4,
	S = 5,
	SW = 6,
	W = 7,
	NW = 8,
}

public class Statement : IEquatable<Statement>
{
	public string Left { get; }
	public P Predicate { get; }
	public string Right { get; }
	
	public Statement(string left, P predicate, string right)
	{
		Left = left;
		Predicate = predicate;
		Right = right;
	}

	// returns statement where Left/Right are exchanged
	// and Predicate is adjusted to form new equvalent statement
	public Statement InverseObjects()
	{
		return new Statement(Right, Inverse(Predicate), Left);
	}
	
	public static P Inverse(P predicate)
	{
		switch (predicate)
		{
			case P.N: return P.S;
			case P.NE: return P.SW;
			case P.E: return P.W;
			case P.SE: return P.NW;
			case P.S: return P.N;
			case P.SW: return P.NE;
			case P.W: return P.E;
			case P.NW: return P.SE;
			default: throw new NotImplementedException();
		}
	}
	
	public static bool AreCompatible(Statement a, Statement b)
	{
		if (a.Left != b.Left || a.Right != b.Right)
			return true; // can not find any contradictions if statements are about different things
			
		// okay, statements are about the same things, check predicates
		Dictionary<P, P[]> compatibilityMatrix = new Dictionary<P, P[]>()
		{
			{ P.N, new [] { P.NW, P.N, P.NE }},
			{ P.NE, new [] { P.N, P.NE, P.E }},
			{ P.E, new [] { P.NE, P.E, P.SE }},
			{ P.SE, new [] { P.E, P.SE, P.S }},
			{ P.S, new [] { P.SE, P.S, P.SW }},
			{ P.SW, new [] { P.S, P.SW, P.W }},
			{ P.W, new [] { P.SW, P.W, P.NW }},
			{ P.NW, new [] { P.NW, P.N, P.W }},
		};
		
		return compatibilityMatrix[a.Predicate].Contains(b.Predicate);
	}
	
	public static Statement Infer(Statement a, Statement b)
	{
		// A p1 B and B p2 C -> A p3 C (in some cases)
		if (a.Right != b.Left)
			return null; // no chaining, can not infer anything
			
		Dictionary<Tuple<P, P>, P> inferenceMap = new Dictionary<Tuple<P, P>, P>()
		{
			{ Tuple.Create(P.N, P.N), P.N }, 
			{ Tuple.Create(P.N, P.NE), P.NE },
			{ Tuple.Create(P.N, P.NW), P.NW },

			{ Tuple.Create(P.NE, P.N), P.NE },
			{ Tuple.Create(P.NE, P.NE), P.NE },
			{ Tuple.Create(P.NE, P.E), P.NE },

			{ Tuple.Create(P.E, P.NE), P.NE },
			{ Tuple.Create(P.E, P.E), P.E },
			{ Tuple.Create(P.E, P.SE), P.SE },

			{ Tuple.Create(P.SE, P.E), P.SE },
			{ Tuple.Create(P.SE, P.SE), P.SE },
			{ Tuple.Create(P.SE, P.S), P.SE },

			{ Tuple.Create(P.S, P.SE), P.SE },
			{ Tuple.Create(P.S, P.S), P.S },
			{ Tuple.Create(P.S, P.SW), P.SW },

			{ Tuple.Create(P.SW, P.S), P.SW },
			{ Tuple.Create(P.SW, P.SW), P.SW },
			{ Tuple.Create(P.SW, P.W), P.SW },

			{ Tuple.Create(P.W, P.SW), P.SW },
			{ Tuple.Create(P.W, P.W), P.W },
			{ Tuple.Create(P.W, P.NW), P.NW },

			{ Tuple.Create(P.NW, P.W), P.NW },
			{ Tuple.Create(P.NW, P.NW), P.NW },
			{ Tuple.Create(P.NW, P.N), P.NW },
		};
		
		var key = Tuple.Create(a.Predicate, b.Predicate);
		
		if (!inferenceMap.ContainsKey(key))
			return null;
			
		// inferred a new statement!
		return new Statement(a.Left, inferenceMap[key], b.Right);
	}

	public override string ToString()
	{
		return $"{Left} {Predicate} {Right}";
	}

	public bool Equals(Statement other)
	{
		return Left == other.Left
			&& Predicate == other.Predicate
			&& Right == other.Right;
	}
}

public static class Validator
{
	public static bool AreCompatible(IEnumerable<Statement> statements)
	{
		List<Statement> workingSet = new List<Statement>();
		
		foreach (var statement in statements)
		{
			workingSet.Add(statement);
			workingSet.Add(statement.InverseObjects());
		}
		
		IEnumerable<Statement> inferred;
		
		do
		{
			Util.Metatext("New inferrence round!").Dump();
			
			// and interesting thing there is that
			// when we add new statement we must recheck all pairs to make sure they are 
			// _still_ compatible

			inferred = InferAll(workingSet);

			foreach (var inferredStatement in inferred)
			{
				Util.Metatext($" Inferred: {inferredStatement}").Dump();
				workingSet.Add(inferredStatement);
			}

			if (!AreCompatibleSimple(workingSet))
				return false;

		} while (inferred.Any());
		
		return true;
	}

	// checks if all pairs of statements are compatible
	private static bool AreCompatibleSimple(IEnumerable<Statement> statements)
	{
		foreach (var a in statements)
		{
			foreach (var b in statements)
			{
				if (!Statement.AreCompatible(a, b))
				{
					Util.Metatext($"{a} is not compatible with {b}").Dump();
					return false;
				}
			}
		}
		
		return true;
	}
	
	private static IEnumerable<Statement> InferAll(IEnumerable<Statement> statements)
	{
		List<Statement> inferred = new List<Statement>();
		
		foreach (var a in statements)
		{
			foreach (var b in statements)
			{
				if (a.Equals(b))
					continue;
				
				var inferredStatement = Statement.Infer(a, b);
				
				if (inferredStatement != null
					&& !statements.Contains(inferredStatement)
					&& !inferred.Contains(inferredStatement))
					inferred.Add(inferredStatement);
			}
		}
		
		return inferred;
	}
}