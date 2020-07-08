# This problem was asked by LinkedIn.

# You are given a string consisting of the letters x and y, such as xyxxxyxyy. In addition,
# you have an operation called flip, which changes a single x to y or vice versa.

# Determine how many times you would need to apply this operation to ensure that all x's come
# before all y's. In the preceding example, it suffices to flip the second and sixth characters,
# so you should return 2.


def flip_count(s: str) -> int:
    best = len(s)
    n = len(s)
    y_to_left = [0 for _ in range(n)]
    x_to_right = [0 for _ in range(n)]

    for idx in range(0, n):
        if s[idx] == 'y':
            y_to_left[idx] += 1

        if idx > 0:
            y_to_left[idx] += y_to_left[idx - 1]

    for idx in range(n-1, -1, -1):
        if s[idx] == 'x':
            x_to_right[idx] += 1
        if idx < n - 1:
            x_to_right[idx] += x_to_right[idx + 1]

    # suppose 'y' series starts at idx
    for idx in range(0, n + 1):
        cost = 0

        # we need to flip all these 'x' to the right
        if idx < n:
            cost += x_to_right[idx]

        # AND we need to flip all these 'y' to the left
        if idx > 1:
            cost += y_to_left[idx - 1]

        best = min(best, cost)

    return best


assert flip_count('xyxxxyxyy') == 2  # xXxxxXxyy
assert flip_count('xyxxxyxyyx') == 3  # xXxxxXxyyY or xXxxxyYyyY
assert flip_count('xyxxxyxyyxx') == 4  # xXxxxXxyyYY or ..
assert flip_count('xyxxxyxyyxxx') == 4  # xXxxxXxXXxxx
assert flip_count('yxxxxxxx') == 1  # Xxxxxxxx
assert flip_count('yyx') == 1  # yyY
assert flip_count('yyxx') == 2  # yyYY or XXxx
assert flip_count('yyxxx') == 2  # XXxxx
