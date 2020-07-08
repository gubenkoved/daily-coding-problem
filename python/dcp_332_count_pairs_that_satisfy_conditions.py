# This problem was asked by Jane Street.

# Given integers M and N, write a program that counts how many positive
# integer pairs (a, b) satisfy the following conditions:

# a + b = M
# a XOR b = N

# O(M)
def count(m, n):
    result = 0
    for a in range(m):
        b = m - a
        if a ^ b == n:
            result += 1

    return result


for m in range(2, 16):
    for n in range(m + 1):
        print(f'f({m}, {n}) = {count(m, n)}')