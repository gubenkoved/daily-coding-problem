# This problem was asked by Facebook.
#
# Describe an algorithm to compute the longest increasing subsequence of an array of
# numbers in O(n log n) time.

# Solution #1
# O(2^n) in the worst case -- brute force
def _naive(a, idx, min):
    if idx >= len(a):
        yield []
        return

    # can try to take into the set
    if not min or a[idx] > min:
        nested = _naive(a, idx+1, a[idx])
        for seq in nested:
            yield [a[idx]] + seq

    # always can skip
    for seq in _naive(a, idx + 1, min):
        yield seq

def naive(a):
    result = sorted(_naive(a, 0, None), key=lambda x: len(x))[-1]
    print(f'{a} -> {result}')
    return result

# Solution #2
# a faster, graph-based approach
# it looks like the task is equivalent to searching the longest
# path in the graph composed of the numbers and edge from "a" to "b" would mean that
# 1. "b" goes after the "a"
# 2. "b" > "a"
# such graph can be constructed in O(n^2), and the "longest path" search is
# O(n + v), and in the worst case (always increasing chain?) will be O(n^2) as well
# may be there could be some optimizations to avoid creating the edge that will
# shorten the paths, but anyway we can not do in O(n*logn) as requested...

def showcase(func):
    def wrapper(*args, **kwargs):
        result = func(*args, **kwargs)
        print(f'{args} {kwargs} -> {result}')
        return result
    return wrapper

# Solution #3 -- simpler, O(n^2)
@showcase
def g(a):
    d = [None for _ in a]
    p_map = {}  # parents map

    # preprocessing pass for O(n**2)
    for idx in range(len(a)):
        max_dist = 0  # max distance to predecessor
        for l in range(idx):
            if a[l] < a[idx]:
                if d[l] > max_dist:
                    max_dist = d[l]
                    p_map[idx] = l
        d[idx] = max_dist + 1

    # trace back for O(n)
    result = []
    # sort by the max distance, get the index of that element
    idx = max(enumerate(d), key=lambda x: x[1])[0]
    while idx is not None:
        result.append(a[idx])
        idx = p_map[idx] if idx in p_map else None
    return list(reversed(result))


# Solution 4 -- sort based, O(n*logn)
# does NOT work :)
@showcase
def h(a):
    # sort by value, preserve index
    s = sorted(enumerate(a), key=lambda tuple: tuple[1])

    # then, the only thing which is left is finding the biggest increasing
    # (by the stored index) adjacent subsequence, which can be easily be done
    # in linear time
    result = None
    l = 0

    def _update_result_if_better(l, r):
        nonlocal result
        if result is None or len(result) < r - l:
            result = [val for source_idx, val in s[l:r]]

    for r in range(1, len(a)+1):
        #if r == len(a) or s[r][0] < s[r-1][0]:
        if r == len(a) or s[r][0] < s[l][0]:
            _update_result_if_better(l, r)
            l = r

    # the last chunk (if any)
    if l < len(a):
        _update_result_if_better(l, len(a) - 1)

    return result

# fuf, did NOT solve myself for O(nlogn)
# see here: https://en.wikipedia.org/wiki/Longest_increasing_subsequence
# very good explanation: https://www.youtube.com/watch?v=22s1xxRvy28
@showcase
def q(a):
    # it's good to think of numbers as the cards, and visualize algorithm with piles
    # of cards...
    # will store the piles of cards
    # each pile has the cars in the decreasing order within it
    # every time we take a new number we will choose the first pile we can stick the
    # card into (meaning the last card in the pile is bigger than the current)
    # note that we will store the indexes of the items in the "piles", not the values
    # themselves for convenience
    piles = []

    # "parents" will hold the index to parent index mapping
    # every time we add a card to the pile we store the index of the card from the
    # left pile which is currently at the end (i.e. the smallest)
    # then in order to get the result, start with the rightmost pile and the last
    # number there and traverse using the parents map to the first pile
    # note that number of piles composed in this way will be equal to the length of
    # the longest increasing subsequence
    parents = {}

    # simple linear (O(n)) implementation that gives total O(n^2) time
    def find_pile_linear(card_idx):
        for pile_idx in range(len(piles)):
            if a[piles[pile_idx][-1]] >= a[card_idx]:
                return pile_idx
        return None  # pile can NOT be found, new is needed

    # binary search (O(logn)) implementation that gives O(nlogn) time in total!
    def find_pile_binary_search(card_idx):
        lo, hi = 0, len(piles)
        while True:
            if lo == len(piles):
                return None  # pile can NOT be found, new is needed

            if hi == lo:
                return lo  # pile IS found

            med = (lo + hi) // 2

            if a[piles[med][-1]] >= a[card_idx]:
                hi = med
            else:
                # max is needed to cover an edge case where med == lo
                # ensure we move lo in that case, otherwise it wil be infinite loop
                lo = max(med, lo + 1)

    def put_into_pile(card_idx):
        # picked_pile_idx = find_pile_linear(card_idx)
        picked_pile_idx = find_pile_binary_search(card_idx)

        # no suitable pile --> create a new one
        if picked_pile_idx is None:
            picked_pile_idx = len(piles)
            piles.append([])

        # put a card index into the new pile
        piles[picked_pile_idx].append(card_idx)

        # store the parent relation with the last card from the left pile
        if picked_pile_idx > 0:
            parents[card_idx] = piles[picked_pile_idx - 1][-1]


    for card_idx in range(len(a)):
        put_into_pile(card_idx)

    # traceback starting with the last card in the rightmost pile!
    result = []
    cur_idx = piles[-1][-1]

    while cur_idx is not None:
        result.append(a[cur_idx])
        cur_idx = parents[cur_idx] if cur_idx in parents else None

    # restore the order as we constructed the result in a reversed way
    return list(reversed(result))


# solver = naive
# solver = g
# solver = h  # gives wring answers
solver = q


# note that there could be multiple results of the same length
assert solver([1, 2, 3]) == [1, 2, 3]
assert solver([5, 3, 3, 25, 8, 4, 3, 7, 21, 34, 2, 34, 44, 1, 2, 342]) == [3, 4, 7, 21, 34, 44, 342]
assert solver([1, 2, 3, 4, 5]) == [1, 2, 3, 4, 5]
assert solver([10, 1, 20, 2, 30]) in ([1, 2, 30], [10, 20, 30])
assert solver([10, 1, 20, 2, 30, 3, 4]) == [1, 2, 3, 4]
assert solver([10, 1, 20, 2, 30, 3, 4, 40, 50]) == [1, 2, 3, 4, 40, 50]
assert solver([10, 20, 1, 2, 3, 10, 20, 4, 5, 1, 2]) in ([1, 2, 3, 4, 5], [1, 2, 3, 10, 20])
assert solver([0, 8, 4, 12, 2, 10, 6, 14, 1, 9, 5, 13, 3, 11, 7, 15]) in ([0, 2, 6, 9, 11, 15], [0, 4, 6, 9, 13, 15])
