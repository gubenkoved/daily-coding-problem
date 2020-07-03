# This problem was asked by Salesforce.

# Write a program to merge two binary trees. Each node in the new tree should hold a value equal
# to the sum of the values of the corresponding nodes of the input trees.

# If only one input tree has a node in a given position, the corresponding node in the new tree
# should match that input node.


class Node(object):
    def __init__(self, value, left=None, right=None):
        self.value = value
        self.left = left
        self.right = right

    def __str__(self):
        left_str = 'None' if self.left is None else str(self.left)
        right_str = 'None' if self.right is None else str(self.right)

        return f'[{self.value}, {left_str}, {right_str}]'


def add_tree(base_root: Node, root: Node):
    if root is None:
        return

    base_root.value += root.value

    if root.left is not None:
        if base_root.left is None:
            base_root.left = Node(0)

        add_tree(base_root.left, root.left)

    if root.right is not None:
        if base_root.right is None:
            base_root.right = Node(0)

        add_tree(base_root.right, root.right)

def merge_trees(root1: Node, root2: Node):
    result_root = Node(0)

    add_tree(result_root, root1)
    add_tree(result_root, root2)

    return result_root


t1 = Node(1,
        Node(2),
        Node(3))

t2 = Node(4,
        None,
        Node(5,
            None,
            Node(6)))

tr = merge_trees(t1, t2)

print(tr)
