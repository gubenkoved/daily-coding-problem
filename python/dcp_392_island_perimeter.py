# This problem was asked by Google.
#
# You are given a 2D matrix of 1s and 0s where 1 represents land and 0 represents
# water.
#
# Grid cells are connected horizontally or vertically (not diagonally). The grid is
# completely surrounded by water, and there is exactly one island (i.e., one or more
# connected land cells).
#
# An island is a group is cells connected horizontally or vertically, but not
# diagonally. There is guaranteed to be exactly one island in this grid, and the
# island doesn't have water inside that isn't connected to the water around the
# island. Each cell has a side length of 1.
#
# Determine the perimeter of this island.
#
# For example, given the following matrix:
#
# [[0, 1, 1, 0],
# [1, 1, 1, 0],
# [0, 1, 1, 0],
# [0, 0, 1, 0]]
#
# Return 14.


# mind dump: perimeter is only affected by the island's cells which are connected
# to the water or to the edge of provided grid
# so, what we basically need to do is calculate sum of the amounts of '0' cells
# adjacent to the every island cells
# and we do not seem to calculate anything twice, so should be pretty easy...
def p(grid):
    rows = len(grid)
    cols = len(grid[0])
    perimeter = 0
    for i in range(rows):
        for j in range(cols):

            def count_watter():
                water = 0
                adjacent = [(i-1, j), (i+1, j), (i, j-1), (i, j+1)]
                for x, y in adjacent:
                    if x < 0 or x >= rows or y < 0 or y >= cols or grid[x][y] == 0:
                        water += 1
                return water

            if grid[i][j] == 1:
                perimeter += count_watter()

    return perimeter


assert p([[0, 0, 0, 0],
          [0, 0, 0, 0],
          [0, 0, 1, 0],
          [0, 0, 0, 0]]) == 4

assert p([[0, 0, 0, 0],
          [0, 0, 1, 0],
          [0, 0, 1, 0],
          [0, 0, 0, 0]]) == 6

assert p([[0, 0, 0, 0],
          [0, 1, 1, 0],
          [0, 0, 1, 0],
          [0, 0, 0, 0]]) == 8

assert p([[0, 0, 0, 0],
          [1, 1, 1, 0],
          [1, 0, 1, 0],
          [0, 0, 0, 0]]) == 12

assert p([[0, 0, 0, 0],
          [1, 1, 1, 1],
          [1, 0, 0, 1],
          [1, 0, 0, 1]]) == 18

assert p([[0, 1, 0, 0],
          [1, 1, 1, 1],
          [1, 0, 0, 1],
          [1, 0, 0, 1]]) == 20

assert p([[0, 0, 0, 0],
          [0, 1, 1, 0],
          [0, 1, 1, 0],
          [0, 0, 0, 0]]) == 8

assert p([[0, 1, 1, 0],
          [1, 1, 1, 0],
          [0, 1, 1, 0],
          [0, 0, 1, 0]]) == 14
