# This problem was asked by Adobe.

# You are given a tree with an even number of nodes. Consider each connection between a parent
# and child node to be an "edge". You would like to remove some of these edges, such that the
# disconnected subtrees that remain each have an even number of nodes.

# For example, suppose your input was the following tree:

#    1
#   / \ 
#  2   3
#     / \ 
#    4   5
#  / | \
# 6  7  8

# In this case, removing the edge (3, 4) satisfies our requirement.

# Write a function that returns the maximum number of edges you can remove while still satisfying
# this requirement.

from typing import Dict

class Node(object):
    def __init__(self, val, children=None):
        self.val = val
        self.children = list(children) if children else []

    def __repr__(self):
        return f'{self.val}'

def solve(root: Node) -> int:

    # parents_map: Dict[Node, Node] = {root: None}
    sizes_map: Dict[Node, int] = {}

    def pre_pass(cur: Node):
        sizes_map[cur] = 1

        for child in cur.children:
            # parents_map[child] = cur
            pre_pass(child)
            sizes_map[cur] += sizes_map[child]

    pre_pass(root)  # fill in size/parents auxiliary maps

    # print(parents_map)
    # print(sizes_map)

    can_be_deleted = 0

    def real_pass(cur: Node):
        nonlocal can_be_deleted

        # see if a path to any child can be removed still maintaining the invariants
        for child in cur.children:

            if sizes_map[child] % 2 == 0:
                can_be_deleted += 1
                print(f' {cur} -> {child} can be deleted')

            real_pass(child)

    real_pass(root)

    return can_be_deleted


r = Node(1,
         [Node(2),
          Node(3,
               [Node(4,
                     [Node(6), Node(7), Node(8)]),
                Node(5)])])

print(solve(r))
