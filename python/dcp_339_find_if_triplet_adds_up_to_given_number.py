# This problem was asked by Microsoft.

# Given an array of numbers and a number k, determine if there are three entries in
# the array which add up to the specified number k.

# For example, given [20, 303, 3, 4, 25] and k = 49, return true as 20 + 4 + 25 = 49.

# O(n*n)
def find(a, k):
    s = set(a)  # lowers the asymptotic complexity

    for x in a:
        for y in a:
            if k - x - y in s:
                return True

    return False


print(find([20, 303, 3, 4, 25], 49))
print(find([20, 303, 3, 4, 25], 50))
