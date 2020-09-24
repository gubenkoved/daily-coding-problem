# This problem was asked by Uber.
#
# Given a binary tree and an integer k, return whether there exists a root-to-leaf
# path that sums up to k.
#
# For example, given k = 18 and the following binary tree:
#
#     8
#    / \
#   4   13
#  / \   \
# 2   6   19
# Return True since the path 8 -> 4 -> 6 sums to 18.
#


class Node:
    def __init__(self, value, left=None, right=None):
        self.value = value
        self.left = left
        self.right = right


def check(cur: Node, sum: int) -> bool:
    if cur.left is None and cur.right is None:
        return cur.value == sum

    if cur.left is not None:
        if check(cur.left, sum - cur.value):
            return True

    if cur.right is not None:
        if check(cur.right, sum - cur.value):
            return True

    return False


root = Node(8,
            Node(4,
                 Node(2),
                 Node(6)),
            Node(13,
                 None,
                 Node(19)))

assert check(root, 18)
assert check(root, 14)
assert check(root, 40)
assert not check(root, 10)
assert not check(root, 50)
