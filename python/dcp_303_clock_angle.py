# This problem was asked by Microsoft.

# Given a clock time in hh:mm format, determine, to the nearest
# degree, the angle between the hour and the minute hands.

# Bonus: When, during the course of a day, will the angle be zero?

from math import floor

def angle_parse(time: str):
    s = time.split(':')
    h, m = int(s[0]), int(s[1])
    return angle(h, m)

def angle(h, m):
    m_angle = 360 * (m / 60)
    h_angle = 360 * (h / 12) + (360 / 12) * (m / 60)
    return round(abs(m_angle - h_angle))

def print_intersections():
    for h in range(0, 12):
        # simple math yeilds the following relation for zero angle:
        # 5 * h = 11 / 12 * m
        m = 5 * h * 12 / 11
        mr = floor(m)

        if mr == 60:
            continue

        s = (m - mr) * 60
        print(f'interests at {h:02}:{mr:02}:{s:06.3f} ({angle(h, mr)})')

assert(angle_parse("03:00") == 90)
assert(angle_parse("06:00") == 180)
assert(angle_parse("03:30") == 75)

print_intersections()
