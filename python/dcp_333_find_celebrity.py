# This problem was asked by Pinterest.

# At a party, there is a single person who everyone knows, but who does not know
# anyone in return (the "celebrity"). To help figure out who this is, you have
# access to an O(1) method called knows(a, b), which returns True if person a knows
# person b, else False.

# Given a list of N people and the above operation, find a way to identify the
# celebrity in O(N) time.

from typing import Dict, List


def knows(a: str, b: str) -> bool:
    pass

def prepare(knowing_map: Dict[str, List[str]]):

    def knows_fn(a, b):
        return b in knowing_map[a]

    people = list(knowing_map.keys())

    return (people, knows_fn)

def find_celebrity(people: List[str], knows_fn) -> str:
    candidates = list(people)

    while len(candidates) > 0:
        candidate = next(iter(candidates))
        knows_list = []

        for another in candidates:
            if another == candidate:
                continue

            if knows_fn(candidate, another):
                knows_list.append(another)

        if len(knows_list) == 0:
            # validate that celebrity does not know anyone
            for p in people:
                if knows_fn(candidate, p):
                    raise Exception('celebrity should NOT know anyone')
            return candidate

        candidates = knows_list

    return None  # unable to find!


# the first version works for O(n*n) for the worst case where everyone but celebrity knows anyone else
# this one instantly follows edge to the known person working around such cases
def find_celebrity_v2(people: List[str], knows_fn) -> str:
    candidates = set(people)

    while True:
        candidate = next(iter(candidates))
        continue_search = False

        for another in candidates:
            if another == candidate:
                continue

            if knows_fn(candidate, another):
                candidates.remove(candidate)  # current candidate can not be a celebrity as there is someone who is known
                continue_search = True
                break

        if not continue_search:
            return candidate


solver = find_celebrity_v2


print(solver(*prepare({
    'a': ['b', 'c', 'd', ],
    'b': ['c', 'a', 'd'],
    'c': ['a', 'b', 'd'],
    'd': []  # celebrity
})))

print(solver(*prepare({
    'a': ['b', 'd'],
    'b': ['a', 'd'],
    'c': ['b', 'd'],
    'd': []  # celebrity
})))

print(solver(*prepare({
    'a': ['b', 'c', 'd'],
    'b': ['a', 'c', 'd'],
    'c': ['a', 'b', 'd'],
    'd': []  # celebrity
})))

print(solver(*prepare({
    'a': ['d'],
    'b': ['d'],
    'c': ['d'],
    'd': []  # celebrity
})))
