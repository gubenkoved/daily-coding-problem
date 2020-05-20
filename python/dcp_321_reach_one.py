# This problem was asked by PagerDuty.
#
# Given a positive integer N, find the smallest number of steps it will take to reach 1.
#
# There are two kinds of permitted steps:
#
# You may decrement N to N - 1.
# If a * b = N, you may decrement N to the larger of a and b.
# For example, given 100, you can reach 1 in five steps with the following route: 100 -> 10 -> 9 -> 3 -> 2 -> 1.

from collections import deque
from math import sqrt, floor


def steps(n: int) -> int:
    distance = {n: 0}
    pool = deque([n])

    def reachable(x):
        nums = [x - 1]
        for a in range(2, floor(sqrt(x)) + 1):
            if x % a == 0:
                nums.append(x // a)

        return nums

    while len(pool) > 0:
        cur = pool.pop()

        if cur == 1:
            break

        for r in reachable(cur):
            if r in distance:
                continue

            distance[r] = distance[cur] + 1
            pool.appendleft(r)

    return distance[1]


print(steps(100))
print(steps(26))
