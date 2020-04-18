# This problem was asked by Walmart Labs.

# There are M people sitting in a row of N seats, where M < N. Your task is
# to redistribute people such that there are no gaps between any of them,
# while keeping overall movement to a minimum.

# For example, suppose you are faced with an input of [0, 1, 1, 0, 1, 0, 0,
# 0, 1],
# where 0 represents an empty seat and 1 represents a person. In this case, one
# solution would be to place the person on the right in the fourth seat.
# We can consider the cost of a solution to be the sum of the absolute distance
# each person must move, so that the cost here would be five.

# Given an input such as the one above, return the lowest possible cost of
# moving people to remove all gaps.

from collections import deque

def cost(people) -> int:
    # greedy intuitive algorithm
    # first let's group seats and people, so that we are working with
    # a chain of tuples (0 or 1, amount), then simply start joining the tuples
    # using the following rule -- given two groups at the ends move the one
    # that has lower amount of people
    g = deque()
    n = len(people)
    last = people[0]
    count = 1

    for idx in range(1, n):
        if people[idx] != last:
            # new group
            g.append([last, count])
            count = 0

        last = people[idx]
        count += 1

    # add the last group!
    g.append([last, count])

    # remove start/trainling '0' groups -- we are not moving it!
    if g[0][0] == 0:
        del g[0]

    if g[-1][0] == 0:
        del g[-1]

    # okay, now start merging it
    cost = 0

    while len(g) > 1:
        last_group = g[-1]
        first_group = g[0]

        if first_group[1] < last_group[1]:
            # moving the first group!
            move_by = g[1][1]
            cost += first_group[1] * move_by
            next_group = g[2]
            next_group[1] += first_group[1]  # add people!
            del g[0]
            del g[0]  # remove first two
        else:
            # move the last one!
            move_by = g[-2][1]
            cost += last_group[1] * move_by
            next_group = g[-3]
            next_group[1] += last_group[1]  # add people!
            del g[-1]
            del g[-1]  # remove last two

    return cost


assert(cost([0, 1, 1, 0, 1, 0, 0, 0, 1]) == 5)
assert(cost([1, 1, 0, 0, 0, 0, 0, 0, 1]) == 6)
assert(cost([1, 1, 0, 0, 0, 0, 1, 1, 1]) == 8)
assert(cost([1, 1, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0]) == 28)
assert(cost([0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0]) == 20)
