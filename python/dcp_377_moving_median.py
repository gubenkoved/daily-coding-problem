# This problem was asked by Microsoft.
#
# Given an array of numbers arr and a window of size k, print out the median of each
# window of size k starting from the left and moving right by one position each time.
#
# For example, given the following array and k = 3:
#
# [-1, 5, 13, 8, 2, 3, 3, 1]
# Your function should print out the following:
#
# 5 <- median of [-1, 5, 13]
# 8 <- median of [5, 13, 8]
# 8 <- median of [13, 8, 2]
# 3 <- median of [8, 2, 3]
# 3 <- median of [2, 3, 3]
# 3 <- median of [3, 3, 1]
#
# Recall that the median of an even-sized list is the average of the two middle
# numbers.

import bisect


# O(n * k)
def moving_median(a, k):
    result = []
    window = []

    for idx in range(len(a)):
        x = a[idx]

        bisect.insort(window, x)

        # populate the k elements for the window
        if len(window) < k:
            continue

        if len(window) > k:
            # evict the element that went out of the window
            victim = a[idx - k]
            victim_idx = bisect.bisect_left(window, victim)
            del window[victim_idx]  # O(k)

        if len(window) == k:
            if k % 2 == 1:
                median = window[k // 2]
            else:
                median = (window[k // 2] + window[k // 2 - 1]) / 2

            # add the median
            result.append(median)

    return result


assert moving_median([-1, 5, 13, 8, 2, 3, 3, 1], k=3) == [5, 8, 8, 3, 3, 3]
assert moving_median([1, 2, 3, 4, 5], k=3) == [2, 3, 4]
assert moving_median([1, 2, 3, 2, 5, 2, 6], k=3) == [2, 2, 3, 2, 5]
