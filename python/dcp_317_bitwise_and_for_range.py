# This problem was asked by Yahoo.

# Write a function that returns the bitwise AND of all integers
# between M and N, inclusive.

# O(n - m)
def f(m, n):
    result = m
    for x in range(m + 1, n + 1):
        result = result & x
    return result

def g(m, n):
    # from analysis it becomes obvious that the result is the common prefix between
    # two numbers
    if n < m:
        raise Exception('m should be less than or equal to n')

    bin_n = f'{n:b}'
    bin_m = f'{m:b}'.rjust(len(bin_n), '0')

    # find the common prefix
    bin_r = ['0' for _ in range(len(bin_n))]

    for idx in range(0, len(bin_n)):
        if bin_n[idx] != bin_m[idx]:
            break
        bin_r[idx] = bin_n[idx]

    return int(''.join(bin_r), base=2)


non_zero_counter = 0

for m in range(1, 1000):
    for n in range(m + 1, 1000):
        r = g(m, n)
        if r:
            # print(f'f({m:2}, {n:2}) = {r:2}, {m:8b} ... {n:<8b} -> {r:8b}')
            non_zero_counter += 1

print(non_zero_counter)
