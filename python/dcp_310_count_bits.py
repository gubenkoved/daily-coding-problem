# This problem was asked by Pivotal.

# Write an algorithm that finds the total number of set bits in
# all integers between 1 and N.

import math


def count_bits(x: int) -> int:
    count = 0

    while x != 0:
        if x % 2 == 1:
            count += 1
        x = x // 2

    return count

def count_bits_range(up_to: int) -> int:
    count = 0
    for x in range(up_to + 1):
        count += count_bits(x)
    return count


def count_bits_analytical(n):
    if n == 0:
        return 0

    # check if n is in form 2^k - 1, where we can analytically calculate
    k = math.floor(math.log2(n + 1))

    # counts items under 2^k
    count = int(k * 2 ** (k - 1))

    # nother else to count if n+1 was in a for 2^k
    if 2 ** k - 1 == n:
        return count

    # otherwise, for items greather or equal to 2^k
    m = n - 2 ** k

    # note that for every of these m numbers we need to set the leading bit to 1
    # eerything else is mapped to already solved problem
    return count + (m + 1) + count_bits_analytical(m)


# print(count_bits_analytical(10))

for x in range(100):
    print(f'f({x:3}) = {count_bits_range(x)}, analytical: {count_bits_analytical(x)}')

# short cut is possible as there is very simple mechanics behind
# notice that for numbers below some whole power of 2 amount of bits
# can be analytically calculated as follows:
# suppose N = 2^k, it means that each number can be encoded as sequence of
# k bits. It's obvious that amount of '0' and '1' will be equal for the whole set
# total amount of binary digits to write it all down is k * N (just write down all under 8 as example)
# so amount of '1' will simply be k * N / 2
# in order to generalize we can find the biggest k we can fit using N as an input which is
# floor(log2(N)), and then only count numbers from (2^k - 1) till N manually (sort of)
# or even better, we can count the remaining part using already solved sub-problems as
# items bigger or equal to 2^k can be mapped to items bellow 2^k simply by adding one more bit
