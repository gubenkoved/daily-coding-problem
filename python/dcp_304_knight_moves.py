# This problem was asked by Two Sigma.

# A knight is placed on a given square on an 8 x 8 chessboard.
# It is then moved randomly several times, where each move is
# a standard knight move. If the knight jumps off the board at
# any point, however, it is not allowed to jump back on.

# After k moves, what is the probability that the knight remains
# on the board?

def check_in_board(x, y):
    return x >= 0 and x < 8 and y >= 0 and y < 8

def generate_possible(x, y):
    
    points = [
        (x - 2, y - 1),
        (x - 1, y - 2),
        (x + 1, y - 2),
        (x + 2, y - 1),
        (x - 2, y + 1),
        (x - 1, y + 2),
        (x + 1, y + 2),
        (x + 2, y + 1),
    ]

    return points

# probability to keep on board after k random moves starting 
# at (x, y)
def p_cell(k, x, y, cache):
    if (k, x, y) in cache:
        return cache[(k, x, y)]

    p = 0

    # base case -- no moves left, just see if we still on board
    if k == 0:
        p = check_in_board(x, y)
    else:
        for point in generate_possible(x, y):
            if check_in_board(point[0], point[1]):
                p += 1/8 * p_cell(k - 1, point[0], point[1], cache)

    cache[(k, x, y)] = p

    return p

def p_board(k):
    p = 0

    # given symmetry -- handle 1/4 of the board
    for x in range(0, 4):
        for y in range(0, 4):
            p_c = p_cell(k, x, y, {})
            p += 1/16 * p_c

            # print (f'{p_c:6.4f}', end=' ')
        
        # print()

    return p

from random import randrange

def p_simulate_once(k):
    x = randrange(0, 8)
    y = randrange(0, 8)

    # do k moves and see if we still on board
    for i in range(k):
        possible = generate_possible(x, y)
        idx = randrange(0, len(possible))
        x = possible[idx][0]
        y = possible[idx][1]

        if not check_in_board(x, y):
            return 0

    # still on board
    return 1


def p_simulate(k, n):
    on_board = 0
    for i in range(n):
        on_board += p_simulate_once(k)

    return on_board / n

# p_board(1)

for k in range(16):
    print(f'p({k:2})={p_board(k):<21} (simulation: {p_simulate(k, 10000)})')
