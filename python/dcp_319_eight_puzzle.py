# This problem was asked by Airbnb.
#
# An 8-puzzle is a game played on a 3 x 3 board of tiles, with the ninth tile missing. The remaining tiles are
# labeled 1 through 8 but shuffled randomly. Tiles may slide horizontally or vertically into an empty space,
# but may not be removed from the board.
#
# Design a class to represent the board, and find a series of steps to bring the board to the state [[1, 2, 3], [4,
# 5, 6], [7, 8, None]].

from typing import Iterable, List, Tuple, Optional
from copy import deepcopy
from collections import deque

Board = List[List[int]]
Position = Tuple[int, int]  # (row, col)
Move = Tuple[Position, Position]  # from -> to


def distance(board: Board) -> int:
    distance = 0
    expected = 0

    for row in range(0, 3):
        for col in range(0, 3):
            expected += 1

            if expected == 9:
                expected = None  # last piece should be missing

            if expected != board[row][col]:
                distance += 1

    return distance


def board_handle(board: Board) -> int:
    r: int = 0
    mult: int = 1
    for row in range(0, 3):
        for col in range(0, 3):
            if board[row][col] is not None:
                r += board[row][col] * mult
            mult *= 10
    return r


def find_empty_cell(board: Board) -> Position:
    for row in range(0, 3):
        for col in range(0, 3):
            if board[row][col] is None:
                return row, col

    raise Exception('Unable to find empty cell in the board!')


def get_moves(board: Board) -> Iterable[Move]:
    target = find_empty_cell(board)
    moves = []

    if target[0] < 2:  # target row is not the last one
        moves.append(((target[0] + 1, target[1]), target))

    if target[0] > 0:  # target row is not the first one
        moves.append(((target[0] - 1, target[1]), target))

    if target[1] < 2:  # target col is not the last one
        moves.append(((target[0], target[1] + 1), target))

    if target[1] > 0:  # target col is not the first one
        moves.append(((target[0], target[1] - 1), target))

    return moves


def apply_move(board: Board, move: Move) -> None:
    src = move[0]
    trg = move[1]

    if board[src[0]][src[1]] is None:
        raise Exception('Source supposed to have a piece!')

    if board[trg[0]][trg[1]] is not None:
        raise Exception('Target supposed to be empty!')

    board[trg[0]][trg[1]] = board[src[0]][src[1]]
    board[src[0]][src[1]] = None


def solve(board: Board) -> Optional[List[Move]]:

    visited = set([board_handle(board)])
    queue = deque([board])  # start with the current board
    breadcrumbs = {}  # map from board to the tuple of (parent_board, move)
    found = False
    cur = None

    while len(queue) > 0:
        cur = queue.pop()

        if distance(cur) == 0:
            found = True
            break

        possible_moves = get_moves(cur)

        for move in possible_moves:
            next = deepcopy(cur)
            apply_move(next, move)
            handle = board_handle(next)

            if handle in visited:  # avoid cycles!
                continue

            visited.add(handle)
            queue.appendleft(next)
            breadcrumbs[id(next)] = (cur, move)

    if not found:
        return None

    # restore the solution!
    solution = []

    while id(cur) in breadcrumbs:
        parent_info = breadcrumbs[id(cur)]
        move: Move = parent_info[1]
        parent_board: Board = parent_info[0]

        solution.append(move)
        cur = parent_board

    return list(reversed(solution))


def annotate_solution(board: Board, moves: Optional[List[Move]]) -> None:
    if moves is None:
        print(f'Solution is NOT found...')
        return

    board = deepcopy(board)  # avoid changing the original one!

    def _board_to_string(board: Board):
        # return '\r\n'.join([' '.join(str(row)) for row in board])
        # return str(board)
        return '\r\n'.join([' '.join([str(el) if el is not None else '*' for el in row]) for row in board])

    print(f'Found a solution with {len(moves)} moves!')
    print()

    print(_board_to_string(board))
    print('')

    for move in moves:
        apply_move(board, move)
        print(_board_to_string(board))
        print('')

    print('done!')
    print('')


def test():
    boards = []

    boards.append([[1, 2, 3], [4, 5, 6], [7, 8, None]])  # already solved
    boards.append([[1, 2, 3], [4, 5, 6], [7, None, 8]])  # already solved
    boards.append([[None, 2, 3], [1, 5, 6], [4, 7, 8]])  # 4 moves
    boards.append([[5, 1, 3], [8, 7, 2], [4, 6, None]])  # 18 moves
    boards.append([[5, 1, 3], [8, 7, 2], [6, 4, None]])  # NO solution!

    for board in boards:
        solution = solve(board)
        annotate_solution(board, solution)


test()
