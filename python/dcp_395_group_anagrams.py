# This problem was asked by Robinhood.
#
# Given an array of strings, group anagrams together.
#
# For example, given the following array:
#
# ['eat', 'ate', 'apt', 'pat', 'tea', 'now']
# Return:
#
# [['eat', 'ate', 'tea'],
#  ['apt', 'pat'],
#  ['now']]

from collections import defaultdict
from itertools import groupby

def group_anagrams(a: list):
    # handle is the same for anagrams
    def handle(s):
        counts_map = defaultdict(lambda: 0)
        for char in s:
            counts_map[char] += 1
        return ''.join([f'{t[0]}_{t[1]}' for t in
                        sorted(counts_map.items(), key=lambda t: t[0])])

    m = [(s, handle(s)) for s in a]

    # sort by handle
    m = sorted(m, key=lambda t: t[1])

    result = []  # list of groups
    group = []

    for idx in range(0, len(m) + 1):
        if idx != 0 and (idx == len(m) or m[idx][1] != m[idx - 1][1]):
            result.append(group)
            group = []

        if idx != len(m):
            group.append(m[idx][0])

    return list(result)


assert group_anagrams(['now']) == [['now']]
assert group_anagrams(['aba', 'aab', 'baa', 'qwe', 'ewq']) == [['aba', 'aab', 'baa'], ['qwe', 'ewq']]
assert group_anagrams(['eat', 'ate', 'apt', 'pat', 'tea', 'now']) == [['eat', 'ate', 'tea'], ['apt', 'pat'], ['now']]
