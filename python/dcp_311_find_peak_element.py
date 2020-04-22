# This problem was asked by Sumo Logic.

# Given an unsorted array, in which all elements are distinct, find a "peak"
# element in O(log N) time.

# An element is considered a peak if it is greater than both its left and
# right neighbors. It is guaranteed that the first and last elements are lower
# than all others.

def find_peak(a, left=None, right=None) -> int:
    if left is None or right is None:
        return find_peak(a, 0, len(a))

    m = (left + right) // 2

    # we now basically have 4 cases:
    # 1. a[m-1] < a[m] and a[m+1] < a[m] -> found peak
    # 2. a[m-1] > a[m] and a[m+1] > a[m] -> go at any direction as there are peaks at either way
    # 3. a[m-1] < a[m] and a[m+1] > a[m] -> go right
    # 4. a[m-1] > a[m] and a[m+1] < a[m] -> go left

    if a[m - 1] < a[m] and a[m + 1] < a[m]:
        return m  # found it!
    elif a[m - 1] > a[m] and a[m + 1] > a[m]:
        return find_peak(a, left, m)  # whatever will work
    elif a[m - 1] < a[m] and a[m + 1] > a[m]:
        return find_peak(a, m, right)  # go right
    elif a[m - 1] > a[m] and a[m + 1] < a[m]:
        return find_peak(a, left, m)  # go left
    else:
        raise Exception('should not be there')


assert(find_peak([1, 3, 4, 5, 10, 12, 1]) == 5)
assert(find_peak([1, 3, 4, 5, 2, 1]) == 3)
assert(find_peak([1, 3, 2]) == 1)
