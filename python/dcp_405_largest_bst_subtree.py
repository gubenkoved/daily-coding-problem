# This problem was asked by Apple.
#
# Given a tree, find the largest tree/subtree that is a BST.
#
# Given a tree, return the size of the largest tree/subtree that is a BST.


class Node:
    def __init__(self, value, left=None, right=None):
        self.value = value
        self.left = left
        self.right = right

    def __str__(self):
        if self.left is None and self.right is None:
            return f'<{self.value}>'
        return f'<{self.value}, {self.left}, {self.right}>'


def max_bst(root):
    max_bst_node = None  # will be pointer to the node which is max BST
    data = {}  # node -> dict we need to track: min, max values, is_bst flag, subtree size

    def process(node):
        nonlocal max_bst_node
        size = 1
        max_value = node.value
        min_value = node.value
        is_bst = True

        if node.left:
            process(node.left)
            l_size, l_max, l_min, l_bst = data[node.left]
            size += l_size
            max_value = max(max_value, l_max)
            min_value = min(min_value, l_min)

            if l_max > node.value:
                is_bst = False

        if node.right:
            process(node.right)
            r_size, r_max, r_min, r_bst = data[node.right]
            size += r_size
            max_value = max(max_value, r_max)
            min_value = min(min_value, r_min)

            if r_min < node.value:
                is_bst = False

        data[node] = (size, max_value, min_value, is_bst)

        if is_bst:
            if max_bst_node is None or data[max_bst_node][0] < size:
                max_bst_node = node

    process(root)

    # print(max_bst_node)

    return max_bst_node


root = Node(4,
            Node(8,
                 Node(5),
                 Node(16)),
            Node(10,
                 Node(4),
                 Node(20,
                      Node(15),
                      Node(30))))

assert str(max_bst(root.left)) == '<8, <5>, <16>>'
assert str(max_bst(root.right.left)) == '<4>'
assert str(max_bst(root)) == '<10, <4>, <20, <15>, <30>>>'
