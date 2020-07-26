# This problem was asked by Palantir.

# A typical American-style crossword puzzle grid is an N x N matrix with black
# and white squares, which obeys the following rules:

# 1. Every white square must be part of an "across" word and a "down" word.
# 2. No word can be fewer than three letters long.
# 3. Every white square must be reachable from every other white square.
# 4. The grid is rotationally symmetric (for example, the colors of the top left
#    and bottom right squares must match).

# Write a program to determine whether a given matrix qualifies as a crossword grid.

from typing import List

def components(m: List[List[int]]) -> int:
    n = len(m)

    count = 0
    visited = set()

    def traverse(i: int, j: int):
        nonlocal visited
        nonlocal n

        visited.add((i, j))

        neighboors = [(i + 1, j), (i - 1, j), (i, j - 1), (i, j +1)]

        for i2, j2 in neighboors:
            if i2 >= 0 and i2 < n and j2 >= 0 and j2 < n:
                if (i2, j2) not in visited:
                    if m[i2][j2] == 'W':
                        traverse(i2, j2)

    for i in range(n):
        for j in range(n):
            if m[i][j] != 'W':
                continue

            if (i, j) not in visited:
                count += 1
                traverse(i, j)

    return count

def is_180_degrees_symmetric(m: List[List[int]]) -> bool:
    n = len(m)

    for i in range(n):
        for j in range(n):
            if m[i][j] != m[n - i - 1][n - j - 1]:
                return False

    return True

def check(m: List[List[int]]) -> bool:
    n = len(m)

    # rule #1 and #2 (horizontally and vertically)
    for i in range(n):
        ln = 0
        for j in range(n):
            if m[i][j] == 'W':
                ln += 1

            if m[i][j] != 'W' or j == n - 1:
                if ln != 0 and ln < 3:
                    print(f'word < 3 chars detected')
                    return False

                ln = 0  # start over!

    # vertically
    for j in range(n):
        ln = 0
        for i in range(n):
            if m[i][j] == 'W':
                ln += 1

            if m[i][j] != 'W' or i == n - 1:
                if ln != 0 and ln < 3:
                    print(f'word < 3 chars detected')
                    return False

                ln = 0  # start over!

    # rule #3
    components_count = components(m)
    if components_count != 1:
        print(f'components: {components_count}')
        return False

    # rule #4
    if not is_180_degrees_symmetric(m):
        print(f'not 180 rotation symmetric')
        return False

    return True


print(check(
    [
        ['B', 'W', 'W', 'W', 'W'],
        ['B', 'W', 'W', 'W', 'W'],
        ['W', 'W', 'W', 'W', 'W'],
        ['W', 'W', 'W', 'W', 'B'],
        ['W', 'W', 'W', 'W', 'B'],
    ]
))
