# This problem was asked by Gusto.
#
# Implement the function embolden(s, lst) which takes in a string s and list of
# substrings lst, and wraps all substrings in s with an HTML bold tag <b> and </b>.
#
# If two bold tags overlap or are contiguous, they should be merged.
#
# For example, given s = abcdefg and lst = ["bc", "ef"], return
# the string a<b>bc</b>d<b>ef</b>g.
#
# Given s = abcdefg and lst = ["bcd", "def"], return the string a<b>bcdef</b>g,
# since they overlap.
#

def showcase(f):
    def wrapped(*args):
        result = f(*args)
        print(f'{args} -> {result}')
        return result
    return wrapped

@showcase
def bold(s: str, l):
    b = [False for _ in s]

    def mark(idx, n):
        for i in range(n):
            b[idx + i] = True

    # phase 1: mark all chars that should be bold
    for x in l:
        start = 0
        while True:
            idx = s.find(x, start)
            if idx == -1:
                break
            start = idx + 1
            # mark the occurrence
            mark(idx, len(x))

    # phase 2: wrap adjacent regions of bold chars with a tag
    result = []

    for idx in range(len(s)):
        if b[idx] and (idx == 0 or b[idx - 1] is False):
            result.append('<b>')

        if b[idx] is False and (idx == len(s) - 1 or b[idx - 1] is True):
            result.append('</b>')

        result.append(s[idx])

    return ''.join(result)


assert bold('abcdefg', ["bc", "ef"]) == 'a<b>bc</b>d<b>ef</b>g'
assert bold('abcdefg', ["bcd", "def"]) == 'a<b>bcdef</b>g'