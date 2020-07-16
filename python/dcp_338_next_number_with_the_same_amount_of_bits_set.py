# This problem was asked by Facebook.

# Given an integer n, find the next biggest integer with the same number of 1-bits on.
# For example, given the number 6 (0110 in binary), return 9 (1001).

def count_bits_set(k: int) -> int:
    c = 0
    while k != 0:
        if k & 1:
            c += 1
        k = k >> 1
    return c

def next_number(k: int) -> int:
    c = count_bits_set(k)

    k += 1

    while count_bits_set(k) != c:
        k += 1

    return k


print(bin(next_number(0b0110)))
