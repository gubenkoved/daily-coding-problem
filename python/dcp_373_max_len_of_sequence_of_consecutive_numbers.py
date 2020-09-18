# This problem was asked by Facebook.
#
# Given a list of integers L, find the maximum length of a sequence of consecutive
# numbers that can be formed using elements from L.
#
# For example, given L = [5, 2, 99, 3, 4, 1, 100], return 5 as we can build a
# sequence [1, 2, 3, 4, 5] which has length 5.

# O(n*logn)
def f(a):
    a = sorted(a)
    cur_l = 1
    max_l = 1
    for idx in range(1, len(a)):
        if a[idx] == a[idx - 1] + 1:
            cur_l += 1
            max_l = max(cur_l, max_l)
        else:
            cur_l = 1
    max_l = max(cur_l, max_l)
    return max_l


assert(f([1, 2, 3]) == 3)
assert(f([3, 2, 1]) == 3)
assert(f([5, 2, 99, 3, 4, 1, 100]) == 5)
assert(f([1, 2, 3, -1]) == 3)
assert(f([1, 2, 3, -1, 0]) == 5)
