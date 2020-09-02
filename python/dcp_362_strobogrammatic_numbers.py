# This problem was asked by Twitter.
#
# A strobogrammatic number is a positive number that appears the same after being
# rotated 180 degrees. For example, 16891 is strobogrammatic.
#
# Create a program that finds all strobogrammatic numbers with N digits.

import math

rotation_map = {
    '0': '0',
    '1': '1',
    '2': '5',
    '5': '2',
    '6': '9',
    '8': '8',
    '9': '6'
}

# def perumations(items):
#     if not items:
#         yield []
#
#     for idx in range(len(items)):
#         leftover = items[:idx] + items[idx + 1:]
#
#         for nested in perumations(leftover):
#             yield [items[idx]] + nested

def perumations_with_repetitions(items, k):
    if k == 0:
        yield []
        return

    for item in items:
        for nested in perumations_with_repetitions(items, k-1):
            yield [item] + nested


def generate_strobogrammatic(n: int):
    # it's actually equivalent to generation of all permutations of 'rotatable'
    # symbols with a len n/2
    # if n is odd, we generate permutations for floor(n/2) and in the middle add
    # '8' OR '0'
    # as it will not change the position and it has to map to itself after rotation

    l = math.floor(n / 2.0)

    items = list(map(str, rotation_map.keys()))

    sequences = list(perumations_with_repetitions(items, l))

    def rotate(seq):
        return list(reversed(list(map(lambda x: rotation_map[x], seq))))

    if n % 2 == 0:
        return [''.join(s + rotate(s)) for s in sequences]
    else:
        rotation_invariant = [key for key in rotation_map.keys()
                              if rotation_map[key] == key]

        result = []

        for c in rotation_invariant:
            result.extend([''.join(s + [c] + rotate(s)) for s in sequences])

        return result


if __name__ == '__main__':
    for n in range(2, 6):
        print(generate_strobogrammatic(n))

