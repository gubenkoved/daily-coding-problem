# This problem was asked by Goldman Sachs.
#
# Given a list of numbers L, implement a method sum(i, j) which returns the sum from
# the sublist L[i:j] (including i, excluding j).
#
# For example, given L = [1, 2, 3, 4, 5], sum(1, 3) should return sum([2, 3]),
# which is 5.
#
# You can assume that you can do some pre-processing. sum() should be optimized over
# the pre-processing step.


# what the hell wrong this the complexity labels second time in a row?
# why this one is marked HARD again?!


def create_summator(a):
    # preprocessing stage... cache in the closed variable
    sum_right = [0 for _ in a]  # sum_right[i] = a[i] + a[i+1] + ... + a[n]
    for idx in range(len(a) - 1, -1, -1):
        sum_right[idx] += a[idx]
        if idx != len(a) - 1:
            sum_right[idx] += sum_right[idx + 1]

    def f(l_inclusive, r_exclusive):
        if l_inclusive > r_exclusive:
            raise Exception('wrong arguments!')
        if l_inclusive == r_exclusive:
            return 0
        result = sum_right[l_inclusive]
        if r_exclusive < len(a):
            result -= sum_right[r_exclusive]
        return result
    return f


summator = create_summator([1, 2, 3, 4, 5])

assert summator(1, 3) == 5
assert summator(0, 5) == 15
assert summator(1, 5) == 14
assert summator(1, 1) == 0
assert summator(1, 2) == 2
assert summator(3, 5) == 9
assert summator(4, 5) == 5
assert summator(4, 4) == 0
