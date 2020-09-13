# This problem was asked by Two Sigma.
#
# Given two sorted iterators, merge it into one iterator.
#
# For example, given these two iterators:
#
# foo = iter([5, 10, 15])
# bar = iter([3, 8, 9])
# You should be able to do:
#
# for num in merge_iterators(foo, bar):
#     print(num)
#
# # 3
# # 5
# # 8
# # 9
# # 10
# # 15
#
# Bonus: Make it work without pulling in the contents of the iterators in memory.


def merge(*iterators):
    # python iterators do NOT allow to get the current value, so we will have
    # to have a separate store

    values = [next(iterator) for iterator in iterators]

    while True:
        if not iterators:
            return

        # pick the smallest
        idx, val = min(enumerate(values), key=lambda x: x[1])

        # advance the idx-th pointer
        try:
            iterator = iterators[idx]
            values[idx] = next(iterator)
        except StopIteration:
            # exhausted iterator, remove it!
            del iterators[idx]
            del values[idx]

        yield val


assert list(merge(iter([1, 2, 3]))) == [1, 2, 3]
assert list(merge(iter([5, 10, 15]), iter(3, 8, 9))) == [3, 5, 8, 9, 10, 15]
assert list(merge(iter([10, 20, 30]), iter([15, 25]), iter([17]))) == [10, 15, 17, 20, 25, 30]