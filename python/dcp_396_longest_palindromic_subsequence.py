# This problem was asked by Google.
#
# Given a string, return the length of the longest palindromic subsequence in the
# string.
#
# For example, given the following string:
#
# MAPTPTMTPA
#
# Return 7, since the longest palindromic subsequence in the string is APTMTPA.
# Recall that a subsequence of a string does not have to be contiguous!
#
# Your algorithm should run in O(n^2) time and space.

from time import time
from unittest import TestCase, main


def measure_time(f):
    def wrapper(*args, **kwargs):
        start = time()
        result = f(*args, **kwargs)
        elapsed = time() - start
        print(f'took {elapsed:.3f} seconds')
        return result
    return wrapper


def cached(f):
    cache = {}

    def wrapper(*args):
        if args not in cache:
            cache[args] = f(*args)
        return cache[args]

    return wrapper


# I thought this solution is a crappy, but i found it in the leetcode to test and
# it first of all was accepted (so that it is functionally correct)
# secondly, on leetcode there is a restriction on the string len -- up to 1000
# so recursive nature becomes not an issue
# thirdly, it actually (even wo optimizations on "rightmost idx") performed well in
# terms on the time -- better than 78% of submissions;
# moreover, it computes the string itself, but only len is required really...
# https://leetcode.com/problems/longest-palindromic-subsequence/

@measure_time
def lps(s):
    @cached
    def rightmost_idx(idx, cutoff_idx):
        # idx, cutoff_idx -> rightmost index of the char at s[idx] given the cutoff
        # can be optimized to have total runtime < O(n**2)
        for cur_idx in range(cutoff_idx, idx - 1, -1):
            if s[cur_idx] == s[idx]:
                return cur_idx
        raise Exception('impossible!')

    @cached
    def f(left, right):
        # handle the primitive base cases
        if left > right:
            return ''
        elif left == right:
            return s[left]

        # important optimization -- it does not make sense to
        # start with some letter 'x' to producing the longest subsequence if
        # we already tried starting with 'x' to the left of it -- it could not
        # be worse, as it only extends the possibilities!
        seen = set()
        best = ''
        for idx in range(left, right + 1):
            if s[idx] in seen:
                continue
            seen.add(s[idx])

            # suppose we take char at idx, then we should find matching char
            # to the right of it and recursively dive!
            rightmost_match_idx = rightmost_idx(idx, right)

            # see if we find matching or itself
            if idx != rightmost_match_idx:
                nested_result = f(idx + 1, rightmost_match_idx - 1)
                cur = s[idx] + nested_result + s[idx]
            else:
                cur = s[idx]

            if len(cur) > len(best):
                best = cur

        return best

    result = f(0, len(s) - 1)
    print(f'{s} -> {result}')
    return result


solver = lps


class MyCasesTest(TestCase):
    def test_case0(self): self.assertEqual(solver('A'), 'A')
    def test_case1(self): self.assertEqual(solver('AA'), 'AA')
    def test_case2(self): self.assertEqual(solver('AAA'), 'AAA')
    def test_case3(self): self.assertEqual(solver('ABCBA'), 'ABCBA')
    def test_case4(self): self.assertEqual(solver('ABCCBA'), 'ABCCBA')
    def test_case5(self): self.assertEqual(solver('AABCBA'), 'ABCBA')
    def test_case6(self): self.assertIn(solver('MAPTPTMTPA'), ['APTMTPA', 'APTPTPA'])
    def test_case7(self): self.assertEqual(solver('QWERTYasdfgfdYTREWQ'), 'QWERTYdfgfdYTREWQ')

    # def test_case8(self):
    #     for n in range(20):
    #         self.assertEqual(solver('x' * 2 ** n), 'x' * 2 ** n)
    #
    # def test_case9(self):
    #     for n in range(10):
    #         # simply cut the last char :)
    #         self.assertEqual(solver('xxy' * 2 ** n), ('xxy' * 2 ** n)[:-1])


if __name__ == '__main__':
    main()
