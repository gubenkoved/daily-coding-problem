# This problem was asked by Two Sigma.
#
# You are given an unsorted list of 999,000 unique integers, each
# from 1 and 1,000,000. Find the missing 1000 numbers.
#
# What is the computational and space complexity of your solution?


# optimizes for speed
# O(n) time, O(n) space
def f(a, n):
    # make it 1-based
    b = [False] + [False for _ in range(n + 1)]

    # preprocessing -- mark all that are found
    for x in a:
        if x > n:
            raise Exception(f'unexpected: {x}')
        b[x] = True

    missing = []
    for x in range(1, n+1):
        if not b[x]:
            missing.append(x)

    return missing


# optimizes for space
# O(n*logn) time, O(1) space
def h(a, n):
    a = sorted(a)
    cursor = 0
    missing = []
    for x in range(1, n+1):
        if cursor < len(a) and a[cursor] == x:
            cursor += 1
        else:
            missing.append(x)
    return missing


print(f([9, 8, 5, 3, 4, 1, 2], 10))
print(h([9, 8, 5, 3, 4, 1, 2], 10))
