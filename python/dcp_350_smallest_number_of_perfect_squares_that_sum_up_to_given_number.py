# This problem was asked by Uber.

# Write a program that determines the smallest number of perfect squares that sum up to N.

# Here are a few examples:

# Given N = 4, return 1 (4)
# Given N = 17, return 2 (16 + 1)
# Given N = 18, return 2 (9 + 9)

from math import floor


# Dynamic Programming Rocks!
# Here is a little explanation -- in order to solve for some N
# we can just try out to use a perfect square which is below N and then we got a problem of smaller dimension, so we can just try out all usable perfect squares in this fashion and take the best result
def f(n: int) -> int:

    if n <= 1:
        return n

    k = floor(n ** 0.5)  # we can take squares up to k * k

    return min([1 + f(n - i ** 2) for i in range(1, k + 1)])


assert f(4) == 1
assert f(17) == 2
assert f(18) == 2
assert f(24) == 3  # 16 + 4 + 4
assert f(15) == 4  # 9 + 4 + 1 + 1
assert f(32) == 2  # optimal: 16 + 16 (2), greedy: 25 + 4 + 1 + 1 + 1 (5)
