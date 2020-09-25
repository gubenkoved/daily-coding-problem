# This problem was asked by Twitter.
#
# A permutation can be specified by an array P, where P[i] represents the location
# of the element at i in the permutation. For example, [2, 1, 0] represents the
# permutation where elements at the index 0 and 2 are swapped.
#
# Given an array and a permutation, apply the permutation to the array. For example,
# given the array ['a', 'b', 'c'] and the permutation [2, 1, 0],
# return ['c', 'b', 'a'].


def permutation(a, p):
    if len(a) != len(p):
        raise Exception('wrong arguments!')
    if len(p) != len(set(p)):
        raise Exception('invalid permutation!')
    result = [None for _ in a]
    for idx in range(len(p)):
        result[idx] = a[p[idx]]
    return result


assert permutation(['a', 'b', 'c'], [2, 1, 0]) == ['c', 'b', 'a']
assert permutation(['a', 'b', 'c'], [0, 1, 2]) == ['a', 'b', 'c']
assert permutation(['a', 'b', 'c'], [2, 0, 1]) == ['c', 'a', 'b']
assert permutation(['a', 'b', 'c'], [1, 2, 0]) == ['b', 'c', 'a']
