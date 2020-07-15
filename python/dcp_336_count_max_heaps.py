# This problem was asked by Microsoft.

# Write a program to determine how many distinct ways there are to create a max heap from a list of N given integers.

# For example, if N = 3, and our integers are [1, 2, 3], there are two ways, shown below.

#   3      3
#  / \    / \
# 1   2  2   1

import math

# amount of sets of len k out of n total elements (n * (n-1) * .. * (n-k-1) / k!)
def choices(k, n):
    return int(math.factorial(n) / math.factorial(n - k) / math.factorial(k))

def f(k: int) -> int:

    # base case
    if k <= 1:
        return 1

    lvl = math.ceil(math.log2(k + 1))  # amount of levels to place k nodes
    full = 2 ** (lvl - 1) - 1  # amount of nodes in the branch if it's FULL
    kl = full if k - 1 >= full else k - 1  # amount of nodes required in the left branch so that tree is COMPLETE
    kr = k - 1 - kl  # amount of nodes in the right branch so that tree is COMPLETE

    return choices(kl, k - 1) * f(kl) * f(kr)


for n in range(1, 20):
    print(f'f({n})={f(n)}')
