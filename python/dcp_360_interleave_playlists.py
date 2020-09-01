# This problem was asked by Spotify.

# You have access to ranked lists of songs for various users. Each song is
# represented as an integer, and more preferred songs appear earlier in
# each list. For example, the list [4, 1, 7] indicates that a user likes
# song 4 the best, followed by songs 1 and 7.

# Given a set of these ranked lists, interleave them to create a playlist
# that satisfies everyone's priorities.

# For example, suppose your input is {[1, 7, 3], [2, 1, 6, 7, 9], [3, 9, 5]}.
# In this case a satisfactory playlist could be [2, 1, 6, 7, 3, 9, 5].

from collections import defaultdict
from typing import List


def interleave(preferences: List[List[int]]):
    # we create a helper data structure that will map every ID to
    # set of the ids that should go before the given ID
    # then we simply start with the ID that is not preceded by anything and
    # count it as visited, and removing from the prerequisites so to speak of others
    # and then we pick the number of which we satisfied all the prerequisites and the
    # process repeats until we pick them all

    prerequisites = defaultdict(set)

    # compose the prerequisites map
    for preference in preferences:
        for idx in range(1, len(preference)):
            prerequisites[preference[idx]].add(preference[idx - 1])

    result = []

    # figure the solution out!
    pool = set.union(*[set(p) for p in preferences])

    # as a performance optimization we can support a linked list where items will
    # additionally hold amount of items in the prerequisites list and so that
    # we will be able to find the items that can be added quicker
    # also, we can support a map from the ID to the iterm it holds to find the
    # items that are affected w/o traversing the whole thing

    while pool:
        progressed = False

        for candidate in pool:

            if not prerequisites[candidate]:
                progressed = True  # found it!
                picked = candidate
                # print(f'  pick: {picked}')

                result.append(picked)
                pool.remove(picked)

                # remove it from the prerequisites of the other elements (if present)
                for el in pool:
                    prerequisites[el].discard(picked)

                # start a new cycle!
                break

        # there must be a cyclic relationship between some preferences!
        if not progressed:
            return None

    return result


print(interleave([
    [1, 7, 3],
    [2, 1, 6, 7, 9],
    [3, 9, 5]
]))

print(interleave([
    [1, 2, 3],
    [1, 2, 3, 4],
    [1, 2, 3, 4, 5]
]))

print(interleave([
    [1, 2, 3],
    [1, 2, 4, 3],
    [1, 2, 5, 4, 3],
]))

# impossible -- as it forms a cycle
print(interleave([
    [1, 2],
    [2, 1]
]))
