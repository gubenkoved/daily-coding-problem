# This problem was asked by Two Sigma.
#
# Using a function rand5() that returns an integer from 1 to 5 (inclusive) with
# uniform probability, implement a function rand7() that returns an integer
# from 1 to 7 (inclusive).

from random import randint
from collections import defaultdict


def rand5():
    return randint(0, 4) + 1


def rand7():
    while True:
        # x is uniformly distributed in [0, 24]
        x = (rand5() - 1) * 5 + (rand5() - 1)
        if x < 21:
            return (x % 7) + 1


def show_distribution(fn, n=1000_000):
    freq = defaultdict(lambda: 0)
    for _ in range(n):
        freq[fn()] += 1
    return dict(freq)


print(show_distribution(rand5))
print(show_distribution(rand7))
