# This problem was asked by Oracle.

# Given a binary search tree, find the floor and ceiling of a given integer. The floor
# is the highest element in the tree less than or equal to an integer, while the ceiling
# is the lowest element in the tree greater than or equal to an integer.

# If either value does not exist, return None.

class node(object):

    def __init__(self, val, left=None, right=None):
        self.val = val
        self.left = left
        self.right = right

    def __repr__(self):
        return f'[{self.val}, {self.left}, {self.right}]'

def bst_floor(head:node, n):
    result = None

    cur = head

    while cur != None:
        if cur.val > n:
            cur = cur.left
        else: # current value is less than or equal to target -> go right
            if result is not None:
                result = max(result, cur.val)
            else:
                result = cur.val
            cur = cur.right

    return result


def bst_ceiling(head: node, n):
    result = None

    cur = head

    while cur != None:
        if cur.val < n:
            cur = cur.right
        else:  # current value is less than or equal to target -> go right
            if result is not None:
                result = min(result, cur.val)
            else:
                result = cur.val
            cur = cur.left

    return result

h = node(10,
    node(4,
        None,
        node(8,
            node(6),
            node(9))),
    node(20,
        node(15),
        node(30)))

print(bst_floor(h, 7)) # 6
print(bst_floor(h, 100)) # 30
print(bst_ceiling(h, 13)) # 15
print(bst_ceiling(h, 15)) # 15
print(bst_ceiling(h, 0))  # 4
