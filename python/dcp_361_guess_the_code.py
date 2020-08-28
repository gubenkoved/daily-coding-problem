# This problem was asked by Facebook.
#
# Mastermind is a two-player game in which the first player attempts to guess the
# secret code of the second. In this version, the code may be any six-digit number
# with all distinct digits.
#
# Each turn the first player guesses some number, and the second player responds by
# saying how many digits in this number correctly matched their location in the
# secret code. For example, if the secret code were 123456, then a guess of 175286
# would score two, since 1 and 6 were correctly placed.
#
# Write an algorithm which, given a sequence of guesses and their scores, determines
# whether there exists some secret code that could have produced them.
#
# For example, for the following scores you should return True, since they correspond
# to the secret code 123456:
#
# {175286: 2, 293416: 3, 654321: 0}
# However, it is impossible for any key to result in the following scores, so in this
# case you should return False:
#
# {123456: 4, 345678: 4, 567890: 4}

# since the limit is 6 digits, we can simply brute-force...

def guess(map: dict):

    def count_matching(a, b):
        a, b = str(a), str(b)
        return sum([1 for i in range(min(len(a), len(b))) if a[i] == b[i]])

    result = []

    for possible in range(100_000, 999_999):
        if len(set(str(possible))) != 6:
            continue

        all_match = True

        for sample, sample_matches in map.items():
            if count_matching(possible, sample) != sample_matches:
                all_match = False
                break

        if all_match:
            result.append(possible)

    return result


print(guess({175286: 2, 293416: 3, 654321: 0}))
print(guess({123456: 4, 345678: 4, 567890: 4}))
