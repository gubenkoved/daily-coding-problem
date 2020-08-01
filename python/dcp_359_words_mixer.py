# This problem was asked by Slack.

# You are given a string formed by concatenating several words
# corresponding to the integers zero through nine and then anagramming.

# For example, the input could be 'niesevehrtfeev', which is an
# anagram of 'threefiveseven'. Note that there can be multiple instances
# of each integer.

# Given this string, return the original integers in sorted order.
# In the example above, this would be 357.

def f(s: str) -> int:
    m = {
        0: 'zero',
        1: 'one',
        2: 'two',
        3: 'three',
        4: 'four',
        5: 'five',
        6: 'six',
        7: 'seven',
        8: 'eight',
        9: 'nine'
    }

    s = sorted(s)

    for x in range(1_000_000):
        digits = [int(d) for d in str(x)]
        cs = ''.join([m[d] for d in digits])

        if sorted(cs) == s:
            return x

    return None


print(f('niesevehrtfeev'))

