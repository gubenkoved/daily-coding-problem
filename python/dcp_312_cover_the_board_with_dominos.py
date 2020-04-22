# This problem was asked by Wayfair.

# You are given a 2 x N board, and instructed to completely cover the
# board with the following shapes:

# Dominoes, or 2 x 1 rectangles.
# Trominoes, or L-shapes.

# For example, if N = 4, here is one possible configuration, where A
# is a domino, and B and C are trominoes.

# A B B C
# A B C C

# Given an integer N, determine in how many ways this task is possible.


# https://leetcode.com/problems/domino-and-tromino-tiling/
# https://photos.google.com/photo/AF1QipNphtR97TaQFZ55uSwdU7hwHURw4PMeEP6KAA_q

def count_ways(n) -> int:
    return f(n)


def cached(function):
    cache = {}

    def wrapper(*args):
        if args in cache:
            return cache[args]
        result = function(*args)
        cache[args] = result
        return result

    return wrapper

@cached
def f(n: int) -> int:

    if n == 1:
        return 1
    elif n == 2:
        return 2

    return f(n - 1) + f(n - 2) + 2 * g(n - 2)


@cached
def g(n: int) -> int:

    if n == 1:
        return 1

    return g(n - 1) + f(n - 1)


assert(count_ways(1) == 1)
assert(count_ways(2) == 2)
assert(count_ways(3) == 5)
assert(count_ways(4) == 11)
assert(count_ways(30) == 9312342245)
