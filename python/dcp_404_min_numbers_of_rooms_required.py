# This problem was asked by Snapchat.
#
# Given an array of time intervals (start, end) for classroom lectures (possibly
# overlapping), find the minimum number of rooms required.
#
# For example, given [(30, 75), (0, 50), (60, 150)], you should return 2.

# what the hell is wrong with DCP?! this is EXACTLY DCP 21!
#   and similar to the DCP 397...


# O(n^2)
# for each end of the interval count how many others has it inside
def min_number_of_rooms(intervals):
    max_needed = 0
    for start, end in intervals:
        cur_needed = 1
        for start2, end2 in intervals:
            if start2 < end < end2:
                cur_needed += 1
        max_needed = max(max_needed, cur_needed)
    return max_needed


assert min_number_of_rooms([(30, 75), (0, 50), (60, 150)]) == 2
assert min_number_of_rooms([(0, 6), (1, 4), (3, 5), (3, 8), (4, 7), (5, 9), (6, 10), (8, 11)]) == 4
assert min_number_of_rooms([(0, 6), (1, 4), (3, 5), (3, 8), (4, 7), (5, 9), (6, 10), (8, 11), (0, 11)]) == 5
