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

# i'm not entiely shure WHY it works, but i made an observation that:
# if target is withing set reachable by the series compsoed by sum of arithmetic progression
# it's the faster way to reach such number -- that's quite obvious -- we go the fastest possible way
# to target number as we always step to the side where we add a number and never sutract anything, e.g.:
# 1 -> 1  --> f(1) = 1
# 1 + 2 -> 3  --> f(3) = 2
# 1 + 2 + 3 -> 6  --> f(6) = 3
# 1 + 2 + 3 + 4 -> 10  --> f(10) = 4
# secondly, drawin a tree it becomes obvious that if n is reachable in k steps (f(n) = k) then other numbers
# which are n - 2, n - 4, n - 6, ..., n - q (where q is a natural number) are also reachable
# and something makes me feel that it might produce an optimal path to a given number and i'm not entierly sure
# why exactly it is the case though, but IT WORKS!
def count_jumps2(n: int):
    n = abs(n)
    k = 0
    sum = 0

    while True:
        k += 1
        sum += k

        if sum == n:
            return k

        if n < sum and (sum - n) % 2 == 0:
            return k


# print(count_jumps2(1000000000))


for i in range(1, 1000):
    #print(f'f({i:2}) = {count_jumps2(i)} ({count_jumps(i)})')
    print(f'f({i:2}) = {count_jumps2(i)}')
