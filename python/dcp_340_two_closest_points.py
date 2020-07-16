# This problem was asked by Google.

# Given a set of points (x, y) on a 2D cartesian plane, find the two closest points.
# For example, given the points [(1, 1), (-1, -1), (3, 4), (6, 1), (-1, -6), (-4, -3)],
# return [(-1, -1), (1, 1)].

import math


def dist(a, b):
    return math.sqrt((a[0] - b[0]) ** 2 + (a[1] - b[1]) ** 2)

def closest(points):

    min_dist = None
    result = None

    for x in points:
        for y in points:
            if x is y:
                continue

            d = dist(x, y)
            if min_dist is None or d < min_dist:
                min_dist = d
                result = (x, y)

    return result


print(closest([(1, 1), (-1, -1), (3, 4), (6, 1), (-1, -6), (-4, -3)]))
