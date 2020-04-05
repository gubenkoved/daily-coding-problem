# This problem was asked by Palantir.

# You are given a list of N numbers, in which each number is located at most k places
# away from its sorted position. For example, if k = 1, a given element at index 4
# might end up at indices 3, 4, or 5.

# Come up with an algorithm that sorts this list in O(N log k) time.

import heapq

def sort(a, k):

    # we can support a min heap with a capacity of k elements as a buffer
    # and gradually push the element and pop the mininum one

    h = a[:k]
    heapq.heapify(h)

    sorted = [None for i in range(len(a))]

    for idx in range(len(a)):
        if (idx + k < len(a)):
            sorted[idx] = heapq.heappushpop(h, a[idx + k])
        else:
            sorted[idx] = heapq.heappop(h)
    return sorted

print(sort([ 2, 1, 3, 5, 4, 6 ], k=1))
print(sort([ 3, 4, 1, 2, 6, 5, 7, 9, 8 ], k=2))
