# This problem was asked by Netflix.

# A Cartesian tree with sequence S is a binary tree defined by the
# following two properties:

# It is heap-ordered, so that each parent value is strictly less than
# that of its children.
# An in-order traversal of the tree produces nodes with values that
# correspond exactly to S.
# For example, given the sequence [3, 2, 6, 1, 9], the resulting Cartesian
# tree would be:

#       1
#     /   \
#   2       9
#  / \
# 3   6
#
# Given a sequence S, construct the corresponding Cartesian tree.


class Node(object):
    def __init__(self, value, left=None, right=None):
        self.value = value
        self.left = left
        self.right = right


def in_order_traverse(root: Node):
    traversed = []

    def f(cur: Node):
        if cur.left is not None:
            f(cur.left)

        traversed.append(cur.value)

        if cur.right is not None:
            f(cur.right)

    f(root)

    return traversed

def construct_cartesian_tree(sequence):
    # take the smallest item -- it becomes the root,
    # items to the left of it become left sub-tree
    # items to the right become right sub-tree

    def _find_min(a):
        result = a[0]
        result_idx = 0
        for idx in range(1, len(a)):
            if a[idx] < result:
                result = a[idx]
                result_idx = idx
        return (result, result_idx)

    # it will be the root!
    (min_el, min_idx) = _find_min(sequence)

    root = Node(min_el)

    # left subtree
    if min_idx > 0:
        left_sequence = sequence[:min_idx]
        root.left = construct_cartesian_tree(left_sequence)

    # right subtree
    if min_idx < len(sequence) - 1:
        right_sequence = sequence[min_idx + 1:]
        root.right = construct_cartesian_tree(right_sequence)

    return root

def is_heap(root: Node):
    if root.left is not None:
        if root.left.value <= root.value or not is_heap(root.left):
            return False

    if root.right is not None:
        if root.right.value <= root.value or not is_heap(root.right):
            return False

    return True

# r = Node(1,
#         Node(2,
#             Node(3),
#             Node(6)),
#         Node(9))

# r = Node(1,
#         Node(5),
#         Node(2,
#             Node(4,
#                 None,
#                 Node(8)),
#             Node(3,
#                 Node(6,
#                     Node(9)),
#                 Node(7))))

# print(in_order_traverse(r))


tree = construct_cartesian_tree([3, 2, 6, 1, 9])
print(is_heap(tree))
print(in_order_traverse(tree))

tree = construct_cartesian_tree([5, 1, 4, 8, 2, 9, 6, 3, 7])
print(is_heap(tree))
print(in_order_traverse(tree))