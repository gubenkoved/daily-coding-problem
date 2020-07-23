# This problem was asked by Grammarly.

# Soundex is an algorithm used to categorize phonetically, such that two names that
# sound alike but are spelled differently have the same representation.

# Soundex maps every name to a string consisting of one letter and three numbers,
# like M460.

# One version of the algorithm is as follows:

# 1. Remove consecutive consonants with the same sound (for example, change ck -> c).
# 2. Keep the first letter. The remaining steps only apply to the rest of the string.
# 3. Remove all vowels, including y, w, and h.
# 4. Replace all consonants with the following digits:
#    b, f, p, v → 1
#    c, g, j, k, q, s, x, z → 2
#    d, t → 3
#    l → 4
#    m, n → 5
#    r → 6
# 5. If you don't have three numbers yet, append zeros until you do. Keep the first three
# numbers.

# Using this scheme, Jackson and Jaxen both map to J250.

# Implement Soundex.


def soundex(s: str) -> str:
    m = {}

    m.update({x: 1 for x in ['b', 'f', 'p', 'v']})
    m.update({x: 2 for x in ['c', 'g', 'j', 'k', 'q', 's', 'x', 'z']})
    m.update({x: 3 for x in ['d', 't']})
    m.update({x: 4 for x in ['l']})
    m.update({x: 5 for x in ['m', 'n']})
    m.update({x: 6 for x in ['r']})

    # non consonants are mapped to 0
    tail = [m[c] if c in m else 0 for c in s[1:]]

    # remove consecutive occurrences
    for idx in range(len(tail) - 2, -1, -1):
        if tail[idx] == tail[idx + 1]:
            del tail[idx + 1]

    # now, remove all '0'
    tail = [x for x in tail if x != 0]

    while len(tail) < 3:
        tail.append(0)

    if len(tail) > 3:
        tail = tail[:3]

    return s[0].upper() + ''.join(map(str, tail))


assert soundex('Jackson') == 'J250'
assert soundex('Jaxen') == 'J250'
assert soundex('implementation') == 'I514'
assert soundex('ammonium') == 'A555'
