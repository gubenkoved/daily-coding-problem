# This problem was asked by Spotify.

# You are the technical director of WSPT radio, serving listeners nationwide.
# For simplicity's sake we can consider each listener to live along a horizontal
# line stretching from 0 (west) to 1000 (east).

# Given a list of N listeners, and a list of M radio towers, each placed at various
# locations along this line, determine what the minimum broadcast range would have
# to be in order for each listener's home to be covered.

# For example, suppose listeners = [1, 5, 11, 20], and towers = [4, 8, 15]. In this
# case the minimum range would be 5, since that would be required for the tower at
# position 15 to reach the listener at position 20.

from typing import Iterable

def min_range(listeners: Iterable[int], towers: Iterable[int]):
    sorted_listeners = sorted(listeners)
    sorted_towers = sorted(towers)
    t = len(towers)

    def dist(listener_idx, tower_idx):
        return abs(sorted_listeners[listener_idx] - sorted_towers[tower_idx])

    # we basically need to find the max of min distances to a tower
    tower_idx = 0
    min_range = 0

    for listener_idx in range(len(listeners)):

        # find the best tower! moving in a single direction
        while tower_idx < t - 1 and dist(listener_idx, tower_idx) > dist(listener_idx, tower_idx + 1):
            tower_idx += 1

        min_range = max(min_range, dist(listener_idx, tower_idx))

    return min_range


assert(min_range([1, 5, 11, 20], [4, 8, 15]) == 5)
assert(min_range([1, 5, 11, 20], [20]) == 19)
assert(min_range([1, 5, 11, 20], [1, 5, 11, 20]) == 0)
assert(min_range([1, 2, 3, 4, 5, 6], [2, 5]) == 1)
