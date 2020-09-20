# This problem was asked by Microsoft.
#
# Given a string, generate all possible subsequences of the string.
#
# For example, given the string xyz, return an array or set with the following
# strings:
#
#
# x
# y
# z
# xy
# xz
# yz
# xyz
#
# Note that zx is not a valid subsequence since it is not in the order of the given
# string.


def gen(s):
    # stop condition
    if len(s) == 0:
        yield ''
        return

    nested = gen(s[1:])

    # we can either take the char or reject it ->try both
    for sub_sequence in nested:
        yield s[0] + sub_sequence
        yield sub_sequence


def gen2(s):
    for x in gen(s):
        if len(x) == 0:
            continue
        yield x


assert set(gen2('xy')) == {'x', 'y', 'xy'}
assert set(gen2('xyz')) == {'x', 'y', 'z', 'xy', 'xz', 'yz', 'xyz'}
