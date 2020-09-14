# This problem was asked by Two Sigma.
#
# You’re tracking stock price at a given instance of time. Implement an API with the
# following functions: add(), update(), remove(), which adds/updates/removes a
# datapoint for the stock price you are tracking. The data is given as
# (timestamp, price), where timestamp is specified in unix epoch time.
#
# Also, provide max(), min(), and average() functions that give the max/min/average
# of all values seen thus far.

import heapq
from collections import defaultdict


class Tracker(object):
    def __init__(self):
        self._map = dict()
        self._count = 0
        self._sum = 0
        self._max_heap = []
        self._min_heap = []  # heapq only can handle min heap, so let's negate the numbers
        self._max_heap_blacklist = defaultdict(lambda: 0)
        self._min_heap_blacklist = defaultdict(lambda: 0)

    # O(nlogn)
    def upsert(self, ts, price):
        if ts in self._map:
            old_price = self._map[ts]
            self._max_heap_blacklist[old_price] += 1
            self._min_heap_blacklist[old_price] += 1
            self._sum -= old_price
            self._count -= 1

        heapq.heappush(self._min_heap, price)
        heapq.heappush(self._max_heap, -price)

        self._sum += price
        self._count += 1
        self._map[ts] = price

    # O(1)
    def remove(self, ts):
        if ts not in self._map:
            raise Exception('not found')

        price = self._map[ts]
        self._sum -= self._map[ts]
        self._count -= 1
        self._max_heap_blacklist[price] += 1
        self._min_heap_blacklist[price] += 1

        del self._map[ts]

    # O(1)
    def _remove_from_blacklist(self, blacklist, val):
        if blacklist[val] <= 0:
            raise Exception('broken blacklist')

        blacklist[val] -= 1
        if blacklist[val] == 0:
            del blacklist[val]

    # O(1) in the best case, O(n) in the worst case
    # hard to tell about the average, O(logn)?
    def min(self):
        if self._count == 0:
            return None

        while True:
            val = self._min_heap[0]

            if val in self._min_heap_blacklist:
                heapq.heappop(self._min_heap)
                self._remove_from_blacklist(self._min_heap_blacklist, val)
                continue

            return val

    # ditto
    def max(self):
        if self._count == 0:
            return None

        while True:
            val = -self._max_heap[0]

            if val in self._max_heap_blacklist:
                heapq.heappop(self._max_heap)
                self._remove_from_blacklist(self._max_heap_blacklist, val)
                continue

            return val

    # O(1)
    def avg(self):
        if self._count == 0:
            return None

        return self._sum / self._count


t = Tracker()

t.upsert(1, 1)
t.upsert(2, 2)
t.upsert(3, 3)
t.upsert(4, 4)
t.upsert(5, 5)

assert t.max() == 5
assert t.min() == 1
assert t.avg() == 3

t.upsert(1, 5)
t.upsert(2, 5)
t.upsert(3, 5)
t.upsert(4, 5)
t.upsert(5, 0)

assert t.min() == 0
assert t.max() == 5
assert t.avg() == 4

t.remove(1)
t.remove(2)
t.upsert(6, 2)

assert t.min() == 0
assert t.max() == 5
assert t.avg() == 3

t.remove(3)
t.remove(4)
t.remove(6)

assert t.min() == 0
assert t.max() == 0
assert t.avg() == 0