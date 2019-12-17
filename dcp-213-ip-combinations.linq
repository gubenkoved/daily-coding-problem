<Query Kind="Program" />

// This problem was asked by Snapchat.
// 
// Given a string of digits, generate all possible valid IP address combinations.
// 
// IP addresses must follow the format A.B.C.D, where A, B, C, and D are numbers
// between 0 and 255. Zero-prefixed numbers, such as 01 and 065, are not allowed,
// except for 0 itself.
// 
// For example, given "2542540123", you should return ['254.25.40.123', '254.254.0.123'].

void Main()
{
	PossibleIPs("2542540123").Dump();
	PossibleIPs("127001").Dump();
	PossibleIPs("0000").Dump();
	PossibleIPs("00000").Dump("should be none");
	PossibleIPs("255255255255").Dump();
	PossibleIPs("10101010").Dump();
}

IEnumerable<string> PossibleIPs(string s)
{
	var found = new HashSet<string>();
	
	Search(s, 0, found);
	
	return found;
}

void Search(string cur, int segmentNumber, HashSet<string> found)
{
	int lastDotIdx = cur.LastIndexOf('.');
	
	int startIdx = lastDotIdx + 1; // handles no dots gracefully as well! -1 + 1 = 0, exactly what we need!
	
	Predicate<string> segmentTester = seg => seg.Length >= 1 && seg.Length <= 3 && int.Parse(seg) <= 255 && (seg[0] != '0' || seg.Length == 1);
	
	int curIdx = startIdx;
	
	string newSegment = string.Empty;

	while (curIdx < cur.Length)
	{
		newSegment += cur[curIdx];

		if (!segmentTester(newSegment))
			break;

		if (segmentNumber < 3) // go deeper
		{
			string newCur = cur.Insert(curIdx + 1, ".");
			
			Search(newCur, segmentNumber + 1, found);
		}
		else // done, check last segment and add if valid
		{
			string lastSegment = cur.Substring(curIdx, cur.Length - curIdx);
			
			if (segmentTester(lastSegment))
				found.Add(cur);
				
			return;
		}
		
		// okay, move!
		curIdx += 1;
	}
}