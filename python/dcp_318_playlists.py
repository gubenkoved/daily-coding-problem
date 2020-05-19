# This problem was asked by Apple.
#
# You are going on a road trip, and would like to create a suitable music playlist. The trip will require N songs,
# though you only have M songs downloaded, where M < N. A valid playlist should select each song at least once,
# and guarantee a buffer of B songs between repeats.
#
# Given N, M, and B, determine the number of valid playlists.

from typing import List, Optional
import math
from functools import lru_cache


def last_index_of(l: list, el) -> int:
    for idx in range(len(l) - 1, -1, -1):
        if l[idx] == el:
            return idx
    return -1


def playlist_count_brute(n: int, m: int, b: int) -> int:
    if m >= n:
        raise Exception('m should be less than n by the problem statement')

    if b >= m:
        raise Exception('buffer is too big')

    results = {'count': 0}

    def _worker(playlist: List[int]) -> None:
        if len(playlist) == n:
            if len(set(playlist)) == m:  # only count ones where ALL songs where played
                # print(playlist)
                results['count'] += 1
            return

        for x in range(0, m):  # try all song
            last_idx = last_index_of(playlist, x)

            if last_idx != -1:  # ensure that there is a big enough buffer
                distance = len(playlist) - last_idx - 1

                if distance < b:
                    continue  # buffer constraint is not fulfilled

            playlist.append(x)

            _worker(playlist)  # recursive dive in!

            del playlist[-1]

    _worker([])

    return results['count']


def playlist_count_analytical(n: int, m: int, b: int) -> int:
    # the relation below was inferred by analyzing the results calculated up to n=9, and recurrent
    # nature of the sequence becomes obvious; I did NOT infer it from scratch though, I've simply noticed the pattern;
    # there is another cloud -- quite obvious that given the songs are not distinguishable the result should always be
    # a multiplier of factorial(m) as each solution can be transformed into factorial(m) solutions by replacing the
    # songs
    # https://photos.app.goo.gl/yCnEr8tap4a4G7uk7

    @lru_cache  # otherwise we recalculating the same things over and over again
    def _c(x: int, y: int) -> int:
        if x == 1 or y == 1:
            return 1  # base case
        res = x * _c(x, y - 1) + _c(x - 1, y)
        # print(f'c({x}, {y})={res}')
        return res

    return math.factorial(m) * _c(m - b, n - m + 1)


for n in range(3, 10):
    for m in range(2, n):
        for b in range(1, m):
            analytical_result = playlist_count_analytical(n, m, b)

            if n <= 8:
                brute_result = playlist_count_brute(n, m, b)
                m_fact = math.factorial(m)
                mult = brute_result // m_fact
                check_info = f'| brute-force: {brute_result:<10} = {mult} x {m_fact}'
            else:
                check_info = ''

            print(f'f({n:2}, {m:2}, {b:2}) = {analytical_result:<10}{check_info}')

# print(solver(5, 3, 2))
# print(solver(10, 5, 3))
# print(solver(20, 10, 5))
# print(solver(50, 10, 5))
