# This problem was asked by Amazon.

# The stable marriage problem is defined as follows:

# Suppose you have N men and N women, and each person has ranked their prospective
# opposite-sex partners in order of preference.

# For example, if N = 3, the input could be something like this:

# guy_preferences = {
#     'andrew': ['caroline', 'abigail', 'betty'],
#     'bill': ['caroline', 'betty', 'abigail'],
#     'chester': ['betty', 'caroline', 'abigail'],
# }

# gal_preferences = {
#     'abigail': ['andrew', 'bill', 'chester'],
#     'betty': ['bill', 'andrew', 'chester'],
#     'caroline': ['bill', 'chester', 'andrew']
# }

# Write an algorithm that pairs the men and women together in such a way that no two
# people of opposite sex would both rather be with each other than with their current partners.


import copy
import random
from typing import Dict, Iterable, List, Tuple

# does not work really in general case...
def find_pairs(guys_preferences: Dict[str, List[str]], girls_preferences: Dict[str, List[str]]) -> Iterable[Tuple[str, str]]:

    guys_preferences = copy.deepcopy(guys_preferences)
    girls_preferences = copy.deepcopy(girls_preferences)

    pairs: List[Tuple[str, str]] = []

    while len(guys_preferences) > 0:

        # start tracing with  first unmatched guy
        unmatched_guy = next(iter(guys_preferences))
        cur_node = (unmatched_guy, True)  # True means it's a guy node
        visited = set([cur_node])

        while True:
            # find the next node using the current node preferences (only the first one!)
            # as per our lemma firsst level of preferences will always form a cycle of len up to N nodes
            next_name = guys_preferences[cur_node[0]][0] if cur_node[1] else girls_preferences[cur_node[0]][0]
            next_node = (next_name, not cur_node[1])

            if next_node in visited:
                # okay, we found a cycle -> get a pair out of it using the last edge
                # which is formed by cur -> next
                matched_guy = cur_node[0] if cur_node[1] else next_node[0]
                matched_girl = cur_node[0] if not cur_node[1] else next_node[0]
                pairs.append((matched_guy, matched_girl))

                # remove matched from the maps
                del guys_preferences[matched_guy]
                del girls_preferences[matched_girl]

                for guy in guys_preferences:
                    guys_preferences[guy].remove(matched_girl)

                for girl in girls_preferences:
                    girls_preferences[girl].remove(matched_guy)

                break  # start over!

            visited.add(next_node)
            cur_node = next_node

    return pairs


# goes in endless (?) cycles sometimes....
def find_pairs2(guys_preferences: Dict[str, List[str]], girls_preferences: Dict[str, List[str]]) -> Iterable[Tuple[str, str]]:
    # starts with trivial assignment and then we try to improve while we see any instability
    guys = list(guys_preferences.keys())
    girls = list(girls_preferences.keys())

    pairs: List[Tuple[str, str]] = [(guys[i], girls[i]) for i in range(len(guys))]

    stable = False
    counter = 0

    while not stable:
        stable = True

        if counter > 1000:
            raise Exception('looping out...')

        for pair in pairs:
            # check if the current pair is stable, and fix it if it is no
            cur_guy = pair[0]
            cur_girl = pair[1]
            cur_girl_rank = guys_preferences[cur_guy].index(cur_girl)

            for girl in girls:
                new_girl_rank = guys_preferences[cur_guy].index(girl)
                new_girl_current_pair = next(filter(lambda p: p[1] == girl, pairs))
                new_girl_present_guy_rank = girls_preferences[girl].index(new_girl_current_pair[0])
                new_girl_switch_guy_rank = girls_preferences[girl].index(cur_guy)

                if new_girl_rank < cur_girl_rank and new_girl_switch_guy_rank < new_girl_present_guy_rank:
                    stable = False  # we found a better pair -- need another round
                    counter += 1

                    # print(f'{cur_guy} + {cur_girl} is broken (and {new_girl_current_pair[0]} + {new_girl_current_pair[1]} as the result)'
                    #       f', {cur_guy} + {girl} is formed (and {new_girl_current_pair[0]} + {cur_girl})')

                    # remove two old pairs
                    pairs.remove(pair)
                    pairs.remove(new_girl_current_pair)

                    # add a new ones!
                    pairs.append((cur_guy, girl))
                    pairs.append((new_girl_current_pair[0], cur_girl))

                if not stable:
                    break  # start processing pairs over again

    return pairs

# did NOT solve myself...
# https://en.wikipedia.org/wiki/Gale%E2%80%93Shapley_algorithm
def find_pairs_gale_shapley(guys_preferences: Dict[str, List[str]], girls_preferences: Dict[str, List[str]]) -> Iterable[Tuple[str, str]]:
    proposal_map = copy.deepcopy(guys_preferences)
    free_man = set(guys_preferences.keys())
    free_women = set(girls_preferences.keys())
    women_to_man_map = {}

    while len(free_man) > 0:
        man = next(iter(free_man))
        proposed_to = proposal_map[man][0]
        del proposal_map[man][0]  # do not propose twice

        if proposed_to in free_women:
            women_to_man_map[proposed_to] = man
            free_women.remove(proposed_to)
            free_man.remove(man)
        else:  # see if woman is willing to change the partner
            woman_current_man = women_to_man_map[proposed_to]

            if girls_preferences[proposed_to].index(man) < girls_preferences[proposed_to].index(woman_current_man):
                women_to_man_map[proposed_to] = man
                free_man.add(woman_current_man)
                free_man.remove(man)

    return [(women_to_man_map[woman], woman) for woman in women_to_man_map]


def is_stable(guys_preferences: Dict[str, List[str]], girls_preferences: Dict[str, List[str]], pairs: Iterable[Tuple[str, str]]):
    for pair in pairs:
        # check if this pair is stable

        # cur_guy_rank = girls_preferences[pair[1]].index(pair[0])
        cur_girl_rank = guys_preferences[pair[0]].index(pair[1])

        guy = pair[0]

        for girl in girls_preferences.keys():
            new_girl_rank = guys_preferences[guy].index(girl)
            new_girl_switch_guy_rank = girls_preferences[girl].index(guy)
            new_girl_current_pair = next(filter(lambda p: p[1] == girl, pairs))
            new_girl_current_pair_guy_rank = girls_preferences[girl].index(new_girl_current_pair[0])

            if new_girl_rank < cur_girl_rank and new_girl_switch_guy_rank < new_girl_current_pair_guy_rank:
                return (False, f'{pair[0]} + {pair[1]} is NOT stable as {pair[0]} would rather be with {girl} AND she ranks {guy} ({new_girl_switch_guy_rank})'
                        f' better than the current pair ({new_girl_current_pair_guy_rank})')

    return (True, None)


def generate_random_preferences(n: int):

    guys = [f'{i + 1}' for i in range(n)]
    girls = [f'{chr(ord("A") + i)}' for i in range(n)]

    guys_preferences = {}
    girls_preferences = {}

    for i in range(n):
        guys_preferences[guys[i]] = random.sample(girls, n)
        girls_preferences[girls[i]] = random.sample(guys, n)

    return (guys_preferences, girls_preferences)


guys_preferences = {
    'andrew': ['caroline', 'abigail', 'betty'],
    'bill': ['caroline', 'betty', 'abigail'],
    'chester': ['betty', 'caroline', 'abigail']
}

girls_preferences = {
    'abigail': ['andrew', 'bill', 'chester'],
    'betty': ['bill', 'andrew', 'chester'],
    'caroline': ['bill', 'chester', 'andrew']
}

pairs = find_pairs_gale_shapley(guys_preferences, girls_preferences)

print(pairs)
print(is_stable(guys_preferences, girls_preferences, pairs))
print(is_stable(guys_preferences, girls_preferences, [('andrew', 'caroline'), ('bill', 'betty'), ('chester', 'abigail')]))

# guys_preferences = {'1': ['A', 'C', 'B'], '2': ['B', 'A', 'C'], '3': ['B', 'A', 'C']}
# girls_preferences = {'A': ['2', '1', '3'], 'B': ['1', '3', '2'], 'C': ['1', '3', '2']}

# pairs = find_pairs2(guys_preferences, girls_preferences)

# print(pairs)
# print(is_stable(guys_preferences, girls_preferences, pairs))

# solver = find_pairs
# solver = find_pairs2
solver = find_pairs_gale_shapley

# randon checks!
for n in range(3, 100):
    print(f'solving for {n}...')

    try:
        random_prefs = generate_random_preferences(n)
        pairs = solver(random_prefs[0], random_prefs[1])
        is_stable_result, stability_error = is_stable(random_prefs[0], random_prefs[1], pairs)

        if not is_stable_result:
            raise Exception(f'composed NOT stable pair for {random_prefs} -- {stability_error}')
    except Exception as ex:
        print(f'  error: {ex}')
