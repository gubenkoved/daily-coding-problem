# This problem was asked by Amazon.
#
# Write a function that takes a natural number as input and returns the number of
# digits the input has.
#
# Constraint: don't use any loops.
#

import math


def f(n):
    k = math.ceil(math.log10(n))
    if 10 ** k == n:
        k += 1
    return k


assert f(1) == 1
assert f(9) == 1
assert f(10) == 2
assert f(11) == 2
assert f(99) == 2
assert f(100) == 3
assert f(123456789) == 9
assert f(12345678901234567890) == 20
