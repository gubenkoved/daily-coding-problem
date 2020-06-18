# This problem was asked by Flipkart.
# Starting from 0 on a number line, you would like to make
# a series of jumps that lead to the integer N.
# On the ith jump, you may move exactly i places to the left
# or right.
# Find a path with the fewest number of jumps required to get
# from 0 to N.


# https://leetcode.com/problems/reach-a-number/  (TLE on big numbers...)

from collections import deque
from typing import Optional

def count_jumps(n: int, max_depth: Optional[int] = None) -> int:
    # classic BFS suffice...
    min_distance = {0: 0}
    queue = deque()
    queue.append((0, 0))
    stop = False
    visited = set((0, 0))

    while len(queue) > 0 and not stop:
        cur, cur_depth = queue.popleft()
        jump_num = cur_depth + 1

        if max_depth and jump_num > max_depth:
            break

        reachable = [cur + jump_num, cur - jump_num]

        for next_val in reachable:
            # if next in distance:
            #     continue  # already visited
            next_node = (next_val, cur_depth + 1)

            if next_node in visited:
                continue

            visited.add(next_node)
            queue.append(next_node)

            if next_val in min_distance:
                min_distance[next_val] = min(min_distance[next_val], cur_depth + 1)
            else:
                min_distance[next_val] = cur_depth + 1

            if next_val == n:
                stop = True
                break  # found!

    if n not in min_distance:
        return None  # not found!

    return min_distance[n]


# print(count_jumps(1000000000))


for i in range(1, 1000):
    print(f'f({i:2}) = {count_jumps(i, 1_000)}')
