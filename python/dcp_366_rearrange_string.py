# This problem was asked by Flexport.
#
# Given a string s, rearrange the characters so that any two adjacent characters are
# not the same. If this is not possible, return null.
#
# For example, if s = yyz then return yzy. If s = yyy then return null.

from collections import defaultdict

# https://leetcode.com/problems/reorganize-string/

# it looks like it's harder to provide that the algorithm than to write it
# from intuition looks like the greedy solution would work where we simply group
# all the chars and start with the char with the biggest frequency and then mix it
# with char from the next group
def f(s):
    freq = defaultdict(lambda: 0)  # element -> frequency
    result = []

    for x in s:
        freq[x] += 1

    # list of lists [char, count] for ease of modifying
    freq_sorted = [[char, count] for char, count in sorted(freq.items(), key=lambda t: t[1], reverse=True)]

    last = None

    while True:

        if not freq_sorted:
            break  # game over!

        found = False
        group_idx = None

        # find the char different from the last
        for idx in range(len(freq_sorted)):
            char, count = freq_sorted[idx]

            # take it!
            if last is None or char != last:
                found = True
                last = char
                group_idx = idx
                freq_sorted[idx][1] -= 1
                result.append(char)

                # used up the last char? remove the group
                if count == 1:
                    del freq_sorted[idx]

                break

        if not found:
            return None

        # see if we need to move the char group because of the frequency change,
        # note that the freq_sorted is pre-sorted, so we only need to swap with the
        # groups immediately to the right of it
        while (group_idx < len(freq_sorted) - 1
               and freq_sorted[group_idx][1] < freq_sorted[group_idx + 1][1]):

            # bubble down!
            freq_sorted[group_idx], freq_sorted[group_idx + 1] = \
                freq_sorted[group_idx + 1], freq_sorted[group_idx]

            group_idx += 1

    return ''.join(result)


assert f('yyz') == 'yzy'
assert f('aaaabbb') == 'abababa'
assert f('aaaabb') is None
assert f('aaaaabbcc') == 'abacacaba'
assert f('aaaaabbbbcc') == 'abababacacb'