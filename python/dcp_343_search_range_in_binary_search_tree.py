# This problem was asked by Google.

# Given a binary search tree and a range [a, b] (inclusive), return the sum
# of the elements of the binary search tree within the range.

# For example, given the following tree:

#     5
#    / \
#   3   8
#  / \ / \
# 2  4 6  10

# and the range [4, 9], return 23 (5 + 4 + 6 + 8).


class Node(object):
    def __init__(self, val, left=None, right=None):
        self.val = val
        self.left = left
        self.right = right


def traverse_range(node: Node, range_from, range_to, func) -> None:

    if node.val >= range_from and node.left:
        traverse_range(node.left, range_from, range_to, func)

    if node.val >= range_from and node.val <= range_to:
        func(node.val)

    if node.val <= range_to and node.right:
        traverse_range(node.right, range_from, range_to, func)


def bst_sum_range(root: Node, range_from, range_to) -> int:
    result = 0

    def add_val(val):
        nonlocal result
        result += val

    traverse_range(root, range_from, range_to, add_val)

    return result


root = Node(5,
            Node(3,
                Node(2),
                Node(4)),
            Node(8,
                Node(6),
                Node(10)))

print(bst_sum_range(root, 4, 9))
