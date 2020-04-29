# This problem was asked by Citrix.

# You are given a circular lock with three wheels, each of which display
# the numbers 0 through 9 in order. Each of these wheels rotate clockwise
# and counterclockwise.

# In addition, the lock has a certain number of "dead ends", meaning that
# if you turn the wheels to one of these combinations, the lock becomes
# stuck in that state and cannot be opened.

# Let us consider a "move" to be a rotation of a single wheel by one digit,
# in either direction. Given a lock initially set to 000, a target
# combination, and a list of dead ends, write a function that returns the
# minimum number of moves required to reach the target state, or None if
# this is impossible.

from collections import deque

def find_path(target, dead_ends):
    start_at = (0, 0, 0)
    parent_map = {}
    parent_map[start_at] = None  # starting point

    queue = deque([start_at])
    visited = set([start_at])

    while len(queue) > 0:
        cur = queue.popleft()
        for next in adjacent(cur):
            if next not in visited and next not in dead_ends:
                queue.append(next)
                visited.add(next)
                parent_map[next] = cur

    if target not in parent_map:
        return None

    # recover full path
    path = []
    cur = target

    while True:
        path.append(cur)
        cur = parent_map[cur]

        if cur is None:
            break

    print(" -> ".join([str(x) for x in reversed(path)]))

    return len(path) - 1  # amount of moves is total amount of states - 1

def adjacent(state):
    # adjacent states are composed by moving one of the wheels at 1 position
    # either forward or backwards

    def replace_tuple_at_idx(tuple, idx, val):
        return tuple[:idx] + (val,) + tuple[idx + 1:]

    results = []

    for i in range(len(state)):
        # move i-th wheel forward OR backwards
        results.append(replace_tuple_at_idx(state, i, (state[i] + 1) % 10))
        results.append(replace_tuple_at_idx(state, i, (state[i] - 1) % 10))

    return results


print(find_path((3, 0, 0), []))  # 3
print(find_path((3, 0, 0), [(2, 0, 0)]))  # 3 + 2 = 5
print(find_path((1, 2, 2), []))  # 5
print(find_path((1, 2, 2), [(0, 1, 0), (0, 0, 1), (0, 1, 1), (1, 1, 0), (1, 0, 1), (1, 1, 1)]))  # 7
