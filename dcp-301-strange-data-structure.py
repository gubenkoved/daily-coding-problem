# This problem was asked by Triplebyte.

# Implement a data structure which carries out the following operations
# without resizing the underlying array:

# add(value): Add a value to the set of values.
# check(value): Check whether a value is in the set.

# The check method may return occasional false positives (in other words,
# incorrectly identifying an element as part of the set), but should always
# correctly identify a true element.


# given we can not store N elements with constant memory we can at least have
# some constant amount of buckets we will check

class DummySet(object):

    def __init__(self, bits=4):
        self.bits = bits
        self.arr = [None] * (1 << bits)

    def __repr__(self):
        return f"{self.arr}"

    def add(self, x):
        hash = self.__hash(x)
        self.arr[hash] = True

    def check(self, x):
        hash = self.__hash(x)
        return self.arr[hash] == True

    def __hash(self, x):
        mask = (0b1 << self.bits) - 1
        result = hash(x) & mask
        # print(f'  ... hashed {x} to {result}')
        return result

x = DummySet(bits=4)

# tests

assert(x.check(1) == False)
assert(x.check(2) == False)

x.add(1)
x.add(2)
x.add(10)
x.add(20)

assert(x.check(1))
assert(x.check(2))
assert(x.check(10))
assert(x.check(20))
