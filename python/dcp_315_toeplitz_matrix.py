# This problem was asked by Google.

# In linear algebra, a Toeplitz matrix is one in which the
# elements on any given diagonal from top left to bottom right are identical.

# Here is an example:

# 1 2 3 4 8
# 5 1 2 3 4
# 4 5 1 2 3
# 7 4 5 1 2

# Write a program to determine whether a given input is a Toeplitz matrix.

from typing import List

def is_toeplitz(matrix: List[List[int]]):

    # there are two classes of diagonals -- ones that start from
    # left-most items, and ones that start from top-most items
    # go thought left most ones first

    rows = len(matrix)
    cols = len(matrix[0])

    # left most ones
    for row in range(rows):
        el = matrix[row][0]
        dist = 1
        while row + dist < rows and dist < cols:
            if matrix[row + dist][dist] != el:
                return False
            dist += 1

    # top most ones (skip the first -- already covered)
    for col in range(1, cols):
        el = matrix[0][col]
        dist = 1
        while dist < rows and col + dist < cols:
            if matrix[dist][col + dist] != el:
                return False
            dist += 1

    return True


assert(is_toeplitz([[1, 2, 3, 4, 8],
                    [5, 1, 2, 3, 4],
                    [4, 5, 1, 2, 3],
                    [7, 4, 5, 1, 2]]))

assert(is_toeplitz([[2, 2, 3, 4, 8],
                    [5, 1, 2, 3, 4],
                    [4, 5, 1, 2, 3],
                    [7, 4, 5, 1, 2]]) is False)

assert(is_toeplitz([[1, 1],
                    [1, 1]]))

assert(is_toeplitz([[1, 2]]))

assert(is_toeplitz([[1]]))

assert(is_toeplitz([[1, 1],
                    [1, 2]]) is False)