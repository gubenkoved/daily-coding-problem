# This problem was asked by Twitter.
#
# A strobogrammatic number is a positive number that appears the same after being
# rotated 180 degrees. For example, 16891 is strobogrammatic.
#
# Create a program that finds all strobogrammatic numbers with N digits.

# it is EXACTLY the same as the DCP 362!
# let's try another take anyways... may be it will be a better solution this time?
# oh, i recalled, it was big, but efficient solution
# let's have there smaller but waaaay less efficient one!

symmetry = {
    '1': '1',
    '2': '5',
    '5': '2',
    '6': '9',
    '8': '8',
    '9': '6',
}


def generate(n):
    def is_strobogrammatic(x):
        x = str(x)
        rotated = ''.join(reversed([symmetry[c] if c in symmetry else '_' for c in x]))
        return x == rotated

    for num in range(10 ** (n-1), 10 ** n):
        if is_strobogrammatic(num):
            yield num

for num in generate(5):
    print(num)
