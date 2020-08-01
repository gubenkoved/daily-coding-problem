# This problem was asked by LinkedIn.

# You are given a binary tree in a peculiar string representation. Each node
# is written in the form (lr), where l corresponds to the left child and r
# corresponds to the right child.

# If either l or r is null, it will be represented as a zero. Otherwise, it
# will be represented by a new (lr) pair.

# Here are a few examples:

# A root node with no children: (00)
# A root node with two children: ((00)(00))
# An unbalanced tree with three consecutive left children: ((((00)0)0)0)
# Given this representation, determine the depth of the tree.

def depth(s: str) -> int:
    max_depth = 0
    depth = 0

    for c in s:
        if c == '(':
            depth += 1
        elif c == ')':
            depth -= 1

        leaf_point = 1 if c == 'l' or c == 'r' else 0

        max_depth = max(max_depth, depth + leaf_point)

    return max_depth


assert(depth('(00)') == 1)
assert(depth('((00)(00))') == 2)
assert(depth('((((00)0)0)0)') == 4)
assert(depth('((((lr)0)0)0)') == 5)
