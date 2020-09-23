# This problem was asked by Twitter.
#
# Given a string, sort it in decreasing order based on the frequency of characters.
# If there are multiple possible solutions, return any of them.
#
# For example, given the string tweet, return tteew. eettw would also be acceptable.


from collections import defaultdict


def f(s):
    counts = defaultdict(lambda: 0)
    for c in s:
        counts[c] += 1

    tuples = sorted(counts.items(), key=lambda t: t[1], reverse=True)
    return ''.join([t[0] * t[1] for t in tuples])


assert f('abbaa') == 'aaabb'
assert f('tweet') in ['tteew', 'eettw']
assert f('cbbaaa') == 'aaabbc'
assert f('axxzzz') == 'zzzxxa'
