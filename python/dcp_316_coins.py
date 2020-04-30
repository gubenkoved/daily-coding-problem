# This problem was asked by Snapchat.

# You are given an array of length N, where each element i represents
# the number of ways we can produce i units of change. For example,
# [1, 0, 1, 1, 2] would indicate that there is only one way to make 0,
# 2, or 3 units, and two ways of making 4 units.

# Given such an array, determine the denominations that must be in use.
# In the case above, for example, there must be coins with value 2, 3, and 4.

from typing import Iterable

def solve(a: Iterable[int]):
    cur = [0 for _ in a]
    coins = set()
    for idx in range(1, len(a)):
        if a[idx] - cur[idx] == 1:
            coins.add(idx)
            # mark every i-th as reachable (like in eratosphen sieve)
            for i in range(idx, len(a), idx):
                cur[i] += 1
        elif a[idx] - cur[idx] != 0:
            return None
    return coins


assert(solve([1, 0, 1, 1, 2]) == set([2, 3, 4]))
assert(solve([1, 1, 2, 0]) is None)
