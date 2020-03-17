<Query Kind="Program" />

// This problem was asked by VMware.
// 
// The skyline of a city is composed of several buildings of various widths
// and heights, possibly overlapping one another when viewed from a distance.
// We can represent the buildings using an array of (left, right, height) tuples,
// which tell us where on an imaginary x-axis a building begins and ends, and how
// tall it is. The skyline itself can be described by a list of (x, height) tuples,
// giving the locations at which the height visible to a distant observer changes,
// and each new height.
// 
// Given an array of buildings as described above, create a function that returns
// the skyline.
// 
// For example, suppose the input consists of the buildings [(0, 15, 3), (4, 11, 5),
// (19, 23, 4)]. In aggregate, these buildings would create a skyline that looks like
// the one below.
// 
//      ______  
//     |      |        ___
//  ___|      |___    |   | 
// |   |   B  |   |   | C |
// | A |      | A |   |   |
// |   |      |   |   |   |
// ------------------------
//
// As a result, your function should return [(0, 3), (4, 5), (11, 3), (15, 0), (19, 4), (23, 0)].

void Main()
{
	Skyline(new[] { (0, 5, 1), (1, 4, 2), }).Dump();
	Skyline(new[] { (0, 5, 1), (1, 4, 2), (2, 3, 3) }).Dump();
	Skyline(new[] { (0, 15, 3), (4, 11, 5), (19, 23, 4) }).Dump();
	Skyline(new[] { (0, 15, 1), (1, 14, 2), (2, 13, 3), (3, 12, 4) }).Dump();
}

(int x, int h)[] Skyline((int l, int r, int h)[] a)
{
	// the first stage is to sort all these ranges and eliminate the overlappings, possible splitting single building in
	// half it it's overlapped in the middle, then figuring out the skyline is rather trivial
	
	a = Simplify(a);
	
	var skyline = new List<(int x, int h)>();
	
	for (int i = 0; i < a.Length; i++)
	{
		skyline.Add((a[i].l, a[i].h));
		
		// see if there is a gap with the next
		if (i == a.Length - 1 || a[i + 1].l > a[i].r)
			skyline.Add((a[i].r, 0));
	}
	
	return skyline.ToArray();
}

// removes the overlapping peices
// O(n * k) where n is total amount and k is amout of intersections
(int l, int r, int h)[] Simplify((int l, int r, int h)[] a)
{
	// make sure the we order by the beging of the left side ofthe building
	var ll = new LinkedList<(int l, int r, int h)>(a.OrderBy(x => x.l));

	// cases:
	// 1 - no intersction -- do nothing
	// |-----------|
	//               |-------------|
	//
	// 2 - partial intersection -- cut the first one or the second one, whichever is smaller by height
	// |-----------|                >   |-----------|
	//           |-------------|    >               |-----------|
	//
	// 3 - full inclusion -- if the incuded part is higher cut the wider one, otherwise simply get rid of the included one
	// |---------|   >  |-|     |-|      OR     >   |---------|
	//   |-----|     >    |-----|               >

	bool again;

	do
	{
		// single pass:
		var cur = ll.First;
		var next = cur.Next;

		again = false;

		while (next != null)
		{
			// there is an intersction (2) or (3)
			if (cur.Value.r > next.Value.l)
			{
				if (cur.Value.r >= next.Value.r)
				{
					// inclusion case (3)
					if (next.Value.h < cur.Value.h)
					{
						// we included a shorter one, it can be delted
						ll.Remove(next);
					} else
					{
						// included building is higher one, we would need to split
						// the wider building in two parts
						var p1 = (cur.Value.l, next.Value.l, cur.Value.h);
						var p2 = (next.Value.r, cur.Value.r, cur.Value.h);
						
						ll.AddAfter(cur, p1);
						//ll.AddAfter(next, p2);
						ll.Remove(cur);
						
						// p2 order is uncertain, so we have to find the right spot
						InsertPreservingOrder(ll, p2);
					}
				} else
				{
					// partial intersection case (2)
					if (cur.Value.h <= next.Value.h)
					{
						// shrink the first one
						ll.AddAfter(cur, (cur.Value.l, next.Value.l, cur.Value.h));
						ll.Remove(cur);
					} else
					{
						// shrink the second one
						ll.AddAfter(next, (cur.Value.r, next.Value.r, next.Value.h));
						ll.Remove(next);
					}
				}

				again = true;
				break; // start over!
			}
			
			cur = next;
			next = cur.Next;
		}
		
	} while (again);
	
	return ll.ToArray();
}

void InsertPreservingOrder(LinkedList<(int l, int r, int h)> ll, (int l, int r, int h) b)
{
	var cur = ll.First;
	
	while (cur != null)
	{
		if (cur.Value.l > b.l)
		{
			ll.AddBefore(cur, b);
			return;
		}
		
		cur = cur.Next;
	}
	
	// add as the last one
	ll.AddLast(b);
}
