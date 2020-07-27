# This problem was asked by Square.

# You are given a histogram consisting of rectangles of different heights. These 
# heights are represented in an input list, such that [1, 3, 2, 5] corresponds
# to the following diagram:

#       x
#       x  
#   x   x
#   x x x
# x x x x

# Determine the area of the largest rectangle that can be formed only from the
# bars of the histogram. For the diagram above, for example, this would be six,
# representing the 2 x 3 area at the bottom right.

from typing import List


# O(n^2)
def max_area(h: List[int]):
    best_area = 0
    for lowest_idx in range(len(h)):
        # expand while possible to both left and right
        area = h[lowest_idx]

        idx = lowest_idx + 1
        while idx < len(h) and h[idx] >= h[lowest_idx]:
            area += h[lowest_idx]
            idx += 1

        idx = lowest_idx - 1
        while idx >= 0 and h[idx] >= h[lowest_idx]:
            area += h[lowest_idx]
            idx -= 1

        best_area = max(best_area, area)

    return best_area


assert max_area([1, 3, 2, 5]) == 6
assert max_area([1, 2, 3, 4, 5]) == 9
assert max_area([1, 4, 1, 1, 1]) == 5
assert max_area([1, 10, 1, 1, 1]) == 10
