# This problem was asked by Airbnb.
#
# Given an array of integers, return the largest range, inclusive, of integers that
# are all included in the array.
#
# For example, given the array [9, 6, 1, 3, 8, 10, 12, 11], return (8, 12)
# since 8, 9, 10, 11, and 12 are all in the array.


# O(nlogn)
def largest_range(a):
    if not a:
        return None

    a = sorted(set(a))
    start_idx = 0
    best = None

    def best_len():
        return best[1] - best[0] + 1

    for idx in range(1, len(a) + 1):
        if idx == len(a) or a[idx] != a[idx - 1] + 1:
            # we got a new range for indexes [start_idx, idx-1]
            if best is None or idx - start_idx > best_len():
                best = (start_idx, idx-1)
            start_idx = idx

    best_range = (a[best[0]], a[best[1]])
    print(best_range)
    return best_range


# assert largest_range([]) is None
# assert largest_range([1]) == (1, 1)
# assert largest_range([1, 2, 3, 4, 5]) == (1, 5)
# assert largest_range([1, 2, 3, 4, 4, 4, 5]) == (1, 5)
# assert largest_range([1, 2, 3, 400, 5]) == (1, 3)
assert largest_range([1, 2, 3, 400, 5, 6, 7, 8]) == (5, 8)
assert largest_range([1, 2, 3, 400, 5, 6, 7, 8, 4]) == (1, 8)
assert largest_range([2, 9, 6, 1, 3, 8, 10, 12, 11, 4, 5]) == (1, 6)
assert largest_range([9, 6, 1, 3, 8, 10, 12, 11]) == (8, 12)
