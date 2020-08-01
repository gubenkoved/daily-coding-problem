# This problem was asked by Airbnb.

# You are given an array X of floating-point numbers x1, x2, ... xn. These can
# be rounded up or down to create a corresponding array Y of integers
# y1, y2, ... yn.

# Write an algorithm that finds an appropriate Y array with the following
# properties:

# The rounded sums of both arrays should be equal.
# The absolute pairwise difference between elements is minimized. In other
# words, |x1- y1| + |x2- y2| + ... + |xn- yn| should be as small as possible.
# For example, suppose your input is [1.3, 2.3, 4.4]. In this case you cannot
# do better than [1, 2, 5], which has an absolute difference of
# |1.3 - 1| + |2.3 - 2| + |4.4 - 5| = 1.

from typing import List
from math import ceil, floor
from random import random


# not sure if greedy algorithm will yield the optimal result, but
# it feels like it can be a way to go; proving that it works looks
# like a harder problem...
# checking agains brute force solution shows that this algorithm
# does yield optimal results
# Time: O(n logn)
def naive(a: List[int]) -> List[int]:
    target = round(sum(a))
    r = [floor(x) for x in a]
    d = target - sum(r)

    if d > 0:
        # pick the item x which is the closest to the ceil(x)
        tmp = [(idx, a[idx]) for idx in range(len(a)) if a[idx] != round(a[idx])]
        tmp = sorted(tmp, key=lambda kvp: ceil(kvp[1]) - kvp[1])

        for idx in range(d):
            result_idx = tmp[idx][0]
            r[result_idx] += 1

    assert(sum(r) == target)

    return r


print(naive([1.3, 2.3, 4.4]))

# random inputs
for _ in range(10):
    a = [round(random() * 10, 1) for _ in range(5)]
    r = naive(a)
    print(f'{a} -> {r}')
