# This problem was asked by Facebook.
#
# Given a list of strictly positive integers, partition the list into 3 contiguous
# partitions which each sum up to the same value. If not possible, return null.
#
# For example, given the following list:
#
# [3, 5, 8, 0, 8]
#
# Return the following 3 partitions:
#
# [[3, 5],
#  [8, 0],
#  [8]]
#
# Which each add up to 8.


# what the hell? why it has "HARD" complexity label?
# "contiguous partitions" -- that is what makes it rather simple

def f(a):
    target_sum = sum(a)

    if target_sum % 3 != 0:
        return None

    partition_sum = target_sum // 3
    results = []
    group = []

    for x in a:
        group.append(x)

        if sum(group) > partition_sum:
            return None

        if sum(group) == partition_sum:
            # group is complete
            results.append(group)
            group = []

    return results


assert f([1, 1, 1]) == [[1], [1], [1]]
assert f([1, 1, 2, 1, 1]) == [[1, 1], [2], [1, 1]]
assert f([3, 1, 1, 1, 2, 1]) == [[3], [1, 1, 1], [2, 1]]
assert f([3, 5, 8, 0, 8]) in ([[3, 5], [8, 0], [8]], [[3, 5], [8], [0, 8]])
assert f([4, 5, 8, 1, 9]) == [[4, 5], [8, 1], [9]]
