# This problem was asked by Uber.

# You are given a 2-d matrix where each cell consists of either /, \, or
# an empty space. Write an algorithm that determines into how many regions
# the slashes divide the space.

# For example, suppose the input for a three-by-six grid is the following:

# \    /
#  \  /
#   \/

# Considering the edges of the matrix as boundaries, this divides the grid
# into three triangles, so you should return 3.

import unittest

def flood_fill(matrix, start_x, start_y):
    rows, cols = len(matrix), len(matrix[0])

    visted = set()

    visted.add((start_x, start_y))
    queue = [(start_x, start_y)]

    while len(queue) > 0:
        cur = queue.pop()
        x, y = cur

        neighbors = \
        [
            (x - 1, y),
            (x + 1, y),
            (x, y - 1),
            (x, y + 1),
        ]

        for neighbor in neighbors:

            nx, ny = neighbor

            if nx < 0 or nx >= rows:
                continue

            if ny < 0 or ny >= cols:
                continue

            if matrix[nx][ny] != ' ':
                continue

            if neighbor in visted:
                continue

            visted.add(neighbor)
            queue.append(neighbor)

    return visted

def count(matrix):

    # we need to split the matrix into the atomic units
    # and then apply flood fill to every unit intil we color them all
    # amount of passes that will be required to color them all will be
    # equal to amount of regions

    # matrix zooming allows to fight edge cases where areas are very small and contain
    # no original cells
    matrix = zoom(matrix)
    matrix = zoom(matrix)

    visited = set()
    rows, cols = len(matrix), len(matrix[0])
    regions = 0

    for row in range(0, rows):
        for col in range(0, cols):
            if matrix[row][col] == ' ' and (row, col) not in visited:
                cur_visited = flood_fill(matrix, row, col)

                regions += 1

                # add all visited for the pass
                for el in cur_visited:
                    visited.add(el)

    return regions

# allows to handle edge cases where region that is being created
# does not form a free cell
def zoom(matrix):
    rows, cols = len(matrix), len(matrix[0])

    out_matrix = [[' ' for j in range(cols * 2)] for i in range(rows * 2)]

    for row in range(0, rows):
        for col in range(0, cols):
            if matrix[row][col] == '/':
                out_matrix[(row * 2)][(col * 2 + 1)] = '/'
                out_matrix[row * 2 + 1][col * 2] = '/'
            
            if matrix[row][col] == '\\':
                out_matrix[row * 2][col * 2] = '\\'
                out_matrix[row * 2 + 1][col * 2 + 1] = '\\'

    return out_matrix

def parse(s):
    s = s.strip('\r\n')
    a = [list(x) for x in s.splitlines()]
    return a

class Tests(unittest.TestCase):
    def test_case0(self):
        self.assertEqual(1, count(parse(r"""
   
   
   
""")))


    def test_case1(self):
        self.assertEqual(3, count(parse(r"""
\    /
 \  / 
  \/  
""")))


    def test_case2(self):
        self.assertEqual(2, count(parse(r"""
\     
 \    
  \   
""")))


    def test_case3(self):
        self.assertEqual(4, count(parse(r"""
\     
 \   /
\ \ / 
""")))

    def test_case4(self):
        self.assertEqual(9, count(parse(r"""
//////
//////
//////
""")))

    def test_case5(self):
        self.assertEqual(14, count(parse(r"""
/\/\/\
\/\/\/
/\/\/\
""")))
