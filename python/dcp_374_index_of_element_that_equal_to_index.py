# This problem was asked by Amazon.
#
# Given a sorted array arr of distinct integers, return the lowest index i for which
# arr[i] == i. Return null if there is no such index.
#
# For example, given the array [-5, -3, 2, 3], return 2 since arr[2] == 2. Even
# though arr[3] == 3, we return 2 since it's the lowest index.

import math


# O(n)
def idx_naive(a):
    for idx in range(len(a)):
        if idx == a[idx]:
            return idx
    return None


def idx_binary(a):
    def _q(lo, hi):
        if hi - lo <= 1:
            return lo if a[lo] == lo else None

        med = math.floor((lo + hi) / 2)

        if a[med] == med:
            return med

        return _q(lo, med) if a[med] > med else _q(med, hi)

    idx = _q(0, len(a))

    if idx is None:
        return None

    # if we find a matching point, there could be better
    # ones, but they have to be adjacent, otherwise it's impossible
    # given the constraints that array is sorted and elements are the same
    while idx > 0 and a[idx - 1] == idx - 1:
        idx -= 1

    return idx


# f = idx_naive
f = idx_binary

assert f([0]) == 0
assert f([-1, 0, 1, 2, 3, 5]) == 5
assert f([-1, 0, 1, 2, 3, 4, 6]) == 6
assert f([0, 1, 2, 3, 4]) == 0
assert f([-2, 0, 2, 4, 6]) == 2
assert f([-5, -3, 2, 3]) == 2
assert f([-10, -5, 0, 3, 30]) == 3
assert f([5, 50, 500]) is None
assert f([-1]) is None
