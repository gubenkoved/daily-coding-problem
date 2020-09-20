# This problem was asked by Nextdoor.
#
# Implement integer division without using the division operator. Your function
# should return a tuple of (dividend, remainder) and it should take two numbers,
# the product and divisor.
#
# For example, calling divide(10, 3) should return (3, 1) since the divisor is 3 and
# the remainder is 1.
#
# Bonus: Can you do it in O(log n) time?


class tracer:
    def __init__(self, *args):
        self.args = args
        self.counter = 0

    def tick(self):
        self.counter += 1

    def show_summary(self):
        print(f'\t{self.args} took {self.counter} rounds')


# O(num/div)
def int_div(num, divider):
    divisor = 0
    round_tracer = tracer(num, divider)
    while num >= divider:
        round_tracer.tick()
        divisor += 1
        num -= divider
    round_tracer.show_summary()
    return divisor, num


# O(logn)
# we try to cut 2^divider pieces
def int_div_v2(num, divider):
    divisor = 0
    round_tracer = tracer(num, divider)
    while num >= divider:
        round_tracer.tick()
        pow = 1
        delta = 1
        while num >= (divider ** (pow + 1)):
            pow += 1
            delta *= divider

        divisor += delta
        num -= divider ** pow
    round_tracer.show_summary()
    return divisor, num


for solver in [int_div, int_div_v2]:
    print(f'testing {solver.__name__}...')
    assert solver(10, 3) == (3, 1)
    assert solver(16, 2) == (8, 0)
    assert solver(5, 10) == (0, 5)
    assert solver(1_000_001, 2) == (500_000, 1)
