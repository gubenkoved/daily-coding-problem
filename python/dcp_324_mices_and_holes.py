# This problem was asked by Amazon.

# Consider the following scenario: there are N mice and N holes placed at integer points
# along a line. Given this, find a method that maps mice to holes such that the largest
# number of steps any mouse takes is minimized.

# Each move consists of moving one mouse one unit to the left or right, and only one mouse
# can fit inside each hole.

# For example, suppose the mice are positioned at [1, 4, 9, 15], and the holes are located
# at [10, -5, 0, 16]. In this case, the best pairing would require us to send the mouse at
# 1 to the hole at -5, so our function should return 6.

# O(n*logn)
def f(mices, holes) -> int:

    # it's obvious from the simple reason to prove that the best way to minimize the biggest
    # distance is simple match first mice to the first hole, so that there will be no
    # "intersections" if drawn as a bipartite graph as every intersection can be "resolved"
    # into a better solution via a unwiding the intersection

    mices.sort()
    holes.sort()
    return max([abs(mices[i] - holes[i]) for i in range(len(mices))])


print(f([1, 4, 9, 15], [10, -5, 0, 16]))