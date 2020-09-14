# This problem was asked by Google.
#
# Implement a key value store, where keys and values are integers, with the following
# methods:
#
# update(key, vl): updates the value at key to val, or sets it if doesn't exist
# get(key): returns the value with key, or None if no such value exists
# max_key(val): returns the largest key with value val, or None if no key with that
# value exists
# For example, if we ran the following calls:
#
# kv.update(1, 1)
# kv.update(2, 1)
#
# And then called kv.max_key(1), it should return 2, since it's the largest key with
# value 1.

import bisect
from collections import defaultdict

# the only trickery is speeding up the max_key operation
# so, we can maintain max heaps for each of groups of the same value

class PlainStore(object):
    def __init__(self):
        self._dict = dict()

    # O(1)
    def get(self, key):
        return self._dict.get(key, None)

    # O(1)
    def update(self, key, value):
        self._dict[key] = value

    # O(n)
    def max_key(self, val):
        keys = [key for key in self._dict if self._dict[key] == val]

        if not keys:
            return None

        return max(keys)


class BinarySearchBasedStore(object):
    def __init__(self):
        self._dict = dict()
        self._value_groups = defaultdict(list)  # dict from value to sorted list of keys

    # O(1)
    def get(self, key):
        return self._dict.get(key, None)

    def _idx(self, a, x):
        i = bisect.bisect_left(a, x)
        if i != len(a) and a[i] == x:
            return i
        raise ValueError

    # O(n) even with binary search as insertions in lists are not efficient
    def update(self, key, value):
        if key in self._dict:
            # remove the old value from the values group
            cur_val = self._dict[key]
            idx = self._idx(self._value_groups[cur_val], key)
            del self._value_groups[cur_val][idx]

        self._dict[key] = value
        bisect.insort(self._value_groups[value], key)

    # O(1)
    def max_key(self, val):
        if not self._value_groups[val]:
            return None

        return self._value_groups[val][-1]

# the better update case would be if we were to use max heap instead of sorted array
# it would be O(logn) for update


# kv = PlainStore()
kv = BinarySearchBasedStore()

kv.update(1, 1)
kv.update(2, 1)
kv.update(3, 1)

assert kv.max_key(1) == 3

kv.update(3, 33)

assert kv.max_key(1) == 2
assert kv.get(2) == 1

kv.update(1, 33)
kv.update(2, 22)

assert kv.max_key(33) == 3
assert kv.max_key(22) == 2
