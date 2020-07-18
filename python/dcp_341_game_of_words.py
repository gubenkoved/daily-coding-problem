# This problem was asked by Google.

# You are given an N by N matrix of random letters and a dictionary of words. Find the maximum number
# of words that can be packed on the board from the given dictionary.

# A word is considered to be able to be packed on the board if:

# It can be found in the dictionary
# It can be constructed from untaken letters by other words found so far on the board
# The letters are adjacent to each other (vertically and horizontally, not diagonally).
# Each tile can be visited only once by any word.

# For example, given the following dictionary:

# { 'eat', 'rain', 'in', 'rat' }
# and matrix:

# [['e', 'a', 'n'],
#  ['t', 't', 'i'],
#  ['a', 'r', 'a']]

# Your function should return 3, since we can make the words 'eat', 'in', and 'rat' without them touching
# each other. We could have alternatively made 'eat' and 'rain', but that would be incorrect since
# that's only 2 words.

from typing import Dict, List, Set, Tuple

Path = List[Tuple[int, int]]

def solve(matrix: List[List[str]], dic: Set[str]) -> Set[str]:

    n = len(matrix)

    def find_word(word: str) -> List[Path]:
        paths = []

        def worker(path: Path) -> None:
            nonlocal word

            # found it!
            if len(path) == len(word):
                paths.append(list(path))
                return

            cell = path[-1]
            row = cell[0]
            col = cell[1]
            neighboors = [(row, col - 1), (row, col + 1), (row - 1, col), (row + 1, col)]

            look_for = word[len(path)]

            for neighboor in neighboors:
                # check if valid first
                if neighboor[0] < 0 or neighboor[0] >= n:
                    continue

                if neighboor[1] < 0 or neighboor[1] >= n:
                    continue

                if matrix[neighboor[0]][neighboor[1]] != look_for:
                    continue

                # okay, looks like we found the next one -> dive in!
                path.append(neighboor)
                worker(path)
                del path[-1]

        for row in range(n):
            for col in range(n):
                if matrix[row][col] == word[0]:
                    worker([(row, col)])

        return paths

    def find_words() -> Dict[str, List[Path]]:
        return {word: find_word(word) for word in dic}

    def intersects(a: Path, b: Path) -> bool:
        return bool(set(a) & set(b))

    found_words_map = find_words()
    words = list(found_words_map.keys())
    best_solution: Set[str] = {}

    # backtracking search... quite dumb though...
    def search(word_idx: int, taken_paths: List[Path], taken_words: Set[str]):
        nonlocal found_words_map
        nonlocal words
        nonlocal best_solution

        if word_idx >= len(words):
            if len(taken_words) > len(best_solution):
                best_solution = set(taken_words)
            print(f'  possible: {taken_words}')
            return

        word = words[word_idx]

        # try to include word, try each of the available paths to get it
        taken_words.add(word)

        for path in found_words_map[word]:
            # see if it's compatible with already taken paths!
            is_compatible = not any([intersects(x, path) for x in taken_paths if x is not path])

            if is_compatible:  # try it out if compatible!
                taken_paths.append(path)
                search(word_idx + 1, taken_paths, taken_words)
                del taken_paths[-1]

        taken_words.remove(word)

        # and also try NOT to include word
        search(word_idx + 1, taken_paths, taken_words)

    search(0, [], set())

    return best_solution


dic = {'eat', 'rain', 'in', 'rat'}

m = [['e', 'a', 'n'],
     ['t', 't', 'i'],
     ['a', 'r', 'a']]

print(solve(m, dic))
