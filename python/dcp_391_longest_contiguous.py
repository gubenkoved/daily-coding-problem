# This problem was asked by Facebook.
#
# We have some historical clickstream data gathered from our site anonymously using
# cookies. The histories contain URLs that users have visited in chronological order.
#
# Write a function that takes two users' browsing histories as input and returns the
# longest contiguous sequence of URLs that appear in both.
#
# For example, given the following two users' histories:
#
# user1 = ['/home', '/register', '/login', '/user', '/one', '/two']
# user2 = ['/home', '/red', '/login', '/user', '/one', '/pink']
# You should return the following:
#
# ['/login', '/user', '/one']


# O(n^2) time in average, O(n^3) in the worst case (all the same nodes)
def f(a, b):
    # returns longest matching subsequence given the known start points
    def match(a_idx, b_idx):
        result = []
        while a_idx < len(a) and b_idx < len(b) and a[a_idx] == b[b_idx]:
            result.append(a[a_idx])
            a_idx += 1
            b_idx += 1
        return result

    longest = []

    for a_idx in range(len(a)):
        for b_idx in range(len(b)):
            seq = match(a_idx, b_idx)
            if len(seq) > len(longest):
                longest = seq

    return longest


assert f(['/a', '/b', '/c', '/d'],
         ['/a', '/b', '/c', '/d']) == ['/a', '/b', '/c', '/d']

assert f(['/a', '/b', '/c', '/d'],
         ['/x', '/b', '/c', '/y']) == ['/b', '/c']

assert f(['/home', '/register', '/login', '/user', '/one', '/two'],
         ['/home', '/red', '/login', '/user', '/one', '/pink']) == ['/login', '/user', '/one']

assert f(['a', 'b', 'c', 'd', 'e'],
         ['a', 'b', 'x', 'b', 'c', 'd', 'y', 'd', 'e']) == ['b', 'c', 'd']

assert f(['a', 'a', 'b', 'c', 'd', 'e'],
         ['a', 'a', 'a', 'b', 'c', 'd', 'd', 'e']) == ['a', 'a', 'b', 'c', 'd']
