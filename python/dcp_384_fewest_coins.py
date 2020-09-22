# This problem was asked by WeWork.
#
# You are given an array of integers representing coin denominations and a total
# amount of money. Write a function to compute the fewest number of coins needed to
# make up that amount. If it is not possible to make that amount, return null.
#
# For example, given an array of [1, 5, 10] and an amount 56, return 7 since we
# can use 5 dimes, 1 nickel, and 1 penny.
#
# Given an array of [5, 8] and an amount 15, return 3 since we can use 5 5-cent
# coins.

from unittest import TestCase
from functools import lru_cache

# greedy algorithm does not work -- we can noy simply take the biggest available
# so far, as obvious counter examples prove it's not correct


def f_greedy(d, total):
    # start with the biggest possible one, do NOT try ones which are not co-primes
    # it does no make sense to take by "5" if we take "10" as it will only make the
    # path longer (?)
    d = sorted(d)
    count = 0
    leftover = total

    while leftover > 0:
        progress = False
        # pick the coin
        for coin_idx in range(len(d) - 1, -1, -1):
            if d[coin_idx] <= leftover:
                leftover -= d[coin_idx]  # take it!
                count += 1
                progress = True
                break

        if not progress:
            return None

    return count


# does not fall into the greedy traps, but can not handle big numbers
# as it's recursive and exhausts the stack pretty quickly
def f(d, total):
    # nested function so that we can have hashable args
    @lru_cache(maxsize=None)
    def r(k):
        if k == 0:
            return 0

        best = None
        for coin_idx in range(len(d) - 1, -1, -1):
            if d[coin_idx] <= k:
                # let's try!
                nested = r(k - d[coin_idx])

                if nested is None:
                    continue

                if best is None or nested < best:
                    best = nested

        print(f'{k} -> {best}')
        return best + 1 if best is not None else None
    return r(total)


# BFS... seems to work okay
def h(d, total):
    m = {x: 1 for x in d}  # total -> best coin amount mapping
    # we will try to start from the closest to the end
    # lets store ordered list of "frontier" elements
    frontier = sorted(d)
    from bisect import insort

    while frontier:
        cur = frontier.pop()
        dist = m[cur]
        # let's see what is reachable
        for x in d:
            reachable = cur + x
            if reachable > total:
                continue
            if reachable not in m or dist + 1 < m[reachable]:
                m[reachable] = dist + 1
                insort(frontier, reachable)

    return m[total] if total in m else None


# solver = f_greedy  # quite obviously, does not work in all cases
# solver = f
solver = h


class TestCases(TestCase):
    def test_case0(self): self.assertEqual(7, solver([1, 5, 10], 56))
    def test_case1(self): self.assertEqual(3, solver([5, 8], 15))  # greed trap!
    def test_case2(self): self.assertEqual(2, solver([3, 5, 9, 17], 34))
    def test_case3(self): self.assertEqual(2, solver([3, 5, 9, 17], 34))
    def test_case4(self): self.assertEqual(None, solver([2, 4, 8, 10], 33))
    def test_case5(self): self.assertEqual(8, solver([1, 2, 4, 8, 16, 32, 64, 128], 255))  # each one by 1
    def test_case6(self): self.assertEqual(2, solver([1, 75, 100], 150))
    def test_case7(self): self.assertEqual(3, solver([1, 30, 50, 100], 230))
    def test_case8(self): self.assertEqual(5, solver([1, 2], 10))
    def test_case9(self): self.assertEqual(1, solver([1, 10], 10))
    def test_case10(self): self.assertEqual(10, solver([1, 11], 10))
    def test_case11(self): self.assertEqual(2, solver([1, 10], 11))
    def test_case12(self): self.assertEqual(3, solver([1, 4, 10, 15], 24))
    def test_case13(self): self.assertEqual(100_000, solver([1, 5, 10], 1_000_000))
    def test_case14(self): self.assertEqual(100_001, solver([2, 5, 10], 1_000_002))
    def test_case15(self): self.assertEqual(100003, solver([2, 5, 10], 1_000_001))
    def test_case16(self): self.assertEqual(10011, solver([1, 5, 10, 100], 1_000_555))  # 100x10_000 + 100x5 + 10x6
    def test_case17(self): self.assertEqual(4, solver([1, 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37], 100))
