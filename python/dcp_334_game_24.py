# This problem was asked by Twitter.

# The 24 game is played as follows. You are given a list of four integers, each
# between 1 and 9, in a fixed order. By placing the operators +, -, *, and / between
# the numbers, and grouping them with parentheses, determine whether it is possible
# to reach the value 24.

# For example, given the input [5, 2, 7, 8], you should return True,
# since (5 * 2 - 7) * 8 = 24.

# Write a function that plays the 24 game.


from typing import List


def f(a: List[int]):
    reachable = r(a)
    return 24 in reachable

# reachable
def r(a: List[int]) -> set:
    if len(a) == 1:
        return set(a)

    if len(a) == 2:
        x, y = a[0], a[1]
        res = set([x + y, x - y, x * y])
        if y != 0:
            res.add(x / y)
        return res

    reachable = set()

    for offset in range(1, len(a)):
        rl = r(a[:offset])
        rr = r(a[offset:])

        for left in rl:
            for right in rr:
                reachable.update(r([left, right]))

    return reachable


assert f([5, 2, 7, 8]) is True  # (5 * 2 - 7) * 8
assert f([0, 2, 3, 4]) is True  # 0 + 2 * 3 * 4
assert f([1, 2, 3, 4]) is True  # 1 * 2 * 3 * 4
assert f([1, 9, 3, 4]) is True  # 1 * (9 - 3) * 4
assert f([9, 7, 9, 3]) is True  # (9 - 7) * (9 + 3)
assert f([9, 9, 9, 9]) is False
